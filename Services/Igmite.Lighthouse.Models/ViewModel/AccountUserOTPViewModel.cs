using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class AccountUserOTPViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AccountOTPId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid AccountId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid OTPId { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
