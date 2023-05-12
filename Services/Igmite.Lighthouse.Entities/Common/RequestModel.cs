using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    public class RequestModel
    {
        public RequestModel()
        {
            this.ErrorMessages = new List<string>();
            this.RequestType = RequestType.New;
        }

        [NotMapped, DataMember]
        public virtual string AuthUserId { get; set; }

        [NotMapped, DataMember]
        public virtual RequestType RequestType { get; set; }

        [NotMapped, JsonIgnore]
        public virtual bool IsReadonly { get; set; }

        [NotMapped, JsonIgnore]
        public int TotalRows { get; set; }

        [NotMapped, JsonIgnore]
        public virtual IList<string> ErrorMessages { get; set; }
    }
}