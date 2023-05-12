using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class UserOTPDetailModel : UserOTPDetail
    {
        public UserOTPDetailModel()
        {
            //this.AccountOTPModels = new List<AccountUserOTPModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Account User OTP", Description = "Account User OTP")]
        //public string AccountOTPName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<AccountUserOTPModel> AccountOTPModels { get; set; }
    }
}
