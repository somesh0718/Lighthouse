using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("AcademicYears")]
    public partial class AcademicYear : BaseEntity
    {
        public AcademicYear()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        [Column("PhaseId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Phase", Description = "Phase", Order = 2)]
        //[Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid? PhaseId { get; set; }

        [DataMember]
        [Column("YearName", Order = 3)]
        [Display(Name = "Year Name", Description = "Year Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string YearName { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("IsCurrentAcademicYear", TypeName = "BIT", Order = 5)]
        [Display(Name = "Is Current Academic Year?", Description = "Is Current Academic Year?", Order = 5)]
        public virtual bool IsCurrentAcademicYear { get; set; }

        #endregion Public Properties
    }
}