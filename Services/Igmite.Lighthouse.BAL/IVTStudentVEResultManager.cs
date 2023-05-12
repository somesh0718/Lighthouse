using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTStudentVEResult entity
    /// </summary>
    public interface IVTStudentVEResultManager : IGenericManager<VTStudentVEResultModel>
    {
        /// <summary>
        /// Get list of VTStudentVEResults
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentVEResultModel> GetVTStudentVEResults();

        /// <summary>
        /// Get list of VTStudentVEResults by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTStudentVEResultModel> GetVTStudentVEResultsByName(string vtStudentVEResultName);

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        VTStudentVEResultModel GetVTStudentVEResultById(Guid vtStudentVEResultId);

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId using async
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        Task<VTStudentVEResultModel> GetVTStudentVEResultByIdAsync(Guid vtStudentVEResultId);

        /// <summary>
        /// Insert/Update VTStudentVEResult entity
        /// </summary>
        /// <param name="vtStudentVEResultModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTStudentVEResultDetails(VTStudentVEResultModel vtStudentVEResultModel);

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
