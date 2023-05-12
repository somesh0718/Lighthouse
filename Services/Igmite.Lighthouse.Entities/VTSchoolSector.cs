using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTSchoolSectors")]
    public partial class VTSchoolSector : BaseEntity
    {
        public VTSchoolSector()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTSchoolSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTSchool Sector Id", Description = "VTSchool Sector Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTSchoolSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Academic Year", Description = "Academic Year", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Vocational Trainer", Description = "Vocational Trainer", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "School Id", Description = "School Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 6)]
        [Display(Name = "JobRole", Description = "JobRole", Order = 6)]
        public virtual Guid? JobRoleId { get; set; }

        [DataMember]
        [Column("DateOfAllocation", TypeName = "DATETIME", Order = 7)]
        [Display(Name = "Date Of Allocation", Description = "Date Of Allocation", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateOfAllocation { get; set; }

        [DataMember]
        [Column("DateOfRemoval", TypeName = "DATETIME", Order = 8)]
        [Display(Name = "Date Of Removal", Description = "Date Of Removal", Order = 8)]
        public virtual DateTime? DateOfRemoval { get; set; }

        #endregion Public Properties
    }
}