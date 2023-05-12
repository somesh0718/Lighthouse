using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRTeachingNonVocationalSubjectModel
    {
        public VTRTeachingNonVocationalSubjectModel()
        {
        }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OtherClassTakenDetails { get; set; }

        [DataMember]
        public virtual int OtherClassTime { get; set; }
    }
}