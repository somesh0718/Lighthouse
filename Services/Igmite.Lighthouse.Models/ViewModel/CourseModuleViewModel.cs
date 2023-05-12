using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class CourseModuleViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid CourseModuleId { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string CourseModule { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string UnitName { get; set; }

        [DataMember]
        public virtual string Sessions { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}