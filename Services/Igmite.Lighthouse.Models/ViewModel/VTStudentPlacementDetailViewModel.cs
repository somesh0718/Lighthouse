using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTStudentPlacementDetailViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTStudentPlacementDetailId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string PlacementApplyStatus { get; set; }

        [DataMember]
        public virtual string PlacementStatus { get; set; }

        [DataMember]
        public virtual string ApprenticeshipApplyStatus { get; set; }

        [DataMember]
        public virtual string ApprenticeshipStatus { get; set; }

        [DataMember]
        public virtual string HigherEducationVE { get; set; }

        [DataMember]
        public virtual string HigherEductaionOther { get; set; }

        [DataMember]
        public virtual string StudentPlacementStatus { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
