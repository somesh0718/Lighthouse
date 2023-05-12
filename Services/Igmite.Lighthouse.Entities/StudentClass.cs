using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("StudentClasses")]
    public partial class StudentClass : BaseEntity
    {
        public StudentClass()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Student Id", Description = "Student Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "School", Description = "School")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

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
        [Column("FirstName")]
        [Display(Name = "First Name", Description = "First Name")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FirstName { get; set; }

        [DataMember]
        [Column("MiddleName")]
        [Display(Name = "Middle Name", Description = "Middle Name")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MiddleName { get; set; }

        [DataMember]
        [Column("LastName")]
        [Display(Name = "Last Name", Description = "Last Name")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LastName { get; set; }

        [DataMember]
        [Column("FullName")]
        [Display(Name = "Full Name", Description = "Full Name")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FullName { get; set; }

        [DataMember]
        [Column("Gender")]
        [Display(Name = "Gender", Description = "Gender")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Gender { get; set; }

        [DataMember]
        [Column("Mobile")]
        [Display(Name = "Mobile", Description = "Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("DateOfEnrollment", TypeName = "DATETIME")]
        [Display(Name = "Date Of Enrollment", Description = "Date Of Enrollment")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateOfEnrollment { get; set; }

        [DataMember]
        [Column("DateOfDropout", TypeName = "DATETIME")]
        [Display(Name = "Date Of Dropout", Description = "Date Of Dropout")]
        public virtual DateTime? DateOfDropout { get; set; }

        [DataMember]
        [Column("DropoutReason")]
        [Display(Name = "Dropout Reason", Description = "Dropout Reason")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DropoutReason { get; set; }

        [DataMember, JsonIgnore]
        [Column("DeletedBy")]
        [Display(Name = "Deleted By", Description = "Deleted By")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DeletedBy { get; set; }

        [DataMember, JsonIgnore]
        [Column("DeletedOn", TypeName = "DATETIME")]
        [Display(Name = "Deleted On", Description = "Deleted On")]
        public virtual DateTime? DeletedOn { get; set; }

        [DataMember]
        [Display(Name = "VTId", Description = "VTId")]
        public virtual Guid? VTId { get; set; }

        #endregion Public Properties
    }
}