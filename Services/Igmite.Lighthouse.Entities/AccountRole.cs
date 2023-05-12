using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("AccountRoles")]
    public partial class AccountRole : BaseEntity
    {
        public AccountRole()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AccountRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Account Role Id", Description = "Account Role Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountRoleId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Account Id", Description = "Account Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        // Foreign key
        [DataMember]
        [Column("RoleId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Role Id", Description = "Role Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid RoleId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Accounts", Description = "Accounts")]
        //public virtual Account Account { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Roles", Description = "Roles")]
        //public virtual Role Role { get; set; }
        #endregion Public Properties
    }
}
