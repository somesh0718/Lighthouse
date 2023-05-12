using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class PhaseModel : Phase
    {
        public PhaseModel()
        {
            //this.SchoolModels = new List<SchoolModel>();
            //this.SectorJobRoleModels = new List<SectorJobRoleModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "School", Description = "School")]
        //public string SchoolName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SchoolModel> SchoolModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Sector Job Role", Description = "Sector Job Role")]
        //public string SectorJobRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SectorJobRoleModel> SectorJobRoleModels { get; set; }
    }
}
