using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("AccountUserTerms")]
    public partial class AccountUserTerm : BaseEntityCreation
    {
        public AccountUserTerm()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AccountTermsId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Account Terms Id", Description = "Account Terms Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountTermsId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Account Id", Description = "Account Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        // Foreign key
        [DataMember]
        [Column("TermsConditionId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Terms Condition Id", Description = "Terms Condition Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TermsConditionId { get; set; }

        [DataMember]
        [Column("IsLatestTerms", TypeName = "BIT", Order = 4)]
        [Display(Name = "Is Latest Terms?", Description = "Is Latest Terms?", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsLatestTerms { get; set; }

        [DataMember]
        [Column("AcceptedDate", TypeName = "DATETIME", Order = 5)]
        [Display(Name = "Accepted Date", Description = "Accepted Date", Order = 5)]
        public virtual DateTime? AcceptedDate { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Accounts", Description = "Accounts")]
        //public virtual Account Account { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Terms Conditions", Description = "Terms Conditions")]
        //public virtual TermsCondition Scondition { get; set; }
        #endregion Public Properties
    }
}
