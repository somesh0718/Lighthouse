using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Division entity
    /// </summary>
    public interface IDivisionManager : IGenericManager<DivisionModel>
    {
        /// <summary>
        /// Get list of Divisions
        /// </summary>
        /// <returns></returns>
        IQueryable<DivisionModel> GetDivisions();

        /// <summary>
        /// Get list of Divisions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<DivisionModel> GetDivisionsByName(string divisionName);

        /// <summary>
        /// Get Division by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        DivisionModel GetDivisionById(Guid divisionId);

        /// <summary>
        /// Get Division by DivisionId using async
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        Task<DivisionModel> GetDivisionByIdAsync(Guid divisionId);

        /// <summary>
        /// Insert/Update Division entity
        /// </summary>
        /// <param name="divisionModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateDivisionDetails(DivisionModel divisionModel);

        /// <summary>
        /// Delete a record by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        bool DeleteById(Guid divisionId);

        /// <summary>
        /// Check duplicate Division by Name
        /// </summary>
        /// <param name="divisionModel"></param>
        /// <returns></returns>
        bool CheckDivisionExistByName(DivisionModel divisionModel);

        /// <summary>
        /// List of Division with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<DivisionViewModel> GetDivisionsByCriteria(SearchDivisionModel searchModel);
    }
}
