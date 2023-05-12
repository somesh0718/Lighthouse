using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Division entity
    /// </summary>
    public interface IDivisionRepository : IGenericRepository<Division>
    {
        /// <summary>
        /// Get list of Division
        /// </summary>
        /// <returns></returns>
        IQueryable<Division> GetDivisions();

        /// <summary>
        /// Get list of Division by divisionName
        /// </summary>
        /// <param name="divisionName"></param>
        /// <returns></returns>
        IQueryable<Division> GetDivisionsByName(string divisionName);

        /// <summary>
        /// Get Division by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        Division GetDivisionById(Guid divisionId);

        /// <summary>
        /// Get Division by DivisionId using async
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        Task<Division> GetDivisionByIdAsync(Guid divisionId);

        /// <summary>
        /// Insert/Update Division entity
        /// </summary>
        /// <param name="division"></param>
        /// <returns></returns>
        bool SaveOrUpdateDivisionDetails(Division division);

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
