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
    /// Manager of the VCSchoolVisit entity
    /// </summary>
    public class VCSchoolVisitManager : GenericManager<VCSchoolVisitModel>, IVCSchoolVisitManager
    {
        private readonly IVCSchoolVisitRepository vcSchoolVisitRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vcSchoolVisit manager.
        /// </summary>
        /// <param name="vcSchoolVisitRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VCSchoolVisitManager(IVCSchoolVisitRepository _vcSchoolVisitRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vcSchoolVisitRepository = _vcSchoolVisitRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VCSchoolVisits
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCSchoolVisitModel> GetVCSchoolVisits()
        {
            var vcSchoolVisits = this.vcSchoolVisitRepository.GetVCSchoolVisits();

            IList<VCSchoolVisitModel> vcSchoolVisitModels = new List<VCSchoolVisitModel>();
            vcSchoolVisits.ForEach((user) => vcSchoolVisitModels.Add(user.ToModel()));

            return vcSchoolVisitModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VCSchoolVisits by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCSchoolVisitModel> GetVCSchoolVisitsByName(string vcSchoolVisitName)
        {
            var vcSchoolVisits = this.vcSchoolVisitRepository.GetVCSchoolVisitsByName(vcSchoolVisitName);

            IList<VCSchoolVisitModel> vcSchoolVisitModels = new List<VCSchoolVisitModel>();
            vcSchoolVisits.ForEach((user) => vcSchoolVisitModels.Add(user.ToModel()));

            return vcSchoolVisitModels.AsQueryable();
        }

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        public VCSchoolVisitModel GetVCSchoolVisitById(Guid vcSchoolVisitId)
        {
            VCSchoolVisit vcSchoolVisit = this.vcSchoolVisitRepository.GetVCSchoolVisitById(vcSchoolVisitId);

            return (vcSchoolVisit != null) ? vcSchoolVisit.ToModel() : null;
        }

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId using async
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        public async Task<VCSchoolVisitModel> GetVCSchoolVisitByIdAsync(Guid vcSchoolVisitId)
        {
            var vcSchoolVisit = await this.vcSchoolVisitRepository.GetVCSchoolVisitByIdAsync(vcSchoolVisitId);

            return (vcSchoolVisit != null) ? vcSchoolVisit.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VCSchoolVisit entity
        /// </summary>
        /// <param name="vcSchoolVisitModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVCSchoolVisitDetails(VCSchoolVisitModel vcSchoolVisitModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VCSchoolVisit vcSchoolVisit = null;

                //Validate model data
                vcSchoolVisitModel = vcSchoolVisitModel.GetModelValidationErrors<VCSchoolVisitModel>();

                if (vcSchoolVisitModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vcSchoolVisitModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vcSchoolVisitModel.RequestType == RequestType.Edit)
                {
                    vcSchoolVisit = this.vcSchoolVisitRepository.GetVCSchoolVisitById(vcSchoolVisitModel.VCSchoolVisitId);
                }
                else
                {
                    vcSchoolVisit = new VCSchoolVisit();
                    vcSchoolVisit.VCSchoolVisitId = Guid.NewGuid();
                }

                if (vcSchoolVisitModel.ErrorMessages.Count == 0)
                {
                    string errorMessage = this.vcSchoolVisitRepository.CheckVCSchoolVisitExistByName(vcSchoolVisitModel);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        response.Errors.Add(errorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vcSchoolVisit.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    vcSchoolVisit = vcSchoolVisitModel.FromModel(vcSchoolVisit);

                    if (vcSchoolVisitModel.RequestType == RequestType.New)
                    {
                        VCSchoolSector vcSchoolSector = this.commonRepository.GetVCSchoolSectorsByUserId(vcSchoolVisit.AuthUserId);
                        if (vcSchoolSector != null)
                            vcSchoolVisit.VCSchoolSectorId = vcSchoolSector.VCSchoolSectorId;
                    }

                    //Save Or Update vcSchoolVisit details
                    bool isSaved = this.vcSchoolVisitRepository.SaveOrUpdateVCSchoolVisitDetails(vcSchoolVisit);

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
                throw new Exception("BAL > SaveOrUpdateVCSchoolVisitDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcSchoolVisitId)
        {
            return this.vcSchoolVisitRepository.DeleteById(vcSchoolVisitId);
        }

        /// <summary>
        /// Check duplicate VCSchoolVisit by Name
        /// </summary>
        /// <param name="vcSchoolVisitModel"></param>
        /// <returns></returns>
        public string CheckVCSchoolVisitExistByName(VCSchoolVisitModel vcSchoolVisitModel)
        {
            return this.vcSchoolVisitRepository.CheckVCSchoolVisitExistByName(vcSchoolVisitModel);
        }

        /// <summary>}
        /// List of VCSchoolVisit with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCSchoolVisitViewModel> GetVCSchoolVisitsByCriteria(SearchVCSchoolVisitModel searchModel)
        {
            return this.vcSchoolVisitRepository.GetVCSchoolVisitsByCriteria(searchModel);
        }
    }
}