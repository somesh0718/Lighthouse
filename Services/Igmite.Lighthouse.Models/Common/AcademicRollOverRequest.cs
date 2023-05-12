using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models.Common
{
    [DataContract, Serializable]
    public class AcademicRollOverRequest
    {
        [Key, DataMember]
        public string UserId { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public string FromEntityId { get; set; }

        [DataMember]
        public Guid? ToEntityId { get; set; }

        [DataMember]
        public string VTPSectorIds { get; set; }

        [DataMember]
        public string SchoolVTPSectorIds { get; set; }

        [DataMember]
        public string VCSchoolSectorIds { get; set; }

        [DataMember]
        public string VTSchoolSectorIds { get; set; }

        [DataMember]
        public string VTIds { get; set; }

        [DataMember]
        public string VTClassIds { get; set; }

        [DataMember]
        public string StudentIds { get; set; }
    }
}