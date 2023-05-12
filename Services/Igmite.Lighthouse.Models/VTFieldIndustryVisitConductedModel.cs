using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTFieldIndustryVisitConductedModel : VTFieldIndustryVisitConducted
    {
        public VTFieldIndustryVisitConductedModel()
        {
        }

        [DataMember]
        public Guid SectionIds { get; set; }

        [DataMember]
        public IList<UnitSessionsModel> UnitSessionsModels { get; set; }

        [DataMember]
        public IList<StudentAttendanceModel> StudentAttendances { get; set; }

        [DataMember]
        public virtual FileUploadModel FVPictureFile { get; set; }
    }
}