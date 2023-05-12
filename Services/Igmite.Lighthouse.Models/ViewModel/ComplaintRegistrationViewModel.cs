using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ComplaintRegistrationViewModel
    {
        [DataMember, Key]
        public virtual Guid ComplaintRegistrationId { get; set; }

        [DataMember]
        public virtual string UserType { get; set; }

        [DataMember]
        public virtual string UserName { get; set; }

        [DataMember]
        public virtual string EmailId { get; set; }

        [DataMember]
        public virtual string Subject { get; set; }

        [DataMember]
        public virtual string IssueStatus { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}