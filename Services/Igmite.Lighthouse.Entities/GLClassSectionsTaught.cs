using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("GLClassSectionsTaught")]
    public partial class GLClassSectionsTaught : BaseEntityCreation
    {
        public GLClassSectionsTaught()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("GLClassSectionsTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "GL Class Sections Taught", Description = "GL Class Sections Taught")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid GLClassSectionsTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Class", Description = "Class")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectionId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Section", Description = "Section")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionId { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}