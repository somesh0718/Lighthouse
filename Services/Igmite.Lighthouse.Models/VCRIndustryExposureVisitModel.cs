using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VCRIndustryExposureVisitModel
    {
        [DataMember]
        public virtual int EducationalInstituteVisitCount { get; set; }

        [DataMember]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TypeOfIndustryLinkage { get; set; }

        [DataMember]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ContactPersonName { get; set; }

        [DataMember]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ContactPersonMobile { get; set; }

        [DataMember]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ContactPersonEmail { get; set; }
    }
}