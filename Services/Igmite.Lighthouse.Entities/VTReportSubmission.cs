using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTReportSubmissions")]
    public partial class VTReportSubmission : RequestModel
    {
        public VTReportSubmission()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTReportSubmissionId", TypeName = "INT", Order = 1)]
        [Display(Name = "VTReportSubmission Id", Description = "VTReportSubmission Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int VTReportSubmissionId { get; set; }

        [DataMember]
        [Column("VTSchoolSectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTSchoolSectorId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("ReportingDate", TypeName = "DATETIME", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        [Column("IsReportSubmitted", TypeName = "BIT", Order = 5)]
        public virtual bool IsReportSubmitted { get; set; }

        [DataMember]
        [Column("IsHoliday", TypeName = "BIT", Order = 6)]
        public virtual bool IsHoliday { get; set; }

        #endregion Public Properties
    }
}