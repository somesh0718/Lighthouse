using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class TEToolListModel : TEToolList
    {
        public TEToolListModel()
        {
        }

        [DataMember]
        public virtual RequestType RequestType { get; set; }
    }
}