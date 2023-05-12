using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTSchoolSectorJobRoles")]
    public partial class VTSchoolSectorJobRole : BaseEntityCreation
    {
        public VTSchoolSectorJobRole()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTSchoolSectorJobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTSchoolSectorJobRoleId", Description = "VTSchoolSectorJobRoleId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTSchoolSectorJobRoleId { get; set; }

        [DataMember]
        [Column("VTSchoolSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTSchoolSectorId", Description = "VTSchoolSectorId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTSchoolSectorId { get; set; }

        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "JobRoleId", Description = "JobRoleId", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid JobRoleId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}