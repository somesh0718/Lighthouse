using Igmite.Lighthouse.Entities;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Display(Name = "User Email Id", Description = "User Email Id")]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100)]
        public virtual string EmailId { get; set; }
    }
}