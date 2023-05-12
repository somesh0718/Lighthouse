using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTStudentAssessmentViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTStudentAssessmentId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string TestimonialType { get; set; }

        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string StudentGender { get; set; }

        [DataMember]
        public virtual string TestimonialTitle { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}