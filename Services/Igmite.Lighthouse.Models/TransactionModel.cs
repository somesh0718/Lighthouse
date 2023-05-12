using Igmite.Lighthouse.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class TransactionModel : Transaction
    {
        public TransactionModel()
        {
            //this.AccountTransactionModels = new List<AccountTransactionModel>();
            //this.RoleTransactionModels = new List<RoleTransactionModel>();
            //this.SiteSubHeaderModels = new List<SiteSubHeaderModel>();
        }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Account", Description = "Account")]
        //public string AccountTransactionName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<AccountTransactionModel> AccountTransactionModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Role", Description = "Role")]
        //public string RoleTransactionName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<RoleTransactionModel> RoleTransactionModels { get; set; }

        //[JsonIgnore, DataMember]
        //[Display(Name = "Site Sub Header", Description = "Site Sub Header")]
        //public string SiteSubHeaderName { get; set; }

        //[JsonIgnore, DataMember]
        //public IList<SiteSubHeaderModel> SiteSubHeaderModels { get; set; }
    }
}
