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
    /// Expose all dataType WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class DataTypeController : BaseController
    {
        private readonly IDataTypeManager dataTypeManager;

        /// <summary>
        /// Initializes the DataType controller class.
        /// </summary>
        /// <param name="_dataTypeManager"></param>
        public DataTypeController(IDataTypeManager _dataTypeManager)
        {
            this.dataTypeManager = _dataTypeManager;
        }

        /// <summary>
        /// Get list of dataType data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDataTypes")]
        public async Task<ListResponse<DataTypeModel>> GetDataTypes()
        {
            ListResponse<DataTypeModel> response = new ListResponse<DataTypeModel>();

            try
            {
                IQueryable<DataTypeModel> dataTypeModels = await Task.Run(() =>
                {
                    return this.dataTypeManager.GetDataTypes();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataTypeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of DataType with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDataTypesByCriteria")]
        public async Task<ListResponse<DataTypeViewModel>> GetDataTypesByCriteria([FromBody] SearchDataTypeModel searchModel)
        {
            ListResponse<DataTypeViewModel> response = new ListResponse<DataTypeViewModel>();

            try
            {
                var dataTypeModels = await Task.Run(() =>
                {
                    return this.dataTypeManager.GetDataTypesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataTypeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of dataType data by name
        /// </summary>
        /// <param name="dataTypeName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDataTypesByName")]
        public async Task<ListResponse<DataTypeModel>> GetDataTypesByName([FromQuery] string dataTypeName)
        {
            ListResponse<DataTypeModel> response = new ListResponse<DataTypeModel>();

            try
            {
                var dataTypeModels = await Task.Run(() =>
                {
                    return this.dataTypeManager.GetDataTypesByName(dataTypeName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataTypeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get dataType data by Id
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDataTypeById")]
        public async Task<SingularResponse<DataTypeModel>> GetDataTypeById([FromBody] DataRequest dataTypeRequest)
        {
            SingularResponse<DataTypeModel> response = new SingularResponse<DataTypeModel>();

            try
            {
                var dataTypeModel = await Task.Run(() =>
                {
                    return this.dataTypeManager.GetDataTypeById(Convert.ToInt32(dataTypeRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = dataTypeModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new dataType
        /// </summary>
        /// <param name="dataTypeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateDataType"), Route("CreateOrUpdateDataTypeDetails")]
        public async Task<SingularResponse<string>> CreateDataType([FromBody] DataTypeRequest dataTypeRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //dataTypeRequest.RequestType = RequestType.New;
                    return this.dataTypeManager.SaveOrUpdateDataTypeDetails(dataTypeRequest);
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
        /// Update dataType by Id
        /// </summary>
        /// <param name="dataTypeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateDataType")]
        public async Task<SingularResponse<string>> UpdateDataType([FromBody] DataTypeRequest dataTypeRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    dataTypeRequest.RequestType = RequestType.Edit;
                    return this.dataTypeManager.SaveOrUpdateDataTypeDetails(dataTypeRequest);
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
        /// Delete dataType by Id
        /// </summary>
        /// <param name="dataTypeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteDataTypeById")]
        public async Task<SingularResponse<bool>> DeleteDataTypeById([FromBody] DeleteRequest<int> dataTypeRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.dataTypeManager.DeleteById(dataTypeRequest.DataId);
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