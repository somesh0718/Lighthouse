using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ExitSurveyReportModel
    {
        [DataMember, Key]
        public virtual Guid ExitStudentId { get; set; }

        #region Academic Information

        [DataMember]
        public virtual Guid? AcademicYearId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string Sector { get; set; }

        [DataMember]
        public virtual string JobRole { get; set; }

        [DataMember]
        public virtual string State { get; set; }

        [DataMember]
        public virtual string Division { get; set; }

        [DataMember]
        public virtual string District { get; set; }

        [DataMember]
        public virtual string NameOfSchool { get; set; }

        [DataMember]
        public virtual string UdiseCode { get; set; }

        [DataMember]
        public virtual string Class { get; set; }

        #endregion Academic Information

        #region Personal Information

        [DataMember]
        public virtual string SeatNo { get; set; }

        [DataMember]
        public virtual string StudentUniqueId { get; set; }

        [DataMember]
        public virtual string StudentFirstName { get; set; }

        [DataMember]
        public virtual string StudentMiddleName { get; set; }

        [DataMember]
        public virtual string StudentLastName { get; set; }

        [DataMember]
        public virtual string StudentFullName { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime? DOB { get; set; }

        [DataMember]
        public virtual string FatherName { get; set; }

        [DataMember]
        public virtual string MotherName { get; set; }

        [DataMember]
        public virtual string Category { get; set; }

        [DataMember]
        public virtual string Religion { get; set; }

        [DataMember]
        public virtual string StreamName { get; set; }

        [DataMember]
        public virtual string StudentMobileNo { get; set; }

        [DataMember]
        public virtual string StudentWhatsAppNo { get; set; }

        [DataMember]
        public virtual string ParentMobileNo { get; set; }

        #endregion Personal Information

        #region Residential Information

        [DataMember]
        public virtual string CityOfResidence { get; set; }

        [DataMember]
        public virtual string DistrictOfResidence { get; set; }

        [DataMember]
        public virtual string BlockOfResidence { get; set; }

        [DataMember]
        public virtual string PinCode { get; set; }

        [DataMember]
        public virtual string StudentAddress { get; set; }

        #endregion Residential Information

        #region Education post 10/12 th

        [DataMember]
        public virtual string DoneInternship { get; set; }

        [DataMember]
        public virtual string InternshipCompletedSector { get; set; }

        [DataMember]
        public virtual string WillContHigherStudies { get; set; }

        [DataMember]
        public virtual string IsFullTime { get; set; }

        [DataMember]
        public virtual string CourseToPursue { get; set; }

        [DataMember]
        public virtual string OtherCourse { get; set; }

        [DataMember]
        public virtual string StreamOfEducation { get; set; }

        [DataMember]
        public virtual string OtherStreamStudying { get; set; }

        [DataMember]
        public virtual string WillContVocEdu { get; set; }

        [DataMember]
        public virtual string WillContVocational11 { get; set; }

        [DataMember]
        public virtual string ReasonsNOTToContinue { get; set; }

        [DataMember]
        public virtual string WillContSameSector { get; set; }

        [DataMember]
        public virtual string SectorForTraining { get; set; }

        [DataMember]
        public virtual string OtherSector { get; set; }

        #endregion Education post 10/12 th

        #region Employment Details

        [DataMember]
        public virtual string CurrentlyEmployed { get; set; }

        [DataMember]
        public virtual string WorkTitle { get; set; }

        [DataMember]
        public virtual string DetailsOfEmployment { get; set; }

        [DataMember]
        public virtual string SectorsOfEmployment { get; set; }

        [DataMember]
        public virtual string IsVSCompleted { get; set; }

        #endregion Employment Details

        #region Support

        [DataMember]
        public virtual string WantToPursueAnySkillTraining { get; set; }

        [DataMember]
        public virtual string IsFulltimeWillingness { get; set; }

        [DataMember]
        public virtual string HveRegisteredOnEmploymentPortal { get; set; }

        [DataMember]
        public virtual string EmploymentPortalName { get; set; }

        [DataMember]
        public virtual string WillingToGetRegisteredOnNAPS { get; set; }

        [DataMember]
        public virtual string IntrestedInJobOrSelfEmploymentPost12th { get; set; }

        [DataMember]
        public virtual string PreferredLocations { get; set; }

        [DataMember]
        public virtual string ParticularLocation { get; set; }

        [DataMember]
        public virtual string WantToKnowAboutOpportunities { get; set; }

        [DataMember]
        public virtual string CanLahiGetInTouch { get; set; }

        [DataMember]
        public virtual string WantToKnowAbtPgmsForJobsNContEdu { get; set; }

        #endregion Support

        #region Status & Remarks

        [DataMember]
        public virtual string CollectedEmailId { get; set; }

        [DataMember]
        public virtual string SurveyCompletedByStudentORParent { get; set; }

        [DataMember]
        public virtual DateTime? DateOfIntv { get; set; }

        [DataMember]
        public virtual string Remark { get; set; }

        [DataMember]
        public virtual string ExitSurveyStatus { get; set; }

        #endregion Status & Remarks

        [DataMember]
        public virtual string WillBeFullTime { get; set; }

        [DataMember]
        public virtual string OtherReasons { get; set; }

        [DataMember]
        public virtual string DoesFieldStudyHveVocSub { get; set; }

        [DataMember]
        public virtual string AnyPreferredLocForEmployment { get; set; }

        [DataMember]
        public virtual string TrainingType { get; set; }

        [DataMember]
        public virtual string WillingToContSkillTraining { get; set; }

        [DataMember]
        public virtual string SkillTrainingType { get; set; }

        [DataMember]
        public virtual string CourseForTraining { get; set; }

        [DataMember]
        public virtual string CourseNameIfOther { get; set; }

        [DataMember]
        public virtual string OtherSectorsIfAny { get; set; }

        [DataMember]
        public virtual string InterestedInJobOrSelfEmployment { get; set; }

        [DataMember]
        public virtual string TopicsOfInterest { get; set; }

        [DataMember]
        public virtual string IsRelevantToVocCourse { get; set; }

        [DataMember]
        public virtual string SectorForSkillTraining { get; set; }

        [DataMember]
        public virtual string OthersIfAny { get; set; }

        [DataMember]
        public virtual string WillingToGoForTechHighEdu { get; set; }

        [DataMember]
        public virtual string WantToKnowAbtSkillsUnivByGvt { get; set; }

        [DataMember]
        public virtual string CanSendTheUpdates { get; set; }

        [DataMember]
        public virtual DateTime? SubmissionDate { get; set; }

        [DataMember]
        public virtual bool IsAssessmentRequired { get; set; }

        [DataMember]
        public virtual string AssessmentConducted { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }

        //End Region
    }
}