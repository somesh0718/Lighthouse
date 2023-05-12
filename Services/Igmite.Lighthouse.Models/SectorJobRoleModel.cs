using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SectorJobRoleModel : SectorJobRole
    {
        public SectorJobRoleModel()
        {
            //this.VTPSectorModels = new List<VTPSectorModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "VTPSector", Description = "VTPSector")]
        //public string VTPSectorName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<VTPSectorModel> VTPSectorModels { get; set; }
    }
}
