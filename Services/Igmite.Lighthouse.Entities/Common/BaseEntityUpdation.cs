using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    public class BaseEntityUpdation : RequestModel
    {
        [DataMember, JsonIgnore]
        [Column("UpdatedBy", Order = 91)]
        [Display(Name = "Updated By", Description = "Updated By")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UpdatedBy { get; set; }

        [DataMember, JsonIgnore]
        [Column("UpdatedOn", TypeName = "DATETIME", Order = 92)]
        [Display(Name = "Updated On", Description = "Updated On")]
        public virtual DateTime? UpdatedOn { get; set; }

        [DataMember]
        [Column("IsActive", TypeName = "BIT", Order = 93)]
        [Display(Name = "Is Active?", Description = "Is Active?")]
        public virtual bool IsActive { get; set; }
    }
}