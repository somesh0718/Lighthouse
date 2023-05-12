using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SectorModel : Sector
    {
        public SectorModel()
        {
            //this.AcademicYearSchoolVTPJobRoleModels = new List<AcademicYearSchoolVTPSectorJobRoleModel>();
            //this.JobRoleModels = new List<SectorJobRoleModel>();
            //this.VTPJobRoleModels = new List<VTPSectorJobRoleModel>();
            //this.VTSchoolSectorModels = new List<VTSchoolSectorModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Academic Year School VTPJob Role", Description = "Academic Year School VTPJob Role")]
        //public string AcademicYearSchoolVTPJobRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<AcademicYearSchoolVTPSectorJobRoleModel> AcademicYearSchoolVTPJobRoleModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Job Role", Description = "Job Role")]
        //public string JobRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SectorJobRoleModel> JobRoleModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "VTPJob Role", Description = "VTPJob Role")]
        //public string VTPJobRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<VTPSectorJobRoleModel> VTPJobRoleModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "VTSchool", Description = "VTSchool")]
        //public string VTSchoolSectorName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<VTSchoolSectorModel> VTSchoolSectorModels { get; set; }
    }
}
