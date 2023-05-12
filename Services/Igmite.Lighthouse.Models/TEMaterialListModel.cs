using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class TEMaterialListModel : TEMaterialList
    {
        public TEMaterialListModel()
        {
        }

        [DataMember]
        public virtual RequestType RequestType { get; set; }
    }
}