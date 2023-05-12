using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SchoolVEInchargeModel : SchoolVEIncharge
    {
        public SchoolVEInchargeModel()
        {
        }
        [DataMember]
        public virtual Guid? VTPId { get; set; }

        [DataMember]
        public virtual Guid? VCId { get; set; }
    }
}
