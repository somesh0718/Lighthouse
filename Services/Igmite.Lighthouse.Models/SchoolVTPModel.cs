using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SchoolVTPModel
    {
        public SchoolVTPModel()
        {
        }

        [Key, DataMember]
        public int SrNo { get; set; }  

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public string Remarks { get; set; }

    }
}