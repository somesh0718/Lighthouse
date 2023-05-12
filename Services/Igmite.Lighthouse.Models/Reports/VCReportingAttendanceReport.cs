using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VCReportingAttendanceReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public string SchoolAllottedYear { get; set; }

        [DataMember]
        public string PhaseName { get; set; }

        [DataMember]
        public string VTPName { get; set; }

        [DataMember]
        public string VCName { get; set; }

        [DataMember]
        public string VCMobile { get; set; }

        [DataMember]
        public string VCEmail { get; set; }

        [DataMember]
        public int TotalDays { get; set; }

        [DataMember]
        public int NoOfSundays { get; set; }

        [DataMember]
        public int VCReportsSubmitted { get; set; }

        [DataMember]
        public string MonthYear { get; set; }

        [DataMember]
        public int WorkingDays { get; set; }

        [DataMember]
        public int Holidays { get; set; }

        [DataMember]
        public int LeaveDays { get; set; }

        [DataMember]
        public int NumberOfSchools { get; set; }

        [DataMember]
        public int SchoolVisitDays { get; set; }
    }
}