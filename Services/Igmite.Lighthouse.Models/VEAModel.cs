using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VEAModel
    {
        public VEAModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string YearName { get; set; }

        [DataMember]
        public virtual string State { get; set; }

        [DataMember]
        public virtual string District { get; set; }

        [DataMember]
        public virtual string School { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string HMName { get; set; }

        [DataMember]
        public virtual string HMEmail { get; set; }

        [DataMember]
        public virtual string HMMobile { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCEmail { get; set; }

        [DataMember]
        public virtual string VCMobile { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmail { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string MiddleName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string Class { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime DOB { get; set; }

        [DataMember]
        public virtual string AadhaarNumber { get; set; }

        [DataMember]
        public virtual string RollNo { get; set; }

        [DataMember]
        public virtual string FatherName { get; set; }

        [DataMember]
        public virtual string MotherName { get; set; }

        [DataMember]
        public virtual string PrimaryContact { get; set; }

        [DataMember]
        public virtual string AlternativeContact { get; set; }

        [DataMember]
        public virtual string Category { get; set; }

        [DataMember]
        public virtual string Sector { get; set; }

        [DataMember]
        public virtual string JobRole { get; set; }

        [DataMember]
        public virtual string StreamName { get; set; }

        [DataMember]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}