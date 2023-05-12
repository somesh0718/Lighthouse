using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("GLMethodologies")]
    public partial class GLMethodology : BaseEntityCreation
    {
        public GLMethodology()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("GLMethodologyId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "GL Methodology", Description = "GL Methodology")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid GLMethodologyId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Guest Lecture Conducted", Description = "VT Guest Lecture Conducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

        // Foreign key
        [DataMember]
        [Column("MethodologyTypeId")]
        [Display(Name = "GL Methodology", Description = "GL Methodology")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string MethodologyTypeId { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}