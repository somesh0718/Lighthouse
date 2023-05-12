using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("TermsConditions")]
    public partial class TermsCondition : BaseEntityCreation
    {
        public TermsCondition()
        {
            //this.AccountUserTerms = new List<AccountUserTerm>();
            //this.UserAcceptances = new List<UserAcceptance>();

            //this.DeletedAccountIds = new List<Guid>();
            //this.DeletedUserAcceptanceIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("TermsConditionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Terms Condition Id", Description = "Terms Condition Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TermsConditionId { get; set; }

        [DataMember]
        [Column("Name", Order = 2)]
        [Display(Name = "Name", Description = "Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("Description", Order = 3)]
        [Display(Name = "Description", Description = "Description", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(-1, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("ApplicableFrom", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Applicable From", Description = "Applicable From", Order = 4)]
        public virtual DateTime? ApplicableFrom { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Account User Terms", Description = "Account User Terms")]
        //public virtual IList<AccountUserTerm> AccountUserTerms { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "User Acceptances", Description = "User Acceptances")]
        //public virtual IList<UserAcceptance> UserAcceptances { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedAccountIds { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedUserAcceptanceIds { get; set; }

        #endregion Public Properties
    }
}
