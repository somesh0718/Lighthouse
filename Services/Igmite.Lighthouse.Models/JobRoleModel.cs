using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class JobRoleModel : JobRole
    {
        public JobRoleModel()
        {
            //this.AcademicYearSchoolVTPSectorRoleModels = new List<AcademicYearSchoolVTPSectorJobRoleModel>();
            //this.SectorRoleModels = new List<SectorJobRoleModel>();
            //this.VTPSectorRoleModels = new List<VTPSectorJobRoleModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Academic Year School VTPSector", Description = "Academic Year School VTPSector")]
        //public string AcademicYearSchoolVTPSectorRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<AcademicYearSchoolVTPSectorJobRoleModel> AcademicYearSchoolVTPSectorRoleModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Sector", Description = "Sector")]
        //public string SectorRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SectorJobRoleModel> SectorRoleModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "VTPSector", Description = "VTPSector")]
        //public string VTPSectorRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<VTPSectorJobRoleModel> VTPSectorRoleModels { get; set; }
    }
}
