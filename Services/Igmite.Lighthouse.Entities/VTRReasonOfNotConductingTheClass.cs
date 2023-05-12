using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRReasonOfNotConductingTheClasses")]
    public partial class VTRReasonOfNotConductingTheClass : BaseEntityCreation
    {
        public VTRReasonOfNotConductingTheClass()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRReasonOfNotConductingTheClassId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTR ReasonOfNotConductingTheClass", Description = "VTR ReasonOfNotConductingTheClass", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRReasonOfNotConductingTheClassId { get; set; }

        [DataMember]
        [Column("VTRTeachingVocationalEducationId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTRTeaching Vocational Education Id", Description = "VTRTeaching Vocational Education Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTeachingVocationalEducationId { get; set; }

        [DataMember]
        [Column("ReasonTypeId", Order = 3)]
        [Display(Name = "Reason Type", Description = "Reason Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ReasonTypeId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}