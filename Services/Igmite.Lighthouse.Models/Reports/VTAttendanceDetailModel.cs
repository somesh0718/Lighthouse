using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTAttendanceDetailModel
    {
        public VTAttendanceDetailModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Guid VTId { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual string ReportType { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual DateTime ReportingDate { get; set; }
    }
}