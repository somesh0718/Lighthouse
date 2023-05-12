using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VocationalTrainers")]
    public partial class VocationalTrainer : BaseEntity
    {
        public VocationalTrainer()
        {
            this.VCTrainer = new VCTrainerMap();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTId", Description = "VTId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

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
        [Column("Mobile")]
        [Display(Name = "Mobile", Description = "Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("Mobile1")]
        [Display(Name = "Mobile1", Description = "Mobile1")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile1 { get; set; }

        [DataMember]
        [Column("Email")]
        [Display(Name = "Email", Description = "Email")]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string Email { get; set; }

        [DataMember]
        [Column("Gender")]
        [Display(Name = "Gender", Description = "Gender")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Gender { get; set; }

        [DataMember]
        [Column("DateOfBirth", TypeName = "DATETIME")]
        [Display(Name = "Date Of Birth", Description = "Date Of Birth")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateOfBirth { get; set; }

        [DataMember]
        [Column("SocialCategory")]
        [Display(Name = "Social Category", Description = "Social Category")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string SocialCategory { get; set; }

        [DataMember]
        [Column("AcademicQualification")]
        [Display(Name = "Academic Qualification", Description = "Academic Qualification")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string AcademicQualification { get; set; }

        [DataMember]
        [Column("ProfessionalQualification")]
        [Display(Name = "Professional Qualification", Description = "Professional Qualification")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string ProfessionalQualification { get; set; }

        [DataMember]
        [Column("ProfessionalQualificationDetails")]
        [Display(Name = "Professional Qualification Details", Description = "Professional Qualification Details")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ProfessionalQualificationDetails { get; set; }

        [DataMember]
        [Column("IndustryExperienceMonths", TypeName = "INT")]
        [Display(Name = "Industry Experience Months", Description = "Industry Experience Months")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int IndustryExperienceMonths { get; set; }

        [DataMember]
        [Column("TrainingExperienceMonths", TypeName = "INT")]
        [Display(Name = "Training Experience Months", Description = "Training Experience Months")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int TrainingExperienceMonths { get; set; }

        [DataMember]
        [Column("AadhaarNumber")]
        [Display(Name = "Aadhaar Number", Description = "Aadhaar Number")]
        [MaxLength(12, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string AadhaarNumber { get; set; }

        #endregion Public Properties

        [NotMapped, JsonIgnore]
        public virtual VCTrainerMap VCTrainer { get; set; }
    }
}