using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class StudentByVTModel
    {
        [DataMember, Key]
        public Guid VTId { get; set; }

        [DataMember, Key]
        public Guid ClassId { get; set; }

        [DataMember, Key]
        public Guid SectionId { get; set; }

        [DataMember, Key]
        public Guid StudentId { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public bool IsPresent { get; set; }
    }
}