using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class PraticalAssesmentModel
    {
        public PraticalAssesmentModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Guid VCRPraticalId { get; set; }

        [DataMember]
        public virtual int SrNo { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string EmailId { get; set; }

        [DataMember]
        public virtual DateTime? ReportDate { get; set; }

        [DataMember]
        public virtual string IsPratical { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string EnrolledStudents { get; set; }

        [DataMember]
        public virtual string VTPresent { get; set; }

        [DataMember]
        public virtual int PresentStudents { get; set; }

        [DataMember]
        public virtual string AssesorName { get; set; }

        [DataMember]
        public virtual string AssessorMobile { get; set; }

        [DataMember]
        public virtual string Remarks { get; set; }

    }
}