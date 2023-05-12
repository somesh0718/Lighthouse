using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("FVStudentAttendances")]
    public partial class FVStudentAttendance : BaseEntity
    {
        public FVStudentAttendance()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("FVStudentAttendanceId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "FV Student Attendance", Description = "FV Student Attendance")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid FVStudentAttendanceId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTFieldIndustryVisitConductedId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Field Industry Visit Conducted", Description = "Field Industry Visit Conducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Vocational Trainer", Description = "Vocational Trainer")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Class", Description = "Class")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Student", Description = "Student")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        // Foreign key
        [DataMember]
        [Column("IsPresent", TypeName = "BIT")]
        [Display(Name = "Is Present?", Description = "Is Present?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsPresent { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}