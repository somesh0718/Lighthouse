using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTGuestLectureConductedRequest : VTGuestLectureConductedModel
    {
    }

    public class VTGuestLectureConductedResponse : VTGuestLectureConductedModel
    {
    }

    public class SearchVTGuestLectureConductedRequest : BaseSearchModel
    {
    }

    public class VTGuestLectureApprovalRequest
    {
        [DataMember]
        [Display(Name = "VT Guest Lecture Id", Description = "VT Guest Lecture Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

        [DataMember]
        [Display(Name = "Approval Status", Description = "Approval Status", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalStatus { get; set; }
    }
}