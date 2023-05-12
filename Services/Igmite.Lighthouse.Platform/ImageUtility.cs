using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Igmite.Lighthouse.Platform
{
    public class ImageUtility
    {
        /// <summary>
        /// Resizes and rotates an image, keeping the original aspect ratio. Does not dispose the original
        /// Image instance.
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="width">desired width</param>
        /// <param name="height">desired height</param>
        /// <returns>new resized/rotated Image instance</returns>
        public static void ResizeImage(string originalImagePath, Size newImageSize, string newImagePath)
        {
            //Get original image from the File Server
            Image originalImage = Image.FromFile(originalImagePath);

            ImageFormat imageFormat = GetImageFormat(originalImagePath);

            try
            {
                // clone the Image instance, since we don't want to resize the original Image instance
                //var cloneImage = originalImage.Clone() as Image;

                if (originalImage.Width > newImageSize.Width)
                {
                    float resolution = 72;

                    Size newSize = CalculateResizedDimensions(originalImage, newImageSize.Width, newImageSize.Height);

                    var resizedImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);
                    resizedImage.SetResolution(resolution, resolution);

                    Graphics graphic = Graphics.FromImage(resizedImage);
                    graphic.CompositingQuality = CompositingQuality.HighQuality;
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // use an image attribute in order to remove the black/gray border around image after resize (most obvious on white images)
                    //using (var attribute = new ImageAttributes())
                    //{
                    //    attribute.SetWrapMode(WrapMode.TileFlipXY);

                    //    // draws the resized image to the bitmap
                    //    graphic.DrawImage(image, new Rectangle(new Point(0, 0), newSize), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attribute);
                    //}

                    var imageRectangle = new Rectangle(0, 0, resizedImage.Width, resizedImage.Height);
                    graphic.DrawImage(originalImage, imageRectangle);

                    resizedImage.Save(newImagePath, imageFormat);

                    GC.Collect();

                    graphic.Dispose();
                    resizedImage.Dispose();
                }
                else
                {
                    originalImage.Save(newImagePath);
                }

                originalImage.Dispose();
            }
            catch (Exception ex)
            {
                originalImage.Save(newImagePath);
                throw ex;
            }
        }

        /// <summary>
        /// Calculates resized dimensions for an image, preserving the aspect ratio.
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="desiredWidth">desired width</param>
        /// <param name="desiredHeight">desired height</param>
        /// <returns>Size instance with the resized dimensions</returns>
        private static Size CalculateResizedDimensions(Image image, int desiredWidth, int desiredHeight)
        {
            var widthScale = (double)(image.Width / desiredWidth);
            var heightScale = (double)(image.Height / desiredHeight);

            // scale to whichever ratio is smaller, this works for both scaling up and scaling down
            var scale = Math.Max(widthScale, heightScale);

            return new Size
            {
                Width = Convert.ToInt32(image.Width / scale),
                Height = Convert.ToInt32(image.Height / scale)
            };
        }

        public static bool CreateThumbnail(int Width, int Height, Stream filePath, string saveFilePath)
        {
            try
            {
                var byteArray = filePath;
                var streamImg = Image.FromStream(byteArray);

                Bitmap sourceImage = new Bitmap(streamImg);
                using (Bitmap objBitmap = new Bitmap(Width, Height))
                {
                    objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                    using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                    {
                        // Set the graphic format for better result cropping
                        objGraphics.SmoothingMode = SmoothingMode.HighQuality;
                        objGraphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        objGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        objGraphics.CompositingQuality = CompositingQuality.HighQuality;
                        objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                        // Save the file path, note we use png format to support png file
                        objBitmap.Save(saveFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log("Create Thumbnail: ERROR:" + ex.Message + "\n" + ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add a text watermark to an image
        /// </summary>
        /// <param name="sourceImage">path to source image</param>
        /// <param name="targetImage">path to the modified image</param>
        /// <param name="watermarkText">text to add as a watermark</param>
        /// <param name="imageFormat">ImageFormat type</param>
        public static void AddWatermarkImage(string sourceImage, string targetImage, string watermarkText)
        {
            Image imgSource = default, imgTarget = default;
            try
            {
                ImageFormat imageFormat = GetImageFormat(sourceImage);

                // open source image as stream and create a memorystream for output
                FileStream source = new FileStream(sourceImage, FileMode.Open);
                Stream output = new MemoryStream();
                imgSource = Image.FromStream(source);

                int xAsixText = (imgSource.Width / 2) - 100;
                if (xAsixText < 10)
                {
                    xAsixText = 50;
                }

                int yAsixText = (imgSource.Height / 2) - 50;
                if (yAsixText < 50)
                {
                    yAsixText = 100;
                }

                //location of the watermark text in the parent image
                Point pt = new Point(xAsixText, yAsixText);

                //choose color and transparency
                Color color = Color.FromArgb(255, 255, 255);
                SolidBrush brush = new SolidBrush(color);

                double diagonal = Math.Sqrt(imgSource.Width * imgSource.Width + imgSource.Height * imgSource.Height);

                Rectangle containerBox = new Rectangle();
                containerBox.X = (int)(diagonal / 12);

                float messageLength = (float)(diagonal / watermarkText.Length * 1);
                containerBox.Y = -(int)(messageLength / 1.6);

                // choose font for text
                Font font = new Font("Verdana", messageLength, FontStyle.Bold, GraphicsUnit.Pixel);

                StringFormat sf = new StringFormat();
                float slope = (float)(Math.Atan2(imgSource.Height, imgSource.Width) * 180 / Math.PI);

                //draw text on image
                Graphics graphics = Graphics.FromImage(imgSource);
                graphics.RotateTransform(slope);
                graphics.DrawString(watermarkText, font, brush, containerBox);
                graphics.Dispose();

                //update image memorystream
                imgSource.Save(output, imageFormat);
                imgTarget = Image.FromStream(output);

                //write modified image to file
                Bitmap bmp = new Bitmap(imgSource.Width, imgSource.Height, imgSource.PixelFormat);
                Graphics graphics2 = Graphics.FromImage(bmp);
                graphics2.DrawImage(imgTarget, new Point(0, 0));
                bmp.Save(targetImage, imageFormat);
            }
            catch (Exception ex)
            {
                imgTarget.Dispose();
                imgSource.Dispose();

                throw ex;
            }

            imgTarget.Dispose();
            imgSource.Dispose();
        }

        /// <summary>
        /// Add a text watermark to an image
        /// </summary>
        /// <param name="sourceImage">path to source image</param>
        /// <param name="targetImage">path to the modified image</param>
        /// <param name="watermarkText">text to add as a watermark</param>
        /// <summary>
        public static void AddWatermarkImage(FileStream fsSourceImage, FileStream fsTargetImage, string watermarkText)
        {
            try
            {
                Image sourceImage = Image.FromStream(fsSourceImage);
                Font font = new Font("Verdana", 16, FontStyle.Bold, GraphicsUnit.Pixel);

                //Adds a transparent watermark with an 100 alpha value.
                Color color = Color.FromArgb(100, 0, 0, 0);

                //The position where to draw the watermark on the image
                Point pt = new Point(10, 30);
                SolidBrush sbrush = new SolidBrush(color);

                Graphics graphics = null;
                try
                {
                    graphics = Graphics.FromImage(sourceImage);
                }
                catch
                {
                    Image img1 = sourceImage;
                    sourceImage = new Bitmap(sourceImage.Width, sourceImage.Height);

                    graphics = Graphics.FromImage(sourceImage);

                    graphics.DrawImage(img1, new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel);
                    img1.Dispose();
                }

                graphics.DrawString(watermarkText, font, sbrush, pt);
                graphics.Dispose();

                sourceImage.Save(fsTargetImage, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
            }
        }

        public static ImageFormat GetImageFormat(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            switch (extension.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                case @".wmf":
                    return ImageFormat.Wmf;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}