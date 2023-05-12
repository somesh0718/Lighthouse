using System.Collections.Generic;

namespace Igmite.Lighthouse.Models
{
    public class AccountSession
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public KeyValuePair<string, string> AccountRole { get; set; }
        public string AccountType { get; set; }
        public string SessionId { get; set; }
    }
}