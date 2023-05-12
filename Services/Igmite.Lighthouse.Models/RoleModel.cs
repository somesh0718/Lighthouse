using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class RoleModel : Role
    {
        public RoleModel()
        {
            //this.AccountRoleModels = new List<AccountRoleModel>();
            //this.TransactionModels = new List<RoleTransactionModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Account", Description = "Account")]
        //public string AccountRoleName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<AccountRoleModel> AccountRoleModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Transaction", Description = "Transaction")]
        //public string TransactionName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<RoleTransactionModel> TransactionModels { get; set; }
    }
}
