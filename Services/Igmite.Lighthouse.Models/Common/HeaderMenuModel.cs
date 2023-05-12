using System;

namespace Igmite.Lighthouse.Models
{
    public class HeaderMenuModel
    {
        public string HeaderName { get; set; }
        public byte HeaderOrder { get; set; }
        public bool IsHeaderMenu { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionName { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string UrlAction { get; set; }
        public string UrlController { get; set; }
        public byte MenuOrder { get; set; }
        public bool Rights { get; set; }
        public bool ListView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
    }
}