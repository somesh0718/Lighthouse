using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Models
{
    public class BroadcastMessageViewModel
    {
        [Key, DataMember]
        public virtual Guid BroadcastMessageId { get; set; }

        [DataMember]
        public virtual string MessageText { get; set; }

        [DataMember]
        public virtual DateTime FromDate { get; set; }

        [DataMember]
        public virtual DateTime ToDate { get; set; }

        [DataMember]
        public virtual string ApplicableFor { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
