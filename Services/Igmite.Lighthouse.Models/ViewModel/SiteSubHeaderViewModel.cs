using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SiteSubHeaderViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SiteSubHeaderId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SiteHeaderName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string TransactionName { get; set; }

        [DataMember]
        public virtual bool IsHeaderMenu { get; set; }

        [DataMember]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
