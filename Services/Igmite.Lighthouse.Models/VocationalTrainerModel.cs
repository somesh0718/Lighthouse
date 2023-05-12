using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VocationalTrainerModel : VocationalTrainer
    {
        public VocationalTrainerModel()
        {
        }

        [DataMember]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }

        [DataMember]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPId { get; set; }

        [DataMember]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        public virtual DateTime? DateOfResignation { get; set; }

        [DataMember]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string NatureOfAppointment { get; set; }
    }
}