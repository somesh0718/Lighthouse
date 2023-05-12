using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class HeadMasterModel : HeadMaster
    {
        public HeadMasterModel()
        {
        }

        [DataMember]
        public virtual Guid? VTPId { get; set; }

        [DataMember]
        public virtual Guid? VCId { get; set; }

        [DataMember]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        public virtual DateTime? DateOfResignation { get; set; }
    }
}