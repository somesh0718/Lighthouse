using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the DataValue entity
    /// </summary>
    public class DataValueManager : GenericManager<DataValueModel>, IDataValueManager
    {
        private readonly IDataValueRepository dataValueRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the dataValue manager.
        /// </summary>
        /// <param name="dataValueRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public DataValueManager(IDataValueRepository _dataValueRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.dataValueRepository = _dataValueRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of DataValues
        /// </summary>
        /// <returns></returns>
        public IQueryable<DataValueModel> GetDataValues()
        {
            var dataValues = this.dataValueRepository.GetDataValues();

            IList<DataValueModel> dataValueModels = new List<DataValueModel>();
            dataValues.ForEach((user) => dataValueModels.Add(user.ToModel()));

            return dataValueModels.AsQueryable();
        }

        /// <summary>
        /// Get list of DataValues by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DataValueModel> GetDataValuesByName(string dataValueName)
        {
            var dataValues = this.dataValueRepository.GetDataValuesByName(dataValueName);

            IList<DataValueModel> dataValueModels = new List<DataValueModel>();
            dataValues.ForEach((user) => dataValueModels.Add(user.ToModel()));

            return dataValueModels.AsQueryable();
        }

        /// <summary>
        /// Get DataValue by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        public DataValueModel GetDataValueById(string dataValueId)
        {
            DataValue dataValue = this.dataValueRepository.GetDataValueById(dataValueId);

            return (dataValue != null) ? dataValue.ToModel() : null;
        }

        /// <summary>
        /// Get DataValue by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataValueModel GetDataValueByCode(string code)
        {
            DataValue dataValue = this.dataValueRepository.GetDataValueByCode(code);

            return (dataValue != null) ? dataValue.ToModel() : null;
        }

        /// <summary>
        /// Get DataValue by DataValueId using async
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        public async Task<DataValueModel> GetDataValueByIdAsync(string dataValueId)
        {
            var dataValue = await this.dataValueRepository.GetDataValueByIdAsync(dataValueId);

            return (dataValue != null) ? dataValue.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update DataValue entity
        /// </summary>
        /// <param name="dataValueModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateDataValueDetails(DataValueModel dataValueModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            DataValue dataValue = null;

            //Validate model data
            dataValueModel = dataValueModel.GetModelValidationErrors<DataValueModel>();

            if (dataValueModel.ErrorMessages.Count > 0)
            {
                response.Errors = dataValueModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (dataValueModel.RequestType == RequestType.Edit)
            {
                dataValue = this.dataValueRepository.GetDataValueById(dataValueModel.DataValueId);
            }
            else
            {
                dataValue = new DataValue();

                var dataValueItem = this.dataValueRepository.GetLastDataValue();
                int intDataValueId = 0;

                if (dataValueItem != null)
                    int.TryParse(dataValueItem.DataValueId, out intDataValueId);

                dataValue.DataValueId = (intDataValueId + 1).ToString();
            }

            if (dataValueModel.ErrorMessages.Count == 0 && (dataValueModel.DataTypeId.StringVal().ToLower() != dataValue.DataTypeId.StringVal().ToLower() && dataValueModel.Code.StringVal().ToLower() != dataValue.Code.StringVal().ToLower() && dataValueModel.Name.StringVal().ToLower() != dataValue.Name.StringVal().ToLower()))
            {
                bool isDataValueExists = this.dataValueRepository.CheckDataValueExistByName(dataValueModel);

                if (isDataValueExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                dataValue.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                dataValue = dataValueModel.FromModel(dataValue);

                //Save Or Update dataValue details
                bool isSaved = this.dataValueRepository.SaveOrUpdateDataValueDetails(dataValue);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        public bool DeleteById(string dataValueId)
        {
            return this.dataValueRepository.DeleteById(dataValueId);
        }

        /// <summary>
        /// Check duplicate DataValue by Name
        /// </summary>
        /// <param name="dataValueModel"></param>
        /// <returns></returns>
        public bool CheckDataValueExistByName(DataValueModel dataValueModel)
        {
            return this.dataValueRepository.CheckDataValueExistByName(dataValueModel);
        }

        /// <summary>}
        /// List of DataValue with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DataValueViewModel> GetDataValuesByCriteria(SearchDataValueModel searchModel)
        {
            return this.dataValueRepository.GetDataValuesByCriteria(searchModel);
        }

        /// <summary>
        /// Get list of DataValue by DataType
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public IQueryable<DataValueModel> GetDataValuesByType(string dataTypeId)
        {
            var dataValues = this.dataValueRepository.GetDataValuesByType(dataTypeId);

            IList<DataValueModel> dataValueModels = new List<DataValueModel>();
            dataValues.ForEach((user) => dataValueModels.Add(user.ToModel()));

            return dataValueModels.AsQueryable();
        }
    }
}