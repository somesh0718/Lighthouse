using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VCSchoolSectorViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VCSchoolSectorId { get; set; }

        [Key, DataMember]
        public virtual string AcademicYear { get; set; }

        [Key, DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SchoolVTPSector { get; set; }

        [DataMember]
        public virtual DateTime DateOfAllocation { get; set; }

        [DataMember]
        public virtual DateTime? DateOfRemoval { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsAYRollover { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}