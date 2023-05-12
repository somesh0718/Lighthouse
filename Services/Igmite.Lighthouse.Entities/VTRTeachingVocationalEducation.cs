using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRTeachingVocationalEducations")]
    public partial class VTRTeachingVocationalEducation : BaseEntityCreation
    {
        public VTRTeachingVocationalEducation()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRTeachingVocationalEducationId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTRTeaching Vocational Education Id", Description = "VTRTeaching Vocational Education Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTeachingVocationalEducationId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("ClassTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Class Taught Id", Description = "Class Taught Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassTaughtId { get; set; }

        [DataMember]
        [Column("SectionTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Section Taught Id", Description = "Section Taught Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionTaughtId { get; set; }

        [DataMember]
        [Column("ClassTypeId")]
        [Display(Name = "Class Type Id", Description = "Class Type Id")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClassTypeId { get; set; }

        [DataMember]
        [Column("ClassTime")]
        [Display(Name = "Class Time", Description = "Class Time")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClassTime { get; set; }

        [DataMember]
        [Column("ClassPicture")]
        [Display(Name = "Class Picture", Description = "Class Picture")]
        public virtual string ClassPicture { get; set; }

        [DataMember]
        [Column("LessonPlanPicture")]
        [Display(Name = "Lesson Plan Picture", Description = "Lesson Plan Picture")]
        public virtual string LessonPlanPicture { get; set; }

        [DataMember]
        [Column("ReasonDetails")]
        [Display(Name = "Reason Details", Description = "Reason Details")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ReasonDetails { get; set; }

        [DataMember]
        [Column("IsTeachToday", TypeName = "BIT")]
        [Display(Name = "Did You Teach Today?", Description = "Did You Teach Today?")]
        public virtual bool IsTeachToday { get; set; }

        [DataMember]
        [Column("SequenceNo", TypeName = "INT")]
        [Display(Name = "Sequence No", Description = "Sequence No")]
        public virtual int SequenceNo { get; set; }
        
        #endregion Public Properties
    }
}