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
    /// Expose all vcSchoolVisit WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VCSchoolVisitController : BaseController
    {
        private readonly IVCSchoolVisitManager vcSchoolVisitManager;

        /// <summary>
        /// Initializes the VCSchoolVisit controller class.
        /// </summary>
        /// <param name="_vcSchoolVisitManager"></param>
        public VCSchoolVisitController(IVCSchoolVisitManager _vcSchoolVisitManager)
        {
            this.vcSchoolVisitManager = _vcSchoolVisitManager;
        }

        /// <summary>
        /// Get list of vcSchoolVisit data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVCSchoolVisits")]
        public async Task<ListResponse<VCSchoolVisitModel>> GetVCSchoolVisits()
        {
            ListResponse<VCSchoolVisitModel> response = new ListResponse<VCSchoolVisitModel>();

            try
            {
                IQueryable<VCSchoolVisitModel> vcSchoolVisitModels = await Task.Run(() =>
                {
                    return this.vcSchoolVisitManager.GetVCSchoolVisits();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolVisitModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VCSchoolVisit with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolVisitsByCriteria")]
        public async Task<ListResponse<VCSchoolVisitViewModel>> GetVCSchoolVisitsByCriteria([FromBody] SearchVCSchoolVisitModel searchModel)
        {
            ListResponse<VCSchoolVisitViewModel> response = new ListResponse<VCSchoolVisitViewModel>();

            try
            {
                var vcSchoolVisitModels = await Task.Run(() =>
                {
                    return this.vcSchoolVisitManager.GetVCSchoolVisitsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolVisitModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vcSchoolVisit data by name
        /// </summary>
        /// <param name="vcSchoolVisitName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVCSchoolVisitsByName")]
        public async Task<ListResponse<VCSchoolVisitModel>> GetVCSchoolVisitsByName([FromQuery] string vcSchoolVisitName)
        {
            ListResponse<VCSchoolVisitModel> response = new ListResponse<VCSchoolVisitModel>();

            try
            {
                var vcSchoolVisitModels = await Task.Run(() =>
                {
                    return this.vcSchoolVisitManager.GetVCSchoolVisitsByName(vcSchoolVisitName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolVisitModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vcSchoolVisit data by Id
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolVisitById")]
        public async Task<SingularResponse<VCSchoolVisitModel>> GetVCSchoolVisitById([FromBody] DataRequest vcSchoolVisitRequest)
        {
            SingularResponse<VCSchoolVisitModel> response = new SingularResponse<VCSchoolVisitModel>();

            try
            {
                var vcSchoolVisitModel = await Task.Run(() =>
                {
                    return this.vcSchoolVisitManager.GetVCSchoolVisitById(Guid.Parse(vcSchoolVisitRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vcSchoolVisitModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vcSchoolVisit
        /// </summary>
        /// <param name="vcSchoolVisitRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVCSchoolVisit"), Route("CreateOrUpdateVCSchoolVisitDetails")]
        public async Task<SingularResponse<string>> CreateVCSchoolVisit([FromBody] VCSchoolVisitRequest vcSchoolVisitRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vcSchoolVisitRequest.RequestType = RequestType.New;
                    return this.vcSchoolVisitManager.SaveOrUpdateVCSchoolVisitDetails(vcSchoolVisitRequest);
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
        /// Update vcSchoolVisit by Id
        /// </summary>
        /// <param name="vcSchoolVisitRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVCSchoolVisit")]
        public async Task<SingularResponse<string>> UpdateVCSchoolVisit([FromBody] VCSchoolVisitRequest vcSchoolVisitRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vcSchoolVisitRequest.RequestType = RequestType.Edit;
                    return this.vcSchoolVisitManager.SaveOrUpdateVCSchoolVisitDetails(vcSchoolVisitRequest);
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
        /// Delete vcSchoolVisit by Id
        /// </summary>
        /// <param name="vcSchoolVisitRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVCSchoolVisitById")]
        public async Task<SingularResponse<bool>> DeleteVCSchoolVisitById([FromBody] DeleteRequest<Guid> vcSchoolVisitRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vcSchoolVisitManager.DeleteById(vcSchoolVisitRequest.DataId);
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