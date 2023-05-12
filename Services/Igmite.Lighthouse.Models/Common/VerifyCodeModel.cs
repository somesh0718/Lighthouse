using Igmite.Lighthouse.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Igmite.Lighthouse.Models
{
    public class VerifyCodeModel
    {
        [Display(Name = "Remember Me?", Description = "Remember Me?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool RememberMe { get; set; }

        [Display(Name = "Remember Browser?", Description = "Remember Browser?")]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual bool RememberBrowser { get; set; }

        [Display(Name = "Code", Description = "Code")]
        public virtual string Code { get; set; }

        public SelectList Providers { get; set; }
    }
}