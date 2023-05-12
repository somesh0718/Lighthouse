using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTMonthlyTeachingPlans")]
    public partial class VTMonthlyTeachingPlan : BaseEntity
    {
        public VTMonthlyTeachingPlan()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTMonthlyTeachingPlanId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTMonthly Teaching Plan Id", Description = "VTMonthly Teaching Plan Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTMonthlyTeachingPlanId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 2)]
        public virtual Guid? VTClassId { get; set; }

        [DataMember]
        [Column("Month", Order = 3)]
        [Display(Name = "Month", Description = "Month", Order = 3)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Month { get; set; }

        [DataMember]
        [Column("WeekStartDate", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Week Start Date", Description = "Week Start Date", Order = 4)]
        public virtual DateTime? WeekStartDate { get; set; }

        [DataMember]
        [Column("WeekendDate", TypeName = "DATETIME", Order = 5)]
        [Display(Name = "Weekend Date", Description = "Weekend Date", Order = 5)]
        public virtual DateTime? WeekendDate { get; set; }

        [DataMember]
        [Column("ModulesPlanned", Order = 6)]
        [Display(Name = "Modules Planned", Description = "Modules Planned", Order = 6)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ModulesPlanned { get; set; }

        [DataMember]
        [Column("IVPlannedDate", TypeName = "DATETIME", Order = 7)]
        [Display(Name = "IVPlanned Date", Description = "IVPlanned Date", Order = 7)]
        public virtual DateTime? IVPlannedDate { get; set; }

        [DataMember]
        [Column("IVVCAttend", Order = 8)]
        [Display(Name = "IVVCAttend", Description = "IVVCAttend", Order = 8)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IVVCAttend { get; set; }

        [DataMember]
        [Column("FVPlannedDate", TypeName = "DATETIME", Order = 9)]
        [Display(Name = "FVPlanned Date", Description = "FVPlanned Date", Order = 9)]
        public virtual DateTime? FVPlannedDate { get; set; }

        [DataMember]
        [Column("FVPurpose", Order = 10)]
        [Display(Name = "FVPurpose", Description = "FVPurpose", Order = 10)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVPurpose { get; set; }

        [DataMember]
        [Column("FVLocation", Order = 11)]
        [Display(Name = "FVLocation", Description = "FVLocation", Order = 11)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FVLocation { get; set; }

        [DataMember]
        [Column("GLPlannedDate", TypeName = "DATETIME", Order = 12)]
        [Display(Name = "GLPlanned Date", Description = "GLPlanned Date", Order = 12)]
        public virtual DateTime? GLPlannedDate { get; set; }

        [DataMember]
        [Column("OtherDetails", Order = 13)]
        [Display(Name = "Other Details", Description = "Other Details", Order = 13)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OtherDetails { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTClasses", Description = "VTClasses")]
        //public virtual VTClass VTClass { get; set; }
        #endregion Public Properties
    }
}
