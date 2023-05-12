using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SiteSubHeaders")]
    public partial class SiteSubHeader : BaseEntity
    {
        public SiteSubHeader()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SiteSubHeaderId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Site Sub Header Id", Description = "Site Sub Header Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SiteSubHeaderId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SiteHeaderId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Site Header Id", Description = "Site Header Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SiteHeaderId { get; set; }

        // Foreign key
        [DataMember]
        [Column("TransactionId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Transaction Id", Description = "Transaction Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TransactionId { get; set; }

        [DataMember]
        [Column("IsHeaderMenu", TypeName = "BIT", Order = 4)]
        [Display(Name = "Is Header Menu?", Description = "Is Header Menu?", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsHeaderMenu { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 5)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Site Headers", Description = "Site Headers")]
        //public virtual SiteHeader Header { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Transactions", Description = "Transactions")]
        //public virtual Transaction Transaction { get; set; }
        #endregion Public Properties
    }
}
