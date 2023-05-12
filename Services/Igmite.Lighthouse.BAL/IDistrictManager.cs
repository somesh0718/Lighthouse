using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the District entity
    /// </summary>
    public interface IDistrictManager : IGenericManager<DistrictModel>
    {
        /// <summary>
        /// Get list of Districts
        /// </summary>
        /// <returns></returns>
        IQueryable<DistrictModel> GetDistricts();

        /// <summary>
        /// Get list of Districts by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<DistrictModel> GetDistrictsByName(string districtName);

        /// <summary>
        /// Get District by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        DistrictModel GetDistrictById(string districtCode);

        /// <summary>
        /// Get District by DistrictCode using async
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        Task<DistrictModel> GetDistrictByIdAsync(string districtCode);

        /// <summary>
        /// Insert/Update District entity
        /// </summary>
        /// <param name="districtModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateDistrictDetails(DistrictModel districtModel);

        /// <summary>
        /// Delete a record by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        bool DeleteById(string districtCode);

        /// <summary>
        /// Check duplicate District by Name
        /// </summary>
        /// <param name="districtModel"></param>
        /// <returns></returns>
        bool CheckDistrictExistByName(DistrictModel districtModel);

        /// <summary>
        /// List of District with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<DistrictViewModel> GetDistrictsByCriteria(SearchDistrictModel searchModel);
    }
}
