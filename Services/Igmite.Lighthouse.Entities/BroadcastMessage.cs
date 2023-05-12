using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("BroadcastMessages")]
    public partial class BroadcastMessage : BaseEntity
    {
        public BroadcastMessage()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("BroadcastMessageId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "BroadcastMessageId", Description = "BroadcastMessageId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid BroadcastMessageId { get; set; }

        [DataMember]
        [Column("MessageText")]
        [Display(Name = "Message Text", Description = "Message Text")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(1000, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MessageText { get; set; }

        [DataMember]
        [Column("FromDate", TypeName = "DATETIME")]
        [Display(Name = "From Date", Description = "From Date")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime FromDate { get; set; }

        [DataMember]
        [Column("ToDate", TypeName = "DATETIME")]
        [Display(Name = "To Date", Description = "To Date")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ToDate { get; set; }

        [DataMember]
        [Column("ApplicableFor")]
        [Display(Name = "Applicable For", Description = "Applicable For")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApplicableFor { get; set; }
       
        #endregion Public Properties
    }
}
