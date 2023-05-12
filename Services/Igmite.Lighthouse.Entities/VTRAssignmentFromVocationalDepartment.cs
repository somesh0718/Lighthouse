using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRAssignmentFromVocationalDepartments")]
    public partial class VTRAssignmentFromVocationalDepartment : BaseEntityCreation
    {
        public VTRAssignmentFromVocationalDepartment()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRAssignmentFromVocationalDepartmentId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRAssignment From Vocational Department Id", Description = "VTRAssignment From Vocational Department Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRAssignmentFromVocationalDepartmentId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("AssigmentNumber", Order = 3)]
        [Display(Name = "Assigment Number", Description = "Assigment Number", Order = 3)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssigmentNumber { get; set; }

        [DataMember]
        [Column("AssignmentDetails", Order = 4)]
        [Display(Name = "Assignment Details", Description = "Assignment Details", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssignmentDetails { get; set; }

        [DataMember]
        [Column("AssignmentPhoto", Order = 5)]
        [Display(Name = "Assignment Photo", Description = "Assignment Photo", Order = 5)]
        public virtual string AssignmentPhoto { get; set; }

        #endregion Public Properties
    }
}