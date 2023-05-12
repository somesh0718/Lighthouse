using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRObservationDayModel
    {
        public VTRObservationDayModel()
        {
        }

        [DataMember]
        public virtual Guid VTId { get; set; }

        [DataMember]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        public virtual bool IsPresent { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }
    }
}