using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("IssueMapping")]
    public partial class IssueMapping : BaseEntity
    {
        public IssueMapping()
        {

        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("IssueMappingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Issue Mapping Id", Description = "Issue Mapping Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid IssueMappingId { get; set; }

        [DataMember]
        [Column("MainIssueId")]
        [Display(Name = "Main Issue Id", Description = "Main Issue Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MainIssueId { get; set; }

        [DataMember]
        [Column("SubIssueId")]
        [Display(Name = "Sub Issue Id", Description = "Sub Issue Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SubIssueId { get; set; }

        [DataMember]
        [Column("IssueCategoryId")]
        [Display(Name = "Issue Category Id", Description = "Issue Category Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueCategoryId { get; set; }

        [DataMember]
        [Column("IssuePriority")]
        [Display(Name = "Issue Priority", Description = "Issue Priority")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(30, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssuePriority { get; set; }

        [DataMember]
        [Column("IsApplicableForVC", TypeName = "BIT")]
        [Display(Name = "Is Applicable For VC", Description = "Is Applicable For VC")]
        public virtual bool IsApplicableForVC { get; set; }

        [DataMember]
        [Column("IsApplicableForVT", TypeName = "BIT")]
        [Display(Name = "Is Applicable For VT", Description = "Is Applicable For VT")]
        public virtual bool IsApplicableForVT { get; set; }

        [DataMember]
        [Column("IsApplicableForHM", TypeName = "BIT")]
        [Display(Name = "Is Applicable For HM", Description = "Is Applicable For HM")]
        public virtual bool IsApplicableForHM { get; set; }

        #endregion Public Properties
    }
}
