using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTGuestLectureConductedViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTGuestLectureId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        public virtual string GLType { get; set; }

        [DataMember]
        public virtual string GLTopic { get; set; }

        [DataMember]
        public virtual string GLName { get; set; }

        [DataMember]
        public virtual string ApprovalStatus { get; set; }

        [DataMember]
        public virtual DateTime? ApprovedDate { get; set; }
    }
}