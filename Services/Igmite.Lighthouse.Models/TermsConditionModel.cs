using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class TermsConditionModel : TermsCondition
    {
        public TermsConditionModel()
        {
            //this.AccountUserTermModels = new List<AccountUserTermModel>();
            //this.UserAcceptanceModels = new List<UserAcceptanceModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Account User Term", Description = "Account User Term")]
        //public string AccountUserTermName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<AccountUserTermModel> AccountUserTermModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "User Acceptance", Description = "User Acceptance")]
        //public string UserAcceptanceName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<UserAcceptanceModel> UserAcceptanceModels { get; set; }
    }
}
