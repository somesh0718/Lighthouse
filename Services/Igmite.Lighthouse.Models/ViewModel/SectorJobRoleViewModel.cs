using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SectorJobRoleViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SectorJobRoleId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectorName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string QPCode { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}