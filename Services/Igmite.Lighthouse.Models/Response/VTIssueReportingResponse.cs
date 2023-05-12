using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTIssueReportingRequest : VTIssueReportingModel
    {
    }

    public class VTIssueReportingResponse : VTIssueReportingModel
    {
    }

    public class SearchVTIssueReportingRequest : BaseSearchModel
    {
    }

    public class VTIssueReportingApprovalRequest
    {
        [DataMember]
        [Display(Name = "VT Issue Reporting Id", Description = "VT Issue Reporting Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTIssueReportingId { get; set; }

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