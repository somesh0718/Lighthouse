namespace Igmite.Lighthouse.Models
{
    public class ExportResultModel
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Results { get; set; }
    }
}