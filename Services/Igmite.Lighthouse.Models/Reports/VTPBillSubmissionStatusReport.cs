using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTPBillSubmissionStatusReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string MonthYear { get; set; }

        [DataMember]
        public virtual string Month { get; set; }

        [DataMember]
        public virtual DateTime DateOfSubmissionOfInvoice { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

        [DataMember]
        public virtual string NameOfVTP { get; set; }

        [DataMember]
        public virtual string CategoryName { get; set; }

        [DataMember]
        public virtual string BillsForTheMonthYear { get; set; }

        [DataMember]
        public virtual decimal InvoiceAmountInRs { get; set; }

        [DataMember]
        public virtual string DocumentsSubmittedRelatedToInvoice { get; set; }

        [DataMember]
        public virtual string NameOfTheVCwhoSubmittedTheInvoice { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }
    }
}