using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("DataTypes")]
    public partial class DataType : BaseEntity
    {
        public DataType()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DataTypeId", TypeName = "INT", Order = 1)]
        [Display(Name = "Data Type Id", Description = "Data Type Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DataTypeId { get; set; }

        [DataMember]
        [Column("Name", Order = 2)]
        [Display(Name = "Name", Description = "Name", Order = 2)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Name { get; set; }

        [DataMember]
        [Column("Description", Order = 3)]
        [Display(Name = "Description", Description = "Description", Order = 3)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        #endregion Public Properties
    }
}