using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class HeadMasterViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AcademicYearId { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual Guid SchoolId { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual Guid HMId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SchoolUDISE { get; set; }

        [DataMember]
        public virtual string FullName { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string Email { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual int? YearsInSchool { get; set; }

        [DataMember]
        public virtual bool IsResigned { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}