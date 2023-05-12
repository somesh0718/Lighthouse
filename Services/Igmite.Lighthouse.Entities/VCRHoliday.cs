using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCRHolidays")]
    public partial class VCRHoliday : BaseEntityCreation
    {
        public VCRHoliday()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCRHolidayId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VCRHoliday Id", Description = "VCRHoliday Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCRHolidayId { get; set; }

        [DataMember]
        [Column("VCDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VCDaily Reporting Id", Description = "VCDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCDailyReportingId { get; set; }

        [DataMember]
        [Column("HolidayTypeId", Order = 3)]
        [Display(Name = "Holiday Type Id", Description = "Holiday Type Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string HolidayTypeId { get; set; }

        [DataMember]
        [Column("HolidayDetails", Order = 4)]
        [Display(Name = "Holiday Details", Description = "Holiday Details", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string HolidayDetails { get; set; }

        #endregion Public Properties
    }
}