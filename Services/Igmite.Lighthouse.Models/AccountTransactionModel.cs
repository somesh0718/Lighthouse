using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class AccountTransactionModel : AccountTransaction
    {
        public AccountTransactionModel()
        {
        }

        [DataMember]
        [Display(Name = "Account Id", Description = "Account Id")]
        public virtual Guid RoleId { get; set; }
    }
}