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
    /// Expose all vcSchoolSector WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VCSchoolSectorController : BaseController
    {
        private readonly IVCSchoolSectorManager vcSchoolSectorManager;

        /// <summary>
        /// Initializes the VCSchoolSector controller class.
        /// </summary>
        /// <param name="_vcSchoolSectorManager"></param>
        public VCSchoolSectorController(IVCSchoolSectorManager _vcSchoolSectorManager)
        {
            this.vcSchoolSectorManager = _vcSchoolSectorManager;
        }

        /// <summary>
        /// Get list of vcSchoolSector data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVCSchoolSectors")]
        public async Task<ListResponse<VCSchoolSectorModel>> GetVCSchoolSectors()
        {
            ListResponse<VCSchoolSectorModel> response = new ListResponse<VCSchoolSectorModel>();

            try
            {
                IQueryable<VCSchoolSectorModel> vcSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vcSchoolSectorManager.GetVCSchoolSectors();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VCSchoolSector with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolSectorsByCriteria")]
        public async Task<ListResponse<VCSchoolSectorViewModel>> GetVCSchoolSectorsByCriteria([FromBody] SearchVCSchoolSectorModel searchModel)
        {
            ListResponse<VCSchoolSectorViewModel> response = new ListResponse<VCSchoolSectorViewModel>();

            try
            {
                var vcSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vcSchoolSectorManager.GetVCSchoolSectorsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vcSchoolSector data by name
        /// </summary>
        /// <param name="vcSchoolSectorName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVCSchoolSectorsByName")]
        public async Task<ListResponse<VCSchoolSectorModel>> GetVCSchoolSectorsByName([FromQuery] string vcSchoolSectorName)
        {
            ListResponse<VCSchoolSectorModel> response = new ListResponse<VCSchoolSectorModel>();

            try
            {
                var vcSchoolSectorModels = await Task.Run(() =>
                {
                    return this.vcSchoolSectorManager.GetVCSchoolSectorsByName(vcSchoolSectorName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vcSchoolSector data by Id
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolSectorById")]
        public async Task<SingularResponse<VCSchoolSectorModel>> GetVCSchoolSectorById([FromBody] DataRequest vcSchoolSectorRequest)
        {
            SingularResponse<VCSchoolSectorModel> response = new SingularResponse<VCSchoolSectorModel>();

            try
            {
                var vcSchoolSectorModel = await Task.Run(() =>
                {
                    return this.vcSchoolSectorManager.GetVCSchoolSectorById(Guid.Parse(vcSchoolSectorRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vcSchoolSectorModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vcSchoolSector
        /// </summary>
        /// <param name="vcSchoolSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVCSchoolSector"), Route("CreateOrUpdateVCSchoolSectorDetails")]
        public async Task<SingularResponse<string>> CreateVCSchoolSector([FromBody] VCSchoolSectorRequest vcSchoolSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vcSchoolSectorRequest.RequestType = RequestType.New;
                    return this.vcSchoolSectorManager.SaveOrUpdateVCSchoolSectorDetails(vcSchoolSectorRequest);
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
        /// Update vcSchoolSector by Id
        /// </summary>
        /// <param name="vcSchoolSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVCSchoolSector")]
        public async Task<SingularResponse<string>> UpdateVCSchoolSector([FromBody] VCSchoolSectorRequest vcSchoolSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vcSchoolSectorRequest.RequestType = RequestType.Edit;
                    return this.vcSchoolSectorManager.SaveOrUpdateVCSchoolSectorDetails(vcSchoolSectorRequest);
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
        /// Delete vcSchoolSector by Id
        /// </summary>
        /// <param name="vcSchoolSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVCSchoolSectorById")]
        public async Task<SingularResponse<bool>> DeleteVCSchoolSectorById([FromBody] DeleteRequest<Guid> vcSchoolSectorRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vcSchoolSectorManager.DeleteById(vcSchoolSectorRequest.DataId);
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