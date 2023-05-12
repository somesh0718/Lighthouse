using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("AccountWorkLocations")]
    public partial class AccountWorkLocation : BaseEntity
    {
        public AccountWorkLocation()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AccountWorkLocationId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Account Work Location Id", Description = "Account Work Location Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountWorkLocationId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Account", Description = "Account", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        [Column("StateCode", Order = 3)]
        [Display(Name = "State", Description = "State", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string StateCode { get; set; }

        [DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Division", Description = "Division", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DivisionId { get; set; }

        [DataMember]
        [Column("DistrictId", Order = 5)]
        [Display(Name = "District", Description = "District", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string DistrictId { get; set; }

        [DataMember]
        [Column("BlockId", Order = 6)]
        [Display(Name = "Block", Description = "Block", Order = 6)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string BlockId { get; set; }

        [DataMember]
        [Column("ClusterId", Order = 7)]
        [Display(Name = "Cluster", Description = "Cluster", Order = 7)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClusterId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 8)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 8)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        // Navigation properties
        [DataMember, JsonIgnore]
        [Display(Name = "Accounts", Description = "Accounts")]
        public virtual Account Account { get; set; }

        #endregion Public Properties
    }
}