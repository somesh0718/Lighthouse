using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTPSectorJobRoleModel : VTPSectorJobRole
    {
        public VTPSectorJobRoleModel()
        {
            //this.SchoolVTPSectorModels = new List<SchoolVTPSectorModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "School VTPSector", Description = "School VTPSector")]
        //public string SchoolVTPSectorName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SchoolVTPSectorModel> SchoolVTPSectorModels { get; set; }
    }
}
