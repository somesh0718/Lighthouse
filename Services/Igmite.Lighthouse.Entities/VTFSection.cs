using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTFSections")]
    public partial class VTFSection : BaseEntityCreation
    {
        public VTFSection()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTFSectionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTFSectionId", Description = "VTFSectionId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFSectionId { get; set; }

        [DataMember]
        [Column("VTFieldIndustryVisitConductedId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTFieldIndustryVisitConductedId", Description = "VTFieldIndustryVisitConductedId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

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