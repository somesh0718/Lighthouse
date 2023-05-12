using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTClassViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTClassId { get; set; }

        // Foreign key
        [Key, DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string VTName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string VTEmailId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SchoolName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string UDISE { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string SectionName { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsAYRollover { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}