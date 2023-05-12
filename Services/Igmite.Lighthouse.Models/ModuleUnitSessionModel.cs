using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class ModuleUnitSessionModel
    {
        [Key, DataMember]
        public Guid UnitId { get; set; }

        [Key, DataMember]
        public Guid ClassId { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [Key, DataMember]
        public string ModuleTypeId { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public Guid? SectorId { get; set; }

        [DataMember]
        public string SectorName { get; set; }

        [DataMember]
        public Guid? JobRoleId { get; set; }

        [DataMember]
        public string JobRoleName { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [Key, DataMember]
        public Guid SessionId { get; set; }

        [DataMember]
        public string SessionName { get; set; }
    }
}