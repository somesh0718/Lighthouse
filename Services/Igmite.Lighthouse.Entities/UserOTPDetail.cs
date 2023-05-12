using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("UserOTPDetails")]
    public partial class UserOTPDetail : BaseEntity
    {
        public UserOTPDetail()
        {
            //this.AccountOTPs = new List<AccountUserOTP>();

            //this.DeletedAccountOTPIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("OTPId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "OTPId", Description = "OTPId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid OTPId { get; set; }

        [DataMember]
        [Column("Mobile", Order = 2)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 2)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("OTPToken", Order = 3)]
        [Display(Name = "OTPToken", Description = "OTPToken", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OTPToken { get; set; }

        [DataMember]
        [Column("ExpireOn", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Expire On", Description = "Expire On", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ExpireOn { get; set; }

        [DataMember]
        [Column("IsRedeemed", TypeName = "BIT", Order = 5)]
        [Display(Name = "Is Redeemed?", Description = "Is Redeemed?", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsRedeemed { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Account OTPs", Description = "Account OTPs")]
        //public virtual IList<AccountUserOTP> AccountOTPs { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedAccountOTPIds { get; set; }

        #endregion Public Properties
    }
}
