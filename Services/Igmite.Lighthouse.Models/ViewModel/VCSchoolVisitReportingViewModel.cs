using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VCSchoolVisitReportingViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VCSchoolVisitReportingId { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual DateTime VisitDate { get; set; }

        [DataMember]
        public virtual Int32 TotalBoys { get; set; }

        [DataMember]
        public virtual Int32 TotalGirls { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}