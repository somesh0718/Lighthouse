using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTFieldIndustryVisitConductedRequest : VTFieldIndustryVisitConductedModel
    {
    }

    public class VTFieldIndustryVisitConductedResponse : VTFieldIndustryVisitConductedModel
    {
    }

    public class SearchVTFieldIndustryVisitConductedRequest : BaseSearchModel
    {
    }

    public class VTFieldIndustryApprovalRequest
    {
        [DataMember]
        [Display(Name = "VT Field Industry Visit Conducted Id", Description = "VT Field Industry Visit Conducted Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

        [DataMember]
        [Display(Name = "Approval Status", Description = "Approval Status", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalStatus { get; set; }
    }
}