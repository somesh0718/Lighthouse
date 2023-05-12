using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class StudentClassViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid StudentId { get; set; }

        // Foreign key
        [Key, DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        // Foreign key
        [Key, DataMember]
        public virtual string ClassName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectionName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime DateOfEnrollment { get; set; }

        [DataMember]
        public virtual string DeletedBy { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsAYRollover { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}