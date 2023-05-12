using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("StudentClassMapping")]
    public partial class StudentClassMapping : BaseEntity
    {
        public StudentClassMapping()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("StudentClassMappingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Student Class Mapping Id", Description = "Student Class Mapping Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentClassMappingId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "SchoolId", Description = "SchoolId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Class", Description = "Class")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectionId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Section", Description = "Section")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Id", Description = "VT Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Student Id", Description = "Student Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        [Column("StudentRollNumber")]
        [Display(Name = "Student Roll Number", Description = "Student Roll Number")]
        [MaxLength(30, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StudentRollNumber { get; set; }

        #endregion Public Properties
    }
}