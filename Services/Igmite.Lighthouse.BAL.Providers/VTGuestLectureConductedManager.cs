using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.EmailServices;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the VTGuestLectureConducted entity
    /// </summary>
    public class VTGuestLectureConductedManager : GenericManager<VTGuestLectureConductedModel>, IVTGuestLectureConductedManager
    {
        private readonly IVTGuestLectureConductedRepository vtGuestLectureConductedRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes the vtGuestLectureConducted manager.
        /// </summary>
        /// <param name="vtGuestLectureConductedRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTGuestLectureConductedManager(IVTGuestLectureConductedRepository _vtGuestLectureConductedRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository, IEmailSender _emailSender)
        {
            this.vtGuestLectureConductedRepository = _vtGuestLectureConductedRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
            this.emailSender = _emailSender;
        }

        /// <summary>
        /// Get list of VTGuestLectureConducteds
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTGuestLectureConductedModel> GetVTGuestLectureConducteds()
        {
            var vtGuestLectureConducteds = this.vtGuestLectureConductedRepository.GetVTGuestLectureConducteds();

            IList<VTGuestLectureConductedModel> vtGuestLectureConductedModels = new List<VTGuestLectureConductedModel>();
            vtGuestLectureConducteds.ForEach((user) => vtGuestLectureConductedModels.Add(user.ToModel()));

            return vtGuestLectureConductedModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTGuestLectureConducteds by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTGuestLectureConductedModel> GetVTGuestLectureConductedsByName(string vtGuestLectureConductedName)
        {
            var vtGuestLectureConducteds = this.vtGuestLectureConductedRepository.GetVTGuestLectureConductedsByName(vtGuestLectureConductedName);

            IList<VTGuestLectureConductedModel> vtGuestLectureConductedModels = new List<VTGuestLectureConductedModel>();
            vtGuestLectureConducteds.ForEach((user) => vtGuestLectureConductedModels.Add(user.ToModel()));

            return vtGuestLectureConductedModels.AsQueryable();
        }

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        public VTGuestLectureConductedModel GetVTGuestLectureConductedById(Guid vtGuestLectureId)
        {
            VTGuestLectureConductedModel guestLectureModel = null;

            VTGuestLectureConducted guestLectureItem = this.vtGuestLectureConductedRepository.GetVTGuestLectureConductedById(vtGuestLectureId);

            if (guestLectureItem != null)
            {
                guestLectureModel = guestLectureItem.ToModel();

                //guestLectureModel.SectionIds = this.vtGuestLectureConductedRepository.GetVTGSectionsByGuestLectureId(guestLectureItem.VTGuestLectureId);

                guestLectureModel.MethodologyIds = this.vtGuestLectureConductedRepository.GetVTGMethodologiesByGuestLectureId(guestLectureItem.VTGuestLectureId);

                guestLectureModel.UnitSessionsModels = this.vtGuestLectureConductedRepository.GetVTFUnitSessionsTaughtsByGuestLectureId(guestLectureItem.VTGuestLectureId);

                foreach (var unitSessionsItem in guestLectureModel.UnitSessionsModels)
                {
                    unitSessionsItem.SessionIds = new List<Guid>();
                    foreach (string sessionId in unitSessionsItem.SessionIdsValue.Split(","))
                    {
                        unitSessionsItem.SessionIds.Add(Guid.Parse(sessionId));
                    }
                }

                guestLectureModel.StudentAttendances = this.vtGuestLectureConductedRepository.GetVTFStudentsByGuestLectureId(guestLectureItem.VTGuestLectureId);
            }

            return guestLectureModel;
        }

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId using async
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        public async Task<VTGuestLectureConductedModel> GetVTGuestLectureConductedByIdAsync(Guid vtGuestLectureId)
        {
            var vtGuestLectureConducted = await this.vtGuestLectureConductedRepository.GetVTGuestLectureConductedByIdAsync(vtGuestLectureId);

            return (vtGuestLectureConducted != null) ? vtGuestLectureConducted.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTGuestLectureConducted entity
        /// </summary>
        /// <param name="vtGuestLectureConductedModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTGuestLectureConductedDetails(VTGuestLectureConductedModel vtGuestLectureConductedModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VTGuestLectureConducted vtGuestLectureConducted = null;

                //Validate model data
                vtGuestLectureConductedModel = vtGuestLectureConductedModel.GetModelValidationErrors<VTGuestLectureConductedModel>();

                if (vtGuestLectureConductedModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vtGuestLectureConductedModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vtGuestLectureConductedModel.RequestType == RequestType.Edit)
                {
                    vtGuestLectureConducted = this.vtGuestLectureConductedRepository.GetVTGuestLectureConductedById(vtGuestLectureConductedModel.VTGuestLectureId);
                }
                else
                {
                    vtGuestLectureConducted = new VTGuestLectureConducted();
                    vtGuestLectureConducted.VTGuestLectureId = Guid.NewGuid();
                }

                if (vtGuestLectureConductedModel.ErrorMessages.Count == 0)
                {
                    List<string> validationMessages = this.vtGuestLectureConductedRepository.CheckVTGuestLectureConductedExistByName(vtGuestLectureConductedModel);

                    if (validationMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", validationMessages));
                    }

                    VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByCurrentAYVTId(vtGuestLectureConductedModel.VTId);
                    if (vtSchoolSectors == null)
                    {
                        response.Errors.Add(Constants.NotMapVTSchoolSectorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vtGuestLectureConducted.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    vtGuestLectureConducted = vtGuestLectureConductedModel.FromModel(vtGuestLectureConducted);

                    if (vtGuestLectureConductedModel.RequestType == RequestType.New)
                    {
                        VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByVTId(vtGuestLectureConductedModel.VTId);
                        if (vtSchoolSectors != null)
                            vtGuestLectureConducted.VTSchoolSectorId = vtSchoolSectors.VTSchoolSectorId;
                    }

                    if (vtGuestLectureConductedModel.GLPhotoFile != null)
                    {
                        vtGuestLectureConductedModel.GLPhotoFile.ContentId = vtGuestLectureConducted.VTGuestLectureId;
                        var glPhotoFile = UtilityManager.UploadFileInPostByMobile(vtGuestLectureConductedModel.GLPhotoFile);

                        vtGuestLectureConducted.GLPhoto = glPhotoFile.FilePath;

                        if (glPhotoFile.Exception != null)
                            Logging.ErrorManager.Instance.WriteErrorLogsInFile(glPhotoFile.Exception);
                    }

                    if (vtGuestLectureConductedModel.GLLecturerPhotoFile != null)
                    {
                        vtGuestLectureConductedModel.GLLecturerPhotoFile.ContentId = vtGuestLectureConducted.VTGuestLectureId;
                        var lecturerPhotoFile = UtilityManager.UploadFileInPostByMobile(vtGuestLectureConductedModel.GLLecturerPhotoFile);

                        vtGuestLectureConducted.GLPhotoInClass = lecturerPhotoFile.FilePath;

                        if (lecturerPhotoFile.Exception != null)
                            Logging.ErrorManager.Instance.WriteErrorLogsInFile(lecturerPhotoFile.Exception);
                    }

                    //Save Or Update vtGuestLectureConducted details
                    bool isSaved = this.vtGuestLectureConductedRepository.SaveOrUpdateVTGuestLectureConductedDetails(vtGuestLectureConducted, vtGuestLectureConductedModel);

                    if (vtGuestLectureConductedModel.RequestType == RequestType.New && isSaved)
                    {
                        VocationalTrainer vocationalTrainer = this.commonRepository.GetVocationalTrainerById(vtGuestLectureConducted.VTId);

                        try
                        {
                            if (string.IsNullOrEmpty(vtGuestLectureConductedModel.GLEmail))
                            {
                                string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : vtGuestLectureConductedModel.GLEmail;
                                string subject = string.Format("Thank you for conducting the Guest Lecture");

                                StringBuilder sbGLTemplate = this.GetConductingGuestLectureTemplate(vocationalTrainer, vtGuestLectureConducted);
                                Message message = new Message(new string[] { toEmailId }, subject, sbGLTemplate.ToString(), null);

                                this.emailSender.SendEmailAsync(message);
                            }
                        }
                        catch (Exception exEmail)
                        {
                            //throw new Exception("Sending Email for GL - failed", exEmail);
                            Logging.ErrorManager.Instance.GetErrorMessages(exEmail);
                        }

                        try
                        {
                            SmsServiceProvider smsServiceProvider = new SmsServiceProvider();
                            IList<MessageTemplate> messageTemplates = this.commonRepository.GetMessageTemplates();

                            MessageTemplate messageTemplateItem = messageTemplates.FirstOrDefault(m => m.MessageTypeId == "VT" && m.MessageSubTypeId == "GL");

                            if (messageTemplateItem != null && !string.IsNullOrEmpty(vtGuestLectureConductedModel.GLMobile) && messageTemplateItem.IsActive && messageTemplateItem.ApplicableFor.Contains("SMS"))
                            {
                                GLRequest glRequest = new GLRequest();
                                glRequest.MessageType = "GL";
                                glRequest.SendTo = Constants.IsDeveloperMode ? Constants.TestToMobile : vtGuestLectureConductedModel.GLMobile;

                                glRequest.VTName = vtGuestLectureConductedModel.GLName;
                                glRequest.VTEmailId = vtGuestLectureConductedModel.GLEmail;
                                glRequest.ReportingDate = vtGuestLectureConducted.ReportingDate.ToString("dd/MM/yyyy hh:mm:ss tt");

                                smsServiceProvider.SendSMSFromMSG91(glRequest.SendTo, glRequest, messageTemplateItem);
                            }
                        }
                        catch (Exception exSMS)
                        {
                            //throw new Exception("Sending SMS for GL - failed", exSMS);
                            Logging.ErrorManager.Instance.GetErrorMessages(exSMS);
                        }
                    }

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
                throw new Exception("BAL > SaveOrUpdateVTGuestLectureConductedDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtGuestLectureId)
        {
            return this.vtGuestLectureConductedRepository.DeleteById(vtGuestLectureId);
        }

        /// <summary>
        /// Check duplicate VTGuestLectureConducted by Name
        /// </summary>
        /// <param name="vtGuestLectureConductedModel"></param>
        /// <returns></returns>
        public List<string> CheckVTGuestLectureConductedExistByName(VTGuestLectureConductedModel vtGuestLectureConductedModel)
        {
            return this.vtGuestLectureConductedRepository.CheckVTGuestLectureConductedExistByName(vtGuestLectureConductedModel);
        }

        /// <summary>}
        /// List of VTGuestLectureConducted with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTGuestLectureConductedViewModel> GetVTGuestLectureConductedsByCriteria(SearchVTGuestLectureConductedModel searchModel)
        {
            return this.vtGuestLectureConductedRepository.GetVTGuestLectureConductedsByCriteria(searchModel);
        }

        /// <summary>
        /// Approved VT Guest Lecture Conducted
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> ApprovedVTGuestLectureConducted(VTGuestLectureApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            VTGuestLectureConducted vtGuestLecture = this.vtGuestLectureConductedRepository.GetVTGuestLectureConductedById(vtApprovalRequest.VTGuestLectureId);

            vtGuestLecture.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
            vtGuestLecture.ApprovalStatus = vtApprovalRequest.ApprovalStatus;
            vtGuestLecture.ApprovedDate = Constants.GetCurrentDateTime;
            vtGuestLecture.RequestType = RequestType.Edit;
            vtGuestLecture.SetAuditValues(RequestType.Edit);

            bool results = this.vtGuestLectureConductedRepository.SaveOrUpdateVTGuestLectureConductedDetails(vtGuestLecture, null);

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