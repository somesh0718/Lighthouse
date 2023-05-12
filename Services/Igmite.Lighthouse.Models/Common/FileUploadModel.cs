using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    /// <summary>
    /// Upload file from Lighthouse application (Mobile and Web)
    /// </summary>
    [DataContract, Serializable]
    public class FileUploadModel
    {
        /// <summary>
        /// Current logged-in User Id
        /// </summary>
        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Content Id may be VCId/VTId
        /// </summary>
        [DataMember]
        [Display(Name = "Content Id", Description = "Content Id")]
        public Guid ContentId { get; set; }

        /// <summary>
        /// Content Type (Ex. VC, VT, Field Visits and Guest Lectures)
        /// </summary>
        [DataMember]
        [Display(Name = "Content Type", Description = "Content Type (Ex. VC, VT, Field Visits and Guest Lectures)")]
        public string ContentType { get; set; }

        /// <summary>
        /// File name with extention
        /// </summary>
        [DataMember]
        [Display(Name = "File Name", Description = "File Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string FileName { get; set; }

        /// <summary>
        /// File name with extention
        /// </summary>
        [DataMember]
        [Display(Name = "File Type", Description = "File Type")]
        public string FileType { get; set; }

        /// <summary>
        /// File name with extention
        /// </summary>
        [DataMember]
        [Display(Name = "File Size", Description = "File Size")]
        public int FileSize { get; set; }

        /// <summary>
        /// File name with extention
        /// </summary>
        [DataMember]
        [Display(Name = "File Path", Description = "File Path")]
        public string FilePath { get; set; }

        /// <summary>
        /// File data in format of the Base64
        /// </summary>
        [DataMember]
        [Display(Name = "Base64 Data", Description = "Base64 Data")]
        [Required(ErrorMessage = "{0} is required")]
        public string Base64Data { get; set; }

        /// <summary>
        /// Uploaded File
        /// </summary>
        [DataMember]
        public IFormFile UploadFile { get; set; }

        /// <summary>
        /// Exception Messages
        /// </summary>
        [Display(Name = "Exception", Description = "Exception")]
        public Exception Exception { get; set; }
    }
}