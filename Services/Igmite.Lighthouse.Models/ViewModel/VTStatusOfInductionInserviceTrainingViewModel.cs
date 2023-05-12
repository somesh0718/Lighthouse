using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTStatusOfInductionInserviceTrainingViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTStatusOfInductionInserviceTrainingId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SchoolName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectorName { get; set; }

        // Foreign key
        [DataMember]
        public virtual DateTime DateOfAllocation { get; set; }

        [DataMember]
        public virtual string IndustryTrainingStatus { get; set; }

        [DataMember]
        public virtual string InserviceTrainingStatus { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}