using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTFieldIndustryVisitConductedViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        public virtual string FVOrganisation { get; set; }

        [DataMember]
        public virtual string FVContactPersonName { get; set; }

        [DataMember]
        public virtual string ApprovalStatus { get; set; }

        [DataMember]
        public virtual DateTime? ApprovedDate { get; set; }
    }
}