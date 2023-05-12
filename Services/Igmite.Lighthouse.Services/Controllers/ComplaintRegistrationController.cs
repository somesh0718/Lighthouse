using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all complaintRegistration WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class ComplaintRegistrationController : Controller
    {
        private readonly IComplaintRegistrationManager complaintRegistrationManager;

        /// <summary>
        /// Initializes the ComplaintRegistration controller class.
        /// </summary>
        /// <param name="_complaintRegistrationManager"></param>
        public ComplaintRegistrationController(IComplaintRegistrationManager _complaintRegistrationManager)
        {
            this.complaintRegistrationManager = _complaintRegistrationManager;
        }

        /// <summary>
        /// Get list of complaintRegistration data
        /// </summary>
        /// <returns></returns>
        [HttpGet, IgmiteAuthorize, Route("GetComplaintRegistrations")]
        public async Task<ListResponse<ComplaintRegistrationModel>> GetComplaintRegistrations()
        {
            ListResponse<ComplaintRegistrationModel> response = new ListResponse<ComplaintRegistrationModel>();

            try
            {
                IQueryable<ComplaintRegistrationModel> complaintRegistrationModels = await Task.Run(() =>
                {
                    return this.complaintRegistrationManager.GetComplaintRegistrations();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = complaintRegistrationModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// List of ComplaintRegistration with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route("GetComplaintRegistrationsByCriteria")]
        public async Task<ListResponse<ComplaintRegistrationViewModel>> GetComplaintRegistrationsByCriteria([FromBody] SearchComplaintRegistrationModel searchModel)
        {
            ListResponse<ComplaintRegistrationViewModel> response = new ListResponse<ComplaintRegistrationViewModel>();

            try
            {
                var complaintRegistrationModels = await Task.Run(() =>
                {
                    return this.complaintRegistrationManager.GetComplaintRegistrationsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = complaintRegistrationModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Get list of complaintRegistration data by name
        /// </summary>
        /// <param name="complaintRegistrationName"></param>
        /// <returns></returns>
        [HttpGet, IgmiteAuthorize, Route("GetComplaintRegistrationsByName")]
        public async Task<ListResponse<ComplaintRegistrationModel>> GetComplaintRegistrationsByName([FromQuery] string complaintRegistrationName)
        {
            ListResponse<ComplaintRegistrationModel> response = new ListResponse<ComplaintRegistrationModel>();

            try
            {
                var complaintRegistrationModels = await Task.Run(() =>
                {
                    return this.complaintRegistrationManager.GetComplaintRegistrationsByName(complaintRegistrationName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = complaintRegistrationModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Get complaintRegistration data by Id
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route("GetComplaintRegistrationById")]
        public async Task<SingularResponse<ComplaintRegistrationModel>> GetComplaintRegistrationById([FromBody] DataRequest complaintRegistrationRequest)
        {
            SingularResponse<ComplaintRegistrationModel> response = new SingularResponse<ComplaintRegistrationModel>();

            try
            {
                var complaintRegistrationModel = await Task.Run(() =>
                {
                    return this.complaintRegistrationManager.GetComplaintRegistrationById(Guid.Parse(complaintRegistrationRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = complaintRegistrationModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Create new complaintRegistration
        /// </summary>
        /// <param name="complaintRegistrationRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateComplaintRegistration"), Route("CreateOrUpdateComplaintRegistrationDetails")]
        public async Task<SingularResponse<string>> CreateComplaintRegistration([FromBody] ComplaintRegistrationRequest complaintRegistrationRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //complaintRegistrationRequest.RequestType = RequestType.New;
                    return this.complaintRegistrationManager.SaveOrUpdateComplaintRegistrationDetails(complaintRegistrationRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Update complaintRegistration by Id
        /// </summary>
        /// <param name="complaintRegistrationRequest"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route("UpdateComplaintRegistration")]
        public async Task<SingularResponse<string>> UpdateComplaintRegistration([FromBody] ComplaintRegistrationRequest complaintRegistrationRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    complaintRegistrationRequest.RequestType = RequestType.Edit;
                    return this.complaintRegistrationManager.SaveOrUpdateComplaintRegistrationDetails(complaintRegistrationRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Delete complaintRegistration by Id
        /// </summary>
        /// <param name="complaintRegistrationRequest"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route("DeleteComplaintRegistrationById")]
        public async Task<SingularResponse<bool>> DeleteComplaintRegistrationById([FromBody] DeleteRequest<string> complaintRegistrationRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.complaintRegistrationManager.DeleteById(Guid.Parse(complaintRegistrationRequest.DataId));
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }
    }
}