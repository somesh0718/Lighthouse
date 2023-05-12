using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTStudentPlacementDetail entity
    /// </summary>
    public interface IVTStudentPlacementDetailRepository : IGenericRepository<VTStudentPlacementDetail>
    {
        /// <summary>
        /// Get list of VTStudentPlacementDetail
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentPlacementDetail> GetVTStudentPlacementDetails();

        /// <summary>
        /// Get list of VTStudentPlacementDetail by vtStudentPlacementDetailName
        /// </summary>
        /// <param name="vtStudentPlacementDetailName"></param>
        /// <returns></returns>
        IQueryable<VTStudentPlacementDetail> GetVTStudentPlacementDetailsByName(string vtStudentPlacementDetailName);

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        VTStudentPlacementDetail GetVTStudentPlacementDetailById(Guid vtStudentPlacementDetailId);

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId using async
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        Task<VTStudentPlacementDetail> GetVTStudentPlacementDetailByIdAsync(Guid vtStudentPlacementDetailId);

        /// <summary>
        /// Insert/Update VTStudentPlacementDetail entity
        /// </summary>
        /// <param name="vtStudentPlacementDetail"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTStudentPlacementDetailDetails(VTStudentPlacementDetail vtStudentPlacementDetail);

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
