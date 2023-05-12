using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRVisitToIndustryModel
    {
        public VTRVisitToIndustryModel()
        {
        }

        [DataMember]
        public virtual int IndustryVisitCount { get; set; }

        [DataMember]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryName { get; set; }

        [DataMember]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryContactPerson { get; set; }

        [DataMember]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryContactNumber { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryVisitDetails { get; set; }
    }
}