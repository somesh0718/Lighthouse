using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class HMIssueReportingRequest : HMIssueReportingModel
    {
    }

    public class HMIssueReportingResponse : HMIssueReportingModel
    {
    }

    public class SearchHMIssueReportingRequest : BaseSearchModel
    {
    }

    public class HMIssueReportingApprovalRequest
    {
        [DataMember]
        [Display(Name = "HM Issue Reporting Id", Description = "HM Issue Reporting Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid HMIssueReportingId { get; set; }

        [DataMember]
        [Display(Name = "Approval Status", Description = "Approval Status", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalStatus { get; set; }

        [DataMember]
        [Display(Name = "Remarks", Description = "Remarks", Order = 3)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }
    }
}