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
    /// Expose all vtpSectorJobRole WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTPSectorJobRoleController : BaseController
    {
        private readonly IVTPSectorJobRoleManager vtpSectorJobRoleManager;

        /// <summary>
        /// Initializes the VTPSectorJobRole controller class.
        /// </summary>
        /// <param name="_vtpSectorJobRoleManager"></param>
        public VTPSectorJobRoleController(IVTPSectorJobRoleManager _vtpSectorJobRoleManager)
        {
            this.vtpSectorJobRoleManager = _vtpSectorJobRoleManager;
        }

        /// <summary>
        /// Get list of vtpSectorJobRole data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTPSectorJobRoles")]
        public async Task<ListResponse<VTPSectorJobRoleModel>> GetVTPSectorJobRoles()
        {
            ListResponse<VTPSectorJobRoleModel> response = new ListResponse<VTPSectorJobRoleModel>();

            try
            {
                IQueryable<VTPSectorJobRoleModel> vtpSectorJobRoleModels = await Task.Run(() =>
                {
                    return this.vtpSectorJobRoleManager.GetVTPSectorJobRoles();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpSectorJobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTPSectorJobRole with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPSectorJobRolesByCriteria")]
        public async Task<ListResponse<VTPSectorJobRoleViewModel>> GetVTPSectorJobRolesByCriteria([FromBody] SearchVTPSectorJobRoleModel searchModel)
        {
            ListResponse<VTPSectorJobRoleViewModel> response = new ListResponse<VTPSectorJobRoleViewModel>();

            try
            {
                var vtpSectorJobRoleModels = await Task.Run(() =>
                {
                    return this.vtpSectorJobRoleManager.GetVTPSectorJobRolesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpSectorJobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtpSectorJobRole data by name
        /// </summary>
        /// <param name="vtpSectorJobRoleName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTPSectorJobRolesByName")]
        public async Task<ListResponse<VTPSectorJobRoleModel>> GetVTPSectorJobRolesByName([FromQuery] string vtpSectorJobRoleName)
        {
            ListResponse<VTPSectorJobRoleModel> response = new ListResponse<VTPSectorJobRoleModel>();

            try
            {
                var vtpSectorJobRoleModels = await Task.Run(() =>
                {
                    return this.vtpSectorJobRoleManager.GetVTPSectorJobRolesByName(vtpSectorJobRoleName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpSectorJobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtpSectorJobRole data by Id
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPSectorJobRoleById")]
        public async Task<SingularResponse<VTPSectorJobRoleModel>> GetVTPSectorJobRoleById([FromBody] DataRequest vtpSectorJobRoleRequest)
        {
            SingularResponse<VTPSectorJobRoleModel> response = new SingularResponse<VTPSectorJobRoleModel>();

            try
            {
                var vtpSectorJobRoleModel = await Task.Run(() =>
                {
                    return this.vtpSectorJobRoleManager.GetVTPSectorJobRoleById(Guid.Parse(vtpSectorJobRoleRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtpSectorJobRoleModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtpSectorJobRole
        /// </summary>
        /// <param name="vtpSectorJobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTPSectorJobRole"), Route("CreateOrUpdateVTPSectorJobRoleDetails")]
        public async Task<SingularResponse<string>> CreateVTPSectorJobRole([FromBody] VTPSectorJobRoleRequest vtpSectorJobRoleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtpSectorJobRoleRequest.RequestType = RequestType.New;
                    return this.vtpSectorJobRoleManager.SaveOrUpdateVTPSectorJobRoleDetails(vtpSectorJobRoleRequest);
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
        /// Update vtpSectorJobRole by Id
        /// </summary>
        /// <param name="vtpSectorJobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTPSectorJobRole")]
        public async Task<SingularResponse<string>> UpdateVTPSectorJobRole([FromBody] VTPSectorJobRoleRequest vtpSectorJobRoleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtpSectorJobRoleRequest.RequestType = RequestType.Edit;
                    return this.vtpSectorJobRoleManager.SaveOrUpdateVTPSectorJobRoleDetails(vtpSectorJobRoleRequest);
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
        /// Delete vtpSectorJobRole by Id
        /// </summary>
        /// <param name="vtpSectorJobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTPSectorJobRoleById")]
        public async Task<SingularResponse<bool>> DeleteVTPSectorJobRoleById([FromBody] DeleteRequest<Guid> vtpSectorJobRoleRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtpSectorJobRoleManager.DeleteById(vtpSectorJobRoleRequest.DataId);
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