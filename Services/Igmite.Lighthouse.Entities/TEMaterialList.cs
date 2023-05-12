using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("TEMaterialLists")]
    public partial class TEMaterialList : BaseEntityCreation
    {
        public TEMaterialList()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("TEMaterialListId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "TE MaterialList Id", Description = "TE MaterialList Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TEMaterialListId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ToolEquipmentId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Tool EquipmentId", Description = "Tool EquipmentId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ToolEquipmentId { get; set; }

        // Foreign key
        [DataMember]
        [Column("RawMaterialId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Raw Material Id", Description = "Raw Material Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid RawMaterialId { get; set; }

        [DataMember]
        [Column("RawMaterialName", Order = 4)]
        [Display(Name = "Raw Material Name", Description = "Raw Material Name", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RawMaterialName { get; set; }

        [DataMember]
        [Column("RawMaterialStatus", Order = 5)]
        [Display(Name = "RawMaterialStatus", Description = "Raw Material Status", Order = 5)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RawMaterialStatus { get; set; }

        [DataMember]
        [Column("RMLastReceivedDate", TypeName = "DATE", Order = 6)]
        [Display(Name = "RMLastReceivedDate", Description = "Date on which [Raw Material name] last received", Order = 6)]
        public virtual DateTime? RMLastReceivedDate { get; set; }

        [DataMember]
        [Column("RawMaterialAction", Order = 7)]
        [Display(Name = "RawMaterialAction", Description = "Action needed", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RawMaterialAction { get; set; }

        [DataMember]
        [Column("QuantityCount", Order = 8)]
        [Display(Name = "QuantityCount", Description = "Quantity Count", Order = 8)]
        public virtual int? QuantityCount { get; set; }


        #endregion Public Properties
    }
}