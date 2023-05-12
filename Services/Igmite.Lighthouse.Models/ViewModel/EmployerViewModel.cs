using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class EmployerViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid EmployerId { get; set; }

        [DataMember]
        public virtual string StateName { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual string BusinessType { get; set; }

        [DataMember]
        public virtual int? EmployeeCount { get; set; }

        [DataMember]
        public virtual string Outlets { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}