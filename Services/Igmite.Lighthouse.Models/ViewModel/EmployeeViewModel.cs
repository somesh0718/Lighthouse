using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class EmployeeViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        public virtual string EmployeeCode { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string MiddleName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime? DateOfBirth { get; set; }

        [DataMember]
        public virtual string Department { get; set; }

        [DataMember]
        public virtual string Telephone { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string EmailId { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
