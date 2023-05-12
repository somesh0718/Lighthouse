using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the State entity
    /// </summary>
    public class StateManager : GenericManager<StateModel>, IStateManager
    {
        private readonly IStateRepository stateRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the state manager.
        /// </summary>
        /// <param name="stateRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public StateManager(IStateRepository _stateRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.stateRepository = _stateRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of States
        /// </summary>
        /// <returns></returns>
        public IQueryable<StateModel> GetStates()
        {
            var states = this.stateRepository.GetStates();

            IList<StateModel> stateModels = new List<StateModel>();
            states.ForEach((user) => stateModels.Add(user.ToModel()));

            return stateModels.AsQueryable();
        }

        /// <summary>
        /// Get list of States by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StateModel> GetStatesByName(string stateName)
        {
            var states = this.stateRepository.GetStatesByName(stateName);

            IList<StateModel> stateModels = new List<StateModel>();
            states.ForEach((user) => stateModels.Add(user.ToModel()));

            return stateModels.AsQueryable();
        }

        /// <summary>
        /// Get State by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public StateModel GetStateById(string stateCode)
        {
            State state = this.stateRepository.GetStateById(stateCode);

            return (state != null) ? state.ToModel() : null;
        }

        /// <summary>
        /// Get State by StateCode using async
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public async Task<StateModel> GetStateByIdAsync(string stateCode)
        {
            var state = await this.stateRepository.GetStateByIdAsync(stateCode);

            return (state != null) ? state.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update State entity
        /// </summary>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateStateDetails(StateModel stateModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            State state = null;

            //Validate model data
            stateModel = stateModel.GetModelValidationErrors<StateModel>();

            if (stateModel.ErrorMessages.Count > 0)
            {
                response.Errors = stateModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (stateModel.RequestType == RequestType.Edit)
            {
                state = this.stateRepository.GetStateById(stateModel.StateCode);

                //state.Districts.ForEach((oldDistrictItem) =>
                //{
                //    var valDistrictItem = stateModel.DistrictModels.FirstOrDefault(a => a.DistrictCode == oldDistrictItem.DistrictCode);
                //    if (valDistrictItem == null)
                //    {
                //        state.Deleted//s.Add(oldDistrictCodeItem.District);
                //    }
                //});
            }
            else
            {
                state = new State();
            }

            if (stateModel.ErrorMessages.Count == 0 && (stateModel.StateName.StringVal().ToLower() != state.StateName.StringVal().ToLower()))
            {
                bool isStateExists = this.stateRepository.CheckStateExistByName(stateModel);

                if (isStateExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                state.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                state = stateModel.FromModel(state);

                //Save Or Update state details
                bool isSaved = this.stateRepository.SaveOrUpdateStateDetails(state);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public bool DeleteById(string stateCode)
        {
            return this.stateRepository.DeleteById(stateCode);
        }

        /// <summary>
        /// Check duplicate State by Name
        /// </summary>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        public bool CheckStateExistByName(StateModel stateModel)
        {
            return this.stateRepository.CheckStateExistByName(stateModel);
        }

        /// <summary>}
        /// List of State with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<StateViewModel> GetStatesByCriteria(SearchStateModel searchModel)
        {
            return this.stateRepository.GetStatesByCriteria(searchModel);
        }
    }
}