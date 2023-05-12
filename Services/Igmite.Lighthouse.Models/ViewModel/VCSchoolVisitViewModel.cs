using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VCSchoolVisitViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VCSchoolVisitId { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual DateTime ReportDate { get; set; }

        [DataMember]
        public virtual string VTReportSubmitted { get; set; }

        [DataMember]
        public virtual int VTWorkingDays { get; set; }

        [DataMember]
        public virtual int VRLeaveDays { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}