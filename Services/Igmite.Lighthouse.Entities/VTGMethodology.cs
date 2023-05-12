using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTGMethodologies")]
    public partial class VTGMethodology : BaseEntityCreation
    {
        public VTGMethodology()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTGMethodologyId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTG Methodology", Description = "VTG Methodology", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGMethodologyId { get; set; }

        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTGuestLectureId", Description = "VTGuestLectureId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

        [DataMember]
        [Column("MethodologyId", Order = 3)]
        [Display(Name = "Methodology", Description = "Methodology", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MethodologyId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}