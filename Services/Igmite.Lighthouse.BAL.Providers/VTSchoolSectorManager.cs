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
    /// Manager of the VTSchoolSector entity
    /// </summary>
    public class VTSchoolSectorManager : GenericManager<VTSchoolSectorModel>, IVTSchoolSectorManager
    {
        private readonly IVTSchoolSectorRepository vtSchoolSectorRepository;
        private readonly ICommonRepository commonRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vtSchoolSector manager.
        /// </summary>
        /// <param name="vtSchoolSectorRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VTSchoolSectorManager(IVTSchoolSectorRepository _vtSchoolSectorRepository, ICommonRepository _commonRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vtSchoolSectorRepository = _vtSchoolSectorRepository;
            this.commonRepository = _commonRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VTSchoolSectors
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTSchoolSectorModel> GetVTSchoolSectors()
        {
            var vtSchoolSectors = this.vtSchoolSectorRepository.GetVTSchoolSectors();

            IList<VTSchoolSectorModel> vtSchoolSectorModels = new List<VTSchoolSectorModel>();
            vtSchoolSectors.ForEach((user) => vtSchoolSectorModels.Add(user.ToModel()));

            return vtSchoolSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTSchoolSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTSchoolSectorModel> GetVTSchoolSectorsByName(string vtSchoolSectorName)
        {
            var vtSchoolSectors = this.vtSchoolSectorRepository.GetVTSchoolSectorsByName(vtSchoolSectorName);

            IList<VTSchoolSectorModel> vtSchoolSectorModels = new List<VTSchoolSectorModel>();
            vtSchoolSectors.ForEach((user) => vtSchoolSectorModels.Add(user.ToModel()));

            return vtSchoolSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        public VTSchoolSectorModel GetVTSchoolSectorById(Guid vtSchoolSectorId)
        {
            VTSchoolSectorModel schoolSectorModel = null;
            VTSchoolSector vtSchoolSector = this.vtSchoolSectorRepository.GetVTSchoolSectorById(vtSchoolSectorId);

            if (vtSchoolSector != null)
            {
                schoolSectorModel = vtSchoolSector.ToModel();

                VocationalTrainer vocationalTrainer = this.commonRepository.GetVocationalTrainerById(vtSchoolSector.VTId);
                schoolSectorModel.VCId = vocationalTrainer.VCTrainer.VCId;

                //schoolSectorModel.JobRoleIds = this.vtSchoolSectorRepository.GetJobRolesByVTSchoolSectorId(vtSchoolSector.VTSchoolSectorId);
            }

            return schoolSectorModel;
        }

        /// <summary>
        /// Get VTSchoolSector by SchoolId and SectorId
        /// </summary>
        /// <param name="SchoolId,SectorId"></param>
        /// <returns></returns>
        public VTSchoolSectorModel GetVTSchoolSectorBySchoolIdANDSectorId(Guid SchoolId, Guid SectorId)
        {
            VTSchoolSectorModel schoolSectorModel = null;
            VTSchoolSector vtSchoolSector = this.vtSchoolSectorRepository.GetVTSchoolSectorBySchoolIdAndSectorId(SchoolId, SectorId);

            if (vtSchoolSector != null)
            {
                schoolSectorModel = vtSchoolSector.ToModel();

                schoolSectorModel.JobRoleIds = this.vtSchoolSectorRepository.GetJobRolesByVTSchoolSectorId(vtSchoolSector.VTSchoolSectorId);
            }

            return schoolSectorModel;
        }

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId using async
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        public async Task<VTSchoolSectorModel> GetVTSchoolSectorByIdAsync(Guid vtSchoolSectorId)
        {
            var vtSchoolSector = await this.vtSchoolSectorRepository.GetVTSchoolSectorByIdAsync(vtSchoolSectorId);

            return (vtSchoolSector != null) ? vtSchoolSector.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTSchoolSector entity
        /// </summary>
        /// <param name="vtSchoolSectorModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTSchoolSectorDetails(VTSchoolSectorModel vtSchoolSectorModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VTSchoolSector vtSchoolSector = null;

                //Validate model data
                vtSchoolSectorModel = vtSchoolSectorModel.GetModelValidationErrors<VTSchoolSectorModel>();

                if (vtSchoolSectorModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vtSchoolSectorModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vtSchoolSectorModel.RequestType == RequestType.Edit)
                {
                    vtSchoolSector = this.vtSchoolSectorRepository.GetVTSchoolSectorById(vtSchoolSectorModel.VTSchoolSectorId);
                }
                else
                {
                    vtSchoolSector = new VTSchoolSector();
                    vtSchoolSectorModel.VTSchoolSectorId = Guid.NewGuid();
                }

                if (!vtSchoolSectorModel.DateOfRemoval.HasValue)
                {
                    string existVTSchoolSectorMessage = this.vtSchoolSectorRepository.CheckVTSchoolSectorExistByName(vtSchoolSector, vtSchoolSectorModel);

                    if (!string.IsNullOrEmpty(existVTSchoolSectorMessage))
                    {
                        response.Errors.Add(existVTSchoolSectorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    bool isChangeStatus = (vtSchoolSector.IsActive && !vtSchoolSectorModel.IsActive) || (!vtSchoolSector.IsActive && vtSchoolSectorModel.IsActive);
                    vtSchoolSector.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    vtSchoolSector = vtSchoolSectorModel.FromModel(vtSchoolSector);

                    //Save Or Update vtSchoolSector details
                    bool isSaved = this.vtSchoolSectorRepository.SaveOrUpdateVTSchoolSectorDetails(vtSchoolSector, vtSchoolSectorModel, isChangeStatus);

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateVTSchoolSectorDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtSchoolSectorId)
        {
            return this.vtSchoolSectorRepository.DeleteById(vtSchoolSectorId);
        }

        /// <summary>}
        /// List of VTSchoolSector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTSchoolSectorViewModel> GetVTSchoolSectorsByCriteria(SearchVTSchoolSectorModel searchModel)
        {
            return this.vtSchoolSectorRepository.GetVTSchoolSectorsByCriteria(searchModel);
        }
    }
}