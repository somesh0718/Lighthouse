using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VCTrainersMap")]
    public partial class VCTrainerMap : BaseEntity
    {
        public VCTrainerMap()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCTrainerId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCTrainerId", Description = "VCTrainerId")]
        public virtual Guid VCTrainerId { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "AcademicYearId", Description = "AcademicYearId")]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VCId", Description = "VCId")]
        public virtual Guid VCId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTId", Description = "VTId")]
        public virtual Guid VTId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "VTPId", Description = "VTPId")]
        public virtual Guid VTPId { get; set; }

        [DataMember]
        [Column("DateOfJoining", TypeName = "DATETIME")]
        [Display(Name = "Date Of Joining", Description = "Date Of Joining")]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        [Column("DateOfResignation", TypeName = "DATETIME")]
        [Display(Name = "Date Of Resignation", Description = "Date Of Resignation")]
        public virtual DateTime? DateOfResignation { get; set; }

        [DataMember]
        [Column("NatureOfAppointment")]
        [Display(Name = "Nature Of Appointment", Description = "Nature Of Appointment")]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string NatureOfAppointment { get; set; }

        #endregion Public Properties
    }
}