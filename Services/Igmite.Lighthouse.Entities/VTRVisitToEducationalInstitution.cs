using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRVisitToEducationalInstitutions")]
    public partial class VTRVisitToEducationalInstitution : BaseEntityCreation
    {
        public VTRVisitToEducationalInstitution()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRVisitToEducationalInstitutionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRVisit To Educational Institution Id", Description = "VTRVisit To Educational Institution Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRVisitToEducationalInstitutionId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("EducationalInstituteVisitCount", TypeName = "INT", Order = 3)]
        [Display(Name = "Educational Institute Visit Count", Description = "Educational Institute Visit Count", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int EducationalInstituteVisitCount { get; set; }

        [DataMember]
        [Column("EducationalInstitute", Order = 4)]
        [Display(Name = "Educational Institute", Description = "Educational Institute", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EducationalInstitute { get; set; }

        [DataMember]
        [Column("InstituteContactPerson", Order = 5)]
        [Display(Name = "Institute Contact Person", Description = "Institute Contact Person", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InstituteContactPerson { get; set; }

        [DataMember]
        [Column("InstituteContactNumber", Order = 6)]
        [Display(Name = "Institute Contact Number", Description = "Institute Contact Number", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InstituteContactNumber { get; set; }

        [DataMember]
        [Column("InstituteVisitDetails", Order = 7)]
        [Display(Name = "Institute Visit Details", Description = "Institute Visit Details", Order = 7)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string InstituteVisitDetails { get; set; }

        #endregion Public Properties
    }
}