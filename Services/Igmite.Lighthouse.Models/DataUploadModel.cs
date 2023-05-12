using Microsoft.AspNetCore.Http;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class DataUploadModel
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserTypeId { get; set; }

        [DataMember]
        public string DataType { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string Base64Data { get; set; }

        [DataMember]
        public IFormFile UploadFile { get; set; }
    }
}