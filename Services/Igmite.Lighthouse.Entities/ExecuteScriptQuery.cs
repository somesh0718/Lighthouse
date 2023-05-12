using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    public class ExecuteScriptQuery
    {
        // Primary key
        [Key, DataMember]
        public int SrNo { get; set; }

        [DataMember]
        public string Results { get; set; }

        [DataMember]
        public string Messages { get; set; }
    }
}
