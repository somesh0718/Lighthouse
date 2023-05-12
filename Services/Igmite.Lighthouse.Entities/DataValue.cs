using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("DataValues")]
    public partial class DataValue : BaseEntity
    {
        public DataValue()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DataValueId", Order = 1)]
        [Display(Name = "Data Value Id", Description = "Data Value Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DataValueId { get; set; }

        [DataMember]
        [Column("DataTypeId", Order = 2)]
        [Display(Name = "Data Type Id", Description = "Data Type Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DataTypeId { get; set; }

        [DataMember]
        [Column("ParentId", Order = 3)]
        [Display(Name = "Parent Id", Description = "Parent Id", Order = 3)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ParentId { get; set; }

        [DataMember]
        [Column("Code", Order = 4)]
        [Display(Name = "Code", Description = "Code", Order = 4)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Code { get; set; }

        [DataMember]
        [Column("Name", Order = 5)]
        [Display(Name = "Name", Description = "Name", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("Description", Order = 6)]
        [Display(Name = "Description", Description = "Description", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 7)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        #endregion Public Properties
    }
}