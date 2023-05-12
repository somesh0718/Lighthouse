using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("CourseModules")]
    public partial class CourseModule : BaseEntity
    {
        public CourseModule()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("CourseModuleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Course Module Id", Description = "Course Module Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid CourseModuleId { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Class", Description = "Class", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClassId { get; set; }

        [DataMember]
        [Column("ModuleTypeId", Order = 3)]
        [Display(Name = "Course Module", Description = "Course Module", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ModuleTypeId { get; set; }

        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Sector", Description = "Sector", Order = 4)]
        public virtual Guid? SectorId { get; set; }

        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "Job Role", Description = "Job Role", Order = 5)]
        public virtual Guid? JobRoleId { get; set; }

        [DataMember]
        [Column("UnitName", Order = 6)]
        [Display(Name = "Units", Description = "Units", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string UnitName { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 7)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("Remarks", Order = 8)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 8)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        [DataMember, NotMapped]
        public List<CourseUnitSession> CourseUnitSessions { get; set; }

        #endregion Public Properties
    }
}