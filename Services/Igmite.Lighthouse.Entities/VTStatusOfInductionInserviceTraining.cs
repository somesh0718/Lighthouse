using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTStatusOfInductionInserviceTraining")]
    public partial class VTStatusOfInductionInserviceTraining : BaseEntity
    {
        public VTStatusOfInductionInserviceTraining()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTStatusOfInductionInserviceTrainingId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTStatus Of Induction Inservice Training Id", Description = "VTStatus Of Induction Inservice Training Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTStatusOfInductionInserviceTrainingId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTSchoolSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTSchool Sector Id", Description = "VTSchool Sector Id", Order = 2)]
        public virtual Guid? VTSchoolSectorId { get; set; }

        [DataMember]
        [Column("IndustryTrainingStatus", Order = 3)]
        [Display(Name = "Industry Training Status", Description = "Industry Training Status", Order = 3)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryTrainingStatus { get; set; }

        [DataMember]
        [Column("InserviceTrainingStatus", Order = 4)]
        [Display(Name = "Inservice Training Status", Description = "Inservice Training Status", Order = 4)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InserviceTrainingStatus { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTSchool Sectors", Description = "VTSchool Sectors")]
        //public virtual VTSchoolSector VTSchoolSector { get; set; }
        #endregion Public Properties
    }
}
