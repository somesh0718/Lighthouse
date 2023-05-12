using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class SchoolViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        // Foreign key
        [DataMember]
        public virtual string CategoryName { get; set; }

        [DataMember]
        public virtual string Udise { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolType { get; set; }

        [DataMember]
        public virtual string SchoolManagement { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

        [DataMember]
        public virtual string StateName { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual string Pincode { get; set; }

        [DataMember]
        public virtual bool IsImplemented { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}