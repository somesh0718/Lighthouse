using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VCAttendanceDetailModel
    {
        public VCAttendanceDetailModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Guid VCId { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual string ReportType { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual DateTime ReportingDate { get; set; }
    }
}