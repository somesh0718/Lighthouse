using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Countries")]
    public partial class Country : BaseEntity
    {
        public Country()
        {
            //this.States = new List<State>();

            //this.DeletedStateCodes = new List<string>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("CountryCode", Order = 1)]
        [Display(Name = "Country Code", Description = "Country Code", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CountryCode { get; set; }

        [DataMember]
        [Column("CountryName", Order = 2)]
        [Display(Name = "Country Name", Description = "Country Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(75, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CountryName { get; set; }

        [DataMember]
        [Column("ISDCode", Order = 3)]
        [Display(Name = "ISDCode", Description = "ISDCode", Order = 3)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ISDCode { get; set; }

        [DataMember]
        [Column("ISOCode", Order = 4)]
        [Display(Name = "ISOCode", Description = "ISOCode", Order = 4)]
        [MaxLength(5, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ISOCode { get; set; }

        [DataMember]
        [Column("CurrencyName", Order = 5)]
        [Display(Name = "Currency Name", Description = "Currency Name", Order = 5)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CurrencyName { get; set; }

        [DataMember]
        [Column("CurrencyCode", Order = 6)]
        [Display(Name = "Currency Code", Description = "Currency Code", Order = 6)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CurrencyCode { get; set; }

        [DataMember]
        [Column("CountryIcon", Order = 7)]
        [Display(Name = "Country Icon", Description = "Country Icon", Order = 7)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CountryIcon { get; set; }

        [DataMember]
        [Column("Description", Order = 8)]
        [Display(Name = "Description", Description = "Description", Order = 8)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "States", Description = "States")]
        //public virtual IList<State> States { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<string> DeletedStateCodes { get; set; }

        #endregion Public Properties
    }
}
