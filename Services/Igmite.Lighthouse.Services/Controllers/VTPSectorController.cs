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
    /// Expose all vtpSector WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTPSectorController : BaseController
    {
        private readonly IVTPSectorManager vtpSectorManager;

        /// <summary>
        /// Initializes the VTPSector controller class.
        /// </summary>
        /// <param name="_vtpSectorManager"></param>
        public VTPSectorController(IVTPSectorManager _vtpSectorManager)
        {
            this.vtpSectorManager = _vtpSectorManager;
        }

        /// <summary>
        /// Get list of vtpSector data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTPSectors")]
        public async Task<ListResponse<VTPSectorModel>> GetVTPSectors()
        {
            ListResponse<VTPSectorModel> response = new ListResponse<VTPSectorModel>();

            try
            {
                IQueryable<VTPSectorModel> vtpSectorModels = await Task.Run(() =>
                {
                    return this.vtpSectorManager.GetVTPSectors();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTPSector with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPSectorsByCriteria")]
        public async Task<ListResponse<VTPSectorViewModel>> GetVTPSectorsByCriteria([FromBody] SearchVTPSectorModel searchModel)
        {
            ListResponse<VTPSectorViewModel> response = new ListResponse<VTPSectorViewModel>();

            try
            {
                var vtpSectorModels = await Task.Run(() =>
                {
                    return this.vtpSectorManager.GetVTPSectorsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtpSector data by name
        /// </summary>
        /// <param name="vtpSectorName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTPSectorsByName")]
        public async Task<ListResponse<VTPSectorModel>> GetVTPSectorsByName([FromQuery] string vtpSectorName)
        {
            ListResponse<VTPSectorModel> response = new ListResponse<VTPSectorModel>();

            try
            {
                var vtpSectorModels = await Task.Run(() =>
                {
                    return this.vtpSectorManager.GetVTPSectorsByName(vtpSectorName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtpSector data by Id
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPSectorById")]
        public async Task<SingularResponse<VTPSectorModel>> GetVTPSectorById([FromBody] DataRequest vtpSectorRequest)
        {
            SingularResponse<VTPSectorModel> response = new SingularResponse<VTPSectorModel>();

            try
            {
                var vtpSectorModel = await Task.Run(() =>
                {
                    return this.vtpSectorManager.GetVTPSectorById(Guid.Parse(vtpSectorRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtpSectorModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtpSector
        /// </summary>
        /// <param name="vtpSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTPSector"), Route("CreateOrUpdateVTPSectorDetails")]
        public async Task<SingularResponse<string>> CreateVTPSector([FromBody] VTPSectorRequest vtpSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtpSectorRequest.RequestType = RequestType.New;
                    return this.vtpSectorManager.SaveOrUpdateVTPSectorDetails(vtpSectorRequest);
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
        /// Update vtpSector by Id
        /// </summary>
        /// <param name="vtpSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTPSector")]
        public async Task<SingularResponse<string>> UpdateVTPSector([FromBody] VTPSectorRequest vtpSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtpSectorRequest.RequestType = RequestType.Edit;
                    return this.vtpSectorManager.SaveOrUpdateVTPSectorDetails(vtpSectorRequest);
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
        /// Delete vtpSector by Id
        /// </summary>
        /// <param name="vtpSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTPSectorById")]
        public async Task<SingularResponse<bool>> DeleteVTPSectorById([FromBody] DeleteRequest<Guid> vtpSectorRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtpSectorManager.DeleteById(vtpSectorRequest.DataId);
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