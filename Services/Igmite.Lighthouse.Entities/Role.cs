using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Roles")]
    public partial class Role : BaseEntity
    {
        public Role()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("RoleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Role Id", Description = "Role Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid RoleId { get; set; }

        [DataMember]
        [Column("Code", Order = 2)]
        [Display(Name = "Code", Description = "Code", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Code { get; set; }

        [DataMember]
        [Column("Name", Order = 3)]
        [Display(Name = "Name", Description = "Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("LandingPageUrl", Order = 5)]
        [Display(Name = "Landing Page Url", Description = "Landing Page Url", Order = 5)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LandingPageUrl { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }
        
        #endregion Public Properties
    }
}
