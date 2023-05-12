using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class AccountRoleViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AccountRoleId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid AccountId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid RoleId { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
