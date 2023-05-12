using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("StudentClassDetails")]
    public partial class StudentClassDetail : BaseEntity
    {
        public StudentClassDetail()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember, ForeignKey(name: "StudentId")]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Student Id", Description = "Student Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 2)]      
        public virtual Guid? SectorId { get; set; }

        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 3)]
        public virtual Guid? JobRoleId { get; set; }

        [DataMember]
        [Column("FatherName", Order = 4)]
        [Display(Name = "Father Name", Description = "Father Name", Order = 4)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string FatherName { get; set; }

        [DataMember]
        [Column("MotherName", Order = 5)]
        [Display(Name = "Mother Name", Description = "Mother Name", Order = 5)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string MotherName { get; set; }

        [DataMember]
        [Column("GuardianName", Order = 6)]
        [Display(Name = "Guardian Name", Description = "Guardian Name", Order = 6)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GuardianName { get; set; }

        [DataMember]
        [Column("DateOfBirth", TypeName = "DATETIME", Order = 7)]
        [Display(Name = "Date Of Birth", Description = "Date Of Birth", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateOfBirth { get; set; }

        [DataMember]
        [Column("AadhaarNumber", Order = 8)]
        [Display(Name = "Aadhaar Number", Description = "Aadhaar Number", Order = 8)]
        [MaxLength(12, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AadhaarNumber { get; set; }

        [DataMember]
        [Column("StudentRollNumber", Order = 9)]
        [Display(Name = "Student Roll Number", Description = "Student Roll Number", Order = 9)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string StudentRollNumber { get; set; }

        [DataMember]
        [Column("SocialCategory", Order = 10)]
        [Display(Name = "Social Category", Description = "Social Category", Order = 10)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string SocialCategory { get; set; }

        [DataMember]
        [Column("Religion", Order = 11)]
        [Display(Name = "Religion", Description = "Other Category", Order = 11)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Religion { get; set; }

        [DataMember]
        [Column("CWSNStatus", Order = 12)]
        [Display(Name = "CWSN Status", Description = "CWSN Status", Order = 12)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CWSNStatus { get; set; }

        [DataMember]
        [Column("Mobile")]
        [Display(Name = "Mobile", Description = "Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("Mobile1")]
        [Display(Name = "Mobile 1", Description = "Mobile 1")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile1 { get; set; }

        [DataMember]
        [Column("WhatsAppNo")]
        [Display(Name = "WhatsApp No", Description = "WhatsApp No")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string WhatsAppNo { get; set; }

        [DataMember]
        [Column("AssessmentConducted")]
        [Display(Name = "Assessment to be conducted", Description = "Assessment to be conducted")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessmentConducted { get; set; }

        [DataMember]
        [Column("StreamId")]
        [Display(Name = "Stream", Description = "Stream")]
        public virtual string StreamId { get; set; }

        [DataMember]
        [Column("IsStudentVE9And10")]
        [Display(Name = "Did students have VE in 9 & 10th?", Description = "Did students have VE in 9 & 10th?")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsStudentVE9And10 { get; set; }

        [DataMember]
        [Column("IsSameStudentTrade")]
        [Display(Name = "Is the student continuing the same trade?", Description = "Is the student continuing the same trade?")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsSameStudentTrade { get; set; }

        #endregion Public Properties
    }
}