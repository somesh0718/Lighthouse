using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class DRPDailyReportingViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid DRPDailyReportingId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string DRPName { get; set; }

        [DataMember]
        public virtual DateTime ReportDate { get; set; }

        [DataMember]
        public virtual string ReportType { get; set; }

        [DataMember]
        public virtual string WorkTypes { get; set; }
    }
}