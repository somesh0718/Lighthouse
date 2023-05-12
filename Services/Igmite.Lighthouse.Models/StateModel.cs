using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class StateModel : State
    {
        public StateModel()
        {
            //this.DistrictModels = new List<DistrictModel>();
            //this.SchoolModels = new List<SchoolModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "District", Description = "District")]
        //public string DistrictName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<DistrictModel> DistrictModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "School", Description = "School")]
        //public string SchoolName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SchoolModel> SchoolModels { get; set; }
    }
}
