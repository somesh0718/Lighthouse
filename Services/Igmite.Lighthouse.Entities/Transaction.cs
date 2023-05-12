using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Transactions")]
    public partial class Transaction : BaseEntity
    {
        public Transaction()
        {
            //this.AccountTransactions = new List<AccountTransaction>();
            //this.RoleTransactions = new List<RoleTransaction>();
            //this.SiteSubHeaders = new List<SiteSubHeader>();

            //this.DeletedAccountIds = new List<Guid>();
            //this.DeletedRoleIds = new List<Guid>();
            //this.DeletedSiteSubHeaderIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("TransactionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Transaction Id", Description = "Transaction Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TransactionId { get; set; }

        [DataMember]
        [Column("Code", Order = 2)]
        [Display(Name = "Code", Description = "Code", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Code { get; set; }

        [DataMember]
        [Column("Name", Order = 3)]
        [Display(Name = "Name", Description = "Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(70, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("PageTitle", Order = 4)]
        [Display(Name = "Page Title", Description = "Page Title", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PageTitle { get; set; }

        [DataMember]
        [Column("PageDescription", Order = 5)]
        [Display(Name = "Page Description", Description = "Page Description", Order = 5)]
        [MaxLength(500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PageDescription { get; set; }

        [DataMember]
        [Column("UrlAction", Order = 6)]
        [Display(Name = "Url Action", Description = "Url Action", Order = 6)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UrlAction { get; set; }

        [DataMember]
        [Column("UrlController", Order = 7)]
        [Display(Name = "Url Controller", Description = "Url Controller", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UrlController { get; set; }

        [DataMember]
        [Column("UrlPara", Order = 8)]
        [Display(Name = "Url Para", Description = "Url Para", Order = 8)]
        [MaxLength(300, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UrlPara { get; set; }

        [DataMember]
        [Column("RouteUrl", Order = 9)]
        [Display(Name = "Route Url", Description = "Route Url", Order = 9)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RouteUrl { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 10)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 10)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("Remarks", Order = 11)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 11)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Account Transactions", Description = "Account Transactions")]
        //public virtual IList<AccountTransaction> AccountTransactions { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Role Transactions", Description = "Role Transactions")]
        //public virtual IList<RoleTransaction> RoleTransactions { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Site Sub Headers", Description = "Site Sub Headers")]
        //public virtual IList<SiteSubHeader> SiteSubHeaders { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedAccountIds { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedRoleIds { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedSiteSubHeaderIds { get; set; }

        #endregion Public Properties
    }
}
