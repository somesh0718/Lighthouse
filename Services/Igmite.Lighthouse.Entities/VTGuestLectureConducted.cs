using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTGuestLectureConducted")]
    public partial class VTGuestLectureConducted : BaseEntity
    {
        public VTGuestLectureConducted()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTGuestLectureId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTGuest Lecture Id", Description = "VTGuest Lecture Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTGuestLectureId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTSchoolSectorId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT School Sector", Description = "VT School Sector")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTSchoolSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VT Id", Description = "VT Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ClassTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Class", Description = "Class")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassTaughtId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectionTaughtId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "Section", Description = "Section")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionTaughtId { get; set; }

        [DataMember]
        [Column("ReportingDate", TypeName = "DATETIME")]
        [Display(Name = "Reporting Date", Description = "Reporting Date")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        [Column("GLType")]
        [Display(Name = "Guest Lecture Type", Description = "Guest Lecture Type")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(36, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLType { get; set; }

        [DataMember]
        [Column("GLTopic")]
        [Display(Name = "Guest Lecture Topic", Description = "Guest Lecture Topic")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLTopic { get; set; }

        [DataMember]
        [Column("ClassTime", TypeName = "INT")]
        [Display(Name = "Class Time", Description = "Class Time")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int ClassTime { get; set; }

        [DataMember]
        [Column("GLMethodologyDetails")]
        [Display(Name = "GLMethodology Details", Description = "GLMethodology Details")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLMethodologyDetails { get; set; }

        [DataMember]
        [Column("GLPhotoInClass")]
        [Display(Name = "GL Photo In Class", Description = "GL Photo In Class")]
        public virtual string GLPhotoInClass { get; set; }

        [DataMember]
        [Column("GLConductedBy")]
        [Display(Name = "GL Conducted By", Description = "GL Conducted By")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLConductedBy { get; set; }

        [DataMember]
        [Column("GLPersonDetails")]
        [Display(Name = "GL Person Details", Description = "GL Person Details")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLPersonDetails { get; set; }

        [DataMember]
        [Column("GLName")]
        [Display(Name = "Guest Lecturer", Description = "Guest Lecturer")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLName { get; set; }

        [DataMember]
        [Column("GLMobile")]
        [Display(Name = "Guest Lecturer Mobile", Description = "Guest Lecturer Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLMobile { get; set; }

        [DataMember]
        [Column("GLEmail")]
        [Display(Name = "Guest Lecturer Email", Description = "Guest Lecturer Email")]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLEmail { get; set; }

        [DataMember]
        [Column("GLQualification")]
        [Display(Name = "Guest Lecturer Qualification", Description = "Guest Lecturer Qualification")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLQualification { get; set; }

        [DataMember]
        [Column("GLWorkExperience")]
        [Display(Name = "Guest Lecturer Work Experience", Description = "Guest Lecturer Work Experience")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLWorkExperience { get; set; }

        [DataMember]
        [Column("GLAddress")]
        [Display(Name = "Guest Lecturer Address", Description = "Guest Lecturer Address")]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLAddress { get; set; }

        [DataMember]
        [Column("GLWorkStatus")]
        [Display(Name = "Guest Lecturer Work Status", Description = "Guest Lecturer Work Status")]
        [MaxLength(36, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLWorkStatus { get; set; }

        [DataMember]
        [Column("GLCompany")]
        [Display(Name = "Guest Lecturer Company", Description = "Guest Lecturer Company")]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLCompany { get; set; }

        [DataMember]
        [Column("GLDesignation")]
        [Display(Name = "Guest Lecturer Designation", Description = "Guest Lecturer Designation")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GLDesignation { get; set; }

        [DataMember]
        [Column("GLPhoto")]
        [Display(Name = "Guest Lecturer Photo", Description = "Guest Lecturer Photo")]
        public virtual string GLPhoto { get; set; }

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