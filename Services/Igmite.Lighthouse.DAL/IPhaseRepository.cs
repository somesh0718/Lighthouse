using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Phase entity
    /// </summary>
    public interface IPhaseRepository : IGenericRepository<Phase>
    {
        /// <summary>
        /// Get list of Phase
        /// </summary>
        /// <returns></returns>
        IQueryable<Phase> GetPhases();

        /// <summary>
        /// Get list of Phase by phaseName
        /// </summary>
        /// <param name="phaseName"></param>
        /// <returns></returns>
        IQueryable<Phase> GetPhasesByName(string phaseName);

        /// <summary>
        /// Get Phase by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        Phase GetPhaseById(Guid phaseId);

        /// <summary>
        /// Get Phase by PhaseId using async
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        Task<Phase> GetPhaseByIdAsync(Guid phaseId);

        /// <summary>
        /// Insert/Update Phase entity
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        bool SaveOrUpdatePhaseDetails(Phase phase);

        /// <summary>
        /// Delete a record by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        bool DeleteById(Guid phaseId);

        /// <summary>
        /// Check duplicate Phase by Name
        /// </summary>
        /// <param name="phaseModel"></param>
        /// <returns></returns>
        bool CheckPhaseExistByName(PhaseModel phaseModel);

        /// <summary>
        /// List of Phase with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<PhaseViewModel> GetPhasesByCriteria(SearchPhaseModel searchModel);
    }
}
