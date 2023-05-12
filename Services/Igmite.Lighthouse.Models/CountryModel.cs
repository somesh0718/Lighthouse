using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class CountryModel : Country
    {
        public CountryModel()
        {
            //this.StateModels = new List<StateModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "State", Description = "State")]
        //public string StateName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<StateModel> StateModels { get; set; }
    }
}
