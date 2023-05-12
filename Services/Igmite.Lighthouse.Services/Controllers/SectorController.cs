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
    /// Expose all sector WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SectorController : BaseController
    {
        private readonly ISectorManager sectorManager;

        /// <summary>
        /// Initializes the Sector controller class.
        /// </summary>
        /// <param name="_sectorManager"></param>
        public SectorController(ISectorManager _sectorManager)
        {
            this.sectorManager = _sectorManager;
        }

        /// <summary>
        /// Get list of sector data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSectors")]
        public async Task<ListResponse<SectorModel>> GetSectors()
        {
            ListResponse<SectorModel> response = new ListResponse<SectorModel>();

            try
            {
                IQueryable<SectorModel> sectorModels = await Task.Run(() =>
                {
                    return this.sectorManager.GetSectors();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Sector with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorsByCriteria")]
        public async Task<ListResponse<SectorViewModel>> GetSectorsByCriteria([FromBody] SearchSectorModel searchModel)
        {
            ListResponse<SectorViewModel> response = new ListResponse<SectorViewModel>();

            try
            {
                var sectorModels = await Task.Run(() =>
                {
                    return this.sectorManager.GetSectorsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of sector data by name
        /// </summary>
        /// <param name="sectorName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSectorsByName")]
        public async Task<ListResponse<SectorModel>> GetSectorsByName([FromQuery] string sectorName)
        {
            ListResponse<SectorModel> response = new ListResponse<SectorModel>();

            try
            {
                var sectorModels = await Task.Run(() =>
                {
                    return this.sectorManager.GetSectorsByName(sectorName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get sector data by Id
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorById")]
        public async Task<SingularResponse<SectorModel>> GetSectorById([FromBody] DataRequest sectorRequest)
        {
            SingularResponse<SectorModel> response = new SingularResponse<SectorModel>();

            try
            {
                var sectorModel = await Task.Run(() =>
                {
                    return this.sectorManager.GetSectorById(Guid.Parse(sectorRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = sectorModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new sector
        /// </summary>
        /// <param name="sectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSector"), Route("CreateOrUpdateSectorDetails")]
        public async Task<SingularResponse<string>> CreateSector([FromBody] SectorRequest sectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //sectorRequest.RequestType = RequestType.New;
                    return this.sectorManager.SaveOrUpdateSectorDetails(sectorRequest);
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
        /// Update sector by Id
        /// </summary>
        /// <param name="sectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSector")]
        public async Task<SingularResponse<string>> UpdateSector([FromBody] SectorRequest sectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    sectorRequest.RequestType = RequestType.Edit;
                    return this.sectorManager.SaveOrUpdateSectorDetails(sectorRequest);
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
        /// Delete sector by Id
        /// </summary>
        /// <param name="sectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSectorById")]
        public async Task<SingularResponse<bool>> DeleteSectorById([FromBody] DeleteRequest<Guid> sectorRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.sectorManager.DeleteById(sectorRequest.DataId);
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