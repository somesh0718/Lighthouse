using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VCDailyReportingViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VCDailyReportingId { get; set; }

        [DataMember]
        public virtual string VCId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual DateTime ReportDate { get; set; }

        [DataMember]
        public virtual string ReportType { get; set; }

        [DataMember]
        public virtual string WorkTypes { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}