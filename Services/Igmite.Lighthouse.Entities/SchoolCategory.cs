using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SchoolCategories")]
    public partial class SchoolCategory : BaseEntity
    {
        public SchoolCategory()
        {
            //this.Schools = new List<School>();

            //this.DeletedIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SchoolCategoryId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "School Category Id", Description = "School Category Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolCategoryId { get; set; }

        [DataMember]
        [Column("CategoryName", Order = 2)]
        [Display(Name = "Category Name", Description = "Category Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CategoryName { get; set; }

        [DataMember]
        [Column("Description", Order = 3)]
        [Display(Name = "Description", Description = "Description", Order = 3)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Schools", Description = "Schools")]
        //public virtual IList<School> Schools { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedIds { get; set; }

        #endregion Public Properties
    }
}
