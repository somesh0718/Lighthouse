using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class StudentClassDetailViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectionName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        // Primary key
        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string FatherName { get; set; }

        [DataMember]
        public virtual string MotherName { get; set; }

        [DataMember]
        public virtual string GuardianName { get; set; }

        [DataMember]
        public virtual DateTime? DateOfBirth { get; set; }

        [DataMember]
        public virtual string AadhaarNumber { get; set; }

        [DataMember]
        public virtual string StudentRollNumber { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsSubmittedClassDetails { get; set; }

        [DataMember]
        public virtual string AssessmentConducted { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}