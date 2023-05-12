using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class ExitSurveyDetailsMapper
    {
        public static ExitSurveyDetailsModel ToModel(this ExitSurveyDetails exitSurveyDetails)
        {
            if (exitSurveyDetails == null)
                return null;

            ExitSurveyDetailsModel exitSurveyDetailsModel = new ExitSurveyDetailsModel
            {
                Id = exitSurveyDetails.Id,
                ExitStudentId = exitSurveyDetails.ExitStudentId,
                AcademicYearId = exitSurveyDetails.AcademicYearId,

                #region Personal Information

                SeatNo = exitSurveyDetails.SeatNo,
                StudentMobileNo = exitSurveyDetails.StudentMobileNo,
                ParentMobileNo = exitSurveyDetails.ParentMobileNo,
                StudentWANo = exitSurveyDetails.StudentWANo,
                Religion = exitSurveyDetails.Religion,

                #endregion Personal Information

                #region Residential Information

                CityOfResidence = exitSurveyDetails.CityOfResidence,
                DistrictOfResidence = exitSurveyDetails.DistrictOfResidence,
                BlockOfResidence = exitSurveyDetails.BlockOfResidence,
                PinCode = exitSurveyDetails.PinCode,
                StudentAddress = exitSurveyDetails.StudentAddress,

                #endregion Residential Information

                #region Education post 10/12 th

                DoneInternship = exitSurveyDetails.DoneInternship,
                InternshipCompletedSector = exitSurveyDetails.InternshipCompletedSector,
                WillContHigherStudies = exitSurveyDetails.WillContHigherStudies,
                IsFullTime = exitSurveyDetails.IsFullTime,
                CourseToPursue = exitSurveyDetails.CourseToPursue,
                OtherCourse = exitSurveyDetails.OtherCourse,
                StreamOfEducation = exitSurveyDetails.StreamOfEducation,
                OtherStreamStudying = exitSurveyDetails.OtherStreamStudying,
                WillContVocEdu = exitSurveyDetails.WillContVocEdu,
                WillContVocational11 = exitSurveyDetails.WillContVocational11,
                ReasonsNOTToContinue = exitSurveyDetails.ReasonsNOTToContinue,
                WillContSameSector = exitSurveyDetails.WillContSameSector,
                SectorForTraining = exitSurveyDetails.SectorForTraining,
                OtherSector = exitSurveyDetails.OtherSector,

                #endregion Education post 10/12 th

                #region Employment Details

                CurrentlyEmployed = exitSurveyDetails.CurrentlyEmployed,
                WorkTitle = exitSurveyDetails.WorkTitle,
                DetailsOfEmployment = exitSurveyDetails.DetailsOfEmployment,
                SectorsOfEmployment = exitSurveyDetails.SectorsOfEmployment,
                IsVSCompleted = exitSurveyDetails.IsVSCompleted,

                #endregion Employment Details

                #region Support

                WantToPursueAnySkillTraining = exitSurveyDetails.WantToPursueAnySkillTraining,
                IsFulltimeWillingness = exitSurveyDetails.IsFulltimeWillingness,
                HveRegisteredOnEmploymentPortal = exitSurveyDetails.HveRegisteredOnEmploymentPortal,
                EmploymentPortalName = exitSurveyDetails.EmploymentPortalName,
                WillingToGetRegisteredOnNAPS = exitSurveyDetails.WillingToGetRegisteredOnNAPS,
                IntrestedInJobOrSelfEmploymentPost12th = exitSurveyDetails.IntrestedInJobOrSelfEmploymentPost12th,
                PreferredLocations = exitSurveyDetails.PreferredLocations,
                ParticularLocation = exitSurveyDetails.ParticularLocation,
                WantToKnowAboutOpportunities = exitSurveyDetails.WantToKnowAboutOpportunities,
                CanLahiGetInTouch = exitSurveyDetails.CanLahiGetInTouch,
                WantToKnowAbtPgmsForJobsNContEdu = exitSurveyDetails.WantToKnowAbtPgmsForJobsNContEdu,

                #endregion Support

                #region Status & Remarks

                CollectedEmailId = exitSurveyDetails.CollectedEmailId,
                SurveyCompletedByStudentORParent = exitSurveyDetails.SurveyCompletedByStudentORParent,
                DateOfIntv = exitSurveyDetails.DateOfIntv,
                Remark = exitSurveyDetails.Remark,

                CreatedBy = exitSurveyDetails.CreatedBy,
                CreatedOn = exitSurveyDetails.CreatedOn,
                UpdatedBy = exitSurveyDetails.UpdatedBy,
                UpdatedOn = exitSurveyDetails.UpdatedOn,
                IsActive = exitSurveyDetails.IsActive,

                #endregion Status & Remarks

                IsRelevantToVocCourse = exitSurveyDetails.IsRelevantToVocCourse,
                WillBeFullTime = exitSurveyDetails.WillBeFullTime,
                IsOtherCourse = exitSurveyDetails.IsOtherCourse,
                OtherReasons = exitSurveyDetails.OtherReasons,
                DoesFieldStudyHveVocSub = exitSurveyDetails.DoesFieldStudyHveVocSub,
                InterestedInJobOrSelfEmployment = exitSurveyDetails.InterestedInJobOrSelfEmployment,
                TopicsOfInterest = exitSurveyDetails.TopicsOfInterest,
                AnyPreferredLocForEmployment = exitSurveyDetails.AnyPreferredLocForEmployment,
                CanSendTheUpdates = exitSurveyDetails.CanSendTheUpdates,
                WillingToContSkillTraining = exitSurveyDetails.WillingToContSkillTraining,
                CourseForTraining = exitSurveyDetails.CourseForTraining,
                CourseNameIfOther = exitSurveyDetails.CourseNameIfOther,
                SkillTrainingType = exitSurveyDetails.SkillTrainingType,
                OtherSectorsIfAny = exitSurveyDetails.OtherSectorsIfAny,
                WantToKnowAbtSkillsUnivByGvt = exitSurveyDetails.WantToKnowAbtSkillsUnivByGvt,
                TrainingType = exitSurveyDetails.TrainingType,
                SectorForSkillTraining = exitSurveyDetails.SectorForSkillTraining,
                OthersIfAny = exitSurveyDetails.OthersIfAny,
                WillingToGoForTechHighEdu = exitSurveyDetails.WillingToGoForTechHighEdu,
                DifferentProgramOpportunities = exitSurveyDetails.DifferentProgramOpportunities
            };

            return exitSurveyDetailsModel;
        }

        public static ExitSurveyDetails FromModel(this ExitSurveyDetailsModel exitSurveyDetailsModel, ExitSurveyDetails exitSurveyDetails)
        {
            exitSurveyDetails.ExitStudentId = exitSurveyDetailsModel.ExitStudentId;
            exitSurveyDetails.AcademicYearId = exitSurveyDetailsModel.AcademicYearId;

            #region Personal Information

            exitSurveyDetails.SeatNo = exitSurveyDetailsModel.SeatNo;
            exitSurveyDetails.StudentMobileNo = exitSurveyDetailsModel.StudentMobileNo;
            exitSurveyDetails.ParentMobileNo = exitSurveyDetailsModel.ParentMobileNo;
            exitSurveyDetails.StudentWANo = exitSurveyDetailsModel.StudentWANo;
            exitSurveyDetails.Religion = exitSurveyDetailsModel.Religion;

            #endregion Personal Information

            #region Residential Information

            exitSurveyDetails.CityOfResidence = exitSurveyDetailsModel.CityOfResidence;
            exitSurveyDetails.DistrictOfResidence = exitSurveyDetailsModel.DistrictOfResidence;
            exitSurveyDetails.BlockOfResidence = exitSurveyDetailsModel.BlockOfResidence;
            exitSurveyDetails.PinCode = exitSurveyDetailsModel.PinCode;
            exitSurveyDetails.StudentAddress = exitSurveyDetailsModel.StudentAddress;

            #endregion Residential Information

            #region Education post 10/12 th

            exitSurveyDetails.DoneInternship = exitSurveyDetailsModel.DoneInternship;
            exitSurveyDetails.InternshipCompletedSector = exitSurveyDetailsModel.InternshipCompletedSector;
            exitSurveyDetails.WillContHigherStudies = exitSurveyDetailsModel.WillContHigherStudies;
            exitSurveyDetails.IsFullTime = exitSurveyDetailsModel.IsFullTime;
            exitSurveyDetails.CourseToPursue = exitSurveyDetailsModel.CourseToPursue;
            exitSurveyDetails.OtherCourse = exitSurveyDetailsModel.OtherCourse;
            exitSurveyDetails.StreamOfEducation = exitSurveyDetailsModel.StreamOfEducation;
            exitSurveyDetails.OtherStreamStudying = exitSurveyDetailsModel.OtherStreamStudying;
            exitSurveyDetails.WillContVocEdu = exitSurveyDetailsModel.WillContVocEdu;
            exitSurveyDetails.WillContVocational11 = exitSurveyDetailsModel.WillContVocational11;
            exitSurveyDetails.ReasonsNOTToContinue = exitSurveyDetailsModel.ReasonsNOTToContinue;
            exitSurveyDetails.WillContSameSector = exitSurveyDetailsModel.WillContSameSector;
            exitSurveyDetails.SectorForTraining = exitSurveyDetailsModel.SectorForTraining;
            exitSurveyDetails.OtherSector = exitSurveyDetailsModel.OtherSector;

            #endregion Education post 10/12 th

            #region Employment Details

            exitSurveyDetails.CurrentlyEmployed = exitSurveyDetailsModel.CurrentlyEmployed;
            exitSurveyDetails.WorkTitle = exitSurveyDetailsModel.WorkTitle;
            exitSurveyDetails.DetailsOfEmployment = exitSurveyDetailsModel.DetailsOfEmployment;
            exitSurveyDetails.SectorsOfEmployment = exitSurveyDetailsModel.SectorsOfEmployment;
            exitSurveyDetails.IsVSCompleted = exitSurveyDetailsModel.IsVSCompleted;

            #endregion Employment Details

            #region Support

            exitSurveyDetails.WantToPursueAnySkillTraining = exitSurveyDetailsModel.WantToPursueAnySkillTraining;
            exitSurveyDetails.IsFulltimeWillingness = exitSurveyDetailsModel.IsFulltimeWillingness;
            exitSurveyDetails.HveRegisteredOnEmploymentPortal = exitSurveyDetailsModel.HveRegisteredOnEmploymentPortal;
            exitSurveyDetails.EmploymentPortalName = exitSurveyDetailsModel.EmploymentPortalName;
            exitSurveyDetails.WillingToGetRegisteredOnNAPS = exitSurveyDetailsModel.WillingToGetRegisteredOnNAPS;
            exitSurveyDetails.IntrestedInJobOrSelfEmploymentPost12th = exitSurveyDetailsModel.IntrestedInJobOrSelfEmploymentPost12th;
            exitSurveyDetails.PreferredLocations = exitSurveyDetailsModel.PreferredLocations;
            exitSurveyDetails.ParticularLocation = exitSurveyDetailsModel.ParticularLocation;
            exitSurveyDetails.WantToKnowAboutOpportunities = exitSurveyDetailsModel.WantToKnowAboutOpportunities;
            exitSurveyDetails.CanLahiGetInTouch = exitSurveyDetailsModel.CanLahiGetInTouch;
            exitSurveyDetails.WantToKnowAbtPgmsForJobsNContEdu = exitSurveyDetailsModel.WantToKnowAbtPgmsForJobsNContEdu;

            #endregion Support

            #region Status & Remarks

            exitSurveyDetails.CollectedEmailId = exitSurveyDetailsModel.CollectedEmailId;
            exitSurveyDetails.SurveyCompletedByStudentORParent = exitSurveyDetailsModel.SurveyCompletedByStudentORParent;
            exitSurveyDetails.DateOfIntv = exitSurveyDetailsModel.DateOfIntv;
            exitSurveyDetails.Remark = exitSurveyDetailsModel.Remark;

            #endregion Status & Remarks

            exitSurveyDetails.IsRelevantToVocCourse = exitSurveyDetailsModel.IsRelevantToVocCourse;
            exitSurveyDetails.WillBeFullTime = exitSurveyDetailsModel.WillBeFullTime;
            exitSurveyDetails.IsOtherCourse = exitSurveyDetailsModel.IsOtherCourse;
            exitSurveyDetails.OtherReasons = exitSurveyDetailsModel.OtherReasons;
            exitSurveyDetails.DoesFieldStudyHveVocSub = exitSurveyDetailsModel.DoesFieldStudyHveVocSub;
            exitSurveyDetails.InterestedInJobOrSelfEmployment = exitSurveyDetailsModel.InterestedInJobOrSelfEmployment;
            exitSurveyDetails.TopicsOfInterest = exitSurveyDetailsModel.TopicsOfInterest;
            exitSurveyDetails.AnyPreferredLocForEmployment = exitSurveyDetailsModel.AnyPreferredLocForEmployment;
            exitSurveyDetails.CanSendTheUpdates = exitSurveyDetailsModel.CanSendTheUpdates;
            exitSurveyDetails.WillingToContSkillTraining = exitSurveyDetailsModel.WillingToContSkillTraining;
            exitSurveyDetails.CourseForTraining = exitSurveyDetailsModel.CourseForTraining;
            exitSurveyDetails.CourseNameIfOther = exitSurveyDetailsModel.CourseNameIfOther;
            exitSurveyDetails.SkillTrainingType = exitSurveyDetailsModel.SkillTrainingType;
            exitSurveyDetails.OtherSectorsIfAny = exitSurveyDetailsModel.OtherSectorsIfAny;
            exitSurveyDetails.WantToKnowAbtSkillsUnivByGvt = exitSurveyDetailsModel.WantToKnowAbtSkillsUnivByGvt;
            exitSurveyDetails.TrainingType = exitSurveyDetailsModel.TrainingType;
            exitSurveyDetails.SectorForSkillTraining = exitSurveyDetailsModel.SectorForSkillTraining;
            exitSurveyDetails.OthersIfAny = exitSurveyDetailsModel.OthersIfAny;
            exitSurveyDetails.WillingToGoForTechHighEdu = exitSurveyDetailsModel.WillingToGoForTechHighEdu;
            exitSurveyDetails.DifferentProgramOpportunities = exitSurveyDetailsModel.DifferentProgramOpportunities;

            exitSurveyDetails.SetAuditValues(exitSurveyDetails.RequestType);
            return exitSurveyDetails;
        }
    }
}