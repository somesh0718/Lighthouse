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
    /// Expose all vtSchoolSector WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTSchoolSectorController : BaseController
    {
        private readonly IVTSchoolSectorManager vtSchoolSectorManager;

        /// <summary>
        /// Initializes the VTSchoolSector controller class.
        /// </summary>
        /// <param name="_vtSchoolSectorManager"></param>
        public VTSchoolSectorController(IVTSchoolSectorManager _vtSchoolSectorManager)
        {
            this.vtSchoolSectorManager = _vtSchoolSectorManager;
        }

        /// <summary>
        /// Get list of vtSchoolSector data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTSchoolSectors")]
        public async Task<ListResponse<VTSchoolSectorModel>> GetVTSchoolSectors()
        {
            ListResponse<VTSchoolSectorModel> response = new ListResponse<VTSchoolSectorModel>();

            try
            {
                IQueryable<VTSchoolSectorModel> vtSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vtSchoolSectorManager.GetVTSchoolSectors();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtSchoolSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTSchoolSector with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTSchoolSectorsByCriteria")]
        public async Task<ListResponse<VTSchoolSectorViewModel>> GetVTSchoolSectorsByCriteria([FromBody] SearchVTSchoolSectorModel searchModel)
        {
            ListResponse<VTSchoolSectorViewModel> response = new ListResponse<VTSchoolSectorViewModel>();

            try
            {
                var vtSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vtSchoolSectorManager.GetVTSchoolSectorsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtSchoolSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtSchoolSector data by name
        /// </summary>
        /// <param name="vtSchoolSectorName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTSchoolSectorsByName")]
        public async Task<ListResponse<VTSchoolSectorModel>> GetVTSchoolSectorsByName([FromQuery] string vtSchoolSectorName)
        {
            ListResponse<VTSchoolSectorModel> response = new ListResponse<VTSchoolSectorModel>();

            try
            {
                var vtSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vtSchoolSectorManager.GetVTSchoolSectorsByName(vtSchoolSectorName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtSchoolSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpGet, Route("GetVTSchoolSectorBySchoolIdANDSectorId")]
        public async Task<SingularResponse<VTSchoolSectorModel>> GetVTSchoolSectorBySchoolIdANDSectorId([FromQuery] string SchoolId,string SectorId)
        {
            SingularResponse<VTSchoolSectorModel> response = new SingularResponse<VTSchoolSectorModel>();

            try
            {
                var vtSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vtSchoolSectorManager.GetVTSchoolSectorBySchoolIdANDSectorId(Guid.Parse(SchoolId),Guid.Parse(SectorId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtSchoolSectorModels;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtSchoolSector data by Id
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTSchoolSectorById")]
        public async Task<SingularResponse<VTSchoolSectorModel>> GetVTSchoolSectorById([FromBody] DataRequest vtSchoolSectorRequest)
        {
            SingularResponse<VTSchoolSectorModel> response = new SingularResponse<VTSchoolSectorModel>();

            try
            {
                var vtSchoolSectorModel = await Task.Run(() =>
                {
                    return this.vtSchoolSectorManager.GetVTSchoolSectorById(Guid.Parse(vtSchoolSectorRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtSchoolSectorModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtSchoolSector
        /// </summary>
        /// <param name="vtSchoolSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTSchoolSector"), Route("CreateOrUpdateVTSchoolSectorDetails")]
        public async Task<SingularResponse<string>> CreateVTSchoolSector([FromBody] VTSchoolSectorRequest vtSchoolSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtSchoolSectorRequest.RequestType = RequestType.New;
                    return this.vtSchoolSectorManager.SaveOrUpdateVTSchoolSectorDetails(vtSchoolSectorRequest);
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
        /// Update vtSchoolSector by Id
        /// </summary>
        /// <param name="vtSchoolSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTSchoolSector")]
        public async Task<SingularResponse<string>> UpdateVTSchoolSector([FromBody] VTSchoolSectorRequest vtSchoolSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtSchoolSectorRequest.RequestType = RequestType.Edit;
                    return this.vtSchoolSectorManager.SaveOrUpdateVTSchoolSectorDetails(vtSchoolSectorRequest);
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
        /// Delete vtSchoolSector by Id
        /// </summary>
        /// <param name="vtSchoolSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTSchoolSectorById")]
        public async Task<SingularResponse<bool>> DeleteVTSchoolSectorById([FromBody] DeleteRequest<Guid> vtSchoolSectorRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtSchoolSectorManager.DeleteById(vtSchoolSectorRequest.DataId);
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