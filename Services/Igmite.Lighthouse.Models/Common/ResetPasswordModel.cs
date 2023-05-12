using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class ResetPasswordModel : RequestModel
    {
        [Display(Name = "Email Id", Description = "Email Id")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(40)]
        public virtual string EmailId { get; set; }

        [Display(Name = "Current Password", Description = "Current Password")]
        [RegularExpression(Constants.RegxPasswordPattern, ErrorMessage = Constants.PasswordErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(25)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public virtual string CurrentPassword { get; set; }

        [Display(Name = "Password", Description = "Password")]
        [RegularExpression(Constants.RegxPasswordPattern, ErrorMessage = Constants.PasswordErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(25)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "Confirm password", Description = "Confirm password")]
        [RegularExpression(Constants.RegxPasswordPattern, ErrorMessage = Constants.PasswordErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(25)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public virtual string ConfirmPassword { get; set; }

        [JsonIgnore]
        public string VerifyCode { get; set; }

        [JsonIgnore]
        public bool IsValidTokenCode { get; set; }

        [JsonIgnore]
        public string PasswordResetToken { get; set; }

        [JsonIgnore]
        public bool IsPasswordHasChanged { get; set; }

        [JsonIgnore]
        public bool IsPasswordReset { get; set; }
    }
}