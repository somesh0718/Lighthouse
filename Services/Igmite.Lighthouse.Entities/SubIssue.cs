using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SubIssues")]
    public partial class SubIssue : BaseEntity
    {
        public SubIssue()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SubIssueId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "SubIssueId", Description = "SubIssueId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SubIssueId { get; set; }

        [DataMember]
        [Column("MainIssueId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "MainIssueId", Description = "MainIssueId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid MainIssueId { get; set; }

        [DataMember]
        [Column("IssueName", Order = 3)]
        [Display(Name = "IssueName", Description = "IssueName", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual Guid IssueName { get; set; }

        [DataMember]
        [Column("IssueCategoryId", Order = 4)]
        [Display(Name = "IssueCategoryId", Description = "IssueCategoryId", Order = 4)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual Guid IssueCategoryId { get; set; }

        [DataMember]
        [Column("IssuePriority", Order = 5)]
        [Display(Name = "IssuePriority", Description = "IssuePriority", Order = 5)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual Guid IssuePriority { get; set; }

        [DataMember]
        [Column("Description", Order = 6)]
        [Display(Name = "Description", Description = "Description", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual Guid Description { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 7)]
        [Display(Name = "DisplayOrder", Description = "DisplayOrder", Order = 7)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("IsApplicableForVT", TypeName = "BIT", Order = 8)]
        [Display(Name = "IsApplicableForVT", Description = "IsApplicableForVT", Order = 8)]
        public virtual bool IsApplicableForVT { get; set; }

        [DataMember]
        [Column("IsApplicableForVC", TypeName = "BIT", Order = 9)]
        [Display(Name = "IsApplicableForVC", Description = "IsApplicableForVC", Order = 9)]
        public virtual bool IsApplicableForVC { get; set; }

        [DataMember]
        [Column("IsApplicableForHM", TypeName = "BIT", Order = 10)]
        [Display(Name = "IsApplicableForHM", Description = "IsApplicableForHM", Order = 10)]
        public virtual bool IsApplicableForHM { get; set; }

        [DataMember]
        [Column("AssignForReviewPMU", TypeName = "BIT", Order = 11)]
        [Display(Name = "AssignForReviewPMU", Description = "AssignForReviewPMU", Order = 11)]
        public virtual bool AssignForReviewPMU { get; set; }

        [DataMember]
        [Column("AssignForReviewVC", TypeName = "BIT", Order = 12)]
        [Display(Name = "AssignForReviewVC", Description = "AssignForReviewVC", Order = 12)]
        public virtual bool AssignForReviewVC { get; set; }

        [DataMember]
        [Column("AssignForReviewHM", TypeName = "BIT", Order = 13)]
        [Display(Name = "AssignForReviewHM", Description = "AssignForReviewHM", Order = 13)]
        public virtual bool AssignForReviewHM { get; set; }

        #endregion Public Properties
    }
}