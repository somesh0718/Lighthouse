using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRTrainingOfTeachers")]
    public partial class VTRTrainingOfTeacher : BaseEntityCreation
    {
        public VTRTrainingOfTeacher()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRTrainingOfTeacherId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRTraining Of Teacher Id", Description = "VTRTraining Of Teacher Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTrainingOfTeacherId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("TrainingTypeId", Order = 3)]
        [Display(Name = "Training Type Id", Description = "Training Type Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingTypeId { get; set; }

        [DataMember]
        [Column("TrainingBy", Order = 4)]
        [Display(Name = "Training By", Description = "Training By", Order = 4)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingBy { get; set; }

        [DataMember]
        [Column("TrainingDetails", Order = 5)]
        [Display(Name = "Training Details", Description = "Training Details", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingDetails { get; set; }

        #endregion Public Properties
    }
}