using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRCommunityHomeVisitModel
    {
        public VTRCommunityHomeVisitModel()
        {
        }

        [DataMember]
        public virtual int VocationalParentsCount { get; set; }

        [DataMember]
        public virtual int OtherParentsCount { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CommunityVisitDetails { get; set; }
    }
}