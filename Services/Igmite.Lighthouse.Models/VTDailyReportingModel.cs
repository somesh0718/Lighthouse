using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTDailyReportingModel : VTDailyReporting
    {
        public VTDailyReportingModel()
        {
        }

        [DataMember]
        public IList<string> WorkingDayTypeIds { get; set; }

        [DataMember]
        public IList<VTRTeachingVocationalEducationModel> TeachingVocationalEducations { get; set; }

        [DataMember]
        public VTRTrainingOfTeacherModel TrainingOfTeacher { get; set; }

        [DataMember]
        public VTROnJobTrainingCoordinationModel OnJobTrainingCoordination { get; set; }

        [DataMember]
        public VTRAssessorInOtherSchoolForExamModel AssessorInOtherSchoolForExam { get; set; }

        [DataMember]
        public VTRParentTeachersMeetingModel ParentTeachersMeeting { get; set; }

        [DataMember]
        public VTRCommunityHomeVisitModel CommunityHomeVisit { get; set; }

        [DataMember]
        public IList<VTRVisitToIndustryModel> VisitToIndustries { get; set; }

        [DataMember]
        public IList<VTRVisitToEducationalInstitutionModel> VisitToEducationalInstitutions { get; set; }

        [DataMember]
        public VTRAssignmentFromVocationalDepartmentModel AssignmentFromVocationalDepartment { get; set; }

        [DataMember]
        public VTRTeachingNonVocationalSubjectModel TeachingNonVocationalSubject { get; set; }

        [DataMember]
        public LeaveModel Leave { get; set; }

        [DataMember]
        public HolidayModel Holiday { get; set; }

        [DataMember]
        public VTRObservationDayModel ObservationDay { get; set; }
    }
}