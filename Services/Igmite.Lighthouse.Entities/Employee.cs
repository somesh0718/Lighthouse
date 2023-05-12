using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Employees")]
    public partial class Employee : BaseEntity
    {
        public Employee()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AccountId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Account Id", Description = "Account Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        [Column("EmployeeCode", Order = 2)]
        [Display(Name = "Employee Code", Description = "Employee Code", Order = 2)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmployeeCode { get; set; }

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
        [Column("Gender", Order = 6)]
        [Display(Name = "Gender", Description = "Gender", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Gender { get; set; }

        [DataMember]
        [Column("DateOfBirth", TypeName = "DATETIME", Order = 7)]
        [Display(Name = "Date Of Birth", Description = "Date Of Birth", Order = 7)]
        public virtual DateTime? DateOfBirth { get; set; }

        [DataMember]
        [Column("Department", Order = 8)]
        [Display(Name = "Department", Description = "Department", Order = 8)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Department { get; set; }

        [DataMember]
        [Column("Telephone", Order = 9)]
        [Display(Name = "Telephone", Description = "Telephone", Order = 9)]
        [RegularExpression(EntityConstants.RegxTelephonePattern, ErrorMessage = EntityConstants.TelephoneErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Telephone { get; set; }

        [DataMember]
        [Column("Mobile", Order = 10)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 10)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Mobile { get; set; }

        [DataMember]
        [Column("EmailId", Order = 11)]
        [Display(Name = "Email Id", Description = "Email Id", Order = 11)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(75, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string EmailId { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Accounts", Description = "Accounts")]
        //public virtual Account Account { get; set; }
        #endregion Public Properties
    }
}
