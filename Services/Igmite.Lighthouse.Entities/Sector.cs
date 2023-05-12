using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Sectors")]
    public partial class Sector : BaseEntity
    {
        public Sector()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        [Column("SectorName", Order = 2)]
        [Display(Name = "Sector Name", Description = "Sector Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SectorName { get; set; }

        [DataMember]
        [Column("Description", Order = 3)]
        [Display(Name = "Description", Description = "Description", Order = 3)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 4)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        #endregion Public Properties
    }
}