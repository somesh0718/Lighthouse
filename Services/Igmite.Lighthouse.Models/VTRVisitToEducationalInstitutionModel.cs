using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRVisitToEducationalInstitutionModel
    {
        public VTRVisitToEducationalInstitutionModel()
        {
        }

        [DataMember]
        public virtual int EducationalInstituteVisitCount { get; set; }

        [DataMember]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EducationalInstitute { get; set; }

        [DataMember]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InstituteContactPerson { get; set; }

        [DataMember]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InstituteContactNumber { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InstituteVisitDetails { get; set; }
    }
}