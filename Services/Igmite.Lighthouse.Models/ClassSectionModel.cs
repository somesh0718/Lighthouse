using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class ClassSectionModel
    {
        public ClassSectionModel()
        {
        }

        [Key, DataMember]
        public Guid VTId { get; set; }

        [Key, DataMember]
        public Guid ClassId { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [Key, DataMember]
        public Guid SectionId { get; set; }

        [DataMember]
        public string SectionName { get; set; }
    }
}