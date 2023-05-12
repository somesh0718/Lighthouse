using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("JobRoles")]
    public partial class JobRole : BaseEntity
    {
        public JobRole()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid JobRoleId { get; set; }

        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Sector", Description = "Sector", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        [Column("JobRoleName", Order = 3)]
        [Display(Name = "Job Role", Description = "Job Role", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        [Column("QPCode", Order = 4)]
        [Display(Name = "QP Code", Description = "QP Code", Order = 4)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [RegularExpression(EntityConstants.RegxQPCodePattern, ErrorMessage = EntityConstants.QPCodeErrorMessage)]
        public virtual string QPCode { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 5)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}