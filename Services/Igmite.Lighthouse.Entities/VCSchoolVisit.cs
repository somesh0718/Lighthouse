using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCSchoolVisits")]
    public partial class VCSchoolVisit : BaseEntity
    {
        public VCSchoolVisit()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCSchoolVisitId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCSchool Visit Id", Description = "VCSchool Visit Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCSchoolVisitId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCSchoolSectorId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCSchool Sector Id", Description = "VCSchool Sector Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCSchoolSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCId", Description = "VCId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }

        [DataMember]
        [Column("ReportDate", TypeName = "DATETIME")]
        [Display(Name = "Report Date", Description = "Report Date")]
        public virtual DateTime? ReportDate { get; set; }

        [DataMember]
        [Column("GeoLocation")]
        [Display(Name = "Geo Location", Description = "Geo Location")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GeoLocation { get; set; }

        [DataMember]
        [Column("Month")]
        [Display(Name = "Month", Description = "Month")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Month { get; set; }

        [DataMember]
        [Column("VTReportSubmitted")]
        [Display(Name = "VT Report Submitted", Description = "VT Report Submitted")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTReportSubmitted { get; set; }

        [DataMember]
        [Column("VTWorkingDays", TypeName = "INT")]
        [Display(Name = "VT Working Days", Description = "VT Working Days")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int VTWorkingDays { get; set; }

        [DataMember]
        [Column("VTLeaveDays", TypeName = "INT")]
        [Display(Name = "VT Leave Days", Description = "VT Leave Days")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int VTLeaveDays { get; set; }

        [DataMember]
        [Column("VTTeachingDays", TypeName = "INT")]
        [Display(Name = "VT Teaching Days", Description = "VT Teaching Days")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int VTTeachingDays { get; set; }

        [DataMember]
        [Column("ClassVisited")]
        [Display(Name = "Class Visited", Description = "Class Visited")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClassVisited { get; set; }

        [DataMember]
        [Column("ClassTeachingDays", TypeName = "INT")]
        [Display(Name = "Class Teaching Days", Description = "Class Teaching Days")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int ClassTeachingDays { get; set; }

        [DataMember]
        [Column("BoysEnrolledCheck", TypeName = "INT")]
        [Display(Name = "Boys Enrolled Check", Description = "Boys Enrolled Check")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int BoysEnrolledCheck { get; set; }

        [DataMember]
        [Column("GirlsEnrolledCheck", TypeName = "INT")]
        [Display(Name = "Girls Enrolled Check", Description = "Girls Enrolled Check")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int GirlsEnrolledCheck { get; set; }

        [DataMember]
        [Column("AvgStudentAttendance", TypeName = "INT")]
        [Display(Name = "Avg Student Attendance", Description = "Avg Student Attendance")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int AvgStudentAttendance { get; set; }

        [DataMember]
        [Column("CMAvailability")]
        [Display(Name = "CMAvailability", Description = "CMAvailability")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CMAvailability { get; set; }

        [DataMember]
        [Column("CMDate", TypeName = "DATETIME")]
        [Display(Name = "CMDate", Description = "CMDate")]
        public virtual DateTime? CMDate { get; set; }

        [DataMember]
        [Column("TEAvailability")]
        [Display(Name = "TEAvailability", Description = "TEAvailability")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TEAvailability { get; set; }

        [DataMember]
        [Column("TEDate", TypeName = "DATETIME")]
        [Display(Name = "TEDate", Description = "TEDate")]
        public virtual DateTime? TEDate { get; set; }

        [DataMember]
        [Column("NoOfGLConducted", TypeName = "INT")]
        [Display(Name = "No Of GLConducted", Description = "No Of GLConducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int NoOfGLConducted { get; set; }

        [DataMember]
        [Column("NoOfFVConducted", TypeName = "INT")]
        [Display(Name = "No Of FVConducted", Description = "No Of FVConducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int NoOfFVConducted { get; set; }

        [DataMember]
        [Column("SchoolHMVisited")]
        [Display(Name = "School HMVisited", Description = "School HMVisited")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolHMVisited { get; set; }

        [DataMember]
        [Column("HMRatingVTattendance", TypeName = "INT")]
        [Display(Name = "HMRating VTattendance", Description = "HMRating VTattendance")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int HMRatingVTattendance { get; set; }

        [DataMember]
        [Column("HMRatingSyllabuscompletion", TypeName = "INT")]
        [Display(Name = "HMRating Syllabuscompletion", Description = "HMRating Syllabuscompletion")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int HMRatingSyllabuscompletion { get; set; }

        [DataMember]
        [Column("HMRatingVtreporting", TypeName = "INT")]
        [Display(Name = "HMRating Vtreporting", Description = "HMRating Vtreporting")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int HMRatingVtreporting { get; set; }

        [DataMember]
        [Column("HMRatingVtqualityteaching", TypeName = "INT")]
        [Display(Name = "HMRating Vtqualityteaching", Description = "HMRating Vtqualityteaching")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int HMRatingVtqualityteaching { get; set; }

        [DataMember]
        [Column("HMRatingVtglfvquality", TypeName = "INT")]
        [Display(Name = "HMRating Vtglfvquality", Description = "HMRating Vtglfvquality")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int HMRatingVtglfvquality { get; set; }

        [DataMember]
        [Column("HMRatingInitiativestaken", TypeName = "INT")]
        [Display(Name = "HMRating Initiativestaken", Description = "HMRating Initiativestaken")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int HMRatingInitiativestaken { get; set; }

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

        #endregion Public Properties
    }
}