using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SiteHeaderViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SiteHeaderId { get; set; }

        [DataMember]
        public virtual string ShortName { get; set; }

        [DataMember]
        public virtual string LongName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
