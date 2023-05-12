using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the VTDailyReporting entity
    /// </summary>
    public class VTDailyReportingManager : GenericManager<VTDailyReportingModel>, IVTDailyReportingManager
    {
        private readonly IVTDailyReportingRepository vtDailyReportingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtDailyReporting manager.
        /// </summary>
        /// <param name="vtDailyReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTDailyReportingManager(IVTDailyReportingRepository _vtDailyReportingRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtDailyReportingRepository = _vtDailyReportingRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTDailyReportings
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTDailyReportingModel> GetVTDailyReportings()
        {
            var vtDailyReportings = this.vtDailyReportingRepository.GetVTDailyReportings();

            IList<VTDailyReportingModel> vtDailyReportingModels = new List<VTDailyReportingModel>();
            vtDailyReportings.ForEach((user) => vtDailyReportingModels.Add(user.ToModel()));

            return vtDailyReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTDailyReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTDailyReportingModel> GetVTDailyReportingsByName(string vtDailyReportingName)
        {
            var vtDailyReportings = this.vtDailyReportingRepository.GetVTDailyReportingsByName(vtDailyReportingName);

            IList<VTDailyReportingModel> vtDailyReportingModels = new List<VTDailyReportingModel>();
            vtDailyReportings.ForEach((user) => vtDailyReportingModels.Add(user.ToModel()));

            return vtDailyReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        public VTDailyReportingModel GetVTDailyReportingById(Guid vtDailyReportingId)
        {
            VTDailyReportingModel dailyReportingModel = null;

            VTDailyReporting dailyReporting = this.vtDailyReportingRepository.GetVTDailyReportingById(vtDailyReportingId);

            if (dailyReporting != null)
            {
                dailyReportingModel = dailyReporting.ToModel();

                dailyReportingModel.WorkingDayTypeIds = this.vtDailyReportingRepository.GetWorkingTypesById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.TeachingVocationalEducations = this.vtDailyReportingRepository.GetTeachingVocationalEducationsById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.TrainingOfTeacher = this.vtDailyReportingRepository.GetTrainingOfTeacherById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.OnJobTrainingCoordination = this.vtDailyReportingRepository.GetOnJobTrainingCoordinationById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.AssessorInOtherSchoolForExam = this.vtDailyReportingRepository.GetAssessorInOtherSchoolForExamById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.ParentTeachersMeeting = this.vtDailyReportingRepository.GetParentTeachersMeetingById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.CommunityHomeVisit = this.vtDailyReportingRepository.GetCommunityHomeVisitById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.VisitToIndustries = this.vtDailyReportingRepository.GetVisitToIndustriesById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.VisitToEducationalInstitutions = this.vtDailyReportingRepository.GetVisitToEducationalInstitutionsById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.AssignmentFromVocationalDepartment = this.vtDailyReportingRepository.GetAssignmentFromVocationalDepartmentById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.TeachingNonVocationalSubject = this.vtDailyReportingRepository.GetTeachingNonVocationalSubjectById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.Leave = this.vtDailyReportingRepository.GetLeaveById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.Holiday = this.vtDailyReportingRepository.GetHolidayById(dailyReporting.VTDailyReportingId);
                dailyReportingModel.ObservationDay = this.vtDailyReportingRepository.GetObservationDayById(dailyReporting.VTDailyReportingId);
            }

            return dailyReportingModel;
        }

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId using async
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        public async Task<VTDailyReportingModel> GetVTDailyReportingByIdAsync(Guid vtDailyReportingId)
        {
            var vtDailyReporting = await this.vtDailyReportingRepository.GetVTDailyReportingByIdAsync(vtDailyReportingId);

            return (vtDailyReporting != null) ? vtDailyReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTDailyReporting entity
        /// </summary>
        /// <param name="vtDailyReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTDailyReportingDetails(VTDailyReportingModel vtDailyReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VTDailyReporting vtDailyReporting = null;

                //Validate model data
                vtDailyReportingModel = vtDailyReportingModel.GetModelValidationErrors<VTDailyReportingModel>();

                if (vtDailyReportingModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vtDailyReportingModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                string authUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtDailyReportingModel.RequestType == RequestType.Edit)
                {
                    vtDailyReporting = this.vtDailyReportingRepository.GetVTDailyReportingById(vtDailyReportingModel.VTDailyReportingId);
                }
                else
                {
                    vtDailyReporting = this.vtDailyReportingRepository.GetVTDailyReportingById(vtDailyReportingModel.VTId, vtDailyReportingModel.ReportingDate);

                    if (vtDailyReporting == null)
                    {
                        vtDailyReporting = new VTDailyReporting();
                        vtDailyReporting.VTDailyReportingId = Guid.NewGuid();
                    }
                    else
                    {
                        vtDailyReportingModel.RequestType = RequestType.Edit;
                    }
                }

                if (vtDailyReportingModel.ErrorMessages.Count == 0)
                {
                    List<string> validationMessages = this.vtDailyReportingRepository.CheckVTDailyReportingExistByName(vtDailyReportingModel);

                    if (validationMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", validationMessages));
                    }

                    VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByCurrentAYVTId(vtDailyReportingModel.VTId);
                    if (vtSchoolSectors == null)
                    {
                        response.Errors.Add(Constants.NotMapVTSchoolSectorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vtDailyReporting.AuthUserId = authUserId;
                    vtDailyReporting = vtDailyReportingModel.FromModel(vtDailyReporting);

                    if (vtDailyReportingModel.RequestType == RequestType.New)
                    {
                        VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByVTId(vtDailyReportingModel.VTId);
                        if (vtSchoolSectors != null)
                            vtDailyReporting.VTSchoolSectorId = vtSchoolSectors.VTSchoolSectorId;
                    }

                    if (vtDailyReportingModel.TeachingVocationalEducations != null && vtDailyReportingModel.TeachingVocationalEducations.Count > 0)
                    {
                        if (vtDailyReportingModel.TeachingVocationalEducations[0].ClassPictureFile != null)
                        {
                            vtDailyReportingModel.TeachingVocationalEducations[0].ClassPictureFile.ContentId = vtDailyReporting.VTDailyReportingId;
                            var classPictureFile = UtilityManager.UploadFileInPostByMobile(vtDailyReportingModel.TeachingVocationalEducations[0].ClassPictureFile);

                            vtDailyReportingModel.TeachingVocationalEducations[0].ClassPictureFile.FilePath = classPictureFile.FilePath;

                            if (classPictureFile.Exception != null)
                                Logging.ErrorManager.Instance.WriteErrorLogsInFile(classPictureFile.Exception);
                        }

                        if (vtDailyReportingModel.TeachingVocationalEducations[0].LessonPlanPictureFile != null)
                        {
                            vtDailyReportingModel.TeachingVocationalEducations[0].LessonPlanPictureFile.ContentId = vtDailyReporting.VTDailyReportingId;
                            var lessonPlanPictureFile = UtilityManager.UploadFileInPostByMobile(vtDailyReportingModel.TeachingVocationalEducations[0].LessonPlanPictureFile);

                            vtDailyReportingModel.TeachingVocationalEducations[0].LessonPlanPictureFile.FilePath = lessonPlanPictureFile.FilePath;

                            if (lessonPlanPictureFile.Exception != null)
                                Logging.ErrorManager.Instance.WriteErrorLogsInFile(lessonPlanPictureFile.Exception);
                        }
                    }

                    if (vtDailyReportingModel.AssignmentFromVocationalDepartment != null)
                    {
                        if (vtDailyReportingModel.AssignmentFromVocationalDepartment.AssignmentPhotoFile != null)
                        {
                            vtDailyReportingModel.AssignmentFromVocationalDepartment.AssignmentPhotoFile.ContentId = vtDailyReporting.VTDailyReportingId;
                            var assignmentPhotoFile = UtilityManager.UploadFileInPostByMobile(vtDailyReportingModel.AssignmentFromVocationalDepartment.AssignmentPhotoFile);

                            vtDailyReportingModel.AssignmentFromVocationalDepartment.AssignmentPhotoFile.FilePath = assignmentPhotoFile.FilePath;

                            if (assignmentPhotoFile.Exception != null)
                                Logging.ErrorManager.Instance.WriteErrorLogsInFile(assignmentPhotoFile.Exception);
                        }
                    }

                    //Save Or Update vtDailyReporting details
                    bool isSaved = this.vtDailyReportingRepository.SaveOrUpdateVTDailyReportingDetails(vtDailyReporting, vtDailyReportingModel);

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateVTDailyReportingDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtDailyReportingId)
        {
            return this.vtDailyReportingRepository.DeleteById(vtDailyReportingId);
        }

        /// <summary>
        /// Check duplicate Daily Reporting by Type
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns>List of error messages for daily reporting submitted by VT</returns>
        public List<string> CheckVTDailyReportingExistByName(VTDailyReportingModel dailyReportingModel)
        {
            return this.vtDailyReportingRepository.CheckVTDailyReportingExistByName(dailyReportingModel);
        }

        /// <summary>}
        /// List of VTDailyReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTDailyReportingViewModel> GetVTDailyReportingsByCriteria(SearchVTDailyReportingModel searchModel)
        {
            return this.vtDailyReportingRepository.GetVTDailyReportingsByCriteria(searchModel);
        }

        /// <summary>
        /// Approved VT Daily Reporting
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> ApprovedVTDailyReporting(VTDailyReportingApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            VTDailyReporting vtDailyReporting = this.vtDailyReportingRepository.GetVTDailyReportingById(vtApprovalRequest.VTDailyReportingId);

            vtDailyReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
            vtDailyReporting.ApprovalStatus = vtApprovalRequest.ApprovalStatus;
            vtDailyReporting.ApprovedDate = Constants.GetCurrentDateTime;
            vtDailyReporting.RequestType = RequestType.Edit;
            vtDailyReporting.SetAuditValues(RequestType.Edit);

            bool results = this.vtDailyReportingRepository.SaveOrUpdateVTDailyReportingDetails(vtDailyReporting, null);

            if (results)
            {
                response.Result = "Success";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
            }

            return response;
        }
    }
}