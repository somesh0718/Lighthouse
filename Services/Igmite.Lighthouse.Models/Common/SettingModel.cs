using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SettingModel 
    {
        [DataMember]
        [Display(Name = "Role Id", Description = "Role Id")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual Guid RoleId { get; set; }

        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual Guid UserId { get; set; }

        [DataMember]
        [Display(Name = "Login Id", Description = "Login Id")]
        public virtual string LoginId { get; set; }

        [DataMember]
        [Display(Name = "Password", Description = "Password")]
        public virtual string Password { get; set; }
    }
}