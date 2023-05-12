using System.Collections.Generic;
using System.Security.Principal;

namespace Igmite.Lighthouse.Models
{
    public class AccountPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public AccountPrincipal()
        {
            this.Transaction = new AuthTransactionModel();
        }

        public AccountPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
            this.Transaction = new AuthTransactionModel();
        }

        public bool IsInRole(string role)
        {
            //(this.AccountRole.Any(r => role.Contains(r.Key)))
            return string.Equals(this.AccountRole, role);
        }

        public string UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }

        public KeyValuePair<string, string> AccountRole { get; set; }

        public string AccountType { get; set; }

        public AuthTransactionModel Transaction { get; set; }
    }
}