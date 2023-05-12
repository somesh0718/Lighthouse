using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("ComplaintRegistrations")]
    public partial class ComplaintRegistration : BaseEntityUpdation
    {
        public ComplaintRegistration()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("ComplaintRegistrationId", Order = 1)]
        [Display(Name = "Complaint Registration Id", Description = "Complaint Registration Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ComplaintRegistrationId { get; set; }

        [DataMember]
        [Column("UserType", Order = 2)]
        [Display(Name = "User Type", Description = "User Type", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(45, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UserType { get; set; }

        [DataMember]
        [Column("UserName", Order = 3)]
        [Display(Name = "User Name", Description = "User Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UserName { get; set; }

        [DataMember]
        [Column("EmailId", Order = 4)]
        [Display(Name = "Email Id", Description = "Email Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId { get; set; }

        [DataMember]
        [Column("Subject", Order = 5)]
        [Display(Name = "Subject", Description = "Subject", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Subject { get; set; }

        [DataMember]
        [Column("IssueDetails", Order = 6)]
        [Display(Name = "Issue Details", Description = "Issue Details", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string IssueDetails { get; set; }

        [DataMember]
        [Column("IssueStatus", Order = 7)]
        [Display(Name = "Issue Status", Description = "Issue Status", Order = 7)]
        [MaxLength(45, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueStatus { get; set; }

        [DataMember]
        [Column("Attachment", Order = 8)]
        [Display(Name = "Attachment", Description = "Attachment", Order = 8)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Attachment { get; set; }

        [DataMember, JsonIgnore]
        [Column("CreatedOn", TypeName = "DATETIME", Order = 9)]
        [Display(Name = "Created On", Description = "Created On", Order = 9)]
        public virtual DateTime CreatedOn { get; set; }

        [DataMember, JsonIgnore]
        [Column("ResolvedBy", Order = 10)]
        [Display(Name = "Resolved By", Description = "Resolved By", Order = 10)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ResolvedBy { get; set; }

        [DataMember, JsonIgnore]
        [Column("ResolvedOn", TypeName = "DATETIME", Order = 11)]
        [Display(Name = "Resolved On", Description = "Resolved On", Order = 11)]
        public virtual DateTime? ResolvedOn { get; set; }

        #endregion Public Properties
    }
}