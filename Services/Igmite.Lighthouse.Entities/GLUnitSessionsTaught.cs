using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("GLUnitSessionsTaught")]
    public partial class GLUnitSessionsTaught : BaseEntityCreation
    {
        public GLUnitSessionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("GLUnitSessionsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "GL Unit Sessions Taught", Description = "GL Unit Sessions Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid GLUnitSessionsTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("GLUnitsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "GL Units Taught", Description = "GL Units Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid GLUnitsTaughtId { get; set; }

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