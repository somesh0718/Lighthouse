using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VCSchoolVisitReportingModel : VCSchoolVisitReporting
    {
        public VCSchoolVisitReportingModel()
        {
        }

        [DataMember]
        public virtual FileUploadModel SVPhotoWithPrincipalFile { get; set; }

        [DataMember]
        public virtual FileUploadModel SVPhotoWithStudentFile { get; set; }
    }
}