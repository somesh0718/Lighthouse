using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTIssueReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolAllottedYear { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string VTEmail { get; set; }

        [DataMember]
        public virtual DateTime IssueReportDate { get; set; }

        [DataMember]
        public virtual string MainIssue { get; set; }

        [DataMember]
        public virtual string SubIssue { get; set; }

        [DataMember]
        public virtual string IssuePriority { get; set; }

        [DataMember]
        public virtual string StudentClass { get; set; }

        [DataMember]
        public virtual string Month { get; set; }

        [DataMember]
        public virtual string StudentType { get; set; }

        [DataMember]
        public virtual int NoOfStudents { get; set; }

        [DataMember]
        public virtual string IssueDetails { get; set; }

        [DataMember]
        public virtual string ApprovalStatus { get; set; }

        [DataMember]
        public virtual DateTime? ApprovedDate { get; set; }
    }
}