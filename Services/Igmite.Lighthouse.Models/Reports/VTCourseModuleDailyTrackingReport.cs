using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTCourseModuleDailyTrackingReport
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
        public virtual string VTGender { get; set; }

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
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual string SectionName { get; set; }
        
        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual DateTime? ReportingDate { get; set; }

        [DataMember]
        public virtual string ReportingDay { get; set; }

        [DataMember]
        public virtual string ActivityType { get; set; }

        [DataMember]
        public virtual string ClassType { get; set; }

        [DataMember]
        public virtual string ClassDuration { get; set; }

        [DataMember]
        public virtual string ModulesTaught { get; set; }

        [DataMember]
        public virtual string UnitsTaught { get; set; }

        [DataMember]
        public virtual string SessionTaught { get; set; }

        [DataMember]
        public virtual string ClassPictureUrl { get; set; }

        [DataMember]
        public virtual string LessonPlanPictureUrl { get; set; }

        [DataMember]
        public virtual Int64 EnrollmentBoys { get; set; }

        [DataMember]
        public virtual Int64 EnrollmentGirls { get; set; }

        [DataMember]
        public virtual Int64 EnrollmentTotal { get; set; }

        [DataMember]
        public virtual Int64 AttendanceBoys { get; set; }

        [DataMember]
        public virtual Int64 AttendanceGirls { get; set; }

        [DataMember]
        public virtual Int64 AttendanceTotal { get; set; }
    }
}