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
    /// Expose all vtClass WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTClassController : BaseController
    {
        private readonly IVTClassManager vtClassManager;

        /// <summary>
        /// Initializes the VTClass controller class.
        /// </summary>
        /// <param name="_vtClassManager"></param>
        public VTClassController(IVTClassManager _vtClassManager)
        {
            this.vtClassManager = _vtClassManager;
        }

        /// <summary>
        /// Get list of vtClass data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTClasses")]
        public async Task<ListResponse<VTClassModel>> GetVTClasses()
        {
            ListResponse<VTClassModel> response = new ListResponse<VTClassModel>();

            try
            {
                IQueryable<VTClassModel> vtClassModels = await Task.Run(() =>
                {
                    return this.vtClassManager.GetVTClasses();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTClass with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTClassesByCriteria")]
        public async Task<ListResponse<VTClassViewModel>> GetVTClassesByCriteria([FromBody] SearchVTClassModel searchModel)
        {
            ListResponse<VTClassViewModel> response = new ListResponse<VTClassViewModel>();

            try
            {
                var vtClassModels = await Task.Run(() =>
                {
                    return this.vtClassManager.GetVTClassesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtClass data by name
        /// </summary>
        /// <param name="vtClassName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTClassesByName")]
        public async Task<ListResponse<VTClassModel>> GetVTClassesByName([FromQuery] string vtClassName)
        {
            ListResponse<VTClassModel> response = new ListResponse<VTClassModel>();

            try
            {
                var vtClassModels = await Task.Run(() =>
                {
                    return this.vtClassManager.GetVTClassesByName(vtClassName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtClass data by Id
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTClassById")]
        public async Task<SingularResponse<VTClassModel>> GetVTClassById([FromBody] DataRequest vtClassRequest)
        {
            SingularResponse<VTClassModel> response = new SingularResponse<VTClassModel>();

            try
            {
                var vtClassModel = await Task.Run(() =>
                {
                    return this.vtClassManager.GetVTClassById(Guid.Parse(vtClassRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtClassModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtClass
        /// </summary>
        /// <param name="vtClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTClass"), Route("CreateOrUpdateVTClassDetails")]
        public async Task<SingularResponse<string>> CreateVTClass([FromBody] VTClassRequest vtClassRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtClassRequest.RequestType = RequestType.New;
                    return this.vtClassManager.SaveOrUpdateVTClassDetails(vtClassRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = Logging.ErrorManager.Instance.GetErrorMessages(ex, HttpContext);

                response.Errors.Add(errorMessage);
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update vtClass by Id
        /// </summary>
        /// <param name="vtClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTClass")]
        public async Task<SingularResponse<string>> UpdateVTClass([FromBody] VTClassRequest vtClassRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtClassRequest.RequestType = RequestType.Edit;
                    return this.vtClassManager.SaveOrUpdateVTClassDetails(vtClassRequest);
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
        /// Delete vtClass by Id
        /// </summary>
        /// <param name="vtClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTClassById")]
        public async Task<SingularResponse<bool>> DeleteVTClassById([FromBody] DeleteRequest<Guid> vtClassRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtClassManager.DeleteById(vtClassRequest.DataId);
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