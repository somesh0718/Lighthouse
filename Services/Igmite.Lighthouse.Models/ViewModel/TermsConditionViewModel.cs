using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class TermsConditionViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid TermsConditionId { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual DateTime? ApplicableFrom { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
