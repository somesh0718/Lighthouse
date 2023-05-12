using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("FVUnitsTaught")]
    public partial class FVUnitsTaught : BaseEntityCreation
    {
        public FVUnitsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("FVUnitsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "FV Units Taught", Description = "FV Units Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid FVUnitsTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTFieldIndustryVisitConductedId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Field Industry Visit Conducted", Description = "VT Field Industry Visit Conducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

        // Foreign key
        [DataMember]
        [Column("UnitId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Unit Taught", Description = "Unit Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid UnitId { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}