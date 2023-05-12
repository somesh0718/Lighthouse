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
    /// Expose all userOTPDetail WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class UserOTPDetailController : BaseController
    {
        private readonly IUserOTPDetailManager userOTPDetailManager;

        /// <summary>
        /// Initializes the UserOTPDetail controller class.
        /// </summary>
        /// <param name="_userOTPDetailManager"></param>
        public UserOTPDetailController(IUserOTPDetailManager _userOTPDetailManager)
        {
            this.userOTPDetailManager = _userOTPDetailManager;
        }

        /// <summary>
        /// Get list of userOTPDetail data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetUserOTPDetails")]
        public async Task<ListResponse<UserOTPDetailModel>> GetUserOTPDetails()
        {
            ListResponse<UserOTPDetailModel> response = new ListResponse<UserOTPDetailModel>();

            try
            {
                IQueryable<UserOTPDetailModel> userOTPDetailModels = await Task.Run(() =>
                {
                    return this.userOTPDetailManager.GetUserOTPDetails();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = userOTPDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of UserOTPDetail with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetUserOTPDetailsByCriteria")]
        public async Task<ListResponse<UserOTPDetailViewModel>> GetUserOTPDetailsByCriteria([FromBody] SearchUserOTPDetailModel searchModel)
        {
            ListResponse<UserOTPDetailViewModel> response = new ListResponse<UserOTPDetailViewModel>();

            try
            {
                var userOTPDetailModels = await Task.Run(() =>
                {
                    return this.userOTPDetailManager.GetUserOTPDetailsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = userOTPDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of userOTPDetail data by name
        /// </summary>
        /// <param name="userOTPDetailName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUserOTPDetailsByName")]
        public async Task<ListResponse<UserOTPDetailModel>> GetUserOTPDetailsByName([FromQuery] string userOTPDetailName)
        {
            ListResponse<UserOTPDetailModel> response = new ListResponse<UserOTPDetailModel>();

            try
            {
                var userOTPDetailModels = await Task.Run(() =>
                {
                    return this.userOTPDetailManager.GetUserOTPDetailsByName(userOTPDetailName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = userOTPDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get userOTPDetail data by Id
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetUserOTPDetailById")]
        public async Task<SingularResponse<UserOTPDetailModel>> GetUserOTPDetailById([FromBody] DataRequest otpRequest)
        {
            SingularResponse<UserOTPDetailModel> response = new SingularResponse<UserOTPDetailModel>();

            try
            {
                var userOTPDetailModel = await Task.Run(() =>
                {
                    return this.userOTPDetailManager.GetUserOTPDetailById(Guid.Parse(otpRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = userOTPDetailModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new userOTPDetail
        /// </summary>
        /// <param name="userOTPDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateUserOTPDetail"), Route("CreateOrUpdateUserOTPDetailDetails")]
        public async Task<SingularResponse<string>> CreateUserOTPDetail([FromBody] UserOTPDetailRequest userOTPDetailRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //userOTPDetailRequest.RequestType = RequestType.New;
                    return this.userOTPDetailManager.SaveOrUpdateUserOTPDetailDetails(userOTPDetailRequest);
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
        /// Update userOTPDetail by Id
        /// </summary>
        /// <param name="userOTPDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateUserOTPDetail")]
        public async Task<SingularResponse<string>> UpdateUserOTPDetail([FromBody] UserOTPDetailRequest userOTPDetailRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    userOTPDetailRequest.RequestType = RequestType.Edit;
                    return this.userOTPDetailManager.SaveOrUpdateUserOTPDetailDetails(userOTPDetailRequest);
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
        /// Delete userOTPDetail by Id
        /// </summary>
        /// <param name="userOTPDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteUserOTPDetailById")]
        public async Task<SingularResponse<bool>> DeleteUserOTPDetailById([FromBody] DeleteRequest<Guid> userOTPDetailRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.userOTPDetailManager.DeleteById(userOTPDetailRequest.DataId);
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