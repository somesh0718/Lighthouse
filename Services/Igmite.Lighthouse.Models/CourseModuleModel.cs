using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class CourseModuleModel : CourseModule
    {
        public CourseModuleModel()
        {
            this.Sessions = new List<UnitSessionModel>();
        }

        [DataMember]
        public IList<UnitSessionModel> Sessions { get; set; }

        [JsonIgnore]
        public int RowIndex { get; set; }
    }

    public class UnitSessionModel
    {
        [DataMember]
        public string SessionName { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }
    }
}