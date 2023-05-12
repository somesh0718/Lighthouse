using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCRLeaves")]
    public partial class VCRLeave : BaseEntityCreation
    {
        public VCRLeave()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCRLeaveId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VCRLeave Id", Description = "VCRLeave Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCRLeaveId { get; set; }

        [DataMember]
        [Column("VCDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VCDaily Reporting Id", Description = "VCDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCDailyReportingId { get; set; }

        [DataMember]
        [Column("LeaveTypeId", Order = 3)]
        [Display(Name = "Leave Type", Description = "Leave Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveTypeId { get; set; }

        [DataMember]
        [Column("LeaveModeId", Order = 4)]
        [Display(Name = "Leave Mode", Description = "Leave Mode", Order = 4)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveModeId { get; set; }

        [DataMember]
        [Column("LeaveApprovalStatus", Order = 5)]
        [Display(Name = "Leave Approval Status", Description = "Leave Approval Status", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveApprovalStatus { get; set; }

        [DataMember]
        [Column("LeaveApprover", Order = 6)]
        [Display(Name = "Leave Approver", Description = "Leave Approver", Order = 6)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveApprover { get; set; }

        [DataMember]
        [Column("LeaveReason", Order = 7)]
        [Display(Name = "Leave Reason", Description = "Leave Reason", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LeaveReason { get; set; }

        #endregion Public Properties
    }
}