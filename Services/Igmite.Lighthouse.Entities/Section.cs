using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Sections")]
    public partial class Section : BaseEntity
    {
        public Section()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SectionId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Section Id", Description = "Section Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectionId { get; set; }

        [DataMember]
        [Column("Name", Order = 2)]
        [Display(Name = "Name", Description = "Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("Description", Order = 3)]
        [Display(Name = "Description", Description = "Description", Order = 3)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("Remarks", Order = 4)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }
        #endregion Public Properties
    }
}
