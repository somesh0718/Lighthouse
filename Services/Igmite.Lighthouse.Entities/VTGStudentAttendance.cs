﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTGStudentAttendances")]
    public partial class VTGStudentAttendance : BaseEntityCreation
    {
        public VTGStudentAttendance()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTGStudentAttendanceId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTGStudent Attendance Id", Description = "VTGStudent Attendance Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGStudentAttendanceId { get; set; }

        [DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTGuestLectureId", Description = "VTGuestLectureId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "VTId", Description = "VTId", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Class Id", Description = "Class Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Student Id", Description = "Student Id", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        [Column("IsPresent", TypeName = "BIT", Order = 6)]
        [Display(Name = "Is Present?", Description = "Is Present?", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool IsPresent { get; set; }

        [DataMember]
        [Column("Remarks", Order = 7)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 7)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}