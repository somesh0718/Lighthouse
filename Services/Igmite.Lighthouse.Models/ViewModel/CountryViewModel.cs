using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class CountryViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual string CountryCode { get; set; }

        [DataMember]
        public virtual string CountryName { get; set; }

        [DataMember]
        public virtual string ISDCode { get; set; }

        [DataMember]
        public virtual string ISOCode { get; set; }

        [DataMember]
        public virtual string CurrencyName { get; set; }

        [DataMember]
        public virtual string CurrencyCode { get; set; }

        [DataMember]
        public virtual string CountryIcon { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
