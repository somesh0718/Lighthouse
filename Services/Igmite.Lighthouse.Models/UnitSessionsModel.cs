using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class UnitSessionsModel
    {
        public UnitSessionsModel()
        {
            this.UnitSessionId = Guid.NewGuid();
        }

        [DataMember, Key]
        public Guid UnitSessionId { get; set; }

        [DataMember]
        public string ModuleId { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public Guid UnitId { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember, NotMapped]
        public IList<Guid> SessionIds { get; set; }

        [DataMember]
        public string SessionIdsValue { get; set; }

        [DataMember]
        public string SessionNames { get; set; }
    }
}