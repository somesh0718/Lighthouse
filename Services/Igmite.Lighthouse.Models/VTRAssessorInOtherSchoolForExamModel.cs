using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRAssessorInOtherSchoolForExamModel
    {
        public VTRAssessorInOtherSchoolForExamModel()
        {
        }

        [DataMember]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolName { get; set; }

        [DataMember]
        [MaxLength(11, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Udise { get; set; }

        [DataMember]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        public virtual int BoysPresent { get; set; }

        [DataMember]
        public virtual int GirlsPresent { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ExamDetails { get; set; }
    }
}