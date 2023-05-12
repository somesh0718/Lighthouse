using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VocationalTrainingProviderViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTPId { get; set; }

        [DataMember]
        public virtual string VTPShortName { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string ApprovalYear { get; set; }

        [DataMember]
        public virtual string CertificationNo { get; set; }

        [DataMember]
        public virtual string CertificationAgency { get; set; }

        [DataMember]
        public virtual bool IsResigned { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}