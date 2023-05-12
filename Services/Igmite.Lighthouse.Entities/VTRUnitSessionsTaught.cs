using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRUnitSessionsTaught")]
    public partial class VTRUnitSessionsTaught : BaseEntityCreation
    {
        public VTRUnitSessionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRUnitSessionsTaughtId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRUnit Sessions Taught Id", Description = "VTRUnit Sessions Taught Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRUnitSessionsTaughtId { get; set; }

        [DataMember]
        [Column("VTRTeachingVocationalEducationId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTRTeaching Vocational Education Id", Description = "VTRTeaching Vocational Education Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTeachingVocationalEducationId { get; set; }

        [DataMember]
        [Column("ModuleId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Module Taught Id", Description = "Module Taught Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ModuleId { get; set; }

        [DataMember]
        [Column("UnitId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Units Taught Id", Description = "Units Taught Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid UnitId { get; set; }

        [DataMember]
        [Column("SessionId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Session Id", Description = "Session Id", Order = 5)]
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