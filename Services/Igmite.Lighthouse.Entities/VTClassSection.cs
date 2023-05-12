using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTClassSections")]
    public partial class VTClassSection : BaseEntityCreation
    {
        public VTClassSection()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTClassSectionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTClass Section", Description = "VTClass Section", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassSectionId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VT Class", Description = "VT Class", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        // Foreign key
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