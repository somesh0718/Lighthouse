using System.Collections.Generic;

namespace Igmite.Lighthouse.Models
{
    public class EmailModel
    {
        public EmailModel()
        {
            this.Attachments = new List<string>();
        }

        public string ToEmailId { get; set; }

        public string FormEmailId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IList<string> Attachments { get; set; }
    }
}