using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the District entity
    /// </summary>
    public interface IDistrictRepository : IGenericRepository<District>
    {
        /// <summary>
        /// Get list of District
        /// </summary>
        /// <returns></returns>
        IQueryable<District> GetDistricts();

        /// <summary>
        /// Get list of District by districtName
        /// </summary>
        /// <param name="districtName"></param>
        /// <returns></returns>
        IQueryable<District> GetDistrictsByName(string districtName);

        /// <summary>
        /// Get District by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        District GetDistrictById(string districtCode);

        /// <summary>
        /// Get District by DistrictCode using async
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        Task<District> GetDistrictByIdAsync(string districtCode);

        /// <summary>
        /// Insert/Update District entity
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        bool SaveOrUpdateDistrictDetails(District district);

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
