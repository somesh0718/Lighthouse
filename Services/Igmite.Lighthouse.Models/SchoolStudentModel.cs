using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SchoolStudentModel
    {
        public SchoolStudentModel()
        {
        }

        [Key, DataMember]
        public int SrNo { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public Guid SchoolId { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string SectorId { get; set; }

        [DataMember]
        public string SectorName { get; set; }

        [DataMember]
        public string JobRoleName { get; set; }

        [DataMember]
        public string VTName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public int StudentCount { get; set; }
    }
}