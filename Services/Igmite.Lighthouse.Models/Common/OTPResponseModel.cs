using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class OTPRequestModel
    {
        [RegularExpression(Constants.RegxMobilePattern, ErrorMessage = Constants.MobileErrorMessage)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10)]
        public virtual string Mobile { get; set; }

        [JsonIgnore]
        [Display(Name = "OTP Token", Description = "OTP Token", Order = 3)]
        [MaxLength(6)]
        public virtual string OTPToken { get; set; }
    }

    public class OTPResponseModel
    {
        [RegularExpression(Constants.RegxMobilePattern, ErrorMessage = Constants.MobileErrorMessage)]
        [Display(Name = "Mobile", Description = "Mobile", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10)]
        public virtual string Mobile { get; set; }

        [Display(Name = "OTPToken", Description = "OTPToken", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(6)]
        public virtual string OTPToken { get; set; }

        [Display(Name = "Expire On", Description = "Expire On", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ExpireOn { get; set; }
    }
}