using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTStudentVEResultViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTStudentVEResultId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual DateTime DateIssuence { get; set; }

        [DataMember]
        public virtual int ExternalMarks { get; set; }

        [DataMember]
        public virtual int TheoryMarks { get; set; }

        [DataMember]
        public virtual int InternalMarks { get; set; }

        [DataMember]
        public virtual int TotalMarks { get; set; }

        [DataMember]
        public virtual string Grade { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
