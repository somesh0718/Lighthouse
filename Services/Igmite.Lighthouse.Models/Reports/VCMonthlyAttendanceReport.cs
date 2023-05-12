using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VCMonthlyAttendanceReport
    {
        [DataMember]
        public VCAttendanceHeaderModel VCAttendanceHeader { get; set; }

        [DataMember]
        public IList<VCAttendanceDetailModel> VCAttendanceDetails { get; set; }
    }
}