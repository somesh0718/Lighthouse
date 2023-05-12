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
    /// Manager of the VTPSectorJobRole entity
    /// </summary>
    public class VTPSectorJobRoleManager : GenericManager<VTPSectorJobRoleModel>, IVTPSectorJobRoleManager
    {
        private readonly IVTPSectorJobRoleRepository vtpSectorJobRoleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vtpSectorJobRole manager.
        /// </summary>
        /// <param name="vtpSectorJobRoleRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VTPSectorJobRoleManager(IVTPSectorJobRoleRepository _vtpSectorJobRoleRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vtpSectorJobRoleRepository = _vtpSectorJobRoleRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VTPSectorJobRoles
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPSectorJobRoleModel> GetVTPSectorJobRoles()
        {
            var vtpSectorJobRoles = this.vtpSectorJobRoleRepository.GetVTPSectorJobRoles();

            IList<VTPSectorJobRoleModel> vtpSectorJobRoleModels = new List<VTPSectorJobRoleModel>();
            vtpSectorJobRoles.ForEach((user) => vtpSectorJobRoleModels.Add(user.ToModel()));

            return vtpSectorJobRoleModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPSectorJobRoles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPSectorJobRoleModel> GetVTPSectorJobRolesByName(string vtpSectorJobRoleName)
        {
            var vtpSectorJobRoles = this.vtpSectorJobRoleRepository.GetVTPSectorJobRolesByName(vtpSectorJobRoleName);

            IList<VTPSectorJobRoleModel> vtpSectorJobRoleModels = new List<VTPSectorJobRoleModel>();
            vtpSectorJobRoles.ForEach((user) => vtpSectorJobRoleModels.Add(user.ToModel()));

            return vtpSectorJobRoleModels.AsQueryable();
        }

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        public VTPSectorJobRoleModel GetVTPSectorJobRoleById(Guid vtpSectorJobRoleId)
        {
            VTPSectorJobRole vtpSectorJobRole = this.vtpSectorJobRoleRepository.GetVTPSectorJobRoleById(vtpSectorJobRoleId);

            return (vtpSectorJobRole != null) ? vtpSectorJobRole.ToModel() : null;
        }

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId using async
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        public async Task<VTPSectorJobRoleModel> GetVTPSectorJobRoleByIdAsync(Guid vtpSectorJobRoleId)
        {
            var vtpSectorJobRole = await this.vtpSectorJobRoleRepository.GetVTPSectorJobRoleByIdAsync(vtpSectorJobRoleId);

            return (vtpSectorJobRole != null) ? vtpSectorJobRole.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTPSectorJobRole entity
        /// </summary>
        /// <param name="vtpSectorJobRoleModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTPSectorJobRoleDetails(VTPSectorJobRoleModel vtpSectorJobRoleModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTPSectorJobRole vtpSectorJobRole = null;

            //Validate model data
            vtpSectorJobRoleModel = vtpSectorJobRoleModel.GetModelValidationErrors<VTPSectorJobRoleModel>();

            if (vtpSectorJobRoleModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtpSectorJobRoleModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtpSectorJobRoleModel.RequestType == RequestType.Edit)
            {
                vtpSectorJobRole = this.vtpSectorJobRoleRepository.GetVTPSectorJobRoleById(vtpSectorJobRoleModel.VTPSectorJobRoleId);
            }
            else
            {
                vtpSectorJobRole = new VTPSectorJobRole();
                vtpSectorJobRoleModel.VTPSectorJobRoleId = Guid.NewGuid();
            }

            if (vtpSectorJobRoleModel.ErrorMessages.Count == 0 && (vtpSectorJobRoleModel.VTPSectorJobRoleName.StringVal().ToLower() != vtpSectorJobRole.VTPSectorJobRoleName.StringVal().ToLower()))
            {
                bool isVTPSectorJobRoleExists = this.vtpSectorJobRoleRepository.CheckVTPSectorJobRoleExistByName(vtpSectorJobRoleModel);

                if (isVTPSectorJobRoleExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtpSectorJobRole.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vtpSectorJobRole = vtpSectorJobRoleModel.FromModel(vtpSectorJobRole);

                //Save Or Update vtpSectorJobRole details
                bool isSaved = this.vtpSectorJobRoleRepository.SaveOrUpdateVTPSectorJobRoleDetails(vtpSectorJobRole);

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
        /// Delete a record by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpSectorJobRoleId)
        {
            return this.vtpSectorJobRoleRepository.DeleteById(vtpSectorJobRoleId);
        }

        /// <summary>
        /// Check duplicate VTPSectorJobRole by Name
        /// </summary>
        /// <param name="vtpSectorJobRoleModel"></param>
        /// <returns></returns>
        public bool CheckVTPSectorJobRoleExistByName(VTPSectorJobRoleModel vtpSectorJobRoleModel)
        {
            return this.vtpSectorJobRoleRepository.CheckVTPSectorJobRoleExistByName(vtpSectorJobRoleModel);
        }

        /// <summary>}
        /// List of VTPSectorJobRole with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPSectorJobRoleViewModel> GetVTPSectorJobRolesByCriteria(SearchVTPSectorJobRoleModel searchModel)
        {
            return this.vtpSectorJobRoleRepository.GetVTPSectorJobRolesByCriteria(searchModel);
        }
    }
}