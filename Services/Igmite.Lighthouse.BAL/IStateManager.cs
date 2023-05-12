using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the State entity
    /// </summary>
    public interface IStateManager : IGenericManager<StateModel>
    {
        /// <summary>
        /// Get list of States
        /// </summary>
        /// <returns></returns>
        IQueryable<StateModel> GetStates();

        /// <summary>
        /// Get list of States by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<StateModel> GetStatesByName(string stateName);

        /// <summary>
        /// Get State by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        StateModel GetStateById(string stateCode);

        /// <summary>
        /// Get State by StateCode using async
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        Task<StateModel> GetStateByIdAsync(string stateCode);

        /// <summary>
        /// Insert/Update State entity
        /// </summary>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateStateDetails(StateModel stateModel);

        /// <summary>
        /// Delete a record by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        bool DeleteById(string stateCode);

        /// <summary>
        /// Check duplicate State by Name
        /// </summary>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        bool CheckStateExistByName(StateModel stateModel);

        /// <summary>
        /// List of State with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<StateViewModel> GetStatesByCriteria(SearchStateModel searchModel);
    }
}
