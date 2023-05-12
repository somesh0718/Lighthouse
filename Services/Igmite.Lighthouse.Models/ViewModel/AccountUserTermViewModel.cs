using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class AccountUserTermViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AccountTermsId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid AccountId { get; set; }

        // Foreign key
        [DataMember]
        public virtual Guid TermsConditionId { get; set; }

        [DataMember]
        public virtual bool IsLatestTerms { get; set; }

        [DataMember]
        public virtual DateTime? AcceptedDate { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
