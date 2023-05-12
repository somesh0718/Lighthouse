using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class CourseMaterialViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid CourseMaterialId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual DateTime? ReceiptDate { get; set; }

        [DataMember]
        public virtual string Details { get; set; }

        [DataMember]
        public virtual string CMStatus { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}