using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class DistrictViewModel
    {
        // Foreign key
        [DataMember]
        public virtual string CountryName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string StateName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string DivisionName { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual string DistrictCode { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}