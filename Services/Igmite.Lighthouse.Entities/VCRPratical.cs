using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCRPraticals")]
    public partial class VCRPratical : BaseEntityCreation
    {
        public VCRPratical()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRPraticalId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRPratical Id", Description = "VTRPratical Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRPraticalId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VC Daily Reporting", Description = "VC Daily Reporting", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCDailyReportingId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "VC Id", Description = "VC Id", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "School", Description = "School", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 6)]
        [Display(Name = "VT Id", Description = "VT Id", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 7)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 8)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 8)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid JobRoleId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 9)]
        [Display(Name = "Class", Description = "Class", Order = 9)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("IsPratical", Order = 10)]
        [Display(Name = "IsPratical", Description = "IsPratical", Order = 10)]
        public virtual string IsPratical { get; set; }

        [DataMember]
        [Column("StudentCount", Order = 11)]
        [Display(Name = "Enrolled Student Count", Description = "Enrolled Student Count", Order = 11)]
        public virtual string StudentCount { get; set; }

        [DataMember]
        [Column("VTPresent", Order = 12)]
        [Display(Name = "VT Present", Description = "VT Present", Order = 12)]
        public virtual string VTPresent { get; set; }

        [DataMember]
        [Column("PresentStudentCount", Order = 13)]
        [Display(Name = "Present Student Count", Description = "Present Student Count", Order = 13)]
        public virtual int PresentStudentCount { get; set; }

        [DataMember]
        [Column("AssesorName", Order = 14)]
        [Display(Name = "Assesor Name", Description = "Assesor Name", Order = 14)]
        public virtual string AssesorName { get; set; }

        [DataMember]
        [Column("AssesorMobileNo", Order = 15)]
        [Display(Name = "Assesor Mobile No", Description = "Assesor Mobile No", Order = 15)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssesorMobileNo { get; set; }

        [DataMember]
        [Column("Remarks", Order = 16)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 16)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}