using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("GLStudentAttendances")]
    public partial class GLStudentAttendances : BaseEntity
    {
        public GLStudentAttendances()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("GLStudentAttendanceId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "GL Student Attendance", Description = "GL Student Attendance")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid GLStudentAttendanceId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Guest Lecture Conducted", Description = "VT Guest Lecture Conducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

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