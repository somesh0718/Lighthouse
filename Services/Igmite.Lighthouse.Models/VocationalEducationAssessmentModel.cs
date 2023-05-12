using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VocationalEducationAssessmentModel 
    {
        public VocationalEducationAssessmentModel()
        {
        }

        [DataMember]
        public virtual VEAHeaderModel VEAHeaderModels { get; set; }

        [DataMember]
        public IList<VEADetailsModel> VEADetailsModels { get; set; }
    }
}