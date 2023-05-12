using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Blocks")]
    public partial class Block : BaseEntity
    {
        public Block()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("BlockId", Order = 1)]
        [Display(Name = "Block Id", Description = "Block Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid BlockId { get; set; }

        [DataMember]
        [Column("DistrictId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "District", Description = "District", Order = 2)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictId { get; set; }

        [DataMember]
        [Column("BlockName", Order = 3)]
        [Display(Name = "Block Name", Description = "Block Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string BlockName { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember, NotMapped]
        public Guid? DivisionId { get; set; }

        #endregion Public Properties
    }
}