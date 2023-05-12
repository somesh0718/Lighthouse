using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VCDailyReportingModel : VCDailyReporting
    {
        public VCDailyReportingModel()
        {
        }

        [DataMember]
        public IList<string> WorkingDayTypeIds { get; set; }

        [DataMember]
        public VCRIndustryExposureVisitModel IndustryExposureVisit { get; set; }

        [DataMember]
        public LeaveModel Leave { get; set; }

        [DataMember]
        public HolidayModel Holiday { get; set; }

        [DataMember]
        public VCRPraticalModel Pratical { get; set; }
    }
}