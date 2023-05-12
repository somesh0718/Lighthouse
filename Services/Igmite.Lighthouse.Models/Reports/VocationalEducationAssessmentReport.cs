using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VocationalEducationAssessmentReport
    {
        [DataMember]
        public virtual VEAHeaderModel VEAHeaderModels { get; set; }

        [DataMember]
        public IList<VEADetailsModel> VEADetailsModels { get; set; }
    }
}