using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all state WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class StateController : BaseController
    {
        private readonly IStateManager stateManager;

        /// <summary>
        /// Initializes the State controller class.
        /// </summary>
        /// <param name="_stateManager"></param>
        public StateController(IStateManager _stateManager)
        {
            this.stateManager = _stateManager;
        }

        /// <summary>
        /// Get list of state data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetStates")]
        public async Task<ListResponse<StateModel>> GetStates()
        {
            ListResponse<StateModel> response = new ListResponse<StateModel>();

            try
            {
                IQueryable<StateModel> stateModels = await Task.Run(() =>
                {
                    return this.stateManager.GetStates();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = stateModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of State with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStatesByCriteria")]
        public async Task<ListResponse<StateViewModel>> GetStatesByCriteria([FromBody] SearchStateModel searchModel)
        {
            ListResponse<StateViewModel> response = new ListResponse<StateViewModel>();

            try
            {
                var stateModels = await Task.Run(() =>
                {
                    return this.stateManager.GetStatesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = stateModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of state data by name
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStatesByName")]
        public async Task<ListResponse<StateModel>> GetStatesByName([FromQuery] string stateName)
        {
            ListResponse<StateModel> response = new ListResponse<StateModel>();

            try
            {
                var stateModels = await Task.Run(() =>
                {
                    return this.stateManager.GetStatesByName(stateName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = stateModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get state data by Id
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStateById")]
        public async Task<SingularResponse<StateModel>> GetStateById([FromBody] DataRequest stateRequest)
        {
            SingularResponse<StateModel> response = new SingularResponse<StateModel>();

            try
            {
                var stateModel = await Task.Run(() =>
                {
                    return this.stateManager.GetStateById(stateRequest.DataId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = stateModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new state
        /// </summary>
        /// <param name="stateRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateState"), Route("CreateOrUpdateStateDetails")]
        public async Task<SingularResponse<string>> CreateState([FromBody] StateRequest stateRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //stateRequest.RequestType = RequestType.New;
                    return this.stateManager.SaveOrUpdateStateDetails(stateRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update state by Id
        /// </summary>
        /// <param name="stateRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateState")]
        public async Task<SingularResponse<string>> UpdateState([FromBody] StateRequest stateRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    stateRequest.RequestType = RequestType.Edit;
                    return this.stateManager.SaveOrUpdateStateDetails(stateRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete state by Id
        /// </summary>
        /// <param name="stateRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteStateById")]
        public async Task<SingularResponse<bool>> DeleteStateById([FromBody] DeleteRequest<string> stateRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.stateManager.DeleteById(stateRequest.DataId);
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}