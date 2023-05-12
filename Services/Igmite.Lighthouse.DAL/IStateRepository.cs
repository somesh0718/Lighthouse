using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the State entity
    /// </summary>
    public interface IStateRepository : IGenericRepository<State>
    {
        /// <summary>
        /// Get list of State
        /// </summary>
        /// <returns></returns>
        IQueryable<State> GetStates();

        /// <summary>
        /// Get list of State by stateName
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        IQueryable<State> GetStatesByName(string stateName);

        /// <summary>
        /// Get State by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        State GetStateById(string stateCode);

        /// <summary>
        /// Get State by StateCode using async
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        Task<State> GetStateByIdAsync(string stateCode);

        /// <summary>
        /// Insert/Update State entity
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool SaveOrUpdateStateDetails(State state);

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
