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
    /// Manager of the District entity
    /// </summary>
    public class DistrictManager : GenericManager<DistrictModel>, IDistrictManager
    {
        private readonly IDistrictRepository districtRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the district manager.
        /// </summary>
        /// <param name="districtRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public DistrictManager(IDistrictRepository _districtRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.districtRepository = _districtRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Districts
        /// </summary>
        /// <returns></returns>
        public IQueryable<DistrictModel> GetDistricts()
        {
            var districts = this.districtRepository.GetDistricts();

            IList<DistrictModel> districtModels = new List<DistrictModel>();
            districts.ForEach((user) => districtModels.Add(user.ToModel()));

            return districtModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Districts by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DistrictModel> GetDistrictsByName(string districtName)
        {
            var districts = this.districtRepository.GetDistrictsByName(districtName);

            IList<DistrictModel> districtModels = new List<DistrictModel>();
            districts.ForEach((user) => districtModels.Add(user.ToModel()));

            return districtModels.AsQueryable();
        }

        /// <summary>
        /// Get District by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public DistrictModel GetDistrictById(string districtCode)
        {
            District district = this.districtRepository.GetDistrictById(districtCode);

            return (district != null) ? district.ToModel() : null;
        }

        /// <summary>
        /// Get District by DistrictCode using async
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public async Task<DistrictModel> GetDistrictByIdAsync(string districtCode)
        {
            var district = await this.districtRepository.GetDistrictByIdAsync(districtCode);

            return (district != null) ? district.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update District entity
        /// </summary>
        /// <param name="districtModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateDistrictDetails(DistrictModel districtModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            District district = null;

            //Validate model data
            districtModel = districtModel.GetModelValidationErrors<DistrictModel>();

            if (districtModel.ErrorMessages.Count > 0)
            {
                response.Errors = districtModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (districtModel.RequestType == RequestType.Edit)
            {
                district = this.districtRepository.GetDistrictById(districtModel.DistrictCode);
            }
            else
            {
                district = new District();
            }

            if (districtModel.ErrorMessages.Count == 0 && (districtModel.DistrictName.StringVal().ToLower() != district.DistrictName.StringVal().ToLower()))
            {
                bool isDistrictExists = this.districtRepository.CheckDistrictExistByName(districtModel);

                if (isDistrictExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                district.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                district = districtModel.FromModel(district);

                //Save Or Update district details
                bool isSaved = this.districtRepository.SaveOrUpdateDistrictDetails(district);

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
        /// Delete a record by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public bool DeleteById(string districtCode)
        {
            return this.districtRepository.DeleteById(districtCode);
        }

        /// <summary>
        /// Check duplicate District by Name
        /// </summary>
        /// <param name="districtModel"></param>
        /// <returns></returns>
        public bool CheckDistrictExistByName(DistrictModel districtModel)
        {
            return this.districtRepository.CheckDistrictExistByName(districtModel);
        }

        /// <summary>}
        /// List of District with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DistrictViewModel> GetDistrictsByCriteria(SearchDistrictModel searchModel)
        {
            return this.districtRepository.GetDistrictsByCriteria(searchModel);
        }
    }
}