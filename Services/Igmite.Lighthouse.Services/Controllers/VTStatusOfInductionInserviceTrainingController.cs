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
    /// Expose all vtStatusOfInductionInserviceTraining WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTStatusOfInductionInserviceTrainingController : BaseController
    {
        private readonly IVTStatusOfInductionInserviceTrainingManager vtStatusOfInductionInserviceTrainingManager;

        /// <summary>
        /// Initializes the VTStatusOfInductionInserviceTraining controller class.
        /// </summary>
        /// <param name="_vtStatusOfInductionInserviceTrainingManager"></param>
        public VTStatusOfInductionInserviceTrainingController(IVTStatusOfInductionInserviceTrainingManager _vtStatusOfInductionInserviceTrainingManager)
        {
            this.vtStatusOfInductionInserviceTrainingManager = _vtStatusOfInductionInserviceTrainingManager;
        }

        /// <summary>
        /// Get list of vtStatusOfInductionInserviceTraining data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTStatusOfInductionInserviceTrainings")]
        public async Task<ListResponse<VTStatusOfInductionInserviceTrainingModel>> GetVTStatusOfInductionInserviceTrainings()
        {
            ListResponse<VTStatusOfInductionInserviceTrainingModel> response = new ListResponse<VTStatusOfInductionInserviceTrainingModel>();

            try
            {
                IQueryable<VTStatusOfInductionInserviceTrainingModel> vtStatusOfInductionInserviceTrainingModels = await Task.Run(() =>
                {
                    return this.vtStatusOfInductionInserviceTrainingManager.GetVTStatusOfInductionInserviceTrainings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStatusOfInductionInserviceTrainingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTStatusOfInductionInserviceTraining with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStatusOfInductionInserviceTrainingsByCriteria")]
        public async Task<ListResponse<VTStatusOfInductionInserviceTrainingViewModel>> GetVTStatusOfInductionInserviceTrainingsByCriteria([FromBody] SearchVTStatusOfInductionInserviceTrainingModel searchModel)
        {
            ListResponse<VTStatusOfInductionInserviceTrainingViewModel> response = new ListResponse<VTStatusOfInductionInserviceTrainingViewModel>();

            try
            {
                var vtStatusOfInductionInserviceTrainingModels = await Task.Run(() =>
                {
                    return this.vtStatusOfInductionInserviceTrainingManager.GetVTStatusOfInductionInserviceTrainingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStatusOfInductionInserviceTrainingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtStatusOfInductionInserviceTraining data by name
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTStatusOfInductionInserviceTrainingsByName")]
        public async Task<ListResponse<VTStatusOfInductionInserviceTrainingModel>> GetVTStatusOfInductionInserviceTrainingsByName([FromQuery] string vtStatusOfInductionInserviceTrainingName)
        {
            ListResponse<VTStatusOfInductionInserviceTrainingModel> response = new ListResponse<VTStatusOfInductionInserviceTrainingModel>();

            try
            {
                var vtStatusOfInductionInserviceTrainingModels = await Task.Run(() =>
                {
                    return this.vtStatusOfInductionInserviceTrainingManager.GetVTStatusOfInductionInserviceTrainingsByName(vtStatusOfInductionInserviceTrainingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStatusOfInductionInserviceTrainingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtStatusOfInductionInserviceTraining data by Id
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStatusOfInductionInserviceTrainingById")]
        public async Task<SingularResponse<VTStatusOfInductionInserviceTrainingModel>> GetVTStatusOfInductionInserviceTrainingById([FromBody] DataRequest vtStatusOfInductionInserviceTrainingRequest)
        {
            SingularResponse<VTStatusOfInductionInserviceTrainingModel> response = new SingularResponse<VTStatusOfInductionInserviceTrainingModel>();

            try
            {
                var vtStatusOfInductionInserviceTrainingModel = await Task.Run(() =>
                {
                    return this.vtStatusOfInductionInserviceTrainingManager.GetVTStatusOfInductionInserviceTrainingById(Guid.Parse(vtStatusOfInductionInserviceTrainingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtStatusOfInductionInserviceTrainingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtStatusOfInductionInserviceTraining
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTStatusOfInductionInserviceTraining"), Route("CreateOrUpdateVTStatusOfInductionInserviceTrainingDetails")]
        public async Task<SingularResponse<string>> CreateVTStatusOfInductionInserviceTraining([FromBody] VTStatusOfInductionInserviceTrainingRequest vtStatusOfInductionInserviceTrainingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtStatusOfInductionInserviceTrainingRequest.RequestType = RequestType.New;
                    return this.vtStatusOfInductionInserviceTrainingManager.SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(vtStatusOfInductionInserviceTrainingRequest);
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
        /// Update vtStatusOfInductionInserviceTraining by Id
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTStatusOfInductionInserviceTraining")]
        public async Task<SingularResponse<string>> UpdateVTStatusOfInductionInserviceTraining([FromBody] VTStatusOfInductionInserviceTrainingRequest vtStatusOfInductionInserviceTrainingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtStatusOfInductionInserviceTrainingRequest.RequestType = RequestType.Edit;
                    return this.vtStatusOfInductionInserviceTrainingManager.SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(vtStatusOfInductionInserviceTrainingRequest);
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
        /// Delete vtStatusOfInductionInserviceTraining by Id
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTStatusOfInductionInserviceTrainingById")]
        public async Task<SingularResponse<bool>> DeleteVTStatusOfInductionInserviceTrainingById([FromBody] DeleteRequest<Guid> vtStatusOfInductionInserviceTrainingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtStatusOfInductionInserviceTrainingManager.DeleteById(vtStatusOfInductionInserviceTrainingRequest.DataId);
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