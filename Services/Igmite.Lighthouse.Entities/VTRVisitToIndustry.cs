using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRVisitToIndustries")]
    public partial class VTRVisitToIndustry : BaseEntityCreation
    {
        public VTRVisitToIndustry()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRVisitToIndustryId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRVisit To Industry Id", Description = "VTRVisit To Industry Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRVisitToIndustryId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("IndustryVisitCount", TypeName = "INT", Order = 3)]
        [Display(Name = "Industry Visit Count", Description = "Industry Visit Count", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int IndustryVisitCount { get; set; }

        [DataMember]
        [Column("IndustryName", Order = 4)]
        [Display(Name = "Industry Name", Description = "Industry Name", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryName { get; set; }

        [DataMember]
        [Column("IndustryContactPerson", Order = 5)]
        [Display(Name = "Industry Contact Person", Description = "Industry Contact Person", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryContactPerson { get; set; }

        [DataMember]
        [Column("IndustryContactNumber", Order = 6)]
        [Display(Name = "Industry Contact Number", Description = "Industry Contact Number", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryContactNumber { get; set; }

        [DataMember]
        [Column("IndustryVisitDetails", Order = 7)]
        [Display(Name = "Industry Visit Details", Description = "Industry Visit Details", Order = 7)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryVisitDetails { get; set; }

        #endregion Public Properties
    }
}