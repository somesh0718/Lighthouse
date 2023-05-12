using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTStudentTrackingReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCMobile { get; set; }

        [DataMember]
        public virtual string VCEmail { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string VTEmail { get; set; }

        [DataMember]
        public virtual DateTime? VTDateOfJoining { get; set; }

        [DataMember]
        public virtual string HMName { get; set; }

        [DataMember]
        public virtual string HMMobile { get; set; }

        [DataMember]
        public virtual string HMEmail { get; set; }

        [DataMember]
        public virtual string SchoolManagement { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string ReportType { get; set; }

        [DataMember]
        public virtual string WorkingDayType { get; set; }

        [DataMember]
        public virtual string ClassTaught { get; set; }

        [DataMember]
        public virtual string ClassType { get; set; }

        [DataMember]
        public virtual int? Boys { get; set; }

        [DataMember]
        public virtual int? Girls { get; set; }

        [DataMember]
        public virtual DateTime ReportingDate { get; set; }

        [DataMember]
        public virtual DateTime SubmissionDate { get; set; }

        [DataMember]
        public virtual string GeoLocation { get; set; }
    }
}