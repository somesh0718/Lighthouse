using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the VTDailyReporting entity
    /// </summary>
    public class VTDailyReportingRepository : GenericRepository<VTDailyReporting>, IVTDailyReportingRepository
    {
        /// <summary>
        /// Get list of VTDailyReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTDailyReporting> GetVTDailyReportings()
        {
            return this.Context.VTDailyReportings.AsQueryable();
        }

        /// <summary>
        /// Get list of VTDailyReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTDailyReporting> GetVTDailyReportingsByName(string name)
        {
            var vtDailyReportings = (from v in this.Context.VTDailyReportings
                                     where v.ReportType.Contains(name)
                                     select v).AsQueryable();

            return vtDailyReportings;
        }

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        public VTDailyReporting GetVTDailyReportingById(Guid vtDailyReportingId)
        {
            return this.Context.VTDailyReportings.FirstOrDefault(v => v.VTDailyReportingId == vtDailyReportingId);
        }

        /// <summary>
        /// Get VTDailyReporting by VT Id & Reporting Date
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="reportingDate"></param>
        /// <returns></returns>
        public VTDailyReporting GetVTDailyReportingById(Guid vtId, DateTime reportingDate)
        {
            return this.Context.VTDailyReportings.FirstOrDefault(v => v.VTId == vtId && v.ReportingDate.Date == reportingDate.Date);
        }

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId using async
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        public async Task<VTDailyReporting> GetVTDailyReportingByIdAsync(Guid vtDailyReportingId)
        {
            var vtDailyReporting = await (from v in this.Context.VTDailyReportings
                                          where v.VTDailyReportingId == vtDailyReportingId
                                          select v).FirstOrDefaultAsync();

            return vtDailyReporting;
        }

        /// <summary>
        /// Insert/Update VTDailyReporting entity
        /// </summary>
        /// <param name="dailyReporting"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTDailyReportingDetails(VTDailyReporting dailyReporting, VTDailyReportingModel dailyReportingModel)
        {
            try
            {
                if (RequestType.New == dailyReporting.RequestType)
                    Context.VTDailyReportings.Add(dailyReporting);
                else
                {
                    Context.Entry<VTDailyReporting>(dailyReporting).State = EntityState.Modified;
                }

                VTReportSubmission vtReportSubmissionItem = this.Context.VTReportSubmissions.FirstOrDefault(v => v.VTId == dailyReporting.VTId && v.ReportingDate.Date == dailyReporting.ReportingDate.Date);

                if (vtReportSubmissionItem != null)
                {
                    Context.Entry<VTReportSubmission>(vtReportSubmissionItem).State = EntityState.Deleted;
                }

                Context.SaveChanges();

                if (dailyReportingModel != null)
                {
                    #region 1. Working Day Type

                    if (dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Count > 0)
                    {
                        IList<VTRWorkingDayType> workingDayTypes = Context.VTRWorkingDayTypes.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        foreach (string workingTypeId in dailyReportingModel.WorkingDayTypeIds)
                        {
                            VTRWorkingDayType workingTypeItem = workingDayTypes.FirstOrDefault(v => v.WorkingTypeId == workingTypeId);
                            if (workingTypeId == null || workingTypeItem != null)
                                continue;

                            Context.VTRWorkingDayTypes.Add(new VTRWorkingDayType
                            {
                                VTRWorkingDayTypeId = Guid.NewGuid(),
                                VTDailyReportingId = dailyReporting.VTDailyReportingId,
                                WorkingTypeId = workingTypeId,
                                CreatedBy = dailyReporting.CreatedBy,
                                CreatedOn = dailyReporting.CreatedOn,
                                IsActive = true
                            });
                        }
                    }

                    #endregion 1. Working Day Type

                    #region 2. Teaching Vocational Education

                    if (dailyReportingModel.TeachingVocationalEducations != null && dailyReportingModel.TeachingVocationalEducations.Count > 0)
                    {
                        foreach (var teachingVocationalEducationModel in dailyReportingModel.TeachingVocationalEducations)
                        {
                            var teachingVocationalEducationItem = new VTRTeachingVocationalEducation
                            {
                                VTRTeachingVocationalEducationId = Guid.NewGuid(),
                                VTDailyReportingId = dailyReporting.VTDailyReportingId,
                                ClassTaughtId = teachingVocationalEducationModel.ClassTaughtId,
                                SectionTaughtId = teachingVocationalEducationModel.ClassSectionIds,
                                ClassTypeId = teachingVocationalEducationModel.ClassTypeId,
                                ClassTime = teachingVocationalEducationModel.ClassTime,
                                ReasonDetails = teachingVocationalEducationModel.ReasonDetails,
                                IsTeachToday = teachingVocationalEducationModel.DidYouTeachToday,
                                SequenceNo = teachingVocationalEducationModel.SequenceNo,
                                CreatedBy = dailyReporting.CreatedBy,
                                CreatedOn = dailyReporting.CreatedOn,
                                IsActive = true
                            };

                            if (teachingVocationalEducationModel.ClassPictureFile != null)
                                teachingVocationalEducationItem.ClassPicture = teachingVocationalEducationModel.ClassPictureFile.FilePath;

                            if (teachingVocationalEducationModel.LessonPlanPictureFile != null)
                                teachingVocationalEducationItem.LessonPlanPicture = teachingVocationalEducationModel.LessonPlanPictureFile.FilePath;

                            Context.VTRTeachingVocationalEducations.Add(teachingVocationalEducationItem);
                            Context.SaveChanges();

                            #region Class Sections Taught - Teaching Vocational Education

                            if (teachingVocationalEducationModel.ClassSectionIds != null)
                            {
                                Context.VTRClassSectionsTaughts.Add(new VTRClassSectionsTaught
                                {
                                    VTRClassSectionsTaughtId = Guid.NewGuid(),
                                    VTRTeachingVocationalEducationId = teachingVocationalEducationItem.VTRTeachingVocationalEducationId,
                                    ClassId = teachingVocationalEducationModel.ClassTaughtId,
                                    SectionId = teachingVocationalEducationModel.ClassSectionIds,
                                    CreatedBy = dailyReporting.CreatedBy,
                                    CreatedOn = dailyReporting.CreatedOn,
                                    IsActive = true
                                });
                            }

                            #endregion Class Sections Taught - Teaching Vocational Education

                            #region Activity Type - Teaching Vocational Education

                            if (teachingVocationalEducationModel.ActivityTypeIds != null && teachingVocationalEducationModel.ActivityTypeIds.Count > 0)
                            {
                                foreach (string activityTypeId in teachingVocationalEducationModel.ActivityTypeIds)
                                {
                                    Context.VTRActivityTypes.Add(new VTRActivityType
                                    {
                                        VTRActivityTypeId = Guid.NewGuid(),
                                        VTRTeachingVocationalEducationId = teachingVocationalEducationItem.VTRTeachingVocationalEducationId,
                                        ActivityTypeId = activityTypeId,
                                        CreatedBy = dailyReporting.CreatedBy,
                                        CreatedOn = dailyReporting.CreatedOn,
                                        IsActive = true
                                    });
                                }
                            }

                            #endregion Activity Type - Teaching Vocational Education

                            #region Unit Sessions Taught - Teaching Vocational Education

                            if (teachingVocationalEducationModel.UnitSessionsModels != null && teachingVocationalEducationModel.UnitSessionsModels.Count > 0)
                            {
                                foreach (var unitSessionsModel in teachingVocationalEducationModel.UnitSessionsModels)
                                {
                                    foreach (var unitSessionId in unitSessionsModel.SessionIds)
                                    {
                                        Context.VTRUnitSessionsTaughts.Add(new VTRUnitSessionsTaught
                                        {
                                            VTRUnitSessionsTaughtId = Guid.NewGuid(),
                                            VTRTeachingVocationalEducationId = teachingVocationalEducationItem.VTRTeachingVocationalEducationId,
                                            ModuleId = unitSessionsModel.ModuleId,
                                            UnitId = unitSessionsModel.UnitId,
                                            SessionId = unitSessionId,
                                            CreatedBy = dailyReporting.CreatedBy,
                                            CreatedOn = dailyReporting.CreatedOn,
                                            IsActive = true
                                        });
                                    }
                                }
                            }

                            #endregion Unit Sessions Taught - Teaching Vocational Education

                            #region Student Attendance - Teaching Vocational Education

                            if (teachingVocationalEducationModel.StudentAttendances != null && teachingVocationalEducationModel.StudentAttendances.Count > 0)
                            {
                                foreach (var attendanceModel in teachingVocationalEducationModel.StudentAttendances)
                                {
                                    Context.VTRStudentAttendances.Add(new VTRStudentAttendance
                                    {
                                        VTRStudentAttendanceId = Guid.NewGuid(),
                                        VTRTeachingVocationalEducationId = teachingVocationalEducationItem.VTRTeachingVocationalEducationId,
                                        VTId = dailyReportingModel.VTId,
                                        ClassId = teachingVocationalEducationModel.ClassTaughtId,
                                        StudentId = attendanceModel.StudentId,
                                        IsPresent = attendanceModel.IsPresent,
                                        CreatedBy = dailyReporting.CreatedBy,
                                        CreatedOn = dailyReporting.CreatedOn,
                                        IsActive = true
                                    });
                                }
                            }

                            #endregion Student Attendance - Teaching Vocational Education

                            #region Reason Of Not Conducting The Class - Teaching Vocational Education

                            if (teachingVocationalEducationModel.ReasonOfNotConductingTheClassIds != null && teachingVocationalEducationModel.ReasonOfNotConductingTheClassIds.Count > 0)
                            {
                                foreach (string reasonTypeId in teachingVocationalEducationModel.ReasonOfNotConductingTheClassIds)
                                {
                                    Context.VTRReasonOfNotConductingTheClasses.Add(new VTRReasonOfNotConductingTheClass
                                    {
                                        VTRReasonOfNotConductingTheClassId = Guid.NewGuid(),
                                        VTRTeachingVocationalEducationId = teachingVocationalEducationItem.VTRTeachingVocationalEducationId,
                                        ReasonTypeId = reasonTypeId,
                                        CreatedBy = dailyReporting.CreatedBy,
                                        CreatedOn = dailyReporting.CreatedOn,
                                        IsActive = true
                                    });
                                }
                            }

                            #endregion Reason Of Not Conducting The Class - Teaching Vocational Education
                        }
                    }

                    #endregion 2. Teaching Vocational Education

                    #region 3. Training Of Teacher

                    if (dailyReportingModel.TrainingOfTeacher != null)
                    {
                        IList<VTRTrainingOfTeacher> trainingOfTeachers = Context.VTRTrainingOfTeachers.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        trainingOfTeachers.ForEach((trainingOfTeacherItem1) =>
                        {
                            IList<VTRTrainingTopic> trainingTopics = Context.VTRTrainingTopics.Where(v => v.VTRTrainingOfTeacherId == trainingOfTeacherItem1.VTRTrainingOfTeacherId).ToList();
                            trainingTopics.ForEach((trainingTopicItem) =>
                            {
                                Context.Entry<VTRTrainingTopic>(trainingTopicItem).State = EntityState.Deleted;
                            });

                            Context.Entry<VTRTrainingOfTeacher>(trainingOfTeacherItem1).State = EntityState.Deleted;
                        });

                        var trainingOfTeacherModel = dailyReportingModel.TrainingOfTeacher;

                        var trainingOfTeacherItem = new VTRTrainingOfTeacher
                        {
                            VTRTrainingOfTeacherId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            TrainingTypeId = trainingOfTeacherModel.TrainingTypeId,
                            TrainingBy = trainingOfTeacherModel.TrainingBy,
                            TrainingDetails = trainingOfTeacherModel.TrainingDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        };

                        #region Training Topic - Training Of Teacher

                        if (trainingOfTeacherModel.TrainingTopicIds != null && trainingOfTeacherModel.TrainingTopicIds.Count > 0)
                        {
                            foreach (string trainingTopicId in trainingOfTeacherModel.TrainingTopicIds)
                            {
                                Context.VTRTrainingTopics.Add(new VTRTrainingTopic
                                {
                                    VTRTrainingTopicId = Guid.NewGuid(),
                                    VTRTrainingOfTeacherId = trainingOfTeacherItem.VTRTrainingOfTeacherId,
                                    TrainingTopicId = trainingTopicId,
                                    CreatedBy = dailyReporting.CreatedBy,
                                    CreatedOn = dailyReporting.CreatedOn,
                                    IsActive = true
                                });
                            }
                        }

                        #endregion Training Topic - Training Of Teacher

                        Context.VTRTrainingOfTeachers.Add(trainingOfTeacherItem);
                    }

                    #endregion 3. Training Of Teacher

                    #region 4. On-job Training Coordination

                    if (dailyReportingModel.OnJobTrainingCoordination != null)
                    {
                        IList<VTROnJobTrainingCoordination> onJobTrainingCoordinations = Context.VTROnJobTrainingCoordinations.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        onJobTrainingCoordinations.ForEach((onJobTrainingCoordinationItem) =>
                        {
                            Context.Entry<VTROnJobTrainingCoordination>(onJobTrainingCoordinationItem).State = EntityState.Deleted;
                        });

                        var onJobTrainingCoordinationModel = dailyReportingModel.OnJobTrainingCoordination;

                        Context.VTROnJobTrainingCoordinations.Add(new VTROnJobTrainingCoordination
                        {
                            VTROnJobTrainingCoordinationId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            OnJobTrainingActivityId = onJobTrainingCoordinationModel.OnJobTrainingActivityId,
                            IndustryName = onJobTrainingCoordinationModel.IndustryName,
                            IndustryContactPerson = onJobTrainingCoordinationModel.IndustryContactPerson,
                            IndustryContactNumber = onJobTrainingCoordinationModel.IndustryContactNumber,
                            OJTActivityDetails = onJobTrainingCoordinationModel.OJTActivityDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 4. On-job Training Coordination

                    #region 5. Assessor in Other School for Exam

                    if (dailyReportingModel.AssessorInOtherSchoolForExam != null)
                    {
                        IList<VTRAssessorInOtherSchoolForExam> assessorInOtherSchoolForExams = Context.VTRAssessorInOtherSchoolForExams.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        assessorInOtherSchoolForExams.ForEach((assessorInOtherSchoolForExamItem) =>
                        {
                            Context.Entry<VTRAssessorInOtherSchoolForExam>(assessorInOtherSchoolForExamItem).State = EntityState.Deleted;
                        });

                        var assessorInOtherSchoolForExamModel = dailyReportingModel.AssessorInOtherSchoolForExam;

                        Context.VTRAssessorInOtherSchoolForExams.Add(new VTRAssessorInOtherSchoolForExam
                        {
                            VTRAssessorInOtherSchoolForExamId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            SchoolName = assessorInOtherSchoolForExamModel.SchoolName,
                            Udise = assessorInOtherSchoolForExamModel.Udise,
                            ClassId = assessorInOtherSchoolForExamModel.ClassId,
                            BoysPresent = assessorInOtherSchoolForExamModel.BoysPresent,
                            GirlsPresent = assessorInOtherSchoolForExamModel.GirlsPresent,
                            ExamDetails = assessorInOtherSchoolForExamModel.ExamDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 5. Assessor in Other School for Exam

                    #region 6. Parent Teachers Meeting (PTA)

                    if (dailyReportingModel.ParentTeachersMeeting != null)
                    {
                        IList<VTRParentTeachersMeeting> parentTeachersMeetings = Context.VTRParentTeachersMeetings.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        parentTeachersMeetings.ForEach((parentTeachersMeetingItem) =>
                        {
                            Context.Entry<VTRParentTeachersMeeting>(parentTeachersMeetingItem).State = EntityState.Deleted;
                        });

                        var parentTeachersMeetingModel = dailyReportingModel.ParentTeachersMeeting;

                        Context.VTRParentTeachersMeetings.Add(new VTRParentTeachersMeeting
                        {
                            VTRParentTeachersMeetingId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            VocationalParentsCount = parentTeachersMeetingModel.VocationalParentsCount,
                            OtherParentsCount = parentTeachersMeetingModel.OtherParentsCount,
                            PTADetails = parentTeachersMeetingModel.PTADetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 6. Parent Teachers Meeting (PTA)

                    #region 7. Community Home Visit

                    if (dailyReportingModel.CommunityHomeVisit != null)
                    {
                        IList<VTRCommunityHomeVisit> communityHomeVisits = Context.VTRCommunityHomeVisits.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        communityHomeVisits.ForEach((communityHomeVisitItem) =>
                        {
                            Context.Entry<VTRCommunityHomeVisit>(communityHomeVisitItem).State = EntityState.Deleted;
                        });

                        var communityHomeVisitModel = dailyReportingModel.CommunityHomeVisit;
                        Context.VTRCommunityHomeVisits.Add(new VTRCommunityHomeVisit
                        {
                            VTRCommunityHomeVisitId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            VocationalParentsCount = communityHomeVisitModel.VocationalParentsCount,
                            OtherParentsCount = communityHomeVisitModel.OtherParentsCount,
                            CommunityVisitDetails = communityHomeVisitModel.CommunityVisitDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 7. Community Home Visit

                    #region 8. Visit to Industry

                    if (dailyReportingModel.VisitToIndustries != null && dailyReportingModel.VisitToIndustries.Count > 0)
                    {
                        IList<VTRVisitToIndustry> visitToIndustres = Context.VTRVisitToIndustries.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        visitToIndustres.ForEach((visitToIndustryItem) =>
                        {
                            Context.Entry<VTRVisitToIndustry>(visitToIndustryItem).State = EntityState.Deleted;
                        });

                        foreach (var visitToIndustryModel in dailyReportingModel.VisitToIndustries)
                        {
                            Context.VTRVisitToIndustries.Add(new VTRVisitToIndustry
                            {
                                VTRVisitToIndustryId = Guid.NewGuid(),
                                VTDailyReportingId = dailyReporting.VTDailyReportingId,
                                IndustryVisitCount = visitToIndustryModel.IndustryVisitCount,
                                IndustryName = visitToIndustryModel.IndustryName,
                                IndustryContactPerson = visitToIndustryModel.IndustryContactPerson,
                                IndustryContactNumber = visitToIndustryModel.IndustryContactNumber,
                                IndustryVisitDetails = visitToIndustryModel.IndustryVisitDetails,
                                CreatedBy = dailyReporting.CreatedBy,
                                CreatedOn = dailyReporting.CreatedOn,
                                IsActive = true
                            });
                        }
                    }

                    #endregion 8. Visit to Industry

                    #region 9. Visit to Educational Institution

                    if (dailyReportingModel.VisitToEducationalInstitutions != null && dailyReportingModel.VisitToEducationalInstitutions.Count > 0)
                    {
                        IList<VTRVisitToEducationalInstitution> visitToEducationalInstitutions = Context.VTRVisitToEducationalInstitutions.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        visitToEducationalInstitutions.ForEach((visitToEducationalInstitutionItem) =>
                        {
                            Context.Entry<VTRVisitToEducationalInstitution>(visitToEducationalInstitutionItem).State = EntityState.Deleted;
                        });

                        foreach (var visitToEducationalInstitutionModel in dailyReportingModel.VisitToEducationalInstitutions)
                        {
                            Context.VTRVisitToEducationalInstitutions.Add(new VTRVisitToEducationalInstitution
                            {
                                VTRVisitToEducationalInstitutionId = Guid.NewGuid(),
                                VTDailyReportingId = dailyReporting.VTDailyReportingId,
                                EducationalInstituteVisitCount = visitToEducationalInstitutionModel.EducationalInstituteVisitCount,
                                EducationalInstitute = visitToEducationalInstitutionModel.EducationalInstitute,
                                InstituteContactPerson = visitToEducationalInstitutionModel.InstituteContactPerson,
                                InstituteContactNumber = visitToEducationalInstitutionModel.InstituteContactNumber,
                                InstituteVisitDetails = visitToEducationalInstitutionModel.InstituteVisitDetails,
                                CreatedBy = dailyReporting.CreatedBy,
                                CreatedOn = dailyReporting.CreatedOn,
                                IsActive = true
                            });
                        }
                    }

                    #endregion 9. Visit to Educational Institution

                    #region 10. Assignment from Vocational Department

                    if (dailyReportingModel.AssignmentFromVocationalDepartment != null)
                    {
                        IList<VTRAssignmentFromVocationalDepartment> assignmentFromVocationalDepartments = Context.VTRAssignmentFromVocationalDepartments.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        assignmentFromVocationalDepartments.ForEach((assignmentFromVocationalDepartmentItem) =>
                        {
                            Context.Entry<VTRAssignmentFromVocationalDepartment>(assignmentFromVocationalDepartmentItem).State = EntityState.Deleted;
                        });

                        var assignmentFromVocationalDepartmentModel = dailyReportingModel.AssignmentFromVocationalDepartment;

                        string assignmentPhotoPath = null;
                        if (assignmentFromVocationalDepartmentModel.AssignmentPhotoFile != null)
                            assignmentPhotoPath = assignmentFromVocationalDepartmentModel.AssignmentPhotoFile.FilePath;

                        Context.VTRAssignmentFromVocationalDepartments.Add(new VTRAssignmentFromVocationalDepartment
                        {
                            VTRAssignmentFromVocationalDepartmentId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            AssigmentNumber = assignmentFromVocationalDepartmentModel.AssigmentNumber,
                            AssignmentDetails = assignmentFromVocationalDepartmentModel.AssignmentDetails,
                            AssignmentPhoto = assignmentPhotoPath,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 10. Assignment from Vocational Department

                    #region 11. Teaching Non Vocational Subject

                    if (dailyReportingModel.TeachingNonVocationalSubject != null)
                    {
                        IList<VTRTeachingNonVocationalSubject> teachingNonVocationalSubjects = Context.VTRTeachingNonVocationalSubjects.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        teachingNonVocationalSubjects.ForEach((teachingNonVocationalSubjectItem) =>
                        {
                            Context.Entry<VTRTeachingNonVocationalSubject>(teachingNonVocationalSubjectItem).State = EntityState.Deleted;
                        });

                        var teachingNonVocationalSubjectModel = dailyReportingModel.TeachingNonVocationalSubject;

                        Context.VTRTeachingNonVocationalSubjects.Add(new VTRTeachingNonVocationalSubject
                        {
                            VTRTeachingNonVocationalSubjectId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            OtherClassTakenDetails = teachingNonVocationalSubjectModel.OtherClassTakenDetails,
                            OtherClassTime = teachingNonVocationalSubjectModel.OtherClassTime,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 11. Teaching Non Vocational Subject

                    #region 12. On Leave

                    if (dailyReportingModel.Leave != null)
                    {
                        IList<VTRLeave> leaves = Context.VTRLeaves.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        leaves.ForEach((leaveItem) =>
                        {
                            Context.Entry<VTRLeave>(leaveItem).State = EntityState.Deleted;
                        });

                        IList<VTDailyReporting> vtReportingForLeaves = Context.VTDailyReportings.Where(v => v.VTId == dailyReporting.VTId && v.ReportType == "38" && v.ReportingDate.Month == dailyReporting.ReportingDate.Month).ToList();

                        var leaveModel = dailyReportingModel.Leave;
                        leaveModel.LeaveModeId = (vtReportingForLeaves.Count == 1) ? "2" : "3";

                        Context.VTRLeaves.Add(new VTRLeave
                        {
                            VTRLeaveId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            LeaveTypeId = leaveModel.LeaveTypeId,
                            LeaveModeId = leaveModel.LeaveModeId,
                            LeaveApprovalStatus = leaveModel.LeaveApprovalStatus,
                            LeaveApprover = leaveModel.LeaveApprover,
                            LeaveReason = leaveModel.LeaveReason,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 12. On Leave

                    #region 13. Holiday/School Off

                    if (dailyReportingModel.Holiday != null)
                    {
                        IList<VTRHoliday> holidays = Context.VTRHolidays.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        holidays.ForEach((holidayItem) =>
                        {
                            Context.Entry<VTRHoliday>(holidayItem).State = EntityState.Deleted;
                        });

                        var holidayModel = dailyReportingModel.Holiday;

                        Context.VTRHolidays.Add(new VTRHoliday
                        {
                            VTRHolidayId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            HolidayTypeId = holidayModel.HolidayTypeId,
                            HolidayDetails = holidayModel.HolidayDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 13. Holiday/School Off

                    #region 14. Observation Day

                    if (dailyReportingModel.ObservationDay != null)
                    {
                        IList<VTRObservationDay> observationDays = Context.VTRObservationDays.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                        observationDays.ForEach((observationDayItem) =>
                        {
                            Context.Entry<VTRObservationDay>(observationDayItem).State = EntityState.Deleted;
                        });

                        var observationDayModel = dailyReportingModel.ObservationDay;

                        Context.VTRObservationDays.Add(new VTRObservationDay
                        {
                            VTRObservationDayId = Guid.NewGuid(),
                            VTDailyReportingId = dailyReporting.VTDailyReportingId,
                            VTId = observationDayModel.VTId,
                            ClassId = observationDayModel.ClassId,
                            StudentId = observationDayModel.StudentId,
                            IsPresent = observationDayModel.IsPresent,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 14. Observation Day

                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVTDailyReportingDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtDailyReportingId)
        {
            VTDailyReporting vtDailyReporting = this.Context.VTDailyReportings.FirstOrDefault(v => v.VTDailyReportingId == vtDailyReportingId);

            if (vtDailyReporting != null)
            {
                Context.Entry<VTDailyReporting>(vtDailyReporting).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Daily Reporting by Type
        /// </summary>
        /// <param name="vtDailyReportingModel"></param>
        /// <returns>List of error messages for daily reporting submitted by VT</returns>
        public List<string> CheckVTDailyReportingExistByName(VTDailyReportingModel dailyReportingModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                VTDailyReporting dailyReporting = this.Context.VTDailyReportings.FirstOrDefault(v => v.VTId == dailyReportingModel.VTId && v.ReportingDate.Date == dailyReportingModel.ReportingDate.Date);

                if (dailyReporting != null)
                {
                    IList<VTRWorkingDayType> workingDayTypes = Context.VTRWorkingDayTypes.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                    if (dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Count > 0)
                    {
                        dailyReportingModel.WorkingDayTypeIds.ForEach(workTypeId =>
                        {
                            var workTypeItem = workingDayTypes.FirstOrDefault(v => v.WorkingTypeId == workTypeId);
                            if (workTypeItem != null)
                            {
                                //1. Teaching Vocational Education
                                if (workTypeItem.WorkingTypeId == "101")
                                {
                                    IList<VTRTeachingVocationalEducation> teachingVocationalEducations = Context.VTRTeachingVocationalEducations.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).ToList();

                                    if (teachingVocationalEducations.Count > 0)
                                    {
                                        dailyReportingModel.TeachingVocationalEducations.ForEach(classTaughtItem =>
                                        {
                                            var teachingVocationalEducationItem = teachingVocationalEducations.FirstOrDefault(tve => tve.ClassTaughtId == classTaughtItem.ClassTaughtId && tve.SectionTaughtId == classTaughtItem.ClassSectionIds);

                                            if (teachingVocationalEducationItem != null)
                                            {
                                                errorMessageList.Add("Selected class details is already submitted");
                                            }
                                        });
                                    }
                                }

                                //2. Training Of Teacher
                                if (workTypeItem.WorkingTypeId == "102")
                                {
                                    var trainingOfTeacherItem = Context.VTRTrainingOfTeachers.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (trainingOfTeacherItem != null)
                                    {
                                        errorMessageList.Add("'Training Of Teacher' is already submitted");
                                    }
                                }

                                //3. On Job Training Coordination
                                if (workTypeItem.WorkingTypeId == "103")
                                {
                                    var onJobTrainingCoordinationItem = Context.VTROnJobTrainingCoordinations.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (onJobTrainingCoordinationItem != null)
                                    {
                                        errorMessageList.Add("'On Job Training Coordination' is already submitted");
                                    }
                                }

                                //4. Assessor In Other School
                                if (workTypeItem.WorkingTypeId == "104")
                                {
                                    var assessorInOtherSchoolForExamItem = Context.VTRAssessorInOtherSchoolForExams.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (assessorInOtherSchoolForExamItem != null)
                                    {
                                        errorMessageList.Add("'Assessor In Other School For Exam' is already submitted");
                                    }
                                }

                                //5. Parent Teacher Meeting
                                if (workTypeItem.WorkingTypeId == "106")
                                {
                                    var parentTeachersMeetingItem = Context.VTRParentTeachersMeetings.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (parentTeachersMeetingItem != null)
                                    {
                                        errorMessageList.Add("'Parent Teacher Meeting' is already submitted");
                                    }
                                }

                                //6. Community Home Visit
                                if (workTypeItem.WorkingTypeId == "107")
                                {
                                    var communityHomeVisitItem = Context.VTRCommunityHomeVisits.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (communityHomeVisitItem != null)
                                    {
                                        errorMessageList.Add("'Community Home Visit' is already submitted");
                                    }
                                }

                                //7. Industry Visit
                                if (workTypeItem.WorkingTypeId == "108")
                                {
                                    var visitToIndustryItem = Context.VTRVisitToIndustries.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (visitToIndustryItem != null)
                                    {
                                        errorMessageList.Add("'Industry Visit' is already submitted");
                                    }
                                }

                                //8. Visit to Educational Institute
                                if (workTypeItem.WorkingTypeId == "109")
                                {
                                    var visitToEducationalInstitutionItem = Context.VTRVisitToEducationalInstitutions.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (visitToEducationalInstitutionItem != null)
                                    {
                                        errorMessageList.Add("'Visit to Educational Institute' is already submitted");
                                    }
                                }

                                //9. Assignment From Vocational Department
                                if (workTypeItem.WorkingTypeId == "110")
                                {
                                    var assignmentFromVocationalDepartmentItem = Context.VTRAssignmentFromVocationalDepartments.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (assignmentFromVocationalDepartmentItem != null)
                                    {
                                        errorMessageList.Add("'Assignment From Vocational Department' is already submitted");
                                    }
                                }

                                //10. Teaching Non Vocational Subject
                                if (workTypeItem.WorkingTypeId == "111")
                                {
                                    var teachingNonVocationalSubjectItem = Context.VTRTeachingNonVocationalSubjects.Where(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId).FirstOrDefault();

                                    if (teachingNonVocationalSubjectItem != null)
                                    {
                                        errorMessageList.Add("'Teaching Non Vocational Subject' is already submitted");
                                    }
                                }

                                //11. School Event/ Celebration
                                if (workTypeId == "105")
                                {
                                    errorMessageList.Add("'School Event/ Celebration' is already submitted");
                                }

                                //12. Work Assigned By Head Master
                                if (workTypeId == "112")
                                {
                                    errorMessageList.Add("'Work Assigned By Head Master' is already submitted");
                                }

                                //13. School Exam Duty
                                if (workTypeId == "113")
                                {
                                    errorMessageList.Add("'School Exam Duty' is already submitted");
                                }

                                //14. Other Work
                                if (workTypeId == "114")
                                {
                                    errorMessageList.Add("'Other Work' is already submitted");
                                }
                            }
                        });
                    }
                    else if (dailyReporting.ReportType == "37" && workingDayTypes.Count > 0)
                    {
                        //10. Working days
                        errorMessageList.Add("Daily reporting is already submitted for Working day");
                    }
                }

                //11. On Leave
                if (dailyReportingModel.ReportType == "38")
                {
                    if (dailyReporting != null)
                    {
                        var leaveItem = Context.VTRLeaves.FirstOrDefault(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId);

                        if (leaveItem != null)
                        {
                            errorMessageList.Add("'On Leave' is already submitted");
                        }
                    }

                    string valResult = ValidateGuestLEctoreOrFieldVisitSubmitted(dailyReportingModel.VTId, dailyReportingModel.ReportingDate, "On Leave");
                    if (!string.IsNullOrEmpty(valResult))
                    {
                        errorMessageList.Add(valResult);
                    }
                }

                //12. Holiday / School Off
                if (dailyReportingModel.ReportType == "40")
                {
                    if (dailyReporting != null)
                    {
                        var holidayItem = Context.VTRHolidays.FirstOrDefault(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId);

                        if (holidayItem != null)
                        {
                            errorMessageList.Add("'Holiday / School Off' is already submitted");
                        }
                    }

                    string valResult = ValidateGuestLEctoreOrFieldVisitSubmitted(dailyReportingModel.VTId, dailyReportingModel.ReportingDate, "Holiday / School Off");
                    if (!string.IsNullOrEmpty(valResult))
                    {
                        errorMessageList.Add(valResult);
                    }
                }

                //13. Observation Day
                if (dailyReportingModel.ReportType == "123")
                {
                    if (dailyReporting != null)
                    {
                        var observationDayItem = Context.VTRObservationDays.FirstOrDefault(v => v.VTDailyReportingId == dailyReporting.VTDailyReportingId);

                        if (observationDayItem != null)
                        {
                            errorMessageList.Add("'Observation Day' is already submitted");
                        }
                    }

                    string valResult = ValidateGuestLEctoreOrFieldVisitSubmitted(dailyReportingModel.VTId, dailyReportingModel.ReportingDate, "Observation Day");
                    if (!string.IsNullOrEmpty(valResult))
                    {
                        errorMessageList.Add(valResult);
                    }
                }

                //14. Allowed 7 days Back Dated Reporting from Today
                if (!(dailyReportingModel.ReportingDate.Date >= Constants.GetCurrentDateTime.AddDays(-1 * Constants.BackDatedReportingDays).Date))
                {
                    errorMessageList.Add(string.Format("Only allowed {0} days Back Dated Reporting from Today", Constants.BackDatedReportingDays));
                }

                //15. Avoid daily VT Reporting on Sunday
                if (string.Equals(dailyReportingModel.ReportingDate.Date.DayOfWeek.ToString(), "Sunday"))
                {
                    errorMessageList.Add("User cannot submit the VT Daily Reporting on Sunday");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVTDailyReportingExistByName", ex);
            }

            return errorMessageList;
        }

        private string ValidateGuestLEctoreOrFieldVisitSubmitted(Guid vtId, DateTime reportingDate, string reportType)
        {
            string valResults = string.Empty;

            var guestLectureItem = Context.VTGuestLectureConducteds.FirstOrDefault(v => v.VTId == vtId && v.ReportingDate.Date == reportingDate.Date);

            var fieldVisitItem = Context.VTFieldIndustryVisitConducteds.FirstOrDefault(v => v.VTId == vtId && v.ReportingDate.Date == reportingDate.Date);

            if (guestLectureItem != null || fieldVisitItem != null)
            {
                valResults = string.Format("You cannot submit '{0}' on this date because you have already submitted the Field Visit/Guest Lecture.", reportType);
            }

            return valResults;
        }

        /// <summary>}
        /// List of VTDailyReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTDailyReportingViewModel> GetVTDailyReportingsByCriteria(SearchVTDailyReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTDailyReportingViewModels.FromSql<VTDailyReportingViewModel>("CALL GetVTDailyReportingsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of VTWorkingTypes by dailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public IList<string> GetWorkingTypesById(Guid dailyReportingId)
        {
            var workingTypes = this.Context.VTRWorkingDayTypes.Where(s => s.VTDailyReportingId == dailyReportingId).Select(cs => cs.WorkingTypeId).ToList();

            return workingTypes;
        }

        public VTDailyReportingModel GetVTDailyReportingDetailsById(Guid dailyReportingId)
        {
            VTDailyReportingModel dailyReportingModel = null;

            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "dailyReportingId", MySqlDbType = MySqlDbType.Guid, Value = dailyReportingId };

            //Context.VTDailyReportingModels.FromSql<VTDailyReportingModel>("CALL GetVTDailyReportingDetailsById (@dailyReportingId)", sqlParams).ToList();

            return dailyReportingModel;
        }

        public IList<VTRTeachingVocationalEducationModel> GetTeachingVocationalEducationsById(Guid dailyReportingId)
        {
            var teachingVocationalEducationModels = new List<VTRTeachingVocationalEducationModel>();

            var teachingVocationalEducationItems = Context.VTRTeachingVocationalEducations.Where(v => v.VTDailyReportingId == dailyReportingId).ToList();

            if (teachingVocationalEducationItems.Count > 0)
            {
                teachingVocationalEducationItems.ForEach(tve =>
                {
                    List<string> activityTypeIds = Context.VTRActivityTypes.Where(v => v.VTRTeachingVocationalEducationId == tve.VTRTeachingVocationalEducationId).Select(a => a.ActivityTypeId).ToList();

                    List<string> reasonOfNotConductingId = Context.VTRReasonOfNotConductingTheClasses.Where(v => v.VTRTeachingVocationalEducationId == tve.VTRTeachingVocationalEducationId).Select(a => a.ReasonTypeId).ToList();

                    MySqlParameter[] sqlParams = new MySqlParameter[1];
                    sqlParams[0] = new MySqlParameter { ParameterName = "teachingVocationalEducationId", MySqlDbType = MySqlDbType.Guid, Value = tve.VTRTeachingVocationalEducationId };

                    var unitSessions = Context.UnitSessionsModels.FromSql<UnitSessionsModel>("CALL GetUnitSessionsTaughtsByVTDRId (@teachingVocationalEducationId)", sqlParams).ToList();
                    var studentAttendances = Context.StudentAttendanceModels.FromSql<StudentAttendanceModel>("CALL GetStudentAttendancesByVTDRId (@teachingVocationalEducationId)", sqlParams).ToList();

                    teachingVocationalEducationModels.Add(new VTRTeachingVocationalEducationModel
                    {
                        SequenceNo = tve.SequenceNo,
                        DidYouTeachToday = tve.IsTeachToday,
                        ClassTaughtId = tve.ClassTaughtId,
                        SectionTaughtId = tve.SectionTaughtId,
                        ClassTypeId = tve.ClassTypeId,
                        ClassTime = tve.ClassTime,
                        ClassPictureFile = new FileUploadModel { FilePath = tve.ClassPicture },
                        LessonPlanPictureFile = new FileUploadModel { FilePath = tve.LessonPlanPicture },
                        ReasonDetails = tve.ReasonDetails,
                        ClassSectionIds = tve.SectionTaughtId,
                        ActivityTypeIds = activityTypeIds,
                        ReasonOfNotConductingTheClassIds = reasonOfNotConductingId,
                        UnitSessionsModels = unitSessions,
                        StudentAttendances = studentAttendances,
                    });
                });
            }

            return teachingVocationalEducationModels;
        }

        public VTRTrainingOfTeacherModel GetTrainingOfTeacherById(Guid dailyReportingId)
        {
            VTRTrainingOfTeacherModel trainingOfTeacherModel = null;

            var trainingOfTeacherItem = Context.VTRTrainingOfTeachers.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (trainingOfTeacherItem != null)
            {
                var trainingTopicIds = Context.VTRTrainingTopics.Where(t => t.VTRTrainingOfTeacherId == trainingOfTeacherItem.VTRTrainingOfTeacherId).Select(v => v.TrainingTopicId).ToList();

                trainingOfTeacherModel = new VTRTrainingOfTeacherModel
                {
                    TrainingTypeId = trainingOfTeacherItem.TrainingTypeId,
                    TrainingBy = trainingOfTeacherItem.TrainingBy,
                    TrainingDetails = trainingOfTeacherItem.TrainingDetails,
                    TrainingTopicIds = trainingTopicIds,
                };
            }

            return trainingOfTeacherModel;
        }

        public VTROnJobTrainingCoordinationModel GetOnJobTrainingCoordinationById(Guid dailyReportingId)
        {
            VTROnJobTrainingCoordinationModel onJobTrainingCoordinationModel = null;

            var onJobTrainingCoordinationItem = Context.VTROnJobTrainingCoordinations.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (onJobTrainingCoordinationItem != null)
            {
                onJobTrainingCoordinationModel = new VTROnJobTrainingCoordinationModel
                {
                    OnJobTrainingActivityId = onJobTrainingCoordinationItem.OnJobTrainingActivityId,
                    IndustryName = onJobTrainingCoordinationItem.IndustryName,
                    IndustryContactPerson = onJobTrainingCoordinationItem.IndustryContactPerson,
                    IndustryContactNumber = onJobTrainingCoordinationItem.IndustryContactNumber,
                    OJTActivityDetails = onJobTrainingCoordinationItem.OJTActivityDetails
                };
            }

            return onJobTrainingCoordinationModel;
        }

        public VTRAssessorInOtherSchoolForExamModel GetAssessorInOtherSchoolForExamById(Guid dailyReportingId)
        {
            VTRAssessorInOtherSchoolForExamModel assessorInOtherSchoolForExamModel = null;

            var assessorInOtherSchoolForExamItem = Context.VTRAssessorInOtherSchoolForExams.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (assessorInOtherSchoolForExamItem != null)
            {
                assessorInOtherSchoolForExamModel = new VTRAssessorInOtherSchoolForExamModel
                {
                    SchoolName = assessorInOtherSchoolForExamItem.SchoolName,
                    Udise = assessorInOtherSchoolForExamItem.Udise,
                    ClassId = assessorInOtherSchoolForExamItem.ClassId,
                    BoysPresent = assessorInOtherSchoolForExamItem.BoysPresent,
                    GirlsPresent = assessorInOtherSchoolForExamItem.GirlsPresent,
                    ExamDetails = assessorInOtherSchoolForExamItem.ExamDetails
                };
            }

            return assessorInOtherSchoolForExamModel;
        }

        public VTRParentTeachersMeetingModel GetParentTeachersMeetingById(Guid dailyReportingId)
        {
            VTRParentTeachersMeetingModel parentTeachersMeetingModel = null;

            var parentTeachersMeetingItem = Context.VTRParentTeachersMeetings.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (parentTeachersMeetingItem != null)
            {
                parentTeachersMeetingModel = new VTRParentTeachersMeetingModel
                {
                    VocationalParentsCount = parentTeachersMeetingItem.VocationalParentsCount,
                    OtherParentsCount = parentTeachersMeetingItem.OtherParentsCount,
                    PTADetails = parentTeachersMeetingItem.PTADetails
                };
            }

            return parentTeachersMeetingModel;
        }

        public VTRCommunityHomeVisitModel GetCommunityHomeVisitById(Guid dailyReportingId)
        {
            VTRCommunityHomeVisitModel communityHomeVisitModel = null;

            var communityHomeVisitItem = Context.VTRCommunityHomeVisits.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (communityHomeVisitItem != null)
            {
                communityHomeVisitModel = new VTRCommunityHomeVisitModel
                {
                    VocationalParentsCount = communityHomeVisitItem.VocationalParentsCount,
                    OtherParentsCount = communityHomeVisitItem.OtherParentsCount,
                    CommunityVisitDetails = communityHomeVisitItem.CommunityVisitDetails
                };
            }

            return communityHomeVisitModel;
        }

        public IList<VTRVisitToIndustryModel> GetVisitToIndustriesById(Guid dailyReportingId)
        {
            var visitToIndustryModels = new List<VTRVisitToIndustryModel>();

            var visitToIndustryItems = Context.VTRVisitToIndustries.Where(v => v.VTDailyReportingId == dailyReportingId).ToList();

            if (visitToIndustryItems.Count > 0)
            {
                visitToIndustryItems.ForEach(vti =>
                {
                    visitToIndustryModels.Add(new VTRVisitToIndustryModel
                    {
                        IndustryVisitCount = vti.IndustryVisitCount,
                        IndustryName = vti.IndustryName,
                        IndustryContactPerson = vti.IndustryContactPerson,
                        IndustryContactNumber = vti.IndustryContactNumber,
                        IndustryVisitDetails = vti.IndustryVisitDetails,
                    });
                });
            }

            return visitToIndustryModels;
        }

        public IList<VTRVisitToEducationalInstitutionModel> GetVisitToEducationalInstitutionsById(Guid dailyReportingId)
        {
            var visitToEducationalInstitutionModels = new List<VTRVisitToEducationalInstitutionModel>();

            var visitToEducationalInstitutionItems = Context.VTRVisitToEducationalInstitutions.Where(v => v.VTDailyReportingId == dailyReportingId).ToList();

            if (visitToEducationalInstitutionItems.Count > 0)
            {
                visitToEducationalInstitutionItems.ForEach(vei =>
                {
                    visitToEducationalInstitutionModels.Add(new VTRVisitToEducationalInstitutionModel
                    {
                        EducationalInstituteVisitCount = vei.EducationalInstituteVisitCount,
                        EducationalInstitute = vei.EducationalInstitute,
                        InstituteContactPerson = vei.InstituteContactPerson,
                        InstituteContactNumber = vei.InstituteContactNumber,
                        InstituteVisitDetails = vei.InstituteVisitDetails,
                    });
                });
            }

            return visitToEducationalInstitutionModels;
        }

        public VTRAssignmentFromVocationalDepartmentModel GetAssignmentFromVocationalDepartmentById(Guid dailyReportingId)
        {
            VTRAssignmentFromVocationalDepartmentModel assignmentFromVocationalDepartmentModel = null;

            var assignmentFromVocationalDepartmentItem = Context.VTRAssignmentFromVocationalDepartments.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (assignmentFromVocationalDepartmentItem != null)
            {
                assignmentFromVocationalDepartmentModel = new VTRAssignmentFromVocationalDepartmentModel
                {
                    AssigmentNumber = assignmentFromVocationalDepartmentItem.AssigmentNumber,
                    AssignmentDetails = assignmentFromVocationalDepartmentItem.AssignmentDetails,
                    AssignmentPhotoFile = new FileUploadModel { FilePath = assignmentFromVocationalDepartmentItem.AssignmentPhoto }
                };
            }

            return assignmentFromVocationalDepartmentModel;
        }

        public VTRTeachingNonVocationalSubjectModel GetTeachingNonVocationalSubjectById(Guid dailyReportingId)
        {
            VTRTeachingNonVocationalSubjectModel teachingNonVocationalSubjectModel = null;

            var teachingNonVocationalSubjectItem = Context.VTRTeachingNonVocationalSubjects.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (teachingNonVocationalSubjectItem != null)
            {
                teachingNonVocationalSubjectModel = new VTRTeachingNonVocationalSubjectModel
                {
                    OtherClassTakenDetails = teachingNonVocationalSubjectItem.OtherClassTakenDetails,
                    OtherClassTime = teachingNonVocationalSubjectItem.OtherClassTime
                };
            }

            return teachingNonVocationalSubjectModel;
        }

        public LeaveModel GetLeaveById(Guid dailyReportingId)
        {
            LeaveModel leaveModel = null;

            var leaveItem = Context.VTRLeaves.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (leaveItem != null)
            {
                leaveModel = new LeaveModel
                {
                    LeaveTypeId = leaveItem.LeaveTypeId,
                    LeaveModeId = leaveItem.LeaveModeId,
                    LeaveApprovalStatus = leaveItem.LeaveApprovalStatus,
                    LeaveApprover = leaveItem.LeaveApprover,
                    LeaveReason = leaveItem.LeaveReason
                };
            }

            return leaveModel;
        }

        public HolidayModel GetHolidayById(Guid dailyReportingId)
        {
            HolidayModel holidayModel = null;

            var holidayItem = Context.VTRHolidays.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (holidayItem != null)
            {
                holidayModel = new HolidayModel
                {
                    HolidayTypeId = holidayItem.HolidayTypeId,
                    HolidayDetails = holidayItem.HolidayDetails
                };
            }

            return holidayModel;
        }

        public VTRObservationDayModel GetObservationDayById(Guid dailyReportingId)
        {
            VTRObservationDayModel observationDayModel = null;

            var observationDayItem = Context.VTRObservationDays.FirstOrDefault(v => v.VTDailyReportingId == dailyReportingId);

            if (observationDayItem != null)
            {
                observationDayModel = new VTRObservationDayModel
                {
                    ClassId = observationDayItem.ClassId,
                    StudentId = observationDayItem.StudentId,
                    IsPresent = observationDayItem.IsPresent,
                    Remarks = observationDayItem.Remarks
                };
            }

            return observationDayModel;
        }
    }
}