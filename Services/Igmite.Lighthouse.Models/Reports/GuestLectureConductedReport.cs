using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class GuestLectureConductedReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolAllottedYear { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

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
        public virtual string HMName { get; set; }

        [DataMember]
        public virtual string HMMobile { get; set; }

        [DataMember]
        public virtual string HMEmail { get; set; }

        [DataMember]
        public virtual DateTime GuestLectureDate { get; set; }

        [DataMember]
        public virtual int TotalStudentsPresent { get; set; }

        [DataMember]
        public virtual string GuestLectureTopic { get; set; }

        [DataMember]
        public virtual string GuestLectureMethodology { get; set; }

        [DataMember]
        public virtual string GuestLectureModule { get; set; }

        [DataMember]
        public virtual string GuestLecturerName { get; set; }

        [DataMember]
        public virtual string GuestLecturerMobile { get; set; }

        [DataMember]
        public virtual string GuestLecturerEmail { get; set; }

        [DataMember]
        public virtual string GuestLecturerQualification { get; set; }

        [DataMember]
        public virtual string GuestLecturerAddress { get; set; }

        [DataMember]
        public virtual string GuestLecturerOrganisation { get; set; }

        [DataMember]
        public virtual string GuestLecturerDesignation { get; set; }
    }
}