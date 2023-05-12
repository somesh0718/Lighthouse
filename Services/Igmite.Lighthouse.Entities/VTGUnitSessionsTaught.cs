using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTGUnitSessionsTaught")]
    public partial class VTGUnitSessionsTaught : BaseEntityCreation
    {
        public VTGUnitSessionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTGUnitSessionsTaughtId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTGUnit Sessions Taught Id", Description = "VTGUnit Sessions Taught Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGUnitSessionsTaughtId { get; set; }

        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTGuestLecture", Description = "VTGuestLecture", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

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