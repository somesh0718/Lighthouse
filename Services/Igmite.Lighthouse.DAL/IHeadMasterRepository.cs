using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the HeadMaster entity
    /// </summary>
    public interface IHeadMasterRepository : IGenericRepository<HeadMaster>
    {
        /// <summary>
        /// Get list of HeadMaster
        /// </summary>
        /// <returns></returns>
        IQueryable<HeadMaster> GetHeadMasters();

        /// <summary>
        /// Get list of HeadMaster by headMasterName
        /// </summary>
        /// <param name="headMasterName"></param>
        /// <returns></returns>
        IQueryable<HeadMaster> GetHeadMastersByName(string headMasterName);

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="HMId"></param>
        /// <returns></returns>
        HeadMaster GetHeadMasterById(Guid hmId);

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="HMId"></param>
        /// <returns></returns>
        HeadMaster GetHeadMasterById(DataRequest hmRequest);

        /// <summary>
        /// Get HeadMaster by HMId using async
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        Task<HeadMaster> GetHeadMasterByIdAsync(Guid hmId);

        /// <summary>
        /// Insert/Update HeadMaster entity
        /// </summary>
        /// <param name="headMaster"></param>
        /// <returns></returns>
        bool SaveOrUpdateHeadMasterDetails(HeadMaster headMaster);

        /// <summary>
        /// Delete a record by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        bool DeleteById(Guid hmId);

        /// <summary>
        /// Check duplicate HeadMaster by Name
        /// </summary>
        /// <param name="headMaster"></param>
        /// <param name="headMasterModel"></param>
        /// <returns></returns>
        List<string> CheckHeadMasterExistByName(HeadMaster headMaster, HeadMasterModel headMasterModel);

        /// <summary>
        /// Inactive HM Related Data When Resigned
        /// </summary>
        /// <param name="headMaster"></param>
        /// <param name="isActivate"></param>
        /// <returns></returns>
        bool InactiveHMRelatedDataWhenResigned(HeadMaster headMaster);

        /// <summary>
        /// List of HeadMaster with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<HeadMasterViewModel> GetHeadMastersByCriteria(SearchHeadMasterModel searchModel);
    }
}