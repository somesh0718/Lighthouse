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
    /// Expose all role WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleManager roleManager;

        /// <summary>
        /// Initializes the Role controller class.
        /// </summary>
        /// <param name="_roleManager"></param>
        public RoleController(IRoleManager _roleManager)
        {
            this.roleManager = _roleManager;
        }

        /// <summary>
        /// Get list of role data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRoles")]
        public async Task<ListResponse<RoleModel>> GetRoles()
        {
            ListResponse<RoleModel> response = new ListResponse<RoleModel>();

            try
            {
                IQueryable<RoleModel> roleModels = await Task.Run(() =>
                {
                    return this.roleManager.GetRoles();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = roleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of user roles with filters
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns>
        /// All active roles
        /// </returns>
        [HttpPost, Route("GetRolesByCriteria")]
        public async Task<ListResponse<RoleViewModel>> GetRolesByCriteria([FromBody] SearchRoleModel searchModel)
        {
            ListResponse<RoleViewModel> response = new ListResponse<RoleViewModel>();

            try
            {
                var roleModels = await Task.Run(() =>
                {
                    return this.roleManager.GetRolesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = roleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of role data by name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetRolesByName")]
        public async Task<ListResponse<RoleModel>> GetRolesByName([FromQuery] string roleName)
        {
            ListResponse<RoleModel> response = new ListResponse<RoleModel>();

            try
            {
                var roleModels = await Task.Run(() =>
                {
                    return this.roleManager.GetRolesByName(roleName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = roleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get role data by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetRoleById")]
        public async Task<SingularResponse<RoleModel>> GetRoleById([FromBody] DataRequest roleRequest)
        {
            SingularResponse<RoleModel> response = new SingularResponse<RoleModel>();

            try
            {
                var roleModel = await Task.Run(() =>
                {
                    return this.roleManager.GetRoleById(Guid.Parse(roleRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = roleModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateRole"), Route("CreateOrUpdateRoleDetails")]
        public async Task<SingularResponse<string>> CreateRole([FromBody] RoleRequest roleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //roleRequest.RequestType = RequestType.New;
                    return this.roleManager.SaveOrUpdateRoleDetails(roleRequest);
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
        /// Update role by Id
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateRole")]
        public async Task<SingularResponse<string>> UpdateRole([FromBody] RoleRequest roleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    roleRequest.RequestType = RequestType.Edit;
                    return this.roleManager.SaveOrUpdateRoleDetails(roleRequest);
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
        /// Delete role by Id
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteRoleById")]
        public async Task<SingularResponse<bool>> DeleteRoleById([FromBody] DeleteRequest<Guid> roleRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.roleManager.DeleteById(roleRequest.DataId);
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