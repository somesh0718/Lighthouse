using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("TEToolLists")]
    public partial class TEToolList : BaseEntityCreation
    {
        public TEToolList()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("TEToolListId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "TE ToolList Id", Description = "TE ToolList Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TEToolListId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ToolEquipmentId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Tool EquipmentId", Description = "Tool EquipmentId", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ToolEquipmentId { get; set; }
        
        // Foreign key
        [DataMember]
        [Column("ToolListId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Tool List Id", Description = "Tool List Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ToolListId { get; set; }

        [DataMember]
        [Column("ToolListName", Order = 4)]
        [Display(Name = "Tool  List Name", Description = "Tool List Name", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ToolListName { get; set; }

        [DataMember]
        [Column("ToolListStatus", Order = 5)]
        [Display(Name = "ToolListStatus", Description = "Tool List Status", Order = 5)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ToolListStatus { get; set; }

        [DataMember]
        [Column("TLActionNeeded1", Order = 6)]
        [Display(Name = "TLActionNeeded1", Description = "Action Needed1", Order = 6)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TLActionNeeded1 { get; set; }

        [DataMember]
        [Column("TLActionNeeded2", Order = 7)]
        [Display(Name = "TLActionNeeded2", Description = "Action Needed2", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TLActionNeeded2 { get; set; }


        #endregion Public Properties
    }
}