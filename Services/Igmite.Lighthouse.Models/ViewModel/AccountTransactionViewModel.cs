using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class AccountTransactionViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AccountTransactionId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string UserName { get; set; }

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
        public virtual bool CanExport { get; set; }

        [DataMember]
        public virtual bool ListView { get; set; }

        [DataMember]
        public virtual bool BasicView { get; set; }

        [DataMember]
        public virtual bool DetailView { get; set; }

        [DataMember]
        public virtual bool IsPublic { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}