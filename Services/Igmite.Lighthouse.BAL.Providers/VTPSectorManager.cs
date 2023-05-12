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
    /// Manager of the VTPSector entity
    /// </summary>
    public class VTPSectorManager : GenericManager<VTPSectorModel>, IVTPSectorManager
    {
        private readonly IVTPSectorRepository vtpSectorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vtpSector manager.
        /// </summary>
        /// <param name="vtpSectorRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VTPSectorManager(IVTPSectorRepository _vtpSectorRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vtpSectorRepository = _vtpSectorRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VTPSectors
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPSectorModel> GetVTPSectors()
        {
            var vtpSectors = this.vtpSectorRepository.GetVTPSectors();

            IList<VTPSectorModel> vtpSectorModels = new List<VTPSectorModel>();
            vtpSectors.ForEach((user) => vtpSectorModels.Add(user.ToModel()));

            return vtpSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPSectorModel> GetVTPSectorsByName(string vtpSectorName)
        {
            var vtpSectors = this.vtpSectorRepository.GetVTPSectorsByName(vtpSectorName);

            IList<VTPSectorModel> vtpSectorModels = new List<VTPSectorModel>();
            vtpSectors.ForEach((user) => vtpSectorModels.Add(user.ToModel()));

            return vtpSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get VTPSector by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        public VTPSectorModel GetVTPSectorById(Guid vtpSectorId)
        {
            VTPSector vtpSector = this.vtpSectorRepository.GetVTPSectorById(vtpSectorId);

            return (vtpSector != null) ? vtpSector.ToModel() : null;
        }

        /// <summary>
        /// Get VTPSector by VTPSectorId using async
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        public async Task<VTPSectorModel> GetVTPSectorByIdAsync(Guid vtpSectorId)
        {
            var vtpSector = await this.vtpSectorRepository.GetVTPSectorByIdAsync(vtpSectorId);

            return (vtpSector != null) ? vtpSector.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTPSector entity
        /// </summary>
        /// <param name="vtpSectorModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTPSectorDetails(VTPSectorModel vtpSectorModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTPSector vtpSector = null;

            //Validate model data
            vtpSectorModel = vtpSectorModel.GetModelValidationErrors<VTPSectorModel>();

            if (vtpSectorModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtpSectorModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtpSectorModel.RequestType == RequestType.Edit)
            {
                vtpSector = this.vtpSectorRepository.GetVTPSectorById(vtpSectorModel.VTPSectorId);
            }
            else
            {
                vtpSector = new VTPSector();
                vtpSectorModel.VTPSectorId = Guid.NewGuid();
            }

            if (vtpSectorModel.ErrorMessages.Count == 0 && !(Guid.Equals(vtpSectorModel.AcademicYearId, vtpSector.AcademicYearId) && Guid.Equals(vtpSectorModel.VTPId, vtpSector.VTPId) && Guid.Equals(vtpSectorModel.SectorId, vtpSector.SectorId)))
            {
                bool isVTPSectorExists = this.vtpSectorRepository.CheckVTPSectorExistByName(vtpSectorModel);

                if (isVTPSectorExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (vtpSectorModel.RequestType == RequestType.Edit && vtpSector.IsActive && !vtpSectorModel.IsActive)
            {
                bool canInactiveVTPSector = this.vtpSectorRepository.CheckUserCanInactiveVTPSectorById(vtpSector);

                if (!canInactiveVTPSector)
                    response.Errors.Add("Current VTPSector cannot be inactive until all dependencies on that data have been removed or inactive");
            }

            if (response.Errors.Count == 0)
            {
                vtpSector.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vtpSector = vtpSectorModel.FromModel(vtpSector);

                //Save Or Update vtpSector details
                bool isSaved = this.vtpSectorRepository.SaveOrUpdateVTPSectorDetails(vtpSector);

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
        /// Delete a record by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpSectorId)
        {
            return this.vtpSectorRepository.DeleteById(vtpSectorId);
        }

        /// <summary>
        /// Check duplicate VTPSector by Name
        /// </summary>
        /// <param name="vtpSectorModel"></param>
        /// <returns></returns>
        public bool CheckVTPSectorExistByName(VTPSectorModel vtpSectorModel)
        {
            return this.vtpSectorRepository.CheckVTPSectorExistByName(vtpSectorModel);
        }

        /// <summary>
        /// Get list of VTPSector
        /// </summary>
        /// <returns></returns>
        public IList<VTPSectorModel> GetVTPSectorList()
        {
            var vtpSectors = this.vtpSectorRepository.GetVTPSectorList();

            IList<VTPSectorModel> vtpSectorModels = new List<VTPSectorModel>();
            for (int vsIndex = 0; vsIndex < vtpSectors.Count; vsIndex++)
            {
                vtpSectorModels.Add(new VTPSectorModel { VTPSectorId = vtpSectors[vsIndex].VTPSectorId, AcademicYearId = vtpSectors[vsIndex].AcademicYearId, SectorId = vtpSectors[vsIndex].SectorId, VTPId = vtpSectors[vsIndex].VTPId });
            }

            return vtpSectorModels;
        }

        /// <summary>}
        /// List of VTPSector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPSectorViewModel> GetVTPSectorsByCriteria(SearchVTPSectorModel searchModel)
        {
            return this.vtpSectorRepository.GetVTPSectorsByCriteria(searchModel);
        }
    }
}