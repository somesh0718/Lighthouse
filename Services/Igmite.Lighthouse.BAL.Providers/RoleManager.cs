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
    /// Manager of the Role entity
    /// </summary>
    public class RoleManager : GenericManager<RoleModel>, IRoleManager
    {
        private readonly IRoleRepository roleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the role manager.
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public RoleManager(IRoleRepository _roleRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.roleRepository = _roleRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Roles
        /// </summary>
        /// <returns></returns>
        public IQueryable<RoleModel> GetRoles()
        {
            var roles = this.roleRepository.GetRoles();

            IList<RoleModel> roleModels = new List<RoleModel>();
            roles.ForEach((user) => roleModels.Add(user.ToModel()));

            return roleModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Roles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<RoleModel> GetRolesByName(string roleName)
        {
            var roles = this.roleRepository.GetRolesByName(roleName);

            IList<RoleModel> roleModels = new List<RoleModel>();
            roles.ForEach((user) => roleModels.Add(user.ToModel()));

            return roleModels.AsQueryable();
        }

        /// <summary>
        /// Get Role by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleModel GetRoleById(Guid roleId)
        {
            Role role = this.roleRepository.GetRoleById(roleId);

            return (role != null) ? role.ToModel() : null;
        }

        /// <summary>
        /// Get Role by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public RoleModel GetRoleByCode(string code)
        {
            Role role = this.roleRepository.GetRoleByCode(code);

            return (role != null) ? role.ToModel() : null;
        }

        /// <summary>
        /// Get Role by RoleId using async
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<RoleModel> GetRoleByIdAsync(Guid roleId)
        {
            var role = await this.roleRepository.GetRoleByIdAsync(roleId);

            return (role != null) ? role.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Role entity
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateRoleDetails(RoleModel roleModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Role role = null;

            //Validate model data
            roleModel = roleModel.GetModelValidationErrors<RoleModel>();

            if (roleModel.ErrorMessages.Count > 0)
            {
                response.Errors = roleModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (roleModel.RequestType == RequestType.Edit)
            {
                role = this.roleRepository.GetRoleById(roleModel.RoleId);
            }
            else
            {
                role = new Role();
                roleModel.RoleId = Guid.NewGuid();
            }

            if (roleModel.ErrorMessages.Count == 0 && (roleModel.Code.StringVal().ToLower() != role.Code.StringVal().ToLower()))
            {
                bool isRoleExists = this.roleRepository.CheckRoleExistByName(roleModel);

                if (isRoleExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                role.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                role = roleModel.FromModel(role);

                //Save Or Update role details
                bool isSaved = this.roleRepository.SaveOrUpdateRoleDetails(role);

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
        /// Delete a record by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid roleId)
        {
            return this.roleRepository.DeleteById(roleId);
        }

        /// <summary>
        /// Check duplicate Role by Name
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        public bool CheckRoleExistByName(RoleModel roleModel)
        {
            return this.roleRepository.CheckRoleExistByName(roleModel);
        }

        /// <summary>}
        /// List of Role with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<RoleViewModel> GetRolesByCriteria(SearchRoleModel searchModel)
        {
            return this.roleRepository.GetRolesByCriteria(searchModel);
        }
    }
}