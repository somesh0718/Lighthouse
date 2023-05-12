using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("IssueAssignForReviews")]
    public partial class IssueAssignForReview : BaseEntityCreation
    {
        public IssueAssignForReview()
        {

        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("IssueAssignForReviewId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Issue Assign For Review Id", Description = "Issue Assign For Review Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid IssueAssignForReviewId { get; set; }

        [DataMember]
        [Column("IssueMappingId")]
        [Display(Name = "Issue Mapping Id", Description = "Issue Mapping Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueMappingId { get; set; }

        [DataMember]
        [Column("ReviewId")]
        [Display(Name = "Review Id", Description = "Review Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ReviewId { get; set; }

        #endregion Public Properties
    }
}
