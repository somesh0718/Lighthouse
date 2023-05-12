using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class MessageTemplateRequest : MessageTemplateModel
    {
    }

    public class MessageTemplateResponse : MessageTemplateModel
    {
    }

    [DataContract, Serializable]
    public class SearchMessageTemplateRequest : BaseSearchModel
    {
        [DataMember]
        public string MessageTypeId { get; set; }
    }
}