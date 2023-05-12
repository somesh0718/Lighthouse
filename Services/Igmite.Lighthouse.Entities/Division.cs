using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Divisions")]
    public partial class Division : BaseEntity
    {
        public Division()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Division Id", Description = "Division Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DivisionId { get; set; }

        [DataMember]
        [Column("StateCode", Order = 2)]
        [Display(Name = "State", Description = "State", Order = 2)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateCode { get; set; }

        [DataMember]
        [Column("DivisionName", Order = 3)]
        [Display(Name = "Division Name", Description = "Division Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DivisionName { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        #endregion Public Properties
    }
}