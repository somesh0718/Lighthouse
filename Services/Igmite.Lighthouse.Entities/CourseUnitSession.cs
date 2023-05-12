using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("CourseUnitSessions")]
    public partial class CourseUnitSession : BaseEntityCreation
    {
        public CourseUnitSession()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("CourseUnitSessionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Course Unit Session", Description = "Course Unit Session", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid CourseUnitSessionId { get; set; }

        // Foreign key
        [DataMember]
        [Column("CourseModuleId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Course Module", Description = "Course Module", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid CourseModuleId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SessionName", Order = 3)]
        [Display(Name = "Session", Description = "Session", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SessionName { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 4)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("Remarks", Order = 5)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}