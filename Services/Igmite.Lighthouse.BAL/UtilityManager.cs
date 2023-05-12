using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Igmite.Lighthouse.BAL
{
    public static class UtilityManager
    {
        public static string LoginUrl = "/Home/Login";

        public static string GetDefaultPageByRole(string roleName)
        {
            var pages = GetDefaultPages();
            roleName = string.IsNullOrEmpty(roleName) ? "MEM" : roleName;

            string defaultPage = string.Empty;
            pages.TryGetValue(roleName, out defaultPage);

            return defaultPage ?? "Home/Index";
        }

        private static IDictionary<string, string> GetDefaultPages()
        {
            var pages = new Dictionary<string, string>();
            pages.Add("ADM", "Account/AdvAccountView");         // Administrator
            pages.Add("CAP", "Account/AdvAccountView");         // Commercial Approver
            pages.Add("MEM", "Account/AdvAccountView");         // User
            pages.Add("MDM", "Account/AdvAccountView");         // Manager Approvar
            pages.Add("SUR", "Account/AdvAccountView");         // Super User

            return pages;
        }

        public static string GetControllerActionName()
        {
            string controllerName = string.Empty;
            //            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            //
            //            RouteData rd = RouteTable.Routes.GetRouteData(context);
            //            if (rd != null)
            //            {
            //                try
            //                {
            //                    controllerName = string.Format("{0}-{1}", rd.GetRequiredString("action"), rd.GetRequiredString("controller"));
            //                }
            //                catch { }
            //            }

            return controllerName;
        }

        public static string GetControllerName()
        {
            string controllerName = string.Empty;
            //            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            //
            //            RouteData rd = RouteTable.Routes.GetRouteData(context);
            //            if (rd != null)
            //            {
            //                try
            //                {
            //                    controllerName = rd.GetRequiredString("controller");
            //                }
            //                catch { }
            //            }

            return controllerName;
        }

        public static string GetActionName()
        {
            string actionName = string.Empty;
            //            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            //
            //            RouteData rd = RouteTable.Routes.GetRouteData(context);
            //            if (rd != null)
            //            {
            //                try
            //                {
            //                    actionName = rd.GetRequiredString("action");
            //                }
            //                catch { }
            //            }

            return actionName;
        }

        public static FileUploadModel UploadFileInPostByMobile(FileUploadModel fileUploadedData)
        {
            string uploadFolderPath = Path.Combine(Constants.RootPath, Constants.DocumentPath);
            string thumbnailImagePath, smallImagePath, mediumImagePath, originalImagePath, watermarkImagePath;

            string folderPath = StringUtility.GetFolderPath(fileUploadedData.ContentType, uploadFolderPath);

            originalImagePath = Path.Combine(folderPath, StringUtility.GetFileCleanName(fileUploadedData.ContentId, Constants.SaveImageType.Original, fileUploadedData.FileName));

            try
            {
                fileUploadedData.FileName = fileUploadedData.FileName.GetCleanFileName();
                fileUploadedData.FilePath = originalImagePath.Replace(uploadFolderPath, string.Empty);

                string base64Data = fileUploadedData.Base64Data.Split(',').LastOrDefault();
                byte[] fileBytes = Convert.FromBase64String(base64Data);

                //Save original file on the File Server
                if (System.IO.File.Exists(originalImagePath))
                    System.IO.File.Delete(originalImagePath);

                System.IO.File.WriteAllBytes(originalImagePath, fileBytes);

                if (fileUploadedData.FileType.IndexOf("image") > -1)
                {
                    //Images : Protected by Lighthouse
                    watermarkImagePath = Path.Combine(folderPath, StringUtility.GetFileCleanName(fileUploadedData.ContentId, Constants.SaveImageType.Watermark, fileUploadedData.FileName));
                    try
                    {
                        ImageUtility.AddWatermarkImage(originalImagePath, watermarkImagePath, Constants.WatermarkText);
                    }
                    catch (Exception ex)
                    {
                        fileUploadedData.Exception = ex;
                    }

                    //Save thumbnail image on the File Server.
                    //thumbnailImagePath = Path.Combine(folderPath, StringUtility.GetFileCleanName(fileUploadedData.ContentId, Constants.SaveImageType.Thumbnail, fileUploadedData.FileName));
                    //ImageUtility.ResizeImage(watermarkImagePath, Constants.SaveImageType.ThumbnailSize, thumbnailImagePath);

                    //Save small image on the File Server.
                    //smallImagePath = Path.Combine(folderPath, StringUtility.GetFileCleanName(fileUploadedData.ContentId, Constants.SaveImageType.Small, fileUploadedData.FileName));
                    //ImageUtility.ResizeImage(watermarkImagePath, Constants.SaveImageType.SmallSize, smallImagePath);

                    if (string.Equals(fileUploadedData.ContentType, "VTDailyReporting"))
                    {
                        //Save medium image on the File Server.
                        Size compressImageSize = Constants.SaveImageType.MediumSize;
                        compressImageSize = new Size(Convert.ToInt32(Constants.CompressImageSize.Split('X')[0]), Convert.ToInt32(Constants.CompressImageSize.Split('X')[1]));

                        mediumImagePath = Path.Combine(folderPath, StringUtility.GetFileCleanName(fileUploadedData.ContentId, Constants.SaveImageType.Medium, fileUploadedData.FileName));
                        ImageUtility.ResizeImage(watermarkImagePath, compressImageSize, mediumImagePath);

                        // Set medium image size into database
                        fileUploadedData.FilePath = mediumImagePath.Replace(uploadFolderPath, string.Empty);

                        if (System.IO.File.Exists(watermarkImagePath))
                            System.IO.File.Delete(watermarkImagePath);
                    }
                    else
                    {
                        // Set original image size into database
                        fileUploadedData.FilePath = watermarkImagePath.Replace(uploadFolderPath, string.Empty);
                    }

                    if (System.IO.File.Exists(originalImagePath))
                        System.IO.File.Delete(originalImagePath);
                }
            }
            catch (Exception ex)
            {
                fileUploadedData.Exception = ex;
            }

            return fileUploadedData;
        }
    }
}