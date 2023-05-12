using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCRWorkingDayTypes")]
    public partial class VCRWorkingDayType : BaseEntityCreation
    {
        public VCRWorkingDayType()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCRWorkingDayTypeId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VCR Working Day Type", Description = "VCR Working Day Type", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCRWorkingDayTypeId { get; set; }

        [DataMember]
        [Column("VCDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VC Daily Reporting", Description = "VC Daily Reporting", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCDailyReportingId { get; set; }

        [DataMember]
        [Column("WorkingTypeId", Order = 3)]
        [Display(Name = "Working Day Type", Description = "Working Day Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string WorkingTypeId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}