using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SearchVocationalTrainerModel : BaseSearchModel
    {
        [DataMember]
        public string SocialCategoryId { get; set; }
    }
}