using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRTrainingOfTeacherModel
    {
        public VTRTrainingOfTeacherModel()
        {
            this.TrainingTopicIds = new List<string>();
        }

        [DataMember]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingTypeId { get; set; }

        [DataMember]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingBy { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TrainingDetails { get; set; }

        [DataMember]
        public IList<string> TrainingTopicIds { get; set; }
    }
}