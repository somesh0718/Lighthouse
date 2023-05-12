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
    /// Expose all phase WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class PhaseController : BaseController
    {
        private readonly IPhaseManager phaseManager;

        /// <summary>
        /// Initializes the Phase controller class.
        /// </summary>
        /// <param name="_phaseManager"></param>
        public PhaseController(IPhaseManager _phaseManager)
        {
            this.phaseManager = _phaseManager;
        }

        /// <summary>
        /// Get list of phase data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetPhases")]
        public async Task<ListResponse<PhaseModel>> GetPhases()
        {
            ListResponse<PhaseModel> response = new ListResponse<PhaseModel>();

            try
            {
                IQueryable<PhaseModel> phaseModels = await Task.Run(() =>
                {
                    return this.phaseManager.GetPhases();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = phaseModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Phase with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetPhasesByCriteria")]
        public async Task<ListResponse<PhaseViewModel>> GetPhasesByCriteria([FromBody] SearchPhaseModel searchModel)
        {
            ListResponse<PhaseViewModel> response = new ListResponse<PhaseViewModel>();

            try
            {
                var phaseModels = await Task.Run(() =>
                {
                    return this.phaseManager.GetPhasesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = phaseModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of phase data by name
        /// </summary>
        /// <param name="phaseName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetPhasesByName")]
        public async Task<ListResponse<PhaseModel>> GetPhasesByName([FromQuery] string phaseName)
        {
            ListResponse<PhaseModel> response = new ListResponse<PhaseModel>();

            try
            {
                var phaseModels = await Task.Run(() =>
                {
                    return this.phaseManager.GetPhasesByName(phaseName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = phaseModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get phase data by Id
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetPhaseById")]
        public async Task<SingularResponse<PhaseModel>> GetPhaseById([FromBody] DataRequest phaseRequest)
        {
            SingularResponse<PhaseModel> response = new SingularResponse<PhaseModel>();

            try
            {
                var phaseModel = await Task.Run(() =>
                {
                    return this.phaseManager.GetPhaseById(Guid.Parse(phaseRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = phaseModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new phase
        /// </summary>
        /// <param name="phaseRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreatePhase"), Route("CreateOrUpdatePhaseDetails")]
        public async Task<SingularResponse<string>> CreatePhase([FromBody] PhaseRequest phaseRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //phaseRequest.RequestType = RequestType.New;
                    return this.phaseManager.SaveOrUpdatePhaseDetails(phaseRequest);
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
        /// Update phase by Id
        /// </summary>
        /// <param name="phaseRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdatePhase")]
        public async Task<SingularResponse<string>> UpdatePhase([FromBody] PhaseRequest phaseRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    phaseRequest.RequestType = RequestType.Edit;
                    return this.phaseManager.SaveOrUpdatePhaseDetails(phaseRequest);
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
        /// Delete phase by Id
        /// </summary>
        /// <param name="phaseRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeletePhaseById")]
        public async Task<SingularResponse<bool>> DeletePhaseById([FromBody] DeleteRequest<Guid> phaseRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.phaseManager.DeleteById(phaseRequest.DataId);
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