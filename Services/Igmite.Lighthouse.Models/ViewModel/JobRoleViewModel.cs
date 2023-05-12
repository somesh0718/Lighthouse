using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class JobRoleViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid JobRoleId { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string QPCode { get; set; }
 
        [DataMember]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}