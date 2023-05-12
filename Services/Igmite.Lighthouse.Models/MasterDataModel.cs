using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class MasterDataModel
    {
        [Key, DataMember]
        public string DataValueId { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public string ParentId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }
    }
}