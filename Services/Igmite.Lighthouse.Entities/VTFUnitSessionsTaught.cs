using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTFUnitSessionsTaught")]
    public partial class VTFUnitSessionsTaught : BaseEntityCreation
    {
        public VTFUnitSessionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTFUnitSessionsTaughtId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTFUnit Sessions Taught Id", Description = "VTFUnit Sessions Taught Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFUnitSessionsTaughtId { get; set; }

        [DataMember]
        [Column("VTFieldIndustryVisitConductedId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTFieldIndustryVisitConducted", Description = "VTFieldIndustryVisitConducted", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

        [DataMember]
        [Column("ModuleId", Order = 3)]
        [Display(Name = "Module Taught", Description = "Module Taught", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ModuleId { get; set; }

        [DataMember]
        [Column("UnitId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Units Taught", Description = "Units Taught", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid UnitId { get; set; }

        [DataMember]
        [Column("SessionId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Session Taught", Description = "Session Taught", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SessionId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}