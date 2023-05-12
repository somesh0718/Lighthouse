using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SchoolVTPSectors")]
    public partial class SchoolVTPSector : BaseEntity
    {
        public SchoolVTPSector()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SchoolVTPSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "School VTP Sector Id", Description = "School VTP Sector Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolVTPSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Academic Year", Description = "Academic Year", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Sector", Description = "Sector", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "VTP", Description = "VTP", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "School", Description = "School", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}