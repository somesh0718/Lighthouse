using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SiteHeaderModel : SiteHeader
    {
        public SiteHeaderModel()
        {
            //this.SubHeaderModels = new List<SiteSubHeaderModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Site Sub Header", Description = "Site Sub Header")]
        //public string SubHeaderName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SiteSubHeaderModel> SubHeaderModels { get; set; }
    }
}
