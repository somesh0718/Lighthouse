using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTStatusOfInductionInserviceTrainingModel : VTStatusOfInductionInserviceTraining
    {
        public VTStatusOfInductionInserviceTrainingModel()
        {
        }

        [DataMember]
        public Guid VTId { get; set; }
    }
}