using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("UserAcceptances")]
    public partial class UserAcceptance : BaseEntityCreation
    {
        public UserAcceptance()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("UserAcceptanceId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "User Acceptance Id", Description = "User Acceptance Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid UserAcceptanceId { get; set; }

        // Foreign key
        [DataMember]
        [Column("TermsConditionId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Terms Condition Id", Description = "Terms Condition Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TermsConditionId { get; set; }

        [DataMember]
        [Column("UserMachineId", Order = 3)]
        [Display(Name = "User Machine Id", Description = "User Machine Id", Order = 3)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UserMachineId { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Terms Conditions", Description = "Terms Conditions")]
        //public virtual TermsCondition TermsCondition { get; set; }
        #endregion Public Properties
    }
}
