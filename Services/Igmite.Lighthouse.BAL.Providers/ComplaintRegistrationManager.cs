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
    /// Manager of the ComplaintRegistration entity
    /// </summary>
    public class ComplaintRegistrationManager : GenericManager<ComplaintRegistrationModel>, IComplaintRegistrationManager
    {
        private readonly IComplaintRegistrationRepository complaintRegistrationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes the complaintRegistration manager.
        /// </summary>
        /// <param name="complaintRegistrationRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public ComplaintRegistrationManager(IComplaintRegistrationRepository _complaintRegistrationRepository, IHttpContextAccessor _httpContextAccessor, IEmailSender _emailSender)
        {
            this.complaintRegistrationRepository = _complaintRegistrationRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.emailSender = _emailSender;
        }

        /// <summary>
        /// Get list of ComplaintRegistrations
        /// </summary>
        /// <returns></returns>
        public IQueryable<ComplaintRegistrationModel> GetComplaintRegistrations()
        {
            var complaintRegistrations = this.complaintRegistrationRepository.GetComplaintRegistrations();

            IList<ComplaintRegistrationModel> complaintRegistrationModels = new List<ComplaintRegistrationModel>();
            complaintRegistrations.ForEach((user) => complaintRegistrationModels.Add(user.ToModel()));

            return complaintRegistrationModels.AsQueryable();
        }

        /// <summary>
        /// Get list of ComplaintRegistrations by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ComplaintRegistrationModel> GetComplaintRegistrationsByName(string complaintRegistrationName)
        {
            var complaintRegistrations = this.complaintRegistrationRepository.GetComplaintRegistrationsByName(complaintRegistrationName);

            IList<ComplaintRegistrationModel> complaintRegistrationModels = new List<ComplaintRegistrationModel>();
            complaintRegistrations.ForEach((user) => complaintRegistrationModels.Add(user.ToModel()));

            return complaintRegistrationModels.AsQueryable();
        }

        /// <summary>
        /// Get ComplaintRegistration by ComplaintRegistrationId
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        public ComplaintRegistrationModel GetComplaintRegistrationById(Guid complaintRegistrationId)
        {
            ComplaintRegistration complaintRegistration = this.complaintRegistrationRepository.GetComplaintRegistrationById(complaintRegistrationId);

            return (complaintRegistration != null) ? complaintRegistration.ToModel() : null;
        }

        /// <summary>
        /// Get ComplaintRegistration by ComplaintRegistrationId using async
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        public async Task<ComplaintRegistrationModel> GetComplaintRegistrationByIdAsync(Guid complaintRegistrationId)
        {
            var complaintRegistration = await this.complaintRegistrationRepository.GetComplaintRegistrationByIdAsync(complaintRegistrationId);

            return (complaintRegistration != null) ? complaintRegistration.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update ComplaintRegistration entity
        /// </summary>
        /// <param name="complaintRegistrationModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateComplaintRegistrationDetails(ComplaintRegistrationModel complaintRegistrationModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            ComplaintRegistration complaintRegistration = null;

            //Validate model data
            complaintRegistrationModel = complaintRegistrationModel.GetModelValidationErrors<ComplaintRegistrationModel>();

            if (complaintRegistrationModel.ErrorMessages.Count > 0)
            {
                response.Errors = complaintRegistrationModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (complaintRegistrationModel.RequestType == RequestType.Edit)
            {
                complaintRegistration = this.complaintRegistrationRepository.GetComplaintRegistrationById(complaintRegistrationModel.ComplaintRegistrationId);
            }
            else
            {
                complaintRegistration = new ComplaintRegistration();
                complaintRegistration.ComplaintRegistrationId = Guid.NewGuid();
            }

            if (complaintRegistrationModel.ErrorMessages.Count == 0 && (complaintRegistrationModel.Subject.StringVal().ToLower() != complaintRegistration.Subject.StringVal().ToLower()))
            {
                bool isComplaintRegistrationExists = this.complaintRegistrationRepository.CheckComplaintRegistrationExistByName(complaintRegistrationModel);

                if (isComplaintRegistrationExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                complaintRegistration.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                complaintRegistration = complaintRegistrationModel.FromModel(complaintRegistration);

                if (complaintRegistrationModel.ScreenshotFile != null)
                {
                    complaintRegistrationModel.ScreenshotFile.ContentId = complaintRegistration.ComplaintRegistrationId;
                    var fvPictureFile = UtilityManager.UploadFileInPostByMobile(complaintRegistrationModel.ScreenshotFile);

                    complaintRegistration.Attachment = fvPictureFile.FilePath;

                    if (fvPictureFile.Exception != null)
                        Logging.ErrorManager.Instance.WriteErrorLogsInFile(fvPictureFile.Exception);
                }

                //Save Or Update complaintRegistration details
                bool isSaved = this.complaintRegistrationRepository.SaveOrUpdateComplaintRegistrationDetails(complaintRegistration);

                try
                {
                    string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : Constants.SupportEmail;
                    string subject = string.Format("{0}-Complaint Registration : {1}", complaintRegistration.UserType, complaintRegistration.Subject);

                    StringBuilder sbNewUserTemplate = this.GetNewComplaintRegistrationTemplate(complaintRegistrationModel);
                    Message message = new Message(new string[] { toEmailId }, subject, sbNewUserTemplate.ToString(), null);

                    this.emailSender.SendEmail(message);
                }
                catch (Exception eMailEx)
                {
                    Logging.ErrorManager.Instance.WriteErrorLogsInFile(eMailEx);
                }

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by ComplaintRegistrationId
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid complaintRegistrationId)
        {
            return this.complaintRegistrationRepository.DeleteById(complaintRegistrationId);
        }

        /// <summary>
        /// Check duplicate ComplaintRegistration by Name
        /// </summary>
        /// <param name="complaintRegistrationModel"></param>
        /// <returns></returns>
        public bool CheckComplaintRegistrationExistByName(ComplaintRegistrationModel complaintRegistrationModel)
        {
            return this.complaintRegistrationRepository.CheckComplaintRegistrationExistByName(complaintRegistrationModel);
        }

        /// <summary>}
        /// List of ComplaintRegistration with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ComplaintRegistrationViewModel> GetComplaintRegistrationsByCriteria(SearchComplaintRegistrationModel searchModel)
        {
            return this.complaintRegistrationRepository.GetComplaintRegistrationsByCriteria(searchModel);
        }
    }
}