using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRWorkingDayTypes")]
    public partial class VTRWorkingDayType : BaseEntityCreation
    {
        public VTRWorkingDayType()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRWorkingDayTypeId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTR Working Day Type", Description = "VTR Working Day Type", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRWorkingDayTypeId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VT Daily Reporting", Description = "VT Daily Reporting", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

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