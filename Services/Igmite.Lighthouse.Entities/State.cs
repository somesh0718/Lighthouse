using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("States")]
    public partial class State : BaseEntity
    {
        public State()
        {
            //this.Districts = new List<District>();
            //this.Schools = new List<School>();

            //this.DeletedDistrictCodes = new List<string>();
            //this.DeletedSchoolIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("StateCode", Order = 1)]
        [Display(Name = "State Code", Description = "State Code", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateCode { get; set; }

        [DataMember]
        [Column("StateId", Order = 2)]
        [Display(Name = "State Id", Description = "State Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(2, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateId { get; set; }

        // Foreign key
        [DataMember]
        [Column("CountryCode", Order = 3)]
        [Display(Name = "Country Code", Description = "Country Code", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CountryCode { get; set; }

        [DataMember]
        [Column("StateName", Order = 4)]
        [Display(Name = "State Name", Description = "State Name", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(75, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateName { get; set; }

        [DataMember]
        [Column("Description", Order = 5)]
        [Display(Name = "Description", Description = "Description", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember]
        [Column("SequenceNo", TypeName = "INT", Order = 6)]
        [Display(Name = "Sequence No", Description = "Sequence No", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int SequenceNo { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Districts", Description = "Districts")]
        //public virtual IList<District> Districts { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Schools", Description = "Schools")]
        //public virtual IList<School> Schools { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Countries", Description = "Countries")]
        //public virtual Country Country { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<string> DeletedDistrictCodes { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedSchoolIds { get; set; }

        #endregion Public Properties
    }
}
