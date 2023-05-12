using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTNotReportedDailyAttendanceViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual int SrNo { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual DateTime FromReportDate { get; set; }

        [DataMember]
        public virtual DateTime ToReportDate { get; set; }

        [DataMember]
        public virtual int NotReported { get; set; }
    }
}