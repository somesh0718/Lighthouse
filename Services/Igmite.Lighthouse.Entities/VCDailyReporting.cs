using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCDailyReporting")]
    public partial class VCDailyReporting : BaseEntity
    {
        public VCDailyReporting()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCDailyReportingId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCDaily Reporting Id", Description = "VCDaily Reporting Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCDailyReportingId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCSchoolSectorId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCSchoolSectorId", Description = "VCSchoolSectorId")]
        public virtual Guid VCSchoolSectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCId", Description = "VCId")]
        public virtual Guid VCId { get; set; }

        [DataMember]
        [Column("ReportDate", TypeName = "DATETIME")]
        [Display(Name = "Report Date", Description = "Report Date")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ReportDate { get; set; }

        [DataMember]
        [Column("ReportType")]
        [Display(Name = "Report Type", Description = "Report Type")]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string ReportType { get; set; }

        [DataMember]
        [Column("WorkTypeDetails")]
        [Display(Name = "Work Type Details", Description = "Work Type Details")]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string WorkTypeDetails { get; set; }

        [DataMember]
        [Column("SchoolId")]
        [Display(Name = "School", Description = "School")]
        public virtual Guid? SchoolId { get; set; }

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

        #endregion Public Properties
    }
}