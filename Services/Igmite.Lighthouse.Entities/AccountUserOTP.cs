using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("AccountUserOTPs")]
    public partial class AccountUserOTP : BaseEntityCreation
    {
        public AccountUserOTP()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AccountOTPId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Account OTPId", Description = "Account OTPId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountOTPId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Account Id", Description = "Account Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        // Foreign key
        [DataMember]
        [Column("OTPId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "OTPId", Description = "OTPId", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid OTPId { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Accounts", Description = "Accounts")]
        //public virtual Account Account { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "User OTPDetails", Description = "User OTPDetails")]
        //public virtual UserOTPDetail Detail { get; set; }
        #endregion Public Properties
    }
}
