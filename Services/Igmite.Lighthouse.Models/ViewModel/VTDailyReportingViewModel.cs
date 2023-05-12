using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTDailyReportingViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        public virtual string ReportType { get; set; }

        [DataMember]
        public virtual string WorkTypes { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}