using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SchoolCategoryViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SchoolCategoryId { get; set; }

        [DataMember]
        public virtual string CategoryName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
