using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SchoolStudentDetailModel
    {
        public SchoolStudentDetailModel()
        {
        }

        [Key, DataMember]
        public int SrNo { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public Guid VTSchoolSectorId { get; set; }

        [DataMember]
        public Guid VTId { get; set; }

        [DataMember]
        public Guid SchoolId { get; set; }

        [DataMember]
        public Guid SectorId { get; set; }

        [DataMember]
        public Guid JobRoleId { get; set; }

        [DataMember]
        public Guid VTClassId { get; set; }

        [DataMember]
        public Guid ClassId { get; set; }

        [DataMember]
        public Guid SectionId { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }
    }
}