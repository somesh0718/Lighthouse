using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTClassStudents")]
    public partial class VTClassStudent : BaseEntity
    {
        public VTClassStudent()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTClassStudentId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTClassStudent", Description = "VTClassStudent", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassStudentId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VT", Description = "VT", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Student", Description = "Student", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        #endregion Public Properties
    }
}