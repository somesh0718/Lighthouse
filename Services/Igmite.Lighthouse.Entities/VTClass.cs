using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTClasses")]
    public partial class VTClass : BaseEntity
    {
        public VTClass()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Vocational Trainer", Description = "Vocational Trainer", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "School", Description = "School", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Class", Description = "Class", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("SectionId", TypeName = "UNIQUEIDENTIFIER", Order = 6)]
        [Display(Name = "Section", Description = "Section", Order = 6)]
        public virtual Guid? SectionId { get; set; }

        #endregion Public Properties
    }
}