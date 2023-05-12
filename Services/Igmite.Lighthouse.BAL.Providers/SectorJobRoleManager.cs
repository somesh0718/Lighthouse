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
    /// Manager of the SectorJobRole entity
    /// </summary>
    public class SectorJobRoleManager : GenericManager<SectorJobRoleModel>, ISectorJobRoleManager
    {
        private readonly ISectorJobRoleRepository sectorJobRoleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the sectorJobRole manager.
        /// </summary>
        /// <param name="sectorJobRoleRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SectorJobRoleManager(ISectorJobRoleRepository _sectorJobRoleRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.sectorJobRoleRepository = _sectorJobRoleRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of SectorJobRoles
        /// </summary>
        /// <returns></returns>
        public IQueryable<SectorJobRoleModel> GetSectorJobRoles()
        {
            var sectorJobRoles = this.sectorJobRoleRepository.GetSectorJobRoles();

            IList<SectorJobRoleModel> sectorJobRoleModels = new List<SectorJobRoleModel>();
            sectorJobRoles.ForEach((user) => sectorJobRoleModels.Add(user.ToModel()));

            return sectorJobRoleModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SectorJobRoles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SectorJobRoleModel> GetSectorJobRolesByName(string sectorJobRoleName)
        {
            var sectorJobRoles = this.sectorJobRoleRepository.GetSectorJobRolesByName(sectorJobRoleName);

            IList<SectorJobRoleModel> sectorJobRoleModels = new List<SectorJobRoleModel>();
            sectorJobRoles.ForEach((user) => sectorJobRoleModels.Add(user.ToModel()));

            return sectorJobRoleModels.AsQueryable();
        }

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        public SectorJobRoleModel GetSectorJobRoleById(Guid sectorJobRoleId)
        {
            SectorJobRole sectorJobRole = this.sectorJobRoleRepository.GetSectorJobRoleById(sectorJobRoleId);

            return (sectorJobRole != null) ? sectorJobRole.ToModel() : null;
        }

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId using async
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        public async Task<SectorJobRoleModel> GetSectorJobRoleByIdAsync(Guid sectorJobRoleId)
        {
            var sectorJobRole = await this.sectorJobRoleRepository.GetSectorJobRoleByIdAsync(sectorJobRoleId);

            return (sectorJobRole != null) ? sectorJobRole.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SectorJobRole entity
        /// </summary>
        /// <param name="sectorJobRoleModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSectorJobRoleDetails(SectorJobRoleModel sectorJobRoleModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            SectorJobRole sectorJobRole = null;

            //Validate model data
            sectorJobRoleModel = sectorJobRoleModel.GetModelValidationErrors<SectorJobRoleModel>();

            if (sectorJobRoleModel.ErrorMessages.Count > 0)
            {
                response.Errors = sectorJobRoleModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (sectorJobRoleModel.RequestType == RequestType.Edit)
            {
                sectorJobRole = this.sectorJobRoleRepository.GetSectorJobRoleById(sectorJobRoleModel.SectorJobRoleId);
            }
            else
            {
                sectorJobRole = new SectorJobRole();
                sectorJobRoleModel.SectorJobRoleId = Guid.NewGuid();
            }

            if (sectorJobRoleModel.ErrorMessages.Count == 0 && (sectorJobRoleModel.JobRoleId.StringVal().ToLower() != sectorJobRole.JobRoleId.StringVal().ToLower()))
            {
                bool isSectorJobRoleExists = this.sectorJobRoleRepository.CheckSectorJobRoleExistByName(sectorJobRoleModel);

                if (isSectorJobRoleExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                sectorJobRole.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                sectorJobRole = sectorJobRoleModel.FromModel(sectorJobRole);

                //Save Or Update sectorJobRole details
                bool isSaved = this.sectorJobRoleRepository.SaveOrUpdateSectorJobRoleDetails(sectorJobRole);

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
        /// Delete a record by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid sectorJobRoleId)
        {
            return this.sectorJobRoleRepository.DeleteById(sectorJobRoleId);
        }

        /// <summary>
        /// Check duplicate SectorJobRole by Name
        /// </summary>
        /// <param name="sectorJobRoleModel"></param>
        /// <returns></returns>
        public bool CheckSectorJobRoleExistByName(SectorJobRoleModel sectorJobRoleModel)
        {
            return this.sectorJobRoleRepository.CheckSectorJobRoleExistByName(sectorJobRoleModel);
        }

        /// <summary>}
        /// List of SectorJobRole with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SectorJobRoleViewModel> GetSectorJobRolesByCriteria(SearchSectorJobRoleModel searchModel)
        {
            return this.sectorJobRoleRepository.GetSectorJobRolesByCriteria(searchModel);
        }
    }
}