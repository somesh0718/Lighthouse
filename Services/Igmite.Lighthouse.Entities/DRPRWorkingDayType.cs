using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("DRPRWorkingDayTypes")]
    public partial class DRPRWorkingDayType : BaseEntityCreation
    {
        public DRPRWorkingDayType()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DRPRWorkingDayTypeId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "DRPR Working Day Type", Description = "DRPR Working Day Type", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DRPRWorkingDayTypeId { get; set; }

        [DataMember]
        [Column("DRPDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "DRP Daily Reporting", Description = "DRP Daily Reporting", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DRPDailyReportingId { get; set; }

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