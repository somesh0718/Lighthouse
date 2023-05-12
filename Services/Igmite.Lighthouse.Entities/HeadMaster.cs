using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("HeadMasters")]
    public partial class HeadMaster : BaseEntity
    {
        public HeadMaster()
        {
            this.HMSchool = new HMSchoolsMap();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("HMId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "HMId", Description = "HMId")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid HMId { get; set; }
       
        [DataMember]
        [Column("FirstName")]
        [Display(Name = "First Name", Description = "First Name")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FirstName { get; set; }

        [DataMember]
        [Column("MiddleName")]
        [Display(Name = "Middle Name", Description = "Middle Name")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MiddleName { get; set; }

        [DataMember]
        [Column("LastName")]
        [Display(Name = "Last Name", Description = "Last Name")]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LastName { get; set; }

        [DataMember]
        [Column("FullName")]
        [Display(Name = "Full Name", Description = "Full Name")]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FullName { get; set; }

        [DataMember]
        [Column("Mobile")]
        [Display(Name = "Mobile", Description = "Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("Mobile1")]
        [Display(Name = "Mobile1", Description = "Mobile1")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile1 { get; set; }

        [DataMember]
        [Column("Email")]
        [Display(Name = "Email", Description = "Email")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Email { get; set; }

        [DataMember]
        [Column("Gender")]
        [Display(Name = "Gender", Description = "Gender")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Gender { get; set; }

        [DataMember]
        [Column("YearsInSchool", TypeName = "INT")]
        [Display(Name = "Years In School", Description = "Years In School")]
        public virtual int? YearsInSchool { get; set; }

        #endregion Public Properties

        [NotMapped, JsonIgnore]
        public virtual HMSchoolsMap HMSchool { get; set; }
    }
}