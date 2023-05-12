using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("RoleTransactions")]
    public partial class RoleTransaction : BaseEntity
    {
        public RoleTransaction()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("RoleTransactionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Role Transaction Id", Description = "Role Transaction Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid RoleTransactionId { get; set; }

        // Foreign key
        [DataMember]
        [Column("RoleId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Role Id", Description = "Role Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid RoleId { get; set; }

        // Foreign key
        [DataMember]
        [Column("TransactionId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Transaction Id", Description = "Transaction Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TransactionId { get; set; }

        [DataMember]
        [Column("Rights", TypeName = "BIT", Order = 4)]
        [Display(Name = "Rights", Description = "Rights", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool Rights { get; set; }

        [DataMember]
        [Column("CanAdd", TypeName = "BIT", Order = 5)]
        [Display(Name = "Can Add", Description = "Can Add", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool CanAdd { get; set; }

        [DataMember]
        [Column("CanEdit", TypeName = "BIT", Order = 6)]
        [Display(Name = "Can Edit", Description = "Can Edit", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool CanEdit { get; set; }

        [DataMember]
        [Column("CanDelete", TypeName = "BIT", Order = 7)]
        [Display(Name = "Can Delete", Description = "Can Delete", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool CanDelete { get; set; }

        [DataMember]
        [Column("CanView", TypeName = "BIT", Order = 8)]
        [Display(Name = "Can View", Description = "Can View", Order = 8)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool CanView { get; set; }

        [DataMember]
        [Column("CanExport", TypeName = "BIT", Order = 9)]
        [Display(Name = "Can Export", Description = "Can Export", Order = 9)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool CanExport { get; set; }

        [DataMember]
        [Column("ListView", TypeName = "BIT", Order = 10)]
        [Display(Name = "List View", Description = "List View", Order = 10)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool ListView { get; set; }

        [DataMember]
        [Column("BasicView", TypeName = "BIT", Order = 11)]
        [Display(Name = "Basic View", Description = "Basic View", Order = 11)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool BasicView { get; set; }

        [DataMember]
        [Column("DetailView", TypeName = "BIT", Order = 12)]
        [Display(Name = "Detail View", Description = "Detail View", Order = 12)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool DetailView { get; set; }

        [DataMember]
        [Column("IsPublic", TypeName = "BIT", Order = 13)]
        [Display(Name = "Is Public?", Description = "Is Public?", Order = 13)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsPublic { get; set; }

        [DataMember]
        [Column("Remarks", Order = 14)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 14)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Roles", Description = "Roles")]
        //public virtual Role Role { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Transactions", Description = "Transactions")]
        //public virtual Transaction Transaction { get; set; }
        #endregion Public Properties
    }
}
