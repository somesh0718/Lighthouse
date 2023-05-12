using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    public class BaseEntity : RequestModel
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

        [DataMember, JsonIgnore]
        [Column("UpdatedBy", Order = 93)]
        [Display(Name = "Updated By", Description = "Updated By")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UpdatedBy { get; set; }

        [DataMember, JsonIgnore]
        [Column("UpdatedOn", TypeName = "DATETIME", Order = 94)]
        [Display(Name = "Updated On", Description = "Updated On")]
        public virtual DateTime? UpdatedOn { get; set; }

        [DataMember]
        [Column("IsActive", TypeName = "BIT", Order = 95)]
        [Display(Name = "Is Active?", Description = "Is Active?")]
        public virtual bool IsActive { get; set; }
    }
}