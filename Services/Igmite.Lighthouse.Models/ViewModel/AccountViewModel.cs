using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class AccountViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        public virtual string LoginId { get; set; }

        [DataMember]
        public virtual string UserName { get; set; }

        [DataMember]
        public virtual string EmailId { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string AccountType { get; set; }

        [DataMember]
        public virtual string RoleCode { get; set; }

        [DataMember]
        public virtual DateTime? LastLoginDate { get; set; }

        [DataMember]
        public virtual bool IsLocked { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}