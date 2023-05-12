using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VEAHeaderModel
    {
        public VEAHeaderModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolAddress { get; set; }

        [DataMember]
        public virtual string HMName { get; set; }

        [DataMember]
        public virtual string HMMobile { get; set; }

        [DataMember]
        public virtual string HMEmailId { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCEmailId { get; set; }

        [DataMember]
        public virtual string VCMobile { get; set; }

        [DataMember]
        public virtual int TotalNoOfStudents { get; set; }
    }
}