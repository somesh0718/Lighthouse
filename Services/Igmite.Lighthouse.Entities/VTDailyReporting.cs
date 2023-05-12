using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTDailyReporting")]
    public partial class VTDailyReporting : BaseEntity
    {
        public VTDailyReporting()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        // Primary key
        [DataMember]
        [Column("VTSchoolSectorId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT School Sector Id", Description = "VT School Sector Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTSchoolSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Id", Description = "VT Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("ReportingDate", TypeName = "DATETIME")]
        [Display(Name = "Reporting Date", Description = "Reporting Date")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        [Column("ReportType")]
        [Display(Name = "Report Type", Description = "Report Type")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ReportType { get; set; }

        [DataMember]
        [Column("SchoolEventCelebration")]
        [Display(Name = "School Event / Celebration", Description = "School Event / Celebration")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolEventCelebration { get; set; }

        [DataMember]
        [Column("WorkAssignedByHeadMaster")]
        [Display(Name = "Work assigned by Head Master", Description = "Work assigned by Head Master")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string WorkAssignedByHeadMaster { get; set; }

        [DataMember]
        [Column("SchoolExamDuty")]
        [Display(Name = "School Exam Duty", Description = "School Exam Duty")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolExamDuty { get; set; }

        [DataMember]
        [Column("OtherWork")]
        [Display(Name = "Other Work", Description = "Other Work")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OtherWork { get; set; }

        [DataMember]
        [Column("ObservationDetails")]
        [Display(Name = "Observation Details", Description = "Observation Details")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ObservationDetails { get; set; }

        [DataMember]
        [Column("OBStudentCount", TypeName = "INT")]
        [Display(Name = "Student Count", Description = "Student Count")]
        public virtual int OBStudentCount { get; set; }

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

        #endregion Public Properties
    }
}