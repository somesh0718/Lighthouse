using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("MainIssues")]
    public partial class MainIssue : BaseEntity
    {
        public MainIssue()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("MainIssueId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "MainIssueId", Description = "MainIssueId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid MainIssueId { get; set; }

        [DataMember]
        [Column("Code", Order = 2)]
        [Display(Name = "Code", Description = "Code", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Code { get; set; }

        [DataMember]
        [Column("Name", Order = 3)]
        [Display(Name = "Name", Description = "Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 5)]
        [Display(Name = "DisplayOrder", Description = "DisplayOrder", Order = 5)]
        public virtual int DisplayOrder { get; set; }

        #endregion Public Properties
    }
}