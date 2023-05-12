using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTStudentVEResult entity
    /// </summary>
    public interface IVTStudentVEResultRepository : IGenericRepository<VTStudentVEResult>
    {
        /// <summary>
        /// Get list of VTStudentVEResult
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentVEResult> GetVTStudentVEResults();

        /// <summary>
        /// Get list of VTStudentVEResult by vtStudentVEResultName
        /// </summary>
        /// <param name="vtStudentVEResultName"></param>
        /// <returns></returns>
        IQueryable<VTStudentVEResult> GetVTStudentVEResultsByName(string vtStudentVEResultName);

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        VTStudentVEResult GetVTStudentVEResultById(Guid vtStudentVEResultId);

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId using async
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        Task<VTStudentVEResult> GetVTStudentVEResultByIdAsync(Guid vtStudentVEResultId);

        /// <summary>
        /// Insert/Update VTStudentVEResult entity
        /// </summary>
        /// <param name="vtStudentVEResult"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTStudentVEResultDetails(VTStudentVEResult vtStudentVEResult);

        /// <summary>
        /// Delete a record by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtStudentVEResultId);

        /// <summary>
        /// Check duplicate VTStudentVEResult by Name
        /// </summary>
        /// <param name="vtStudentVEResultModel"></param>
        /// <returns></returns>
        bool CheckVTStudentVEResultExistByName(VTStudentVEResultModel vtStudentVEResultModel);

        /// <summary>
        /// List of VTStudentVEResult with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTStudentVEResultViewModel> GetVTStudentVEResultsByCriteria(SearchVTStudentVEResultModel searchModel);
    }
}
