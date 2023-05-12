using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SchoolVEIncharges")]
    public partial class SchoolVEIncharge : BaseEntity
    {
        public SchoolVEIncharge()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VEIId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VEIId", Description = "VEIId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VEIId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Vocational Trainner", Description = "Vocational Trainner", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "School", Description = "School", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        [Column("FirstName", Order = 3)]
        [Display(Name = "First Name", Description = "First Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FirstName { get; set; }

        [DataMember]
        [Column("MiddleName", Order = 4)]
        [Display(Name = "Middle Name", Description = "Middle Name", Order = 4)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MiddleName { get; set; }

        [DataMember]
        [Column("LastName", Order = 5)]
        [Display(Name = "Last Name", Description = "Last Name", Order = 5)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LastName { get; set; }

        [DataMember]
        [Column("FullName", Order = 6)]
        [Display(Name = "Full Name", Description = "Full Name", Order = 6)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FullName { get; set; }

        [DataMember]
        [Column("Mobile", Order = 7)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 7)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("Mobile1", Order = 8)]
        [Display(Name = "Mobile1", Description = "Mobile1", Order = 8)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile1 { get; set; }

        [DataMember]
        [Column("Email", Order = 9)]
        [Display(Name = "Email", Description = "Email", Order = 9)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Email { get; set; }

        [DataMember]
        [Column("Gender", Order = 10)]
        [Display(Name = "Gender", Description = "Gender", Order = 10)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Gender { get; set; }

        [DataMember]
        [Column("DateOfJoining", TypeName = "DATETIME", Order = 11)]
        [Display(Name = "Date Of Joining", Description = "Date Of Joining", Order = 11)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        [Column("DateOfResignationFromRoleSchool", TypeName = "DATETIME", Order = 12)]
        [Display(Name = "Date Of Resignation From Role School", Description = "Date Of Resignation From Role School", Order = 12)]
        public virtual DateTime? DateOfResignationFromRoleSchool { get; set; }

        #endregion Public Properties
    }
}