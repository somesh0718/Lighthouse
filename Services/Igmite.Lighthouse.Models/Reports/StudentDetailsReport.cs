using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class StudentDetailsReport
    {
        [DataMember]
        public virtual int SrNo { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        // Primary key
        [Key, DataMember]
        public virtual Guid LHStudentId { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string MiddleName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string StudentName { get; set; }

        [DataMember]
        public virtual string StudentGender { get; set; }

        [DataMember]
        public virtual string PrimaryContact { get; set; }

        [DataMember]
        public virtual string AlternativeContact { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string SectionName { get; set; }

        [DataMember]
        public virtual string StreamName { get; set; }

        [DataMember]
        public virtual string RoleNo { get; set; }

        [DataMember]
        public virtual string FatherName { get; set; }

        [DataMember]
        public virtual string MotherName { get; set; }

        [DataMember]
        public virtual string GuardianName { get; set; }

        [DataMember]
        public virtual DateTime? DateOfBirth { get; set; }

        [DataMember]
        public virtual string SocialCategory { get; set; }

        [DataMember]
        public virtual string Religion { get; set; }

        [DataMember]
        public virtual string ReadyForAssesment { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string StateName { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual string SchoolManagement { get; set; }

        [DataMember]
        public virtual string SchoolAllottedYear { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

        [DataMember]
        public virtual string HMName { get; set; }

        [DataMember]
        public virtual string HMMobile { get; set; }

        [DataMember]
        public virtual string HMEmail { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

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
        public virtual DateTime? CreatedOn { get; set; }

        [DataMember]
        public virtual DateTime? UpdatedOn { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}