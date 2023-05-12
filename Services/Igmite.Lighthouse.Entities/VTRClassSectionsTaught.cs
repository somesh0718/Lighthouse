using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRClassSectionsTaught")]
    public partial class VTRClassSectionsTaught : BaseEntityCreation
    {
        public VTRClassSectionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRClassSectionsTaughtId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRClass Sections Taught Id", Description = "VTRClass Sections Taught Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRClassSectionsTaughtId { get; set; }

        [DataMember]
        [Column("VTRTeachingVocationalEducationId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTRTeaching Vocational Education Id", Description = "VTRTeaching Vocational Education Id", Order = 2)]
        public virtual Guid? VTRTeachingVocationalEducationId { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Class Id", Description = "Class Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("SectionId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Section Id", Description = "Section Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 5)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}