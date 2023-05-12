using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRAssignmentFromVocationalDepartmentModel
    {
        public VTRAssignmentFromVocationalDepartmentModel()
        {
        }

        [DataMember]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssigmentNumber { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssignmentDetails { get; set; }

        [DataMember]
        public virtual FileUploadModel AssignmentPhotoFile { get; set; }
    }
}