using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VocationalCoordinators")]
    public partial class VocationalCoordinator : BaseEntity
    {
        public VocationalCoordinator()
        {
            this.VTPCoordinator = new VTPCoordinatorsMap();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VCId", Description = "VCId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VCId { get; set; }
        
        [DataMember]
        [Column("FirstName", Order = 3)]
        [Display(Name = "First Name", Description = "First Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FirstName { get; set; }

        [DataMember]
        [Column("MiddleName", Order = 4)]
        [Display(Name = "Middle Name", Description = "Middle Name", Order = 4)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MiddleName { get; set; }

        [DataMember]
        [Column("LastName", Order = 5)]
        [Display(Name = "Last Name", Description = "Last Name", Order = 5)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string LastName { get; set; }

        [DataMember]
        [Column("FullName", Order = 6)]
        [Display(Name = "Full Name", Description = "Full Name", Order = 6)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string FullName { get; set; }

        [DataMember]
        [Column("Mobile", Order = 7)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 7)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("Mobile1", Order = 8)]
        [Display(Name = "Mobile1", Description = "Mobile1", Order = 8)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile1 { get; set; }

        [DataMember]
        [Column("EmailId", Order = 9)]
        [Display(Name = "Email Id", Description = "Email Id", Order = 9)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId { get; set; }
       
        [DataMember]
        [Column("Gender", Order = 11)]
        [Display(Name = "Gender", Description = "Gender", Order = 11)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Gender { get; set; }
       
        #endregion Public Properties

        [NotMapped, JsonIgnore]
        public virtual VTPCoordinatorsMap VTPCoordinator { get; set; }
    }
}