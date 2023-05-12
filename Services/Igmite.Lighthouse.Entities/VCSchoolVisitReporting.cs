using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCSchoolVisitReporting")]
    public partial class VCSchoolVisitReporting : BaseEntity
    {
        public VCSchoolVisitReporting()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCSchoolVisitReportingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VC School Visit Reporting Id", Description = "VC School Visit Reporting Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCSchoolVisitReportingId { get; set; }

        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCId", Description = "VCId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }

        [DataMember]
        [Column("CompanyName")]
        [Display(Name = "CompanyName", Description = "CompanyName")]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CompanyName { get; set; }

        [DataMember]
        [Column("Month")]
        [Display(Name = "Month", Description = "Month")]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Month { get; set; }

        [DataMember]
        [Column("VisitDate", TypeName = "DATETIME")]
        [Display(Name = "Visit Date", Description = "Visit Date")]
        public virtual DateTime? VisitDate { get; set; }

        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "SchoolId", Description = "SchoolId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        [Column("DistrictCode")]
        [Display(Name = "District", Description = "DistrictCode")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictCode { get; set; }

        [DataMember]
        [Column("SchoolEmailId")]
        [Display(Name = "SchoolEmailId", Description = "SchoolEmailId")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolEmailId { get; set; }

        [DataMember]
        [Column("PrincipalName")]
        [Display(Name = "PrincipalName", Description = "PrincipalName")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PrincipalName { get; set; }

        [DataMember]
        [Column("PrincipalPhoneNo")]
        [Display(Name = "PrincipalPhoneNo", Description = "PrincipalPhoneNo")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PrincipalPhoneNo { get; set; }

        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "SectorId", Description = "SectorId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "JobRoleId", Description = "JobRoleId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid JobRoleId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTId", Description = "VTId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("VTPhoneNo")]
        [Display(Name = "VTPhoneNo", Description = "VTPhoneNo")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPhoneNo { get; set; }

        [DataMember]
        [Column("Labs")]
        [Display(Name = "Labs", Description = "Labs")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Labs { get; set; }

        [DataMember]
        [Column("Books")]
        [Display(Name = "Books", Description = "Books")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Books { get; set; }

        [DataMember]
        [Column("NoOfGLConducted", TypeName = "INT")]
        [Display(Name = "No Of GL Conducted", Description = "No Of GL Conducted")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int NoOfGLConducted { get; set; }

        [DataMember]
        [Column("NoOfIndustrialVisits", TypeName = "INT")]
        [Display(Name = "No Of Industrial Visits", Description = "No Of Industrial Visits")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int NoOfIndustrialVisits { get; set; }

        [DataMember]
        [Column("SVPhotoWithPrincipal")]
        [Display(Name = "SVPhotoWithPrincipal", Description = "SVPhotoWithPrincipal")]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SVPhotoWithPrincipal { get; set; }

        [DataMember]
        [Column("SVPhotoWithStudents")]
        [Display(Name = "SVPhotoWithStudents", Description = "SVPhotoWithStudents")]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SVPhotoWithStudents { get; set; }

        [DataMember]
        [Column("Class9Boys", TypeName = "INT")]
        [Display(Name = "Class 9 Boys", Description = "Class 9 Boys")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class9Boys { get; set; }

        [DataMember]
        [Column("Class9Girls", TypeName = "INT")]
        [Display(Name = "Class 9 Girls", Description = "Class 9 Girls")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class9Girls { get; set; }

        [DataMember]
        [Column("Class10Boys", TypeName = "INT")]
        [Display(Name = "Class 10 Boys", Description = "Class 10 Boys")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class10Boys { get; set; }

        [DataMember]
        [Column("Class10Girls", TypeName = "INT")]
        [Display(Name = "Class 10 Girls", Description = "Class 10 Girls")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class10Girls { get; set; }

        [DataMember]
        [Column("Class11Boys", TypeName = "INT")]
        [Display(Name = "Class 11 Boys", Description = "Class 11 Boys")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class11Boys { get; set; }

        [DataMember]
        [Column("Class11Girls", TypeName = "INT")]
        [Display(Name = "Class 11 Girls", Description = "Class 11 Girls")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class11Girls { get; set; }

        [DataMember]
        [Column("Class12Boys", TypeName = "INT")]
        [Display(Name = "Class 12 Boys", Description = "Class 12 Boys")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class12Boys { get; set; }

        [DataMember]
        [Column("Class12Girls", TypeName = "INT")]
        [Display(Name = "Class 12 Girls", Description = "Class 12 Girls")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int Class12Girls { get; set; }

        [DataMember]
        [Column("TotalBoys", TypeName = "INT")]
        [Display(Name = "Total Boys", Description = "Total Boys")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int TotalBoys { get; set; }

        [DataMember]
        [Column("TotalGirls", TypeName = "INT")]
        [Display(Name = "Total Girls", Description = "Total Girls")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int TotalGirls { get; set; }

        [DataMember]
        [Column("GeoLocation")]
        [Display(Name = "Geo Location", Description = "Geo Location")]
        [MaxLength(30, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string GeoLocation { get; set; }

        [DataMember]
        [Column("Latitude")]
        [Display(Name = "Latitude", Description = "Latitude")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Latitude { get; set; }

        [DataMember]
        [Column("Longitude")]
        [Display(Name = "Longitude", Description = "Longitude")]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Longitude { get; set; }

        #endregion Public Properties
    }
}