using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTStudentPlacementDetail entity
    /// </summary>
    public interface IVTStudentPlacementDetailManager : IGenericManager<VTStudentPlacementDetailModel>
    {
        /// <summary>
        /// Get list of VTStudentPlacementDetails
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentPlacementDetailModel> GetVTStudentPlacementDetails();

        /// <summary>
        /// Get list of VTStudentPlacementDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTStudentPlacementDetailModel> GetVTStudentPlacementDetailsByName(string vtStudentPlacementDetailName);

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        VTStudentPlacementDetailModel GetVTStudentPlacementDetailById(Guid vtStudentPlacementDetailId);

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId using async
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        Task<VTStudentPlacementDetailModel> GetVTStudentPlacementDetailByIdAsync(Guid vtStudentPlacementDetailId);

        /// <summary>
        /// Insert/Update VTStudentPlacementDetail entity
        /// </summary>
        /// <param name="vtStudentPlacementDetailModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTStudentPlacementDetailDetails(VTStudentPlacementDetailModel vtStudentPlacementDetailModel);

        /// <summary>
        /// Delete a record by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtStudentPlacementDetailId);

        /// <summary>
        /// Check duplicate VTStudentPlacementDetail by Name
        /// </summary>
        /// <param name="vtStudentPlacementDetailModel"></param>
        /// <returns></returns>
        bool CheckVTStudentPlacementDetailExistByName(VTStudentPlacementDetailModel vtStudentPlacementDetailModel);

        /// <summary>
        /// List of VTStudentPlacementDetail with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTStudentPlacementDetailViewModel> GetVTStudentPlacementDetailsByCriteria(SearchVTStudentPlacementDetailModel searchModel);
    }
}
