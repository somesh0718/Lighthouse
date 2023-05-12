using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("LogoutHistories")]
    public partial class LogoutHistory
    {
        public LogoutHistory()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("LoginUniqueId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Logout Unique Id", Description = "Logout Unique Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid LoginUniqueId { get; set; }

        [DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Account Id", Description = "Account Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        [Column("UserId",  Order = 3)]
        [Display(Name = "User Id", Description = "User Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string UserId { get; set; }

        [DataMember]
        [Column("LoginDateTime", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Login Date & Time", Description = "Login Date & Time", Order = 4)]
        public virtual DateTime LoginDateTime { get; set; }

        [DataMember]
        [Column("LogoutDateTime", TypeName = "DATETIME", Order = 5)]
        [Display(Name = "Logout Date & Time", Description = "Logout Date & Time", Order = 5)]
        public virtual DateTime LogoutDateTime { get; set; }

        [DataMember]
        [Column("AuthToken", Order = 6)]
        [Display(Name = "Auth Token", Description = "Auth Token", Order = 6)]
        [MaxLength(500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AuthToken { get; set; }

        [DataMember]
        [Column("IsMobile", TypeName = "BIT", Order = 6)]
        public virtual bool? IsMobile { get; set; }

        #endregion Public Properties
    }
}