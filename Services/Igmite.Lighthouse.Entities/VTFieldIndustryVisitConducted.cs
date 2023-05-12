using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTFieldIndustryVisitConducted")]
    public partial class VTFieldIndustryVisitConducted : BaseEntity
    {
        public VTFieldIndustryVisitConducted()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTFieldIndustryVisitConductedId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTField Industry Visit Conducted Id", Description = "VTField Industry Visit Conducted Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTFieldIndustryVisitConductedId { get; set; }

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

        [DataMember]
        [Column("ReportingDate", TypeName = "DATETIME")]
        [Display(Name = "Reporting Date", Description = "Reporting Date")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ReportingDate { get; set; }

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
        [Column("FieldVisitTheme")]
        [Display(Name = "Field Visit Theme", Description = "Field Visit Theme")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FieldVisitTheme { get; set; }

        [DataMember]
        [Column("FieldVisitActivities")]
        [Display(Name = "Field Visit Activities", Description = "Field Visit Activities")]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FieldVisitActivities { get; set; }

        [DataMember]
        [Column("FVOrganisation")]
        [Display(Name = "Field Visit Organisation", Description = "Field Visit Organisation")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVOrganisation { get; set; }

        [DataMember]
        [Column("FVOrganisationAddress")]
        [Display(Name = "Organisation Address", Description = "Organisation Address")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVOrganisationAddress { get; set; }

        [DataMember]
        [Column("FVDistance")]
        [Display(Name = "Field Visit Distance", Description = "Field Visit Distance")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVDistance { get; set; }

        [DataMember]
        [Column("FVPicture")]
        [Display(Name = "Field Visit Picture", Description = "Field Visit Picture")]
        public virtual string FVPicture { get; set; }

        [DataMember]
        [Column("FVContactPersonName")]
        [Display(Name = "Field Visit Contact Person", Description = "Field Visit Contact Person")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVContactPersonName { get; set; }

        [DataMember]
        [Column("FVContactPersonMobile")]
        [Display(Name = "Contact Person Mobile", Description = "Contact Person Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVContactPersonMobile { get; set; }

        [DataMember]
        [Column("FVContactPersonEmail")]
        [Display(Name = "Contact Person Email", Description = "Contact Person Email")]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVContactPersonEmail { get; set; }

        [DataMember]
        [Column("FVContactPersonDesignation")]
        [Display(Name = "Contact Person Designation", Description = "Contact Person Designation")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVContactPersonDesignation { get; set; }

        [DataMember]
        [Column("FVOrganisationInterestStatus")]
        [Display(Name = "Organisation Interest Status", Description = "Organisation Interest Status")]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVOrganisationInterestStatus { get; set; }

        [DataMember]
        [Column("FVOrignisationOJTStatus")]
        [Display(Name = "Orignisation OJT Status", Description = "Orignisation OJT Status")]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVOrignisationOJTStatus { get; set; }

        [DataMember]
        [Column("FeedbackFromOrgnisation")]
        [Display(Name = "Feedback from Orgnisation", Description = "Feedback from Orgnisation")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FeedbackFromOrgnisation { get; set; }

        [DataMember]
        [Column("Remarks")]
        [Display(Name = "Remarks", Description = "Remarks")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

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