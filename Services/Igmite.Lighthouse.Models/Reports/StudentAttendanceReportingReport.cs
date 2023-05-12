using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class StudentAttendanceReportingReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolAllottedYear { get; set; }

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
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string MonthYear { get; set; }

        [DataMember]
        public virtual int VTReportSubmitted { get; set; }

        [DataMember]
        public virtual int VTWorkingDays { get; set; }

        [DataMember]
        public virtual int AttendanceDays { get; set; }

        [DataMember]
        public virtual int EnrolledBoys { get; set; }

        [DataMember]
        public virtual int EnrolledGirls { get; set; }

        [DataMember]
        public virtual int EnrolledStudents { get; set; }

        [DataMember]
        public virtual int AttendanceBoys { get; set; }

        [DataMember]
        public virtual int AttendanceGirls { get; set; }

        [DataMember]
        public virtual int StudentAttendances { get; set; }

        [DataMember]
        public virtual double AverageBoysAttendance { get; set; }

        [DataMember]
        public virtual double AverageGirlsAttendance { get; set; }

        [DataMember]
        public virtual double AverageAttendance { get; set; }
    }
}