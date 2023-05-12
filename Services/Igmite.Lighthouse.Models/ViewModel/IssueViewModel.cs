using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Models
{
    public class IssueViewModel
    {
        [Key, DataMember]
        public virtual Guid IssueReportingId { get; set; }

        [DataMember]
        public virtual string ReviewBy { get; set; }

        [DataMember]
        public virtual string ReportedType { get; set; }

        [DataMember]
        public virtual string ReportedBy { get; set; }

        [DataMember]
        public virtual DateTime IssueReportDate { get; set; }

        [DataMember]
        public virtual string MainIssue { get; set; }

        [DataMember]
        public virtual string SubIssue { get; set; }

        [DataMember]
        public virtual string IssueDetails { get; set; }
        
        [DataMember]
        public virtual string IssueCategory { get; set; }

        [DataMember]
        public virtual string ApprovalStatus { get; set; }

        //[DataMember]
        //public virtual string IssuePriority { get; set; }

        //[DataMember]
        //public virtual bool IsActive { get; set; }
    }
}
