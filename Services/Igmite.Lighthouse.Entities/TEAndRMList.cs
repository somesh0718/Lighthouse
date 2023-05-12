using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("TEAndRMList")]
    public partial class TEAndRMList : BaseEntity
    {
        public TEAndRMList()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("TEAndRMId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Tool Equipment Id", Description = "Tool Equipment Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid TEAndRMId { get; set; }

        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 3)]
        public virtual Guid? JobRoleId { get; set; }

        [DataMember]
        [Column("ClassId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "ClassId", Description = "Class Id", Order = 4)]
        public virtual Guid? ClassId { get; set; }

        [DataMember]
        [Column("TEType", Order = 5)]
        [Display(Name = "TE Type", Description = "TE Type", Order = 5)]
        public virtual string TEType { get; set; }

        [DataMember]
        [Column("SrNo", Order = 6)]
        [Display(Name = "Sr No", Description = "Sr No", Order = 6)]
        public virtual int SrNo { get; set; }

        [DataMember]
        [Column("ToolEquipmentName",  Order = 7)]
        [Display(Name = "ToolEquipmentName", Description = "Tool Equipment Name", Order = 7)]
        public virtual string ToolEquipmentName { get; set; }

        [DataMember]
        [Column("Specification",  Order = 8)]
        [Display(Name = "Specification", Description = "Specification", Order = 8)]
        public virtual string Specification { get; set; }

        [DataMember]
        [Column("UnitType", Order = 9)]
        [Display(Name = "UnitType", Description = "UnitType", Order = 9)]
        public virtual string UnitType { get; set; }

        [DataMember]
        [Column("UnitName", Order = 10)]
        [Display(Name = "UnitName", Description = "UnitName", Order = 10)]
        public virtual string UnitName { get; set; }

        #endregion Public Properties
    }
}