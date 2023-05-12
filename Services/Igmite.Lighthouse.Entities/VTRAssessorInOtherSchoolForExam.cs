using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRAssessorInOtherSchoolForExams")]
    public partial class VTRAssessorInOtherSchoolForExam : BaseEntity
    {
        public VTRAssessorInOtherSchoolForExam()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRAssessorInOtherSchoolForExamId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRAssessor In Other School For Exam Id", Description = "VTRAssessor In Other School For Exam Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRAssessorInOtherSchoolForExamId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("SchoolName", Order = 3)]
        [Display(Name = "School Name", Description = "School Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolName { get; set; }

        [DataMember]
        [Column("UDISE", Order = 4)]
        [Display(Name = "Udise", Description = "Udise", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(11, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Udise { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Class Id", Description = "Class Id", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("BoysPresent", TypeName = "INT", Order = 6)]
        [Display(Name = "Boys Present", Description = "Boys Present", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int BoysPresent { get; set; }

        [DataMember]
        [Column("GirlsPresent", TypeName = "INT", Order = 7)]
        [Display(Name = "Girls Present", Description = "Girls Present", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int GirlsPresent { get; set; }

        [DataMember]
        [Column("ExamDetails", Order = 8)]
        [Display(Name = "Exam Details", Description = "Exam Details", Order = 8)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ExamDetails { get; set; }

        #endregion Public Properties
    }
}