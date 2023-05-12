using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SchoolCategoryModel : SchoolCategory
    {
        public SchoolCategoryModel()
        {
            //this.SchoolModels = new List<SchoolModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "School", Description = "School")]
        //public string SchoolName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SchoolModel> SchoolModels { get; set; }
    }
}
