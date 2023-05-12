using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class AccountModel : Account
    {
        public AccountModel()
        {
			this.WorkLocationModels = new List<AccountWorkLocationModel>();
            this.IsLocked = false;
            this.InvalidAttempt = 0;
        }

        [DataMember]
        [Display(Name = "Role Id", Description = "Role Id")]
        public Guid RoleId { get; set; }

        [DataMember]
        [Display(Name = "Role Code", Description = "Role Code")]
        public string RoleCode { get; set; }

        [DataMember]
        public IList<AccountWorkLocationModel> WorkLocationModels { get; set; }
    }
}