using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("ToolEquipmentsRoomDamaged")]
    public partial class ToolEquipmentRoomDamaged : BaseEntityCreation
    {
        public ToolEquipmentRoomDamaged()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("ToolEquipmentRDId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Tool EquipmentRDId", Description = "Tool EquipmentRDId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ToolEquipmentRDId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ToolEquipmentId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Tool EquipmentId", Description = "Tool EquipmentId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ToolEquipmentId { get; set; }

        [DataMember]
        [Column("RoomDamaged", Order = 3)]
        [Display(Name = "RoomDamaged", Description = "Room Damaged", Order = 3)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RoomDamaged { get; set; }


        #endregion Public Properties
    }
}