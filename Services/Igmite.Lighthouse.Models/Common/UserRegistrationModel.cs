using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Igmite.Lighthouse.Models
{
    public class UserRegistrationModel : RequestModel
    {
        [Display(Name = "Account Type", Description = "Account Type", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(25)]
        public virtual string AccountType { get; set; }

        [Display(Name = "User Name", Description = "User Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(250)]
        public virtual string UserName { get; set; }

        [Display(Name = "User Email Id", Description = "User Email Id", Order = 3)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100)]
        public virtual string EmailId { get; set; }

        [Display(Name = "Password", Description = "Password", Order = 4)]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(500)]
        public virtual string Password { get; set; }

        [Display(Name = "Confirm Password", Description = "Confirm Password", Order = 5)]
        [RegularExpression(EntityConstants.RegxPasswordPattern, ErrorMessage = EntityConstants.PasswordErrorMessage)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password does not match")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(30)]
        public virtual string ConfirmPassword { get; set; }

        [Display(Name = "Mobile", Description = "Mobile", Order = 6)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(10)]
        public virtual string Mobile { get; set; }

        [Display(Name = "OTP Code", Description = "Enter OTP Code sent to your registered mobile number", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(6)]
        public virtual string VerifyCode { get; set; }

        [JsonIgnore]
        public SelectList CountryList { get; set; }

        [Display(Name = "Country", Description = "Country", Order = 8)]
        public string CountryId { get; set; }
    }
}