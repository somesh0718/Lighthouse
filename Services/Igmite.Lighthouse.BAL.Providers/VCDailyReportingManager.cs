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
    /// Manager of the VCDailyReporting entity
    /// </summary>
    public class VCDailyReportingManager : GenericManager<VCDailyReportingModel>, IVCDailyReportingManager
    {
        private readonly IVCDailyReportingRepository vcDailyReportingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vcDailyReporting manager.
        /// </summary>
        /// <param name="vcDailyReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VCDailyReportingManager(IVCDailyReportingRepository _vcDailyReportingRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vcDailyReportingRepository = _vcDailyReportingRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VCDailyReportings
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCDailyReportingModel> GetVCDailyReportings()
        {
            var vcDailyReportings = this.vcDailyReportingRepository.GetVCDailyReportings();

            IList<VCDailyReportingModel> vcDailyReportingModels = new List<VCDailyReportingModel>();
            vcDailyReportings.ForEach((user) => vcDailyReportingModels.Add(user.ToModel()));

            return vcDailyReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VCDailyReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCDailyReportingModel> GetVCDailyReportingsByName(string vcDailyReportingName)
        {
            var vcDailyReportings = this.vcDailyReportingRepository.GetVCDailyReportingsByName(vcDailyReportingName);

            IList<VCDailyReportingModel> vcDailyReportingModels = new List<VCDailyReportingModel>();
            vcDailyReportings.ForEach((user) => vcDailyReportingModels.Add(user.ToModel()));

            return vcDailyReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        public VCDailyReportingModel GetVCDailyReportingById(Guid vcDailyReportingId)
        {
            VCDailyReportingModel dailyReportingModel = null;

            VCDailyReporting vcDailyReporting = this.vcDailyReportingRepository.GetVCDailyReportingById(vcDailyReportingId);

            if (vcDailyReporting != null)
            {
                dailyReportingModel = vcDailyReporting.ToModel();

                dailyReportingModel.WorkingDayTypeIds = this.vcDailyReportingRepository.GetWorkTypesByDailyReportingId(vcDailyReporting.VCDailyReportingId);

                dailyReportingModel.IndustryExposureVisit = this.vcDailyReportingRepository.GetIndustryExposureVisitByDailyReportingId(vcDailyReporting.VCDailyReportingId);

                dailyReportingModel.Leave = this.vcDailyReportingRepository.GetLeaveByDailyReportingId(vcDailyReporting.VCDailyReportingId);

                dailyReportingModel.Holiday = this.vcDailyReportingRepository.GetHolidayByDailyReportingId(vcDailyReporting.VCDailyReportingId);

                dailyReportingModel.Pratical = this.vcDailyReportingRepository.GetPraticalByDailyReportingId(vcDailyReporting.VCDailyReportingId);
            }

            return dailyReportingModel;
        }

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId using async
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        public async Task<VCDailyReportingModel> GetVCDailyReportingByIdAsync(Guid vcDailyReportingId)
        {
            var vcDailyReporting = await this.vcDailyReportingRepository.GetVCDailyReportingByIdAsync(vcDailyReportingId);

            return (vcDailyReporting != null) ? vcDailyReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VCDailyReporting entity
        /// </summary>
        /// <param name="vcDailyReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVCDailyReportingDetails(VCDailyReportingModel vcDailyReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VCDailyReporting vcDailyReporting = null;

                //Validate model data
                vcDailyReportingModel = vcDailyReportingModel.GetModelValidationErrors<VCDailyReportingModel>();

                if (vcDailyReportingModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vcDailyReportingModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vcDailyReportingModel.RequestType == RequestType.Edit)
                {
                    vcDailyReporting = this.vcDailyReportingRepository.GetVCDailyReportingById(vcDailyReportingModel.VCDailyReportingId);
                }
                else
                {
                    vcDailyReporting = this.vcDailyReportingRepository.GetVCDailyReportingById(vcDailyReportingModel.VCId, vcDailyReportingModel.ReportDate);

                    if (vcDailyReporting == null)
                    {
                        vcDailyReporting = new VCDailyReporting();
                        vcDailyReporting.VCDailyReportingId = Guid.NewGuid();
                    }
                    else
                    {
                        vcDailyReportingModel.RequestType = RequestType.Edit;
                    }
                }

                if (vcDailyReportingModel.ErrorMessages.Count == 0)
                {
                    List<string> errorMessages = this.vcDailyReportingRepository.CheckVCDailyReportingExistByName(vcDailyReportingModel);

                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vcDailyReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    vcDailyReporting = vcDailyReportingModel.FromModel(vcDailyReporting);

                    if (vcDailyReportingModel.RequestType == RequestType.New)
                    {
                        VCSchoolSector vcSchoolSector = this.commonRepository.GetVCSchoolSectorsByUserId(vcDailyReporting.AuthUserId);
                        if (vcSchoolSector != null)
                        {
                            vcDailyReporting.VCSchoolSectorId = vcSchoolSector.VCSchoolSectorId;
                        }
                    }

                    //Save Or Update vcDailyReporting details
                    bool isSaved = this.vcDailyReportingRepository.SaveOrUpdateVCDailyReportingDetails(vcDailyReporting, vcDailyReportingModel);

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
                throw new Exception("BAL > SaveOrUpdateVCDailyReportingDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcDailyReportingId)
        {
            return this.vcDailyReportingRepository.DeleteById(vcDailyReportingId);
        }

        /// <summary>
        /// Check duplicate VCDailyReporting by Name
        /// </summary>
        /// <param name="vcDailyReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckVCDailyReportingExistByName(VCDailyReportingModel vcDailyReportingModel)
        {
            return this.vcDailyReportingRepository.CheckVCDailyReportingExistByName(vcDailyReportingModel);
        }

        /// <summary>}
        /// List of VCDailyReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCDailyReportingViewModel> GetVCDailyReportingsByCriteria(SearchVCDailyReportingModel searchModel)
        {
            return this.vcDailyReportingRepository.GetVCDailyReportingsByCriteria(searchModel);
        }
    }
}