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
    /// Expose all toolEquipment WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class ToolEquipmentController : BaseController
    {
        private readonly IToolEquipmentManager toolEquipmentManager;

        /// <summary>
        /// Initializes the ToolEquipment controller class.
        /// </summary>
        /// <param name="_toolEquipmentManager"></param>
        public ToolEquipmentController(IToolEquipmentManager _toolEquipmentManager)
        {
            this.toolEquipmentManager = _toolEquipmentManager;
        }

        /// <summary>
        /// Get list of toolEquipment data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetToolEquipments")]
        public async Task<ListResponse<ToolEquipmentModel>> GetToolEquipments()
        {
            ListResponse<ToolEquipmentModel> response = new ListResponse<ToolEquipmentModel>();

            try
            {
                IQueryable<ToolEquipmentModel> toolEquipmentModels = await Task.Run(() =>
                {
                    return this.toolEquipmentManager.GetToolEquipments();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = toolEquipmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of ToolEquipment with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetToolEquipmentsByCriteria")]
        public async Task<ListResponse<ToolEquipmentViewModel>> GetToolEquipmentsByCriteria([FromBody] SearchToolEquipmentModel searchModel)
        {
            ListResponse<ToolEquipmentViewModel> response = new ListResponse<ToolEquipmentViewModel>();

            try
            {
                var toolEquipmentModels = await Task.Run(() =>
                {
                    return this.toolEquipmentManager.GetToolEquipmentsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = toolEquipmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of toolEquipment data by name
        /// </summary>
        /// <param name="toolEquipmentName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetToolEquipmentsByName")]
        public async Task<ListResponse<ToolEquipmentModel>> GetToolEquipmentsByName([FromQuery] string toolEquipmentName)
        {
            ListResponse<ToolEquipmentModel> response = new ListResponse<ToolEquipmentModel>();

            try
            {
                var toolEquipmentModels = await Task.Run(() =>
                {
                    return this.toolEquipmentManager.GetToolEquipmentsByName(toolEquipmentName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = toolEquipmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get toolEquipment data by Id
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetToolEquipmentById")]
        public async Task<SingularResponse<ToolEquipmentModel>> GetToolEquipmentById([FromBody] DataRequest toolEquipmentRequest)
        {
            SingularResponse<ToolEquipmentModel> response = new SingularResponse<ToolEquipmentModel>();

            try
            {
                var toolEquipmentModel = await Task.Run(() =>
                {
                    return this.toolEquipmentManager.GetToolEquipmentById(Guid.Parse(toolEquipmentRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = toolEquipmentModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new toolEquipment
        /// </summary>
        /// <param name="toolEquipmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateToolEquipment"), Route("CreateOrUpdateToolEquipmentDetails")]
        public async Task<SingularResponse<string>> CreateToolEquipment([FromBody] ToolEquipmentRequest toolEquipmentRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //toolEquipmentRequest.RequestType = RequestType.New;
                    return this.toolEquipmentManager.SaveOrUpdateToolEquipmentDetails(toolEquipmentRequest);
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
        /// Update toolEquipment by Id
        /// </summary>
        /// <param name="toolEquipmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateToolEquipment")]
        public async Task<SingularResponse<string>> UpdateToolEquipment([FromBody] ToolEquipmentRequest toolEquipmentRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    toolEquipmentRequest.RequestType = RequestType.Edit;
                    return this.toolEquipmentManager.SaveOrUpdateToolEquipmentDetails(toolEquipmentRequest);
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
        /// Delete toolEquipment by Id
        /// </summary>
        /// <param name="toolEquipmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteToolEquipmentById")]
        public async Task<SingularResponse<bool>> DeleteToolEquipmentById([FromBody] DeleteRequest<Guid> toolEquipmentRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.toolEquipmentManager.DeleteById(toolEquipmentRequest.DataId);
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

        /// <summary>
        /// Get list of TEAndRMList data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTEAndRMList")]
        public async Task<ListResponse<TEAndRMListModel>> GetTEAndRMList()
        {
            ListResponse<TEAndRMListModel> response = new ListResponse<TEAndRMListModel>();

            try
            {
                IQueryable<TEAndRMListModel> tEAndRMListModels = await Task.Run(() =>
                {
                    return this.toolEquipmentManager.GetTEAndRMList();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = tEAndRMListModels.ToList();
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