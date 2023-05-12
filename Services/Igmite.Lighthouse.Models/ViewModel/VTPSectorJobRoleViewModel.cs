using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTPSectorJobRoleViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTPSectorJobRoleId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string VTPName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectorName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}