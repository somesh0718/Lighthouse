using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class ForgotPasswordModel : RequestModel
    {
        [Display(Name = "Email", Description = "Email")]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(150)]
        public virtual string EmailId { get; set; }

        [Display(Name = "Mobile", Description = "Mobile")]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(10)]
        public virtual string Mobile { get; set; }

        [Display(Name = "Email/Mobile", Description = "Email/Mobile")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150)]
        public virtual string EmailOrMobile { get; set; }

        [JsonIgnore]
        public bool IsPasswordTokenGenerated { get; set; }
    }
}