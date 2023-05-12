using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VCSchoolSectorModel : VCSchoolSector
    {
        public VCSchoolSectorModel()
        {
            //this.HeadMasterModels = new List<HeadMasterModel>();
            //this.VisitGeoLocationModels = new List<VCSchoolVisitGeoLocationModel>();
            //this.VisitModels = new List<VCSchoolVisitModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Head Master", Description = "Head Master")]
        //public string HeadMasterName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<HeadMasterModel> HeadMasterModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "VCSchool Visit Geo Location", Description = "VCSchool Visit Geo Location")]
        //public string VisitGeoLocationName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<VCSchoolVisitGeoLocationModel> VisitGeoLocationModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "VCSchool Visit", Description = "VCSchool Visit")]
        //public string VisitName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<VCSchoolVisitModel> VisitModels { get; set; }
    }
}
