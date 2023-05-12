using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTPMonthlyBillSubmissionStatus")]
    public partial class VTPMonthlyBillSubmissionStatus : BaseEntity
    {
        public VTPMonthlyBillSubmissionStatus()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTPMonthlyBillSubmissionStatusId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTPMonthly Bill Submission Status Id", Description = "VTPMonthly Bill Submission Status Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPMonthlyBillSubmissionStatusId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VCId", Description = "VCId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }

        [DataMember]
        [Column("Month", Order = 3)]
        [Display(Name = "Month", Description = "Month", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Month { get; set; }

        [DataMember]
        [Column("DateSubmission", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Date Submission", Description = "Date Submission", Order = 4)]
        public virtual DateTime? DateSubmission { get; set; }

        [DataMember]
        [Column("Incorrect", Order = 5)]
        [Display(Name = "Incorrect", Description = "Incorrect", Order = 5)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Incorrect { get; set; }

        [DataMember]
        [Column("IncorrectDetails", Order = 6)]
        [Display(Name = "Incorrect Details", Description = "Incorrect Details", Order = 6)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IncorrectDetails { get; set; }

        [DataMember]
        [Column("Final", Order = 7)]
        [Display(Name = "Final", Description = "Final", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Final { get; set; }

        [DataMember]
        [Column("ApprovedPMU", Order = 8)]
        [Display(Name = "Approved PMU", Description = "Approved PMU", Order = 8)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovedPMU { get; set; }

        [DataMember]
        [Column("Amount", TypeName = "INT", Order = 9)]
        [Display(Name = "Amount", Description = "Amount", Order = 9)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Amount { get; set; }

        [DataMember]
        [Column("DiaryentryDone", Order = 10)]
        [Display(Name = "Diaryentry Done", Description = "Diaryentry Done", Order = 10)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DiaryentryDone { get; set; }

        [DataMember]
        [Column("DiaryentryNumber", Order = 11)]
        [Display(Name = "Diaryentry Number", Description = "Diaryentry Number", Order = 11)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DiaryentryNumber { get; set; }

        [DataMember]
        [Column("Details", Order = 12)]
        [Display(Name = "Details", Description = "Details", Order = 12)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Details { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Vocational Coordinators", Description = "Vocational Coordinators")]
        //public virtual VocationalCoordinator VocationalCoordinator { get; set; }
        #endregion Public Properties
    }
}
