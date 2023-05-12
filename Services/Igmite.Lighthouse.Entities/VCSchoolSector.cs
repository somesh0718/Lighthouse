using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCSchoolSectors")]
    public partial class VCSchoolSector : BaseEntity
    {
        public VCSchoolSector()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCSchoolSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VCSchool Sector Id", Description = "VCSchool Sector Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCSchoolSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "VCId", Description = "VCId", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolVTPSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "School VTP Sector", Description = "School VTP Sector", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolVTPSectorId { get; set; }

        [DataMember]
        [Column("DateOfAllocation", TypeName = "DATETIME", Order = 5)]
        [Display(Name = "Date Of Allocation", Description = "Date Of Allocation", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateOfAllocation { get; set; }

        [DataMember]
        [Column("DateOfRemoval", TypeName = "DATETIME", Order = 6)]
        [Display(Name = "Date Of Removal", Description = "Date Of Removal", Order = 6)]
        public virtual DateTime? DateOfRemoval { get; set; }

        #endregion Public Properties
    }
}