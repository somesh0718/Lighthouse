using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SchoolModel : School
    {
        public SchoolModel()
        {
        }

        [DataMember]
        [Display(Name = "State Name", Description = "State Name")]
        public virtual string StateName { get; set; }
    }
}