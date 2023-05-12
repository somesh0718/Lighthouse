using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTSchoolSectorViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTSchoolSectorId { get; set; }

        // Foreign key
        [Key, DataMember]
        public virtual string AcademicYear { get; set; }

        // Foreign key
        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual DateTime DateOfAllocation { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsAYRollover { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}