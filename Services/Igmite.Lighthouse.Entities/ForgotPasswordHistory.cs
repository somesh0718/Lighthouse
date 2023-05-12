using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("ForgotPasswordHistories")]
    public partial class ForgotPasswordHistory : RequestModel
    {
        public ForgotPasswordHistory()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("ForgotPasswordId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Forgot Password Id", Description = "Forgot Password Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ForgotPasswordId { get; set; }

        [DataMember]
        [Column("EmailId", Order = 2)]
        [Display(Name = "Email Id", Description = "Email Id", Order = 2)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId { get; set; }

        [DataMember]
        [Column("PasswordResetUrl", Order = 3)]
        [Display(Name = "Password Reset Url", Description = "Password Reset Url", Order = 3)]
        [MaxLength(500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PasswordResetUrl { get; set; }

        [DataMember]
        [Column("UserIPAddress", Order = 4)]
        [Display(Name = "User IPAddress", Description = "User IPAddress", Order = 4)]
        [MaxLength(30, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UserIPAddress { get; set; }

        [DataMember]
        [Column("RequestDate", TypeName = "DATETIME", Order = 5)]
        [Display(Name = "Request Date", Description = "Request Date", Order = 5)]
        public virtual DateTime? RequestDate { get; set; }

        [DataMember]
        [Column("ResetPasswordDate", TypeName = "DATETIME", Order = 6)]
        [Display(Name = "Reset Password Date", Description = "Reset Password Date", Order = 6)]
        public virtual DateTime? ResetPasswordDate { get; set; }
        #endregion Public Properties
    }
}
