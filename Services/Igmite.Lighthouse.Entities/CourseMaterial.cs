using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("CourseMaterials")]
    public partial class CourseMaterial : BaseEntity
    {
        public CourseMaterial()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("CourseMaterialId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Course Material Id", Description = "Course Material Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid CourseMaterialId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VT Id", Description = "VT Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Class Id", Description = "Class Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("ReceiptDate", TypeName = "DATE", Order = 5)]
        [Display(Name = "Receipt Date", Description = "Receipt Date", Order = 5)]
        public virtual DateTime? ReceiptDate { get; set; }

        [DataMember]
        [Column("Details", Order = 6)]
        [Display(Name = "Details", Description = "Details", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Details { get; set; }

        [DataMember]
        [Column("CMStatus", Order = 7)]
        [Display(Name = "CMStatus", Description = "CMStatus", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CMStatus { get; set; }

        #endregion Public Properties
    }
}