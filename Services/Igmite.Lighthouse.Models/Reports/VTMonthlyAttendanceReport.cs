using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTMonthlyAttendanceReport
    {
        [DataMember]
        public VTAttendanceHeaderModel VTAttendanceHeader { get; set; }

        [DataMember]
        public IList<VTAttendanceDetailModel> VTAttendanceDetails { get; set; }
    }
}