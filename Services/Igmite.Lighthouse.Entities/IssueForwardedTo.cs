using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("IssueForwardedTo")]
    public partial class IssueForwardedTo : BaseEntityCreation
    {
        public IssueForwardedTo()
        {

        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("IssueForwardedToId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Issue Forwarded To Id", Description = "Issue Forwarded To Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid IssueForwardedToId { get; set; }

        [DataMember]
        [Column("IssueType")]
        [Display(Name = "Issue Type", Description = "Issue Type")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueType { get; set; }

        [DataMember]
        [Column("IssueReportingId")]
        [Display(Name = "Issue Reporting Id", Description = "Issue Reporting Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(36, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueReportingId { get; set; }

        [DataMember]
        [Column("ReviewId")]
        [Display(Name = "Review Id", Description = "Review Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ReviewId { get; set; }

        #endregion Public Properties
    }
}
