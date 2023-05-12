using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTMonthlyTeachingPlanViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTMonthlyTeachingPlanId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string Month { get; set; }

        [DataMember]
        public virtual DateTime? WeekStartDate { get; set; }

        [DataMember]
        public virtual DateTime? WeekendDate { get; set; }

        [DataMember]
        public virtual string ModulesPlanned { get; set; }

        [DataMember]
        public virtual DateTime? IVPlannedDate { get; set; }

        [DataMember]
        public virtual string IVVCAttend { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
