using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ForgotPasswordHistoryViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid ForgotPasswordId { get; set; }

        [DataMember]
        public virtual string EmailId { get; set; }

        [DataMember]
        public virtual string PasswordResetUrl { get; set; }

        [DataMember]
        public virtual string UserIPAddress { get; set; }

        [DataMember]
        public virtual DateTime? RequestDate { get; set; }

        [DataMember]
        public virtual DateTime? ResetPasswordDate { get; set; }
    }
}
