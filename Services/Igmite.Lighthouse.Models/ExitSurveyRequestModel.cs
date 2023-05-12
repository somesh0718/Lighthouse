using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ExitSurveyRequestModel
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string UserType { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public string StudentId { get; set; }

        [DataMember]
        public Guid ClassId { get; set; }

        [DataMember]
        public string StudentUniqueId { get; set; }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}