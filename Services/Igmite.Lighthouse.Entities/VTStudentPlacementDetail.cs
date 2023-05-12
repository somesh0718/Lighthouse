using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTStudentPlacementDetails")]
    public partial class VTStudentPlacementDetail : BaseEntity
    {
        public VTStudentPlacementDetail()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTStudentPlacementDetailId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTStudent Placement Detail Id", Description = "VTStudent Placement Detail Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTStudentPlacementDetailId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Student Id", Description = "Student Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        [Column("PlacementApplyStatus", Order = 4)]
        [Display(Name = "Placement Apply Status", Description = "Placement Apply Status", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PlacementApplyStatus { get; set; }

        [DataMember]
        [Column("PlacementStatus", Order = 5)]
        [Display(Name = "Placement Status", Description = "Placement Status", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PlacementStatus { get; set; }

        [DataMember]
        [Column("ApprenticeshipApplyStatus", Order = 6)]
        [Display(Name = "Apprenticeship Apply Status", Description = "Apprenticeship Apply Status", Order = 6)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprenticeshipApplyStatus { get; set; }

        [DataMember]
        [Column("ApprenticeshipStatus", Order = 7)]
        [Display(Name = "Apprenticeship Status", Description = "Apprenticeship Status", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprenticeshipStatus { get; set; }

        [DataMember]
        [Column("HigherEducationVE", Order = 8)]
        [Display(Name = "Higher Education VE", Description = "Higher Education VE", Order = 8)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string HigherEducationVE { get; set; }

        [DataMember]
        [Column("HigherEductaionOther", Order = 9)]
        [Display(Name = "Higher Eductaion Other", Description = "Higher Eductaion Other", Order = 9)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string HigherEductaionOther { get; set; }

        [DataMember]
        [Column("StudentPlacementStatus", Order = 10)]
        [Display(Name = "Student Placement Status", Description = "Student Placement Status", Order = 10)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StudentPlacementStatus { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Student Classes", Description = "Student Classes")]
        //public virtual StudentClass StudentClass { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTClasses", Description = "VTClasses")]
        //public virtual VTClass VTClass { get; set; }
        #endregion Public Properties
    }
}
