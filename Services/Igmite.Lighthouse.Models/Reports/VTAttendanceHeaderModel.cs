using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTAttendanceHeaderModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTId { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual DateTime VTDateOfJoining { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string MonthYear { get; set; }

        [DataMember]
        public virtual int WorkingDays { get; set; }

        [DataMember]
        public virtual int Sundays { get; set; }

        [DataMember]
        public virtual int PaidLeaves { get; set; }

        [DataMember]
        public virtual int UnpaidLeaves { get; set; }

        [DataMember]
        public virtual int LongTermHolidays { get; set; }

        [DataMember]
        public virtual int LocalGovHolidays { get; set; }

        [DataMember]
        public virtual int TotalPaidDays { get; set; }

        [DataMember]
        public virtual int TotalDays { get; set; }
    }
}