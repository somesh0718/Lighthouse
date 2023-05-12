using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTStudentResultOtherSubjects")]
    public partial class VTStudentResultOtherSubject : BaseEntity
    {
        public VTStudentResultOtherSubject()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTStudentResultOtherSubjectId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTStudent Result Other Subject Id", Description = "VTStudent Result Other Subject Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTStudentResultOtherSubjectId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Student Id", Description = "Student Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        [Column("SubjectName", Order = 4)]
        [Display(Name = "Subject Name", Description = "Subject Name", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SubjectName { get; set; }

        [DataMember]
        [Column("SubjectNumber", TypeName = "INT", Order = 5)]
        [Display(Name = "Subject Number", Description = "Subject Number", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int SubjectNumber { get; set; }

        [DataMember]
        [Column("SubjectMarks", TypeName = "INT", Order = 6)]
        [Display(Name = "Subject Marks", Description = "Subject Marks", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int SubjectMarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Student Classes", Description = "Student Classes")]
        //public virtual StudentClass StudentClass { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTClasses", Description = "VTClasses")]
        //public virtual VTClass VTClass { get; set; }
        #endregion Public Properties
    }
}
