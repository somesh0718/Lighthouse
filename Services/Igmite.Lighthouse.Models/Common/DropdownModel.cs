using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class DropdownModel<T>
    {
        [Key, DataMember]
        public T Id { get; set; }

        [Key, DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public bool IsDisabled { get; set; }

        [DataMember]
        public int SequenceNo { get; set; }
    }
}