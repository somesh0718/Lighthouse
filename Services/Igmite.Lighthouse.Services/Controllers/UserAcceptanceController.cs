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
    /// Expose all userAcceptance WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class UserAcceptanceController : BaseController
    {
        private readonly IUserAcceptanceManager userAcceptanceManager;

        /// <summary>
        /// Initializes the UserAcceptance controller class.
        /// </summary>
        /// <param name="_userAcceptanceManager"></param>
        public UserAcceptanceController(IUserAcceptanceManager _userAcceptanceManager)
        {
            this.userAcceptanceManager = _userAcceptanceManager;
        }

        /// <summary>
        /// Get list of userAcceptance data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetUserAcceptances")]
        public async Task<ListResponse<UserAcceptanceModel>> GetUserAcceptances()
        {
            ListResponse<UserAcceptanceModel> response = new ListResponse<UserAcceptanceModel>();

            try
            {
                IQueryable<UserAcceptanceModel> userAcceptanceModels = await Task.Run(() =>
                {
                    return this.userAcceptanceManager.GetUserAcceptances();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = userAcceptanceModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of userAcceptance data by name
        /// </summary>
        /// <param name="userAcceptanceName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUserAcceptancesByName")]
        public async Task<ListResponse<UserAcceptanceModel>> GetUserAcceptancesByName([FromQuery] string userAcceptanceName)
        {
            ListResponse<UserAcceptanceModel> response = new ListResponse<UserAcceptanceModel>();

            try
            {
                var userAcceptanceModels = await Task.Run(() =>
                {
                    return this.userAcceptanceManager.GetUserAcceptancesByName(userAcceptanceName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = userAcceptanceModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get userAcceptance data by Id
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetUserAcceptanceById")]
        public async Task<SingularResponse<UserAcceptanceModel>> GetUserAcceptanceById([FromBody] DataRequest userAcceptanceRequest)
        {
            SingularResponse<UserAcceptanceModel> response = new SingularResponse<UserAcceptanceModel>();

            try
            {
                var userAcceptanceModel = await Task.Run(() =>
                {
                    return this.userAcceptanceManager.GetUserAcceptanceById(Guid.Parse(userAcceptanceRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = userAcceptanceModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new userAcceptance
        /// </summary>
        /// <param name="userAcceptanceRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateUserAcceptance"), Route("CreateOrUpdateUserAcceptanceDetails")]
        public async Task<SingularResponse<string>> CreateUserAcceptance([FromBody] UserAcceptanceRequest userAcceptanceRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //userAcceptanceRequest.RequestType = RequestType.New;
                    return this.userAcceptanceManager.SaveOrUpdateUserAcceptanceDetails(userAcceptanceRequest);
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
        /// Update userAcceptance by Id
        /// </summary>
        /// <param name="userAcceptanceRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateUserAcceptance")]
        public async Task<SingularResponse<string>> UpdateUserAcceptance([FromBody] UserAcceptanceRequest userAcceptanceRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    userAcceptanceRequest.RequestType = RequestType.Edit;
                    return this.userAcceptanceManager.SaveOrUpdateUserAcceptanceDetails(userAcceptanceRequest);
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
        /// Delete userAcceptance by Id
        /// </summary>
        /// <param name="userAcceptanceRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteUserAcceptanceById")]
        public async Task<SingularResponse<bool>> DeleteUserAcceptanceById([FromBody] DeleteRequest<Guid> userAcceptanceRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.userAcceptanceManager.DeleteById(userAcceptanceRequest.DataId);
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