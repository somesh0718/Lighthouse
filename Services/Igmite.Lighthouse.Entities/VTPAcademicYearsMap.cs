using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTPAcademicYearsMap")]
    public partial class VTPAcademicYearsMap : BaseEntity
    {
        public VTPAcademicYearsMap()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTPAcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTPAcademicYearId", Description = "VTPAcademicYearId")]
        public virtual Guid VTPAcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "AcademicYearId", Description = "AcademicYearId")]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTPId", Description = "VTPId")]
        public virtual Guid VTPId { get; set; }

        [DataMember]
        [Column("DateOfJoining", TypeName = "DATETIME")]
        [Display(Name = "Date Of Joining", Description = "Date Of Joining")]
        public virtual DateTime? DateOfJoining { get; set; }

        [DataMember]
        [Column("DateOfResignation", TypeName = "DATETIME")]
        [Display(Name = "Date Of Resignation", Description = "Date Of Resignation")]
        public virtual DateTime? DateOfResignation { get; set; }

        #endregion Public Properties
    }
}