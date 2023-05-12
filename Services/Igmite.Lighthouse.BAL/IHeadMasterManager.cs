using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the HeadMaster entity
    /// </summary>
    public interface IHeadMasterManager : IGenericManager<HeadMasterModel>
    {
        /// <summary>
        /// Get list of HeadMasters
        /// </summary>
        /// <returns></returns>
        IQueryable<HeadMasterModel> GetHeadMasters();

        /// <summary>
        /// Get list of HeadMasters by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<HeadMasterModel> GetHeadMastersByName(string headMasterName);

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="HMId"></param>
        /// <returns></returns>
        HeadMasterModel GetHeadMasterById(Guid hmId);

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="HMId"></param>
        /// <returns></returns>
        HeadMasterModel GetHeadMasterById(DataRequest hmRequest);

        /// <summary>
        /// Get HeadMaster by HMId using async
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        Task<HeadMasterModel> GetHeadMasterByIdAsync(Guid hmId);

        /// <summary>
        /// Insert/Update HeadMaster entity
        /// </summary>
        /// <param name="headMasterModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateHeadMasterDetails(HeadMasterModel headMasterModel);

        /// <summary>
        /// Delete a record by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        bool DeleteById(Guid hmId);

        /// <summary>
        /// List of HeadMaster with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<HeadMasterViewModel> GetHeadMastersByCriteria(SearchHeadMasterModel searchModel);
    }
}