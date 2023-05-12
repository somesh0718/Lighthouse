using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class DRPDailyReportingModel : DRPDailyReporting
    {
        public DRPDailyReportingModel()
        {
        }

        [DataMember]
        public IList<string> WorkingDayTypeIds { get; set; }

        [DataMember]
        public DRPRIndustryExposureVisitModel IndustryExposureVisit { get; set; }

        [DataMember]
        public LeaveModel Leave { get; set; }

        [DataMember]
        public HolidayModel Holiday { get; set; }
    }
}