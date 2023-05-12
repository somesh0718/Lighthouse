using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SectorViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}