using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class UserOTPDetailViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid OTPId { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string OTPToken { get; set; }

        [DataMember]
        public virtual DateTime ExpireOn { get; set; }

        [DataMember]
        public virtual bool IsRedeemed { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
