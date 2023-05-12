using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTPMonthlyBillSubmissionStatusViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTPMonthlyBillSubmissionStatusId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string Month { get; set; }

        [DataMember]
        public virtual DateTime? DateSubmission { get; set; }

        [DataMember]
        public virtual string Incorrect { get; set; }

        [DataMember]
        public virtual string IncorrectDetails { get; set; }

        [DataMember]
        public virtual string Final { get; set; }

        [DataMember]
        public virtual string ApprovedPMU { get; set; }

        [DataMember]
        public virtual int Amount { get; set; }

        [DataMember]
        public virtual string DiaryentryDone { get; set; }

        [DataMember]
        public virtual string DiaryentryNumber { get; set; }

        [DataMember]
        public virtual string Details { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
