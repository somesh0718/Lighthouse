using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("DRPRLeaves")]
    public partial class DRPRLeave : BaseEntityCreation
    {
        public DRPRLeave()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DRPRLeaveId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "DRPRLeave Id", Description = "DRPRLeave Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DRPRLeaveId { get; set; }

        [DataMember]
        [Column("DRPDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "DRPDaily Reporting Id", Description = "DRPDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DRPDailyReportingId { get; set; }

        [DataMember]
        [Column("LeaveTypeId", Order = 3)]
        [Display(Name = "Leave Type", Description = "Leave Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveTypeId { get; set; }

        [DataMember]
        [Column("LeaveApprovalStatus", Order = 4)]
        [Display(Name = "Leave Approval Status", Description = "Leave Approval Status", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveApprovalStatus { get; set; }

        [DataMember]
        [Column("LeaveApprover", Order = 5)]
        [Display(Name = "Leave Approver", Description = "Leave Approver", Order = 5)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveApprover { get; set; }

        [DataMember]
        [Column("LeaveReason", Order = 6)]
        [Display(Name = "Leave Reason", Description = "Leave Reason", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveReason { get; set; }

        #endregion Public Properties
    }
}