using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class StudentClassDetailModel : StudentClassDetail
    {
        public StudentClassDetailModel()
        {
        }

        [DataMember]
        public Guid? AcademicYearId { get; set; }

        [DataMember]
        public Guid? SchoolId { get; set; }

        [DataMember]
        public Guid? VTPId { get; set; }

        [DataMember]
        public Guid? VCId { get; set; }

        [DataMember]
        public Guid? VTId { get; set; }

        [DataMember]
        public Guid? ClassId { get; set; }

        [DataMember]
        public Guid? SectionId { get; set; }
    }
}
