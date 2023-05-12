using Igmite.Lighthouse.Entities;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class BlogPasswordModel
    {
        public EmailModel EmailModel { get; set; }

        [Display(Name = "Password", Description = "Password")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(25)]
        public virtual string Password { get; set; }
    }
}