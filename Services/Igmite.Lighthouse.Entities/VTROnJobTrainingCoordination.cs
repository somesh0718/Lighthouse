using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTROnJobTrainingCoordinations")]
    public partial class VTROnJobTrainingCoordination : BaseEntityCreation
    {
        public VTROnJobTrainingCoordination()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTROnJobTrainingCoordinationId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTROn Job Training Coordination Id", Description = "VTROn Job Training Coordination Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTROnJobTrainingCoordinationId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("OnJobTrainingActivityId", Order = 3)]
        [Display(Name = "On Job Training Activity Id", Description = "On Job Training Activity Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OnJobTrainingActivityId { get; set; }

        [DataMember]
        [Column("IndustryName", Order = 4)]
        [Display(Name = "Industry Name", Description = "Industry Name", Order = 4)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryName { get; set; }

        [DataMember]
        [Column("IndustryContactPerson", Order = 5)]
        [Display(Name = "Industry Contact Person", Description = "Industry Contact Person", Order = 5)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryContactPerson { get; set; }

        [DataMember]
        [Column("IndustryContactNumber", Order = 6)]
        [Display(Name = "Industry Contact Number", Description = "Industry Contact Number", Order = 6)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IndustryContactNumber { get; set; }

        [DataMember]
        [Column("OJTActivityDetails", Order = 7)]
        [Display(Name = "OJTActivity Details", Description = "OJTActivity Details", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OJTActivityDetails { get; set; }

        #endregion Public Properties
    }
}