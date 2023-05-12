using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VEADetailsModel
    {
        public VEADetailsModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string Class { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime DOB { get; set; }

        [DataMember]
        public virtual string AadhaarNumber { get; set; }

        [DataMember]
        public virtual string StudentRollNumber { get; set; }

        [DataMember]
        public virtual string PrimaryContact { get; set; }

        [DataMember]
        public virtual string AlternativeContact { get; set; }

        [DataMember]
        public virtual string FatherName { get; set; }

        [DataMember]
        public virtual string Category { get; set; }

        [DataMember]
        public virtual string Sector { get; set; }

        [DataMember]
        public virtual string JobRole { get; set; }

        [DataMember]
        public virtual string Assesment { get; set; }

        [DataMember]
        public virtual string StreamName { get; set; }

        [DataMember]
        public virtual Guid StudentId { get; set; }
    }
}