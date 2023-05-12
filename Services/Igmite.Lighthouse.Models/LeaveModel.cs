using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class LeaveModel
    {
        public LeaveModel()
        {
        }

        [DataMember]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveTypeId { get; set; }

        [DataMember]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveModeId { get; set; }

        [DataMember]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveApprovalStatus { get; set; }

        [DataMember]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveApprover { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveReason { get; set; }
    }
}