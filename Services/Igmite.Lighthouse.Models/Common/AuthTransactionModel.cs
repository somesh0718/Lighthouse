using System;

namespace Igmite.Lighthouse.Models
{
    public class AuthTransactionModel
    {
        public Guid TransactionId { get; set; }
        public string Code { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Rights { get; set; }
        public bool ListView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
    }
}