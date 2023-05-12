using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SectionViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SectionId { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
