using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class DivisionViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid DivisionId { get; set; }

        [DataMember]
        public virtual string StateName { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}