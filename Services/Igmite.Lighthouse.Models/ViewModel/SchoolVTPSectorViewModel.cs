using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SchoolVTPSectorViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SchoolVTPSectorId { get; set; }

        [Key, DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsAYRollover { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}