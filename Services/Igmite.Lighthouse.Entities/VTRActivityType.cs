using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRActivityTypes")]
    public partial class VTRActivityType : BaseEntityCreation
    {
        public VTRActivityType()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRActivityTypeId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRActivity Type Id", Description = "VTRActivity Type Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRActivityTypeId { get; set; }

        [DataMember]
        [Column("VTRTeachingVocationalEducationId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTRTeaching Vocational Education Id", Description = "VTRTeaching Vocational Education Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTeachingVocationalEducationId { get; set; }

        [DataMember]
        [Column("ActivityTypeId", Order = 3)]
        [Display(Name = "Activity Type Id", Description = "Activity Type Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ActivityTypeId { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}