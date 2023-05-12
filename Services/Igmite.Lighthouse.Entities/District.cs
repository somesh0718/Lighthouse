using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Districts")]
    public partial class District : BaseEntity
    {
        public District()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("DistrictCode", Order = 1)]
        [Display(Name = "District Code", Description = "District Code", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictCode { get; set; }

        // Foreign key
        [DataMember]
        [Column("StateCode", Order = 2)]
        [Display(Name = "State", Description = "State", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateCode { get; set; }

        [DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Division", Description = "Division", Order = 3)]
        //[Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid? DivisionId { get; set; }

        [DataMember]
        [Column("DistrictName", Order = 4)]
        [Display(Name = "District Name", Description = "District Name", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictName { get; set; }

        [DataMember]
        [Column("Description", Order = 5)]
        [Display(Name = "Description", Description = "Description", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        #endregion Public Properties
    }
}