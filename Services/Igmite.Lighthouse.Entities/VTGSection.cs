using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTGSections")]
    public partial class VTGSection : BaseEntityCreation
    {
        public VTGSection()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTGSectionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRActivity Type Id", Description = "VTRActivity Type Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGSectionId { get; set; }

        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTGuestLectureId", Description = "VTGuestLectureId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

        [DataMember]
        [Column("SectionId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Section", Description = "Section", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}