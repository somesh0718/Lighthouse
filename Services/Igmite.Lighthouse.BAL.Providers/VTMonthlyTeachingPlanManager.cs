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
    /// Manager of the VTMonthlyTeachingPlan entity
    /// </summary>
    public class VTMonthlyTeachingPlanManager : GenericManager<VTMonthlyTeachingPlanModel>, IVTMonthlyTeachingPlanManager
    {
        private readonly IVTMonthlyTeachingPlanRepository vtMonthlyTeachingPlanRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtMonthlyTeachingPlan manager.
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTMonthlyTeachingPlanManager(IVTMonthlyTeachingPlanRepository _vtMonthlyTeachingPlanRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtMonthlyTeachingPlanRepository = _vtMonthlyTeachingPlanRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTMonthlyTeachingPlans
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTMonthlyTeachingPlanModel> GetVTMonthlyTeachingPlans()
        {
            var vtMonthlyTeachingPlans = this.vtMonthlyTeachingPlanRepository.GetVTMonthlyTeachingPlans();

            IList<VTMonthlyTeachingPlanModel> vtMonthlyTeachingPlanModels = new List<VTMonthlyTeachingPlanModel>();
            vtMonthlyTeachingPlans.ForEach((user) => vtMonthlyTeachingPlanModels.Add(user.ToModel()));

            return vtMonthlyTeachingPlanModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTMonthlyTeachingPlans by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTMonthlyTeachingPlanModel> GetVTMonthlyTeachingPlansByName(string vtMonthlyTeachingPlanName)
        {
            var vtMonthlyTeachingPlans = this.vtMonthlyTeachingPlanRepository.GetVTMonthlyTeachingPlansByName(vtMonthlyTeachingPlanName);

            IList<VTMonthlyTeachingPlanModel> vtMonthlyTeachingPlanModels = new List<VTMonthlyTeachingPlanModel>();
            vtMonthlyTeachingPlans.ForEach((user) => vtMonthlyTeachingPlanModels.Add(user.ToModel()));

            return vtMonthlyTeachingPlanModels.AsQueryable();
        }

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        public VTMonthlyTeachingPlanModel GetVTMonthlyTeachingPlanById(Guid vtMonthlyTeachingPlanId)
        {
            VTMonthlyTeachingPlan vtMonthlyTeachingPlan = this.vtMonthlyTeachingPlanRepository.GetVTMonthlyTeachingPlanById(vtMonthlyTeachingPlanId);

            return (vtMonthlyTeachingPlan != null) ? vtMonthlyTeachingPlan.ToModel() : null;
        }

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId using async
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        public async Task<VTMonthlyTeachingPlanModel> GetVTMonthlyTeachingPlanByIdAsync(Guid vtMonthlyTeachingPlanId)
        {
            var vtMonthlyTeachingPlan = await this.vtMonthlyTeachingPlanRepository.GetVTMonthlyTeachingPlanByIdAsync(vtMonthlyTeachingPlanId);

            return (vtMonthlyTeachingPlan != null) ? vtMonthlyTeachingPlan.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTMonthlyTeachingPlan entity
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTMonthlyTeachingPlanDetails(VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTMonthlyTeachingPlan vtMonthlyTeachingPlan = null;

            //Validate model data
            vtMonthlyTeachingPlanModel = vtMonthlyTeachingPlanModel.GetModelValidationErrors<VTMonthlyTeachingPlanModel>();

            if (vtMonthlyTeachingPlanModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtMonthlyTeachingPlanModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtMonthlyTeachingPlanModel.RequestType == RequestType.Edit)
            {
                vtMonthlyTeachingPlan = this.vtMonthlyTeachingPlanRepository.GetVTMonthlyTeachingPlanById(vtMonthlyTeachingPlanModel.VTMonthlyTeachingPlanId);
            }
            else
            {
                vtMonthlyTeachingPlan = new VTMonthlyTeachingPlan();
                vtMonthlyTeachingPlanModel.VTMonthlyTeachingPlanId = Guid.NewGuid();
            }

            if (vtMonthlyTeachingPlanModel.ErrorMessages.Count == 0 && !(string.Equals(vtMonthlyTeachingPlanModel.Month, vtMonthlyTeachingPlan.Month) && DateTime.Equals(vtMonthlyTeachingPlanModel.WeekStartDate, vtMonthlyTeachingPlan.WeekStartDate) && DateTime.Equals(vtMonthlyTeachingPlanModel.WeekendDate, vtMonthlyTeachingPlan.WeekendDate)))
            {
                bool isVTMonthlyTeachingPlanExists = this.vtMonthlyTeachingPlanRepository.CheckVTMonthlyTeachingPlanExistByName(vtMonthlyTeachingPlanModel);

                if (isVTMonthlyTeachingPlanExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtMonthlyTeachingPlan.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtMonthlyTeachingPlanModel.RequestType == RequestType.New)
                {
                    VTClass vtClass = this.commonRepository.GetVTClassByUserId(vtMonthlyTeachingPlan.AuthUserId);
                    if (vtClass != null)
                        vtMonthlyTeachingPlanModel.VTClassId = vtClass.VTClassId;
                }

                vtMonthlyTeachingPlan = vtMonthlyTeachingPlanModel.FromModel(vtMonthlyTeachingPlan);

                //Save Or Update vtMonthlyTeachingPlan details
                bool isSaved = this.vtMonthlyTeachingPlanRepository.SaveOrUpdateVTMonthlyTeachingPlanDetails(vtMonthlyTeachingPlan);

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
        /// Delete a record by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtMonthlyTeachingPlanId)
        {
            return this.vtMonthlyTeachingPlanRepository.DeleteById(vtMonthlyTeachingPlanId);
        }

        /// <summary>
        /// Check duplicate VTMonthlyTeachingPlan by Name
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanModel"></param>
        /// <returns></returns>
        public bool CheckVTMonthlyTeachingPlanExistByName(VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel)
        {
            return this.vtMonthlyTeachingPlanRepository.CheckVTMonthlyTeachingPlanExistByName(vtMonthlyTeachingPlanModel);
        }

        /// <summary>}
        /// List of VTMonthlyTeachingPlan with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTMonthlyTeachingPlanViewModel> GetVTMonthlyTeachingPlansByCriteria(SearchVTMonthlyTeachingPlanModel searchModel)
        {
            return this.vtMonthlyTeachingPlanRepository.GetVTMonthlyTeachingPlansByCriteria(searchModel);
        }
    }
}