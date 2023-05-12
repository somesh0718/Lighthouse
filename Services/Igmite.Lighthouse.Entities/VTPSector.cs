using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTPSectors")]
    public partial class VTPSector : BaseEntity
    {
        public VTPSector()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTPSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTP Sector Id", Description = "VTP Sector Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Academic Year", Description = "Academic Year", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "VTP", Description = "VTP", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Sector", Description = "Sector", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 5)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}