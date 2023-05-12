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
    /// Expose all dataValue WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class DataValueController : BaseController
    {
        private readonly IDataValueManager dataValueManager;

        /// <summary>
        /// Initializes the DataValue controller class.
        /// </summary>
        /// <param name="_dataValueManager"></param>
        public DataValueController(IDataValueManager _dataValueManager)
        {
            this.dataValueManager = _dataValueManager;
        }

        /// <summary>
        /// Get list of dataValue data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDataValues")]
        public async Task<ListResponse<DataValueModel>> GetDataValues()
        {
            ListResponse<DataValueModel> response = new ListResponse<DataValueModel>();

            try
            {
                IQueryable<DataValueModel> dataValueModels = await Task.Run(() =>
                {
                    return this.dataValueManager.GetDataValues();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataValueModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of DataValue with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDataValuesByCriteria")]
        public async Task<ListResponse<DataValueViewModel>> GetDataValuesByCriteria([FromBody] SearchDataValueModel searchModel)
        {
            ListResponse<DataValueViewModel> response = new ListResponse<DataValueViewModel>();

            try
            {
                var dataValueModels = await Task.Run(() =>
                {
                    return this.dataValueManager.GetDataValuesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataValueModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of dataValue data by name
        /// </summary>
        /// <param name="dataValueName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDataValuesByName")]
        public async Task<ListResponse<DataValueModel>> GetDataValuesByName([FromQuery] string dataValueName)
        {
            ListResponse<DataValueModel> response = new ListResponse<DataValueModel>();

            try
            {
                var dataValueModels = await Task.Run(() =>
                {
                    return this.dataValueManager.GetDataValuesByName(dataValueName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataValueModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get dataValue data by Id
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDataValueById")]
        public async Task<SingularResponse<DataValueModel>> GetDataValueById([FromBody] DataRequest dataValueRequest)
        {
            SingularResponse<DataValueModel> response = new SingularResponse<DataValueModel>();

            try
            {
                var dataValueModel = await Task.Run(() =>
                {
                    return this.dataValueManager.GetDataValueById(dataValueRequest.DataId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = dataValueModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new dataValue
        /// </summary>
        /// <param name="dataValueRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateDataValue"), Route("CreateOrUpdateDataValueDetails")]
        public async Task<SingularResponse<string>> CreateDataValue([FromBody] DataValueRequest dataValueRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //dataValueRequest.RequestType = RequestType.New;
                    return this.dataValueManager.SaveOrUpdateDataValueDetails(dataValueRequest);
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
        /// Update dataValue by Id
        /// </summary>
        /// <param name="dataValueRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateDataValue")]
        public async Task<SingularResponse<string>> UpdateDataValue([FromBody] DataValueRequest dataValueRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    dataValueRequest.RequestType = RequestType.Edit;
                    return this.dataValueManager.SaveOrUpdateDataValueDetails(dataValueRequest);
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
        /// Delete dataValue by Id
        /// </summary>
        /// <param name="dataValueRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteDataValueById")]
        public async Task<SingularResponse<bool>> DeleteDataValueById([FromBody] DeleteRequest<string> dataValueRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.dataValueManager.DeleteById(dataValueRequest.DataId);
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