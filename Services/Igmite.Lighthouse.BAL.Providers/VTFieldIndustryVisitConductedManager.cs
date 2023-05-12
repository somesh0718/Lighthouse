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
    /// Manager of the VTFieldIndustryVisitConducted entity
    /// </summary>
    public class VTFieldIndustryVisitConductedManager : GenericManager<VTFieldIndustryVisitConductedModel>, IVTFieldIndustryVisitConductedManager
    {
        private readonly IVTFieldIndustryVisitConductedRepository vtFieldIndustryVisitConductedRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes the vtFieldIndustryVisitConducted manager.
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTFieldIndustryVisitConductedManager(IVTFieldIndustryVisitConductedRepository _vtFieldIndustryVisitConductedRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository, IEmailSender _emailSender)
        {
            this.vtFieldIndustryVisitConductedRepository = _vtFieldIndustryVisitConductedRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
            this.emailSender = _emailSender;
        }

        /// <summary>
        /// Get list of VTFieldIndustryVisitConducteds
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTFieldIndustryVisitConductedModel> GetVTFieldIndustryVisitConducteds()
        {
            var vtFieldIndustryVisitConducteds = this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConducteds();

            IList<VTFieldIndustryVisitConductedModel> vtFieldIndustryVisitConductedModels = new List<VTFieldIndustryVisitConductedModel>();
            vtFieldIndustryVisitConducteds.ForEach((user) => vtFieldIndustryVisitConductedModels.Add(user.ToModel()));

            return vtFieldIndustryVisitConductedModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTFieldIndustryVisitConducteds by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTFieldIndustryVisitConductedModel> GetVTFieldIndustryVisitConductedsByName(string vtFieldIndustryVisitConductedName)
        {
            var vtFieldIndustryVisitConducteds = this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConductedsByName(vtFieldIndustryVisitConductedName);

            IList<VTFieldIndustryVisitConductedModel> vtFieldIndustryVisitConductedModels = new List<VTFieldIndustryVisitConductedModel>();
            vtFieldIndustryVisitConducteds.ForEach((user) => vtFieldIndustryVisitConductedModels.Add(user.ToModel()));

            return vtFieldIndustryVisitConductedModels.AsQueryable();
        }

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        public VTFieldIndustryVisitConductedModel GetVTFieldIndustryVisitConductedById(Guid vtFieldIndustryVisitConductedId)
        {
            VTFieldIndustryVisitConductedModel fieldIndustryVisitModel = null;

            VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted = this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConductedById(vtFieldIndustryVisitConductedId);

            if (vtFieldIndustryVisitConducted != null)
            {
                fieldIndustryVisitModel = vtFieldIndustryVisitConducted.ToModel();

                //fieldIndustryVisitModel.SectionIds = this.vtFieldIndustryVisitConductedRepository.GetVTFSectionsByFieldIndustryVisitId(vtFieldIndustryVisitConducted.VTFieldIndustryVisitConductedId);

                fieldIndustryVisitModel.UnitSessionsModels = this.vtFieldIndustryVisitConductedRepository.GetVTFUnitSessionsTaughtsByFieldIndustryVisitId(vtFieldIndustryVisitConducted.VTFieldIndustryVisitConductedId);

                foreach (var unitSessionsItem in fieldIndustryVisitModel.UnitSessionsModels)
                {
                    unitSessionsItem.SessionIds = new List<Guid>();
                    foreach (string sessionId in unitSessionsItem.SessionIdsValue.Split(","))
                    {
                        unitSessionsItem.SessionIds.Add(Guid.Parse(sessionId));
                    }
                }

                fieldIndustryVisitModel.StudentAttendances = this.vtFieldIndustryVisitConductedRepository.GetVTFStudentsByFieldIndustryVisitId(vtFieldIndustryVisitConducted.VTFieldIndustryVisitConductedId);
            }

            return fieldIndustryVisitModel;
        }

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId using async
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        public async Task<VTFieldIndustryVisitConductedModel> GetVTFieldIndustryVisitConductedByIdAsync(Guid vtFieldIndustryVisitConductedId)
        {
            var vtFieldIndustryVisitConducted = await this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConductedByIdAsync(vtFieldIndustryVisitConductedId);

            return (vtFieldIndustryVisitConducted != null) ? vtFieldIndustryVisitConducted.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTFieldIndustryVisitConducted entity
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTFieldIndustryVisitConductedDetails(VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted = null;

                //Validate model data
                vtFieldIndustryVisitConductedModel = vtFieldIndustryVisitConductedModel.GetModelValidationErrors<VTFieldIndustryVisitConductedModel>();

                if (vtFieldIndustryVisitConductedModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vtFieldIndustryVisitConductedModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vtFieldIndustryVisitConductedModel.RequestType == RequestType.Edit)
                {
                    vtFieldIndustryVisitConducted = this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConductedById(vtFieldIndustryVisitConductedModel.VTFieldIndustryVisitConductedId);
                }
                else
                {
                    vtFieldIndustryVisitConducted = new VTFieldIndustryVisitConducted();
                    vtFieldIndustryVisitConducted.VTFieldIndustryVisitConductedId = Guid.NewGuid();
                }

                if (vtFieldIndustryVisitConductedModel.ErrorMessages.Count == 0)
                {
                    List<string> validationMessages = this.vtFieldIndustryVisitConductedRepository.CheckVTFieldIndustryVisitConductedExistByName(vtFieldIndustryVisitConductedModel);

                    if (validationMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", validationMessages));
                    }

                    VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByCurrentAYVTId(vtFieldIndustryVisitConductedModel.VTId);
                    if (vtSchoolSectors == null)
                    {
                        response.Errors.Add(Constants.NotMapVTSchoolSectorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vtFieldIndustryVisitConducted.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    vtFieldIndustryVisitConducted = vtFieldIndustryVisitConductedModel.FromModel(vtFieldIndustryVisitConducted);

                    if (vtFieldIndustryVisitConductedModel.RequestType == RequestType.New)
                    {
                        VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByVTId(vtFieldIndustryVisitConductedModel.VTId);
                        if (vtSchoolSectors != null)
                            vtFieldIndustryVisitConducted.VTSchoolSectorId = vtSchoolSectors.VTSchoolSectorId;
                    }

                    if (vtFieldIndustryVisitConductedModel.FVPictureFile != null)
                    {
                        vtFieldIndustryVisitConductedModel.FVPictureFile.ContentId = vtFieldIndustryVisitConducted.VTFieldIndustryVisitConductedId;
                        var fvPictureFile = UtilityManager.UploadFileInPostByMobile(vtFieldIndustryVisitConductedModel.FVPictureFile);

                        vtFieldIndustryVisitConducted.FVPicture = fvPictureFile.FilePath;

                        if (fvPictureFile.Exception != null)
                            Logging.ErrorManager.Instance.WriteErrorLogsInFile(fvPictureFile.Exception);
                    }

                    //Save Or Update vtFieldIndustryVisitConducted details
                    bool isSaved = this.vtFieldIndustryVisitConductedRepository.SaveOrUpdateVTFieldIndustryVisitConductedDetails(vtFieldIndustryVisitConducted, vtFieldIndustryVisitConductedModel);

                    if (vtFieldIndustryVisitConductedModel.RequestType == RequestType.New && isSaved)
                    {
                        VocationalTrainer vocationalTrainer = this.commonRepository.GetVocationalTrainerById(vtFieldIndustryVisitConducted.VTId);

                        try
                        {
                            string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : vocationalTrainer.Email;
                            string subject = string.Format("Thank you for conducting the Guest Lecture");

                            StringBuilder sbGLTemplate = this.GetConductingFieldIndustryVisitTemplate(vocationalTrainer, vtFieldIndustryVisitConducted);
                            Message message = new Message(new string[] { toEmailId }, subject, sbGLTemplate.ToString(), null);

                            this.emailSender.SendEmailAsync(message);
                        }
                        catch (Exception exEmail)
                        {
                            //throw new Exception("Sending Email for FV - failed", exEmail);
                            Logging.ErrorManager.Instance.GetErrorMessages(exEmail);
                        }

                        try
                        {
                            SmsServiceProvider smsServiceProvider = new SmsServiceProvider();
                            IList<MessageTemplate> messageTemplates = this.commonRepository.GetMessageTemplates();

                            MessageTemplate messageTemplateItem = messageTemplates.FirstOrDefault(m => m.MessageTypeId == "VT" && m.MessageSubTypeId == "FV");

                            if (messageTemplateItem != null && messageTemplateItem.IsActive && messageTemplateItem.ApplicableFor.Contains("SMS"))
                            {
                                FVRequest fvRequest = new FVRequest();
                                fvRequest.MessageType = "FV";
                                fvRequest.SendTo = Constants.IsDeveloperMode ? Constants.TestToMobile : vocationalTrainer.Mobile;

                                fvRequest.VTName = vocationalTrainer.FullName;
                                fvRequest.VTEmailId = vocationalTrainer.Email;
                                fvRequest.ReportingDate = vtFieldIndustryVisitConducted.ReportingDate.ToString("dd/MM/yyyy hh:mm:ss tt");

                                smsServiceProvider.SendSMSFromMSG91(fvRequest.SendTo, fvRequest, messageTemplateItem);
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
                throw new Exception("BAL > SaveOrUpdateVTFieldIndustryVisitConductedDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtFieldIndustryVisitConductedId)
        {
            return this.vtFieldIndustryVisitConductedRepository.DeleteById(vtFieldIndustryVisitConductedId);
        }

        /// <summary>
        /// Check duplicate VTFieldIndustryVisitConducted by Name
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        public List<string> CheckVTFieldIndustryVisitConductedExistByName(VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel)
        {
            return this.vtFieldIndustryVisitConductedRepository.CheckVTFieldIndustryVisitConductedExistByName(vtFieldIndustryVisitConductedModel);
        }

        /// <summary>}
        /// List of VTFieldIndustryVisitConducted with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTFieldIndustryVisitConductedViewModel> GetVTFieldIndustryVisitConductedsByCriteria(SearchVTFieldIndustryVisitConductedModel searchModel)
        {
            return this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConductedsByCriteria(searchModel);
        }

        /// <summary>
        /// Approved VT Field Industry Visit Conducted
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> ApprovedVTFieldIndustry(VTFieldIndustryApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted = this.vtFieldIndustryVisitConductedRepository.GetVTFieldIndustryVisitConductedById(vtApprovalRequest.VTFieldIndustryVisitConductedId);

            vtFieldIndustryVisitConducted.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
            vtFieldIndustryVisitConducted.ApprovalStatus = vtApprovalRequest.ApprovalStatus;
            vtFieldIndustryVisitConducted.ApprovedDate = Constants.GetCurrentDateTime;
            vtFieldIndustryVisitConducted.RequestType = RequestType.Edit;
            vtFieldIndustryVisitConducted.SetAuditValues(RequestType.Edit);

            bool results = this.vtFieldIndustryVisitConductedRepository.SaveOrUpdateVTFieldIndustryVisitConductedDetails(vtFieldIndustryVisitConducted, null);

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