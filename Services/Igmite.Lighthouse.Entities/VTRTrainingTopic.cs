using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRTrainingTopics")]
    public partial class VTRTrainingTopic : BaseEntityCreation
    {
        public VTRTrainingTopic()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRTrainingTopicId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRTraining Topic Id", Description = "VTRTraining Topic Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTrainingTopicId { get; set; }

        [DataMember]
        [Column("VTRTrainingOfTeacherId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTRTraining Of Teacher Id", Description = "VTRTraining Of Teacher Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTrainingOfTeacherId { get; set; }

        [DataMember]
        [Column("TrainingTopicId", Order = 3)]
        [Display(Name = "Training Topic Id", Description = "Training Topic Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingTopicId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}