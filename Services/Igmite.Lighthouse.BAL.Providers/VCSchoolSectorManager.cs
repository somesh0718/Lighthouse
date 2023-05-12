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
    /// Manager of the VCSchoolSector entity
    /// </summary>
    public class VCSchoolSectorManager : GenericManager<VCSchoolSectorModel>, IVCSchoolSectorManager
    {
        private readonly IVCSchoolSectorRepository vcSchoolSectorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vcSchoolSector manager.
        /// </summary>
        /// <param name="vcSchoolSectorRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VCSchoolSectorManager(IVCSchoolSectorRepository _vcSchoolSectorRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vcSchoolSectorRepository = _vcSchoolSectorRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VCSchoolSectors
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCSchoolSectorModel> GetVCSchoolSectors()
        {
            var vcSchoolSectors = this.vcSchoolSectorRepository.GetVCSchoolSectors();

            IList<VCSchoolSectorModel> vcSchoolSectorModels = new List<VCSchoolSectorModel>();
            vcSchoolSectors.ForEach((user) => vcSchoolSectorModels.Add(user.ToModel()));

            return vcSchoolSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VCSchoolSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCSchoolSectorModel> GetVCSchoolSectorsByName(string vcSchoolSectorName)
        {
            var vcSchoolSectors = this.vcSchoolSectorRepository.GetVCSchoolSectorsByName(vcSchoolSectorName);

            IList<VCSchoolSectorModel> vcSchoolSectorModels = new List<VCSchoolSectorModel>();
            vcSchoolSectors.ForEach((user) => vcSchoolSectorModels.Add(user.ToModel()));

            return vcSchoolSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        public VCSchoolSectorModel GetVCSchoolSectorById(Guid vcSchoolSectorId)
        {
            VCSchoolSector vcSchoolSector = this.vcSchoolSectorRepository.GetVCSchoolSectorById(vcSchoolSectorId);

            return (vcSchoolSector != null) ? vcSchoolSector.ToModel() : null;
        }

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId using async
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        public async Task<VCSchoolSectorModel> GetVCSchoolSectorByIdAsync(Guid vcSchoolSectorId)
        {
            var vcSchoolSector = await this.vcSchoolSectorRepository.GetVCSchoolSectorByIdAsync(vcSchoolSectorId);

            return (vcSchoolSector != null) ? vcSchoolSector.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VCSchoolSector entity
        /// </summary>
        /// <param name="vcSchoolSectorModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVCSchoolSectorDetails(VCSchoolSectorModel vcSchoolSectorModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VCSchoolSector vcSchoolSector = null;

                //Validate model data
                vcSchoolSectorModel = vcSchoolSectorModel.GetModelValidationErrors<VCSchoolSectorModel>();

                if (vcSchoolSectorModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vcSchoolSectorModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vcSchoolSectorModel.RequestType == RequestType.Edit)
                {
                    vcSchoolSector = this.vcSchoolSectorRepository.GetVCSchoolSectorById(vcSchoolSectorModel.VCSchoolSectorId);
                }
                else
                {
                    vcSchoolSector = new VCSchoolSector();
                    vcSchoolSectorModel.VCSchoolSectorId = Guid.NewGuid();
                }

                if (vcSchoolSectorModel.ErrorMessages.Count == 0)
                {
                    string existVCSchoolSectorMessage = this.vcSchoolSectorRepository.CheckVCSchoolSectorExistByName(vcSchoolSector, vcSchoolSectorModel);

                    if (!string.IsNullOrEmpty(existVCSchoolSectorMessage))
                    {
                        response.Errors.Add(existVCSchoolSectorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vcSchoolSector.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    vcSchoolSector = vcSchoolSectorModel.FromModel(vcSchoolSector);

                    //Save Or Update vcSchoolSector details
                    bool isSaved = this.vcSchoolSectorRepository.SaveOrUpdateVCSchoolSectorDetails(vcSchoolSector);

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
                throw new Exception("BAL > SaveOrUpdateVCSchoolSectorDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcSchoolSectorId)
        {
            return this.vcSchoolSectorRepository.DeleteById(vcSchoolSectorId);
        }

        /// <summary>}
        /// List of VCSchoolSector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCSchoolSectorViewModel> GetVCSchoolSectorsByCriteria(SearchVCSchoolSectorModel searchModel)
        {
            return this.vcSchoolSectorRepository.GetVCSchoolSectorsByCriteria(searchModel);
        }
    }
}