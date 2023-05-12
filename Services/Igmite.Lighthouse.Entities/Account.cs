using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Accounts")]
    public partial class Account : BaseEntity
    {
        public Account()
        {
            this.AccountWorkLocations = new List<AccountWorkLocation>();
            this.DeletedWorkLocationIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Account Id", Description = "Account Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        [Column("LoginId", Order = 2)]
        [Display(Name = "Login Id", Description = "Login Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(135, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LoginId { get; set; }

        [DataMember]
        [Column("Password", Order = 3)]
        [Display(Name = "Password", Description = "Password", Order = 3)]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Password { get; set; }

        [DataMember]
        [Column("UserId", Order = 4)]
        [Display(Name = "User Id", Description = "User Id", Order = 4)]
        [MaxLength(135, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UserId { get; set; }

        [DataMember]
        [Column("UserName", Order = 5)]
        [Display(Name = "User Name", Description = "User Name", Order = 5)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UserName { get; set; }

        [DataMember]
        [Column("FirstName", Order = 6)]
        [Display(Name = "First Name", Description = "First Name", Order = 6)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FirstName { get; set; }

        [DataMember]
        [Column("LastName", Order = 7)]
        [Display(Name = "Last Name", Description = "Last Name", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LastName { get; set; }

        [DataMember]
        [Column("Designation", Order = 8)]
        [Display(Name = "Designation", Description = "Designation", Order = 8)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Designation { get; set; }

        [DataMember]
        [Column("EmailId", Order = 9)]
        [Display(Name = "Email Id", Description = "Email Id", Order = 9)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId { get; set; }

        [DataMember]
        [Column("Mobile", Order = 10)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 10)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("AccountType")]
        [Display(Name = "Account Type", Description = "Account Type")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AccountType { get; set; }

        [DataMember]
        [Column("StateId")]
        [Display(Name = "State", Description = "State")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateId { get; set; }

        [DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Division", Description = "Division")]
        public virtual Guid? DivisionId { get; set; }

        [DataMember]
        [Column("DistrictId")]
        [Display(Name = "District", Description = "District")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictId { get; set; }

        [DataMember]
        [Column("BlockId")]
        [Display(Name = "Block", Description = "Block")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string BlockId { get; set; }

        [DataMember]
        [Column("ClusterId")]
        [Display(Name = "Cluster", Description = "Cluster")]
		[MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClusterId { get; set; }

        [DataMember]
        [Column("PasswordUpdateDate", TypeName = "DATETIME")]
        [Display(Name = "Password Update Date", Description = "Password Update Date")]
        public virtual DateTime? PasswordUpdateDate { get; set; }

        [DataMember]
        [Column("PasswordExpiredOn", TypeName = "DATETIME")]
        [Display(Name = "Password Expired On", Description = "Password Expired On")]
        public virtual DateTime? PasswordExpiredOn { get; set; }

        [DataMember]
        [Column("LastLoginDate", TypeName = "DATETIME")]
        [Display(Name = "Last Login Date", Description = "Last Login Date")]
        public virtual DateTime? LastLoginDate { get; set; }

        [DataMember]
        [Column("InvalidAttempt", TypeName = "INT")]
        [Display(Name = "Invalid Attempt", Description = "Invalid Attempt")]
        public virtual int? InvalidAttempt { get; set; }

        [DataMember]
        [Column("IsPasswordReset", TypeName = "BIT")]
        [Display(Name = "Is Password Reset?", Description = "Is Password Reset?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsPasswordReset { get; set; }

        [DataMember]
        [Column("PasswordResetToken")]
        [Display(Name = "Password Reset Token", Description = "Password Reset Token")]
        [MaxLength(500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PasswordResetToken { get; set; }

        [DataMember]
        [Column("AuthToken")]
        [Display(Name = "Auth Token", Description = "Auth Token")]
        [MaxLength(-1, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AuthToken { get; set; }

        [DataMember]
        [Column("TokenExpiredOn", TypeName = "DATETIME")]
        [Display(Name = "Token Expired On", Description = "Token Expired On")]
        public virtual DateTime? TokenExpiredOn { get; set; }

        [DataMember]
        [Column("IsLocked", TypeName = "BIT")]
        [Display(Name = "Is Locked?", Description = "Is Locked?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsLocked { get; set; }

        // Navigation properties
        [NotMapped, JsonIgnore]
        [Display(Name = "AccountWorkLocations", Description = "AccountWorkLocations")]
        public virtual IList<AccountWorkLocation> AccountWorkLocations { get; set; }

        // Navigation properties
        [NotMapped, JsonIgnore]
        public virtual IList<Guid> DeletedWorkLocationIds { get; set; }

        #endregion Public Properties
    }
}