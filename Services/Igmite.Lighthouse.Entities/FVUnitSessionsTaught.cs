using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("FVUnitSessionsTaught")]
    public partial class FVUnitSessionsTaught : BaseEntityCreation
    {
        public FVUnitSessionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("FVUnitSessionsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "FV Unit Sessions Taught", Description = "FV Unit Sessions Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid FVUnitSessionsTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("FVUnitsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "FV Units Taught", Description = "FV Units Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid FVUnitsTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SessionId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Unit Session Taught", Description = "Unit Session Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SessionId { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}