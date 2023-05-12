using Igmite.Lighthouse.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Igmite.Lighthouse.Models
{
    public class SendCodeModel
    {
        [Display(Name = "Remember Me?", Description = "Remember Me?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool RememberMe { get; set; }

        [Display(Name = "Return Url", Description = "Return Url")]
        public virtual string ReturnUrl { get; set; }

        public string SelectedProvider { get; set; }

        public SelectList Providers { get; set; }
    }
}