using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCRSchoolVisits")]
    public partial class VCRSchoolVisit : BaseEntityCreation
    {
        public VCRSchoolVisit()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCRSchoolVisitId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VCRSchoolVisit", Description = "VCRSchoolVisit", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCRSchoolVisitId { get; set; }

        [DataMember]
        [Column("VCDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VCDaily Reporting Id", Description = "VCDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCDailyReportingId { get; set; }

        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "School", Description = "School", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        [Column("WorkDetails", Order = 4)]
        [Display(Name = "Work Details", Description = "Work Details", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string WorkDetails { get; set; }

        #endregion Public Properties
    }
}