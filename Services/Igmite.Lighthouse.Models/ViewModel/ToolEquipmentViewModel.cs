using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ToolEquipmentViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid ToolEquipmentId { get; set; }

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
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual DateTime? ReceiptDate { get; set; }

        [DataMember]
        public virtual string TEReceiveStatus { get; set; }

        [DataMember]
        public virtual string TEStatus { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}