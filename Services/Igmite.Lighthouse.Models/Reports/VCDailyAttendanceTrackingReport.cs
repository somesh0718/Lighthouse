using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VCDailyAttendanceTrackingReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VCDailyReportingId { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCMobile { get; set; }

        [DataMember]
        public virtual string VCEmail { get; set; }

        [DataMember]
        public virtual DateTime? VCDateOfJoining { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string ReportType { get; set; }

        [DataMember]
        public virtual DateTime DateOfReport { get; set; }

        [DataMember]
        public virtual DateTime ActualSubmissionDateTime { get; set; }

        [DataMember]
        public virtual string GeoLocation { get; set; }

        [DataMember]
        public virtual Int64 SrNo { get; set; }
    }
}