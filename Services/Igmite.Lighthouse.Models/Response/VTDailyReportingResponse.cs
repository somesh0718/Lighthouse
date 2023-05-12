using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTDailyReportingRequest : VTDailyReportingModel
    {
    }

    public class VTDailyReportingResponse : VTDailyReportingModel
    {
    }

    public class SearchVTDailyReportingRequest : BaseSearchModel
    {
    }

    public class VTDailyReportingApprovalRequest
    {
        [DataMember]
        [Display(Name = "VT Daily Reporting Id", Description = "VT Daily Reporting Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Display(Name = "Approval Status", Description = "Approval Status", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalStatus { get; set; }
    }
}