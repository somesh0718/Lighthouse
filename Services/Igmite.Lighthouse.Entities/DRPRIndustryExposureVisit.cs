﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("DRPRIndustryExposureVisits")]
    public partial class DRPRIndustryExposureVisit : BaseEntityCreation
    {
        public DRPRIndustryExposureVisit()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DRPRIndustryExposureVisitId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "DRPR Industry Exposure Visit", Description = "DRPR Industry Exposure Visit", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DRPRIndustryExposureVisitId { get; set; }

        [DataMember]
        [Column("DRPDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "DRP Daily Reporting", Description = "DRP Daily Reporting", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DRPDailyReportingId { get; set; }

        [DataMember]
        [Column("TypeOfIndustryLinkage", Order = 3)]
        [Display(Name = "Type Of Industry Linkage", Description = "Educational Institute Visit Count", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TypeOfIndustryLinkage { get; set; }

        [DataMember]
        [Column("ContactPersonName", Order = 4)]
        [Display(Name = "Contact Person Name", Description = "Contact Person Name", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ContactPersonName { get; set; }

        [DataMember]
        [Column("ContactPersonMobile", Order = 5)]
        [Display(Name = "Contact Person Mobile", Description = "Contact Person Mobile", Order = 5)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ContactPersonMobile { get; set; }

        [DataMember]
        [Column("ContactPersonEmail", Order = 7)]
        [Display(Name = "Contact Person Email", Description = "Contact Person Email", Order = 7)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ContactPersonEmail { get; set; }

        #endregion Public Properties
    }
}