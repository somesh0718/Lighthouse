using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Models
{
    public class IssueMappingViewModel
    {
        [Key, DataMember]
        public virtual Guid IssueMappingId { get; set; }

        [DataMember]
        public virtual string MainIssue { get; set; }

        [DataMember]
        public virtual string SubIssue { get; set; }

        [DataMember]
        public virtual string IssueCategory { get; set; }

        [DataMember]
        public virtual string IssuePriority { get; set; }

        [DataMember]
        public virtual bool IsApplicableForVC { get; set; }

        [DataMember]
        public virtual bool IsApplicableForVT { get; set; }

        [DataMember]
        public virtual bool IsApplicableForHM { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
