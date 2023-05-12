using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class MessageTemplateModel : MessageTemplate
    {
        public MessageTemplateModel()
        {
            this.MessageFieldIds = new List<string>();
            this.ApplicableForIds = new List<string>();
        }

        [DataMember]
        public List<string> MessageFieldIds { get; set; }

        [DataMember]
        public List<string> ApplicableForIds { get; set; }
    }
}