using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class TransactionViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid TransactionId { get; set; }

        [DataMember]
        public virtual string Code { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string PageTitle { get; set; }

        [DataMember]
        public virtual string PageDescription { get; set; }

        [DataMember]
        public virtual string UrlAction { get; set; }

        [DataMember]
        public virtual string UrlController { get; set; }

        [DataMember]
        public virtual string UrlPara { get; set; }

        [DataMember]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
