using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SiteHeaders")]
    public partial class SiteHeader : BaseEntity
    {
        public SiteHeader()
        {
            //this.SubHeaders = new List<SiteSubHeader>();

            //this.DeletedSubIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SiteHeaderId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Site Header Id", Description = "Site Header Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SiteHeaderId { get; set; }

        [DataMember]
        [Column("ShortName", Order = 2)]
        [Display(Name = "Short Name", Description = "Short Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(40, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ShortName { get; set; }

        [DataMember]
        [Column("LongName", Order = 3)]
        [Display(Name = "Long Name", Description = "Long Name", Order = 3)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LongName { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("DisplayOrder", TypeName = "INT", Order = 5)]
        [Display(Name = "Display Order", Description = "Display Order", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        [Column("Remarks", Order = 6)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Sub Headers", Description = "Sub Headers")]
        //public virtual IList<SiteSubHeader> SubHeaders { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedSubIds { get; set; }

        #endregion Public Properties
    }
}
