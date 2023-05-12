using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class RoleTransactionViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid RoleTransactionId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid RoleId { get; set; }

        [DataMember]
        public virtual string RoleName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string TransactionName { get; set; }

        [DataMember]
        public virtual bool Rights { get; set; }

        [DataMember]
        public virtual bool CanAdd { get; set; }

        [DataMember]
        public virtual bool CanEdit { get; set; }

        [DataMember]
        public virtual bool CanDelete { get; set; }

        [DataMember]
        public virtual bool CanView { get; set; }

        [DataMember]
        public virtual bool IsPublic { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}