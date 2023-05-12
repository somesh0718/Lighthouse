using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCIssueReporting")]
    public partial class VCIssueReporting : BaseEntity
    {
        public VCIssueReporting()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCIssueReportingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCIssue Reporting Id", Description = "VCIssue Reporting Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCIssueReportingId { get; set; }

        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "AcademicYearId", Description = "VTId")]
        public virtual Guid? AcademicYearId { get; set; }

        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCId", Description = "VCId")]
        public virtual Guid VCId { get; set; }

        [DataMember]
        [Column("IssueMappingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "IssueMappingId", Description = "IssueMappingId")]
        public virtual Guid? IssueMappingId { get; set; }

        [DataMember]
        [Column("IssueReportDate", TypeName = "DATETIME")]
        [Display(Name = "Report Date", Description = "Report Date?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime IssueReportDate { get; set; }

        [DataMember]
        [Column("MainIssue")]
        [Display(Name = "Main Issue", Description = "Main Issue")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MainIssue { get; set; }

        [DataMember]
        [Column("SubIssue")]
        [Display(Name = "Sub Issue", Description = "Sub Issue")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SubIssue { get; set; }

        [DataMember]
        [Column("StudentClass")]
        [Display(Name = "Student Class", Description = "Student Class")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StudentClass { get; set; }

        [DataMember]
        [Column("Month")]
        [Display(Name = "Month", Description = "Month")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Month { get; set; }

        [DataMember]
        [Column("StudentType")]
        [Display(Name = "Student Type", Description = "Student Type")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StudentType { get; set; }

        [DataMember]
        [Column("NoOfStudents", TypeName = "INT")]
        [Display(Name = "No Of Students", Description = "No Of Students")]
        public virtual int NoOfStudents { get; set; }

        [DataMember]
        [Column("IssueDetails")]
        [Display(Name = "Issue Details?", Description = "Issue Details?")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IssueDetails { get; set; }

        [DataMember]
        [Column("GeoLocation")]
        [Display(Name = "Geo Location", Description = "Geo Location")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GeoLocation { get; set; }

        [DataMember]
        [Column("Latitude")]
        [Display(Name = "Latitude", Description = "Latitude")]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Latitude { get; set; }

        [DataMember]
        [Column("Longitude")]
        [Display(Name = "Longitude", Description = "Longitude")]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Longitude { get; set; }

        [DataMember]
        [Column("ApprovalStatus")]
        [Display(Name = "Approval Status", Description = "Approval Status")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalStatus { get; set; }

        [DataMember]
        [Column("ApprovedDate", TypeName = "DATETIME")]
        [Display(Name = "Approved Date", Description = "Approved Date")]
        public virtual DateTime? ApprovedDate { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}