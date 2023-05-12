using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTStudentResultOtherSubjectViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTStudentResultOtherSubjectId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string SubjectName { get; set; }

        [DataMember]
        public virtual int SubjectNumber { get; set; }

        [DataMember]
        public virtual int SubjectMarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
