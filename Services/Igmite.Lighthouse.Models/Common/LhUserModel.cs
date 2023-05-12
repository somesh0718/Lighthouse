using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class LhUserModel
    {
        public LhUserModel()
        {
        }

        [DataMember]
        public virtual Guid? AcademicYearId { get; set; }

        [DataMember]
        public virtual Guid? VTPId { get; set; }

        [DataMember]
        public virtual Guid? VCId { get; set; }

        [DataMember]
        public virtual Guid? VTId { get; set; }

        [DataMember]
        public virtual Guid? HMId { get; set; }

        [DataMember]
        public virtual Guid? SchoolId { get; set; }

        [DataMember]
        public virtual Guid? ClassId { get; set; }
    }
}