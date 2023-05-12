using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class MessageTemplateViewModel
    {
        [Key, DataMember]
        public virtual int MessageTemplateId { get; set; }

        [DataMember]
        public virtual string TemplateFlowId { get; set; }

        [DataMember]
        public virtual string TemplateName { get; set; }

        [DataMember]
        public virtual string MessageType { get; set; }

        [DataMember]
        public virtual string MessageSubType { get; set; }

        [DataMember]
        public virtual string ApplicableFor { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}