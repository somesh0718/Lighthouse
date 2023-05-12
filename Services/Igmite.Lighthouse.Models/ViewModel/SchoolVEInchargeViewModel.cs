using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SchoolVEInchargeViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VEIId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string FullName { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string Email { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime? DateOfJoining { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}