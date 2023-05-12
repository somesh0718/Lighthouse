using System.Collections.Generic;

namespace Igmite.Lighthouse.Models
{
    public class SerializeAccountData
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string AccountType { get; set; }
        public string SessionId { get; set; }
        public KeyValuePair<string, string> AccountRole { get; set; }
    }
}