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
    /// Manager of the DRPDailyReporting entity
    /// </summary>
    public class DRPDailyReportingManager : GenericManager<DRPDailyReportingModel>, IDRPDailyReportingManager
    {
        private readonly IDRPDailyReportingRepository dailyReportingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the dailyReporting manager.
        /// </summary>
        /// <param name="dailyReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public DRPDailyReportingManager(IDRPDailyReportingRepository _dailyReportingRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.dailyReportingRepository = _dailyReportingRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of DRPDailyReportings
        /// </summary>
        /// <returns></returns>
        public IQueryable<DRPDailyReportingModel> GetDRPDailyReportings()
        {
            var dailyReportings = this.dailyReportingRepository.GetDRPDailyReportings();

            IList<DRPDailyReportingModel> dailyReportingModels = new List<DRPDailyReportingModel>();
            dailyReportings.ForEach((user) => dailyReportingModels.Add(user.ToModel()));

            return dailyReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of DRPDailyReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DRPDailyReportingModel> GetDRPDailyReportingsByName(string dailyReportingName)
        {
            var dailyReportings = this.dailyReportingRepository.GetDRPDailyReportingsByName(dailyReportingName);

            IList<DRPDailyReportingModel> dailyReportingModels = new List<DRPDailyReportingModel>();
            dailyReportings.ForEach((user) => dailyReportingModels.Add(user.ToModel()));

            return dailyReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public DRPDailyReportingModel GetDRPDailyReportingById(Guid dailyReportingId)
        {
            DRPDailyReportingModel dailyReportingModel = null;

            DRPDailyReporting dailyReporting = this.dailyReportingRepository.GetDRPDailyReportingById(dailyReportingId);

            if (dailyReporting != null)
            {
                dailyReportingModel = dailyReporting.ToModel();

                dailyReportingModel.WorkingDayTypeIds = this.dailyReportingRepository.GetWorkTypesByDailyReportingId(dailyReporting.DRPDailyReportingId);

                dailyReportingModel.IndustryExposureVisit = this.dailyReportingRepository.GetIndustryExposureVisitByDailyReportingId(dailyReporting.DRPDailyReportingId);

                dailyReportingModel.Leave = this.dailyReportingRepository.GetLeaveByDailyReportingId(dailyReporting.DRPDailyReportingId);

                dailyReportingModel.Holiday = this.dailyReportingRepository.GetHolidayByDailyReportingId(dailyReporting.DRPDailyReportingId);
            }

            return dailyReportingModel;
        }

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId using async
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public async Task<DRPDailyReportingModel> GetDRPDailyReportingByIdAsync(Guid dailyReportingId)
        {
            var dailyReporting = await this.dailyReportingRepository.GetDRPDailyReportingByIdAsync(dailyReportingId);

            return (dailyReporting != null) ? dailyReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update DRPDailyReporting entity
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateDRPDailyReportingDetails(DRPDailyReportingModel dailyReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                DRPDailyReporting dailyReporting = null;

                //Validate model data
                dailyReportingModel = dailyReportingModel.GetModelValidationErrors<DRPDailyReportingModel>();

                if (dailyReportingModel.ErrorMessages.Count > 0)
                {
                    response.Errors = dailyReportingModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (dailyReportingModel.RequestType == RequestType.Edit)
                {
                    dailyReporting = this.dailyReportingRepository.GetDRPDailyReportingById(dailyReportingModel.DRPDailyReportingId);
                }
                else
                {
                    dailyReporting = new DRPDailyReporting();
                    dailyReporting.DRPDailyReportingId = Guid.NewGuid();
                }

                if (dailyReportingModel.ErrorMessages.Count == 0)
                {
                    List<string> errorMessages = this.dailyReportingRepository.CheckDRPDailyReportingExistByName(dailyReportingModel);

                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    dailyReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    dailyReporting = dailyReportingModel.FromModel(dailyReporting);

                    //Save Or Update dailyReporting details
                    bool isSaved = this.dailyReportingRepository.SaveOrUpdateDRPDailyReportingDetails(dailyReporting, dailyReportingModel);

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
                throw new Exception("BAL > SaveOrUpdateDRPDailyReportingDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid dailyReportingId)
        {
            return this.dailyReportingRepository.DeleteById(dailyReportingId);
        }

        /// <summary>
        /// Check duplicate DRPDailyReporting by Name
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckDRPDailyReportingExistByName(DRPDailyReportingModel dailyReportingModel)
        {
            return this.dailyReportingRepository.CheckDRPDailyReportingExistByName(dailyReportingModel);
        }

        /// <summary>}
        /// List of DRPDailyReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DRPDailyReportingViewModel> GetDRPDailyReportingsByCriteria(SearchDRPDailyReportingModel searchModel)
        {
            return this.dailyReportingRepository.GetDRPDailyReportingsByCriteria(searchModel);
        }
    }
}