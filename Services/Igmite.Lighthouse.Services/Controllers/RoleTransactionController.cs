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
    /// Expose all roleTransaction WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class RoleTransactionController : BaseController
    {
        private readonly IRoleTransactionManager roleTransactionManager;

        /// <summary>
        /// Initializes the RoleTransaction controller class.
        /// </summary>
        /// <param name="_roleTransactionManager"></param>
        public RoleTransactionController(IRoleTransactionManager _roleTransactionManager)
        {
            this.roleTransactionManager = _roleTransactionManager;
        }

        /// <summary>
        /// Get list of roleTransaction data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRoleTransactions")]
        public async Task<ListResponse<RoleTransactionModel>> GetRoleTransactions()
        {
            ListResponse<RoleTransactionModel> response = new ListResponse<RoleTransactionModel>();

            try
            {
                IQueryable<RoleTransactionModel> roleTransactionModels = await Task.Run(() =>
                {
                    return this.roleTransactionManager.GetRoleTransactions();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = roleTransactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of RoleTransaction with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetRoleTransactionsByCriteria")]
        public async Task<ListResponse<RoleTransactionViewModel>> GetRoleTransactionsByCriteria([FromBody] SearchRoleTransactionModel searchModel)
        {
            ListResponse<RoleTransactionViewModel> response = new ListResponse<RoleTransactionViewModel>();

            try
            {
                var roleTransactionModels = await Task.Run(() =>
                {
                    return this.roleTransactionManager.GetRoleTransactionsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = roleTransactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of roleTransaction data by name
        /// </summary>
        /// <param name="roleTransactionName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetRoleTransactionsByName")]
        public async Task<ListResponse<RoleTransactionModel>> GetRoleTransactionsByName([FromQuery] string roleTransactionName)
        {
            ListResponse<RoleTransactionModel> response = new ListResponse<RoleTransactionModel>();

            try
            {
                var roleTransactionModels = await Task.Run(() =>
                {
                    return this.roleTransactionManager.GetRoleTransactionsByName(roleTransactionName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = roleTransactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get roleTransaction data by Id
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetRoleTransactionById")]
        public async Task<SingularResponse<RoleTransactionModel>> GetRoleTransactionById([FromBody] DataRequest roleTransactionRequest)
        {
            SingularResponse<RoleTransactionModel> response = new SingularResponse<RoleTransactionModel>();

            try
            {
                var roleTransactionModel = await Task.Run(() =>
                {
                    return this.roleTransactionManager.GetRoleTransactionById(Guid.Parse(roleTransactionRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = roleTransactionModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new roleTransaction
        /// </summary>
        /// <param name="roleTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateRoleTransaction"), Route("CreateOrUpdateRoleTransactionDetails")]
        public async Task<SingularResponse<string>> CreateRoleTransaction([FromBody] RoleTransactionRequest roleTransactionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //roleTransactionRequest.RequestType = RequestType.New;
                    return this.roleTransactionManager.SaveOrUpdateRoleTransactionDetails(roleTransactionRequest);
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
        /// Update roleTransaction by Id
        /// </summary>
        /// <param name="roleTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateRoleTransaction")]
        public async Task<SingularResponse<string>> UpdateRoleTransaction([FromBody] RoleTransactionRequest roleTransactionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    roleTransactionRequest.RequestType = RequestType.Edit;
                    return this.roleTransactionManager.SaveOrUpdateRoleTransactionDetails(roleTransactionRequest);
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
        /// Delete roleTransaction by Id
        /// </summary>
        /// <param name="roleTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteRoleTransactionById")]
        public async Task<SingularResponse<bool>> DeleteRoleTransactionById([FromBody] DeleteRequest<Guid> roleTransactionRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.roleTransactionManager.DeleteById(roleTransactionRequest.DataId);
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