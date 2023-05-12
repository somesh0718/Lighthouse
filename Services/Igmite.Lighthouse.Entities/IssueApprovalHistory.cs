using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("IssueApprovalHistories")]
    public partial class IssueApprovalHistory
    {
        public IssueApprovalHistory()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("IssueApprovalHistoryId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "IssueApprovalHistory Id", Description = "IssueApprovalHistory Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid IssueApprovalHistoryId { get; set; }

        [DataMember]
        [Column("IssueId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Issue Id", Description = "Issue Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid IssueId { get; set; }

        [DataMember]
        [Column("IssueType", Order = 3)]
        [Display(Name = "Issue Type", Description = "Issue Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueType { get; set; }

        [DataMember]
        [Column("ApprovedBy", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Approved By", Description = "Approved By", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ApprovedBy { get; set; }

        [DataMember]
        [Column("ApprovedDate", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Approved Date", Description = "Approved Date", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ApprovedDate { get; set; }

        [DataMember]
        [Column("ApprovalStatus", Order = 5)]
        [Display(Name = "Approval Status", Description = "Approval Status", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalStatus { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}