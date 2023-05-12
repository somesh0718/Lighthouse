using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("GLUnitsTaught")]
    public partial class GLUnitsTaught : BaseEntityCreation
    {
        public GLUnitsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("GLUnitsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "GL Units Taught", Description = "GL Units Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid GLUnitsTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Guest Lecture Conducted", Description = "VT Guest Lecture Conducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

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