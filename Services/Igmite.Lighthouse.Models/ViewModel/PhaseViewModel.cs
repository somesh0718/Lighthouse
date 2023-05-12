using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class PhaseViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid PhaseId { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
