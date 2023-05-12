using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    public class BaseEntityCreation : RequestModel
    {
        [DataMember, JsonIgnore]
        [Column("CreatedBy", Order = 91)]
        [Display(Name = "Created By", Description = "Created By")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CreatedBy { get; set; }

        [DataMember, JsonIgnore]
        [Column("CreatedOn", TypeName = "DATETIME", Order = 92)]
        [Display(Name = "Created On", Description = "Created On")]
        public virtual DateTime? CreatedOn { get; set; }

        [DataMember]
        [Column("IsActive", TypeName = "BIT", Order = 93)]
        [Display(Name = "Is Active?", Description = "Is Active?")]
        public virtual bool IsActive { get; set; }
    }
}