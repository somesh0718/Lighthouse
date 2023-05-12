using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("ErrorLogs")]
    public partial class ErrorLog : BaseEntity
    {
        public ErrorLog()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("ErrorLogId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Error Log Id", Description = "Error Log Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ErrorLogId { get; set; }

        [DataMember]
        [Column("ModuleName", Order = 2)]
        [Display(Name = "Module Name", Description = "Module Name", Order = 2)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ModuleName { get; set; }

        [DataMember]
        [Column("ErrorCode", Order = 3)]
        [Display(Name = "Error Code", Description = "Error Code", Order = 3)]
        [MaxLength(30, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ErrorCode { get; set; }

        [DataMember]
        [Column("ErrorSeverity", TypeName = "INT", Order = 4)]
        [Display(Name = "Error Severity", Description = "Error Severity", Order = 4)]
        public virtual int? ErrorSeverity { get; set; }

        [DataMember]
        [Column("ErrorState", TypeName = "INT", Order = 5)]
        [Display(Name = "Error State", Description = "Error State", Order = 5)]
        public virtual int? ErrorState { get; set; }

        [DataMember]
        [Column("ErrorProcedure", Order = 6)]
        [Display(Name = "Error Procedure", Description = "Error Procedure", Order = 6)]
        [MaxLength(70, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ErrorProcedure { get; set; }

        [DataMember]
        [Column("ErrorLine", TypeName = "INT", Order = 7)]
        [Display(Name = "Error Line", Description = "Error Line", Order = 7)]
        public virtual int? ErrorLine { get; set; }

        [DataMember]
        [Column("ErrorTime", TypeName = "DATETIME", Order = 8)]
        [Display(Name = "Error Time", Description = "Error Time", Order = 8)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ErrorTime { get; set; }

        [DataMember]
        [Column("ErrorType", Order = 9)]
        [Display(Name = "Error Type", Description = "Error Type", Order = 9)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ErrorType { get; set; }

        [DataMember]
        [Column("ErrorLocation", Order = 10)]
        [Display(Name = "Error Location", Description = "Error Location", Order = 10)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ErrorLocation { get; set; }

        [DataMember]
        [Column("ErrorMessage", Order = 11)]
        [Display(Name = "Error Message", Description = "Error Message", Order = 11)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ErrorMessage { get; set; }

        [DataMember]
        [Column("StackTrace", Order = 12)]
        [Display(Name = "Stack Trace", Description = "Stack Trace", Order = 12)]
        [MaxLength(3500, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StackTrace { get; set; }

        [DataMember]
        [Column("ErrorStatus", Order = 13)]
        [Display(Name = "Error Status", Description = "Error Status", Order = 13)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ErrorStatus { get; set; }

        [DataMember]
        [Column("IsResolved", TypeName = "BIT", Order = 14)]
        [Display(Name = "Is Resolved?", Description = "Is Resolved?", Order = 14)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsResolved { get; set; }
        #endregion Public Properties
    }
}
