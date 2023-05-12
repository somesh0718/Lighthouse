using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class StateViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual string StateCode { get; set; }

        [DataMember]
        public virtual string StateId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string CountryCode { get; set; }

        // Foreign key
        [DataMember]
        public virtual string CountryName { get; set; }

        [DataMember]
        public virtual string StateName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual int SequenceNo { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
