using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Phase entity
    /// </summary>
    public interface IPhaseManager : IGenericManager<PhaseModel>
    {
        /// <summary>
        /// Get list of Phases
        /// </summary>
        /// <returns></returns>
        IQueryable<PhaseModel> GetPhases();

        /// <summary>
        /// Get list of Phases by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<PhaseModel> GetPhasesByName(string phaseName);

        /// <summary>
        /// Get Phase by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        PhaseModel GetPhaseById(Guid phaseId);

        /// <summary>
        /// Get Phase by PhaseId using async
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        Task<PhaseModel> GetPhaseByIdAsync(Guid phaseId);

        /// <summary>
        /// Insert/Update Phase entity
        /// </summary>
        /// <param name="phaseModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdatePhaseDetails(PhaseModel phaseModel);

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
