using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class ComplaintRegistrationModel : ComplaintRegistration
    {
        public ComplaintRegistrationModel()
        {
        }

        [DataMember]
        public virtual FileUploadModel ScreenshotFile { get; set; }
    }
}