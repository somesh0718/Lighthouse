using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTClass entity
    /// </summary>
    public interface IVTClassManager : IGenericManager<VTClassModel>
    {
        /// <summary>
        /// Get list of VTClasses
        /// </summary>
        /// <returns></returns>
        IQueryable<VTClassModel> GetVTClasses();

        /// <summary>
        /// Get list of VTClasses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTClassModel> GetVTClassesByName(string vtClassName);

        /// <summary>
        /// Get VTClass by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        VTClassModel GetVTClassById(Guid vtClassId);

        /// <summary>
        /// Get VTClass by VTClassId using async
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        Task<VTClassModel> GetVTClassByIdAsync(Guid vtClassId);

        /// <summary>
        /// Insert/Update VTClass entity
        /// </summary>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTClassDetails(VTClassModel vtClassModel);

        /// <summary>
        /// Delete a record by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtClassId);

        /// <summary>
        /// List of VTClass with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTClassViewModel> GetVTClassesByCriteria(SearchVTClassModel searchModel);
    }
}