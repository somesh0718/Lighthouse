using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTSchoolSectorModel : VTSchoolSector
    {
        public VTSchoolSectorModel()
        {
        }

        [DataMember]
        public Guid VCId { get; set; }

        [DataMember]
        public IList<Guid> JobRoleIds { get; set; }
    }
}