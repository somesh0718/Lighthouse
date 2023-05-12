using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VCIssueReportingViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid IssueReportingId { get; set; }

        [DataMember]
        public virtual DateTime IssueReportDate { get; set; }

        [DataMember]
        public virtual string MainIssue { get; set; }

        [DataMember]
        public virtual string SubIssue { get; set; }

        [DataMember]
        public virtual string StudentType { get; set; }

        [DataMember]
        public virtual int NoOfStudents { get; set; }

        [DataMember]
        public virtual string ApprovalStatus { get; set; }
    }
}