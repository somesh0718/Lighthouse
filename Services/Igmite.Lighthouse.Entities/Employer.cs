using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Employers")]
    public partial class Employer : BaseEntity
    {
        public Employer()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("EmployerId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Employer Id", Description = "Employer Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid EmployerId { get; set; }

        [DataMember]
        [Column("StateCode", Order = 2)]
        [Display(Name = "State Code", Description = "State Code", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(36, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateCode { get; set; }

        [DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Division Id", Description = "Division Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DivisionId { get; set; }

        [DataMember]
        [Column("DistrictCode", Order = 4)]
        [Display(Name = "District Code", Description = "District Code", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictCode { get; set; }

        [DataMember]
        [Column("BlockName", Order = 5)]
        [Display(Name = "Block Name", Description = "Block Name", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string BlockName { get; set; }

        [DataMember]
        [Column("Address", Order = 6)]
        [Display(Name = "Address", Description = "Address", Order = 6)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Address { get; set; }

        [DataMember]
        [Column("City", Order = 7)]
        [Display(Name = "City", Description = "City", Order = 7)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string City { get; set; }

        [DataMember]
        [Column("Pincode", Order = 8)]
        [Display(Name = "Pincode", Description = "Pincode", Order = 8)]
        [MaxLength(6, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Pincode { get; set; }

        [DataMember]
        [Column("BusinessType", Order = 9)]
        [Display(Name = "Business Type", Description = "Business Type", Order = 9)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string BusinessType { get; set; }

        [DataMember]
        [Column("EmployeeCount", TypeName = "INT", Order = 10)]
        [Display(Name = "Employee Count", Description = "Employee Count", Order = 10)]
        public virtual int? EmployeeCount { get; set; }

        [DataMember]
        [Column("Outlets", Order = 11)]
        [Display(Name = "Outlets", Description = "Outlets", Order = 11)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Outlets { get; set; }

        [DataMember]
        [Column("Contact1", Order = 12)]
        [Display(Name = "Contact1", Description = "Contact1", Order = 12)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Contact1 { get; set; }

        [DataMember]
        [Column("Mobile1", Order = 13)]
        [Display(Name = "Mobile1", Description = "Mobile1", Order = 13)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile1 { get; set; }

        [DataMember]
        [Column("Designation1", Order = 14)]
        [Display(Name = "Designation1", Description = "Designation1", Order = 14)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Designation1 { get; set; }

        [DataMember]
        [Column("EmailId1", Order = 15)]
        [Display(Name = "Email Id1", Description = "Email Id1", Order = 15)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId1 { get; set; }

        [DataMember]
        [Column("Contact2", Order = 16)]
        [Display(Name = "Contact2", Description = "Contact2", Order = 16)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Contact2 { get; set; }

        [DataMember]
        [Column("Mobile2", Order = 17)]
        [Display(Name = "Mobile2", Description = "Mobile2", Order = 17)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile2 { get; set; }

        [DataMember]
        [Column("Designation2", Order = 18)]
        [Display(Name = "Designation2", Description = "Designation2", Order = 18)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Designation2 { get; set; }

        [DataMember]
        [Column("EmailId2", Order = 19)]
        [Display(Name = "Email Id2", Description = "Email Id2", Order = 19)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId2 { get; set; }

        #endregion Public Properties
    }
}