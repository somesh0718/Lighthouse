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
    /// Manager of the DataType entity
    /// </summary>
    public class DataTypeManager : GenericManager<DataTypeModel>, IDataTypeManager
    {
        private readonly IDataTypeRepository dataTypeRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the dataType manager.
        /// </summary>
        /// <param name="dataTypeRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public DataTypeManager(IDataTypeRepository _dataTypeRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.dataTypeRepository = _dataTypeRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of DataTypes
        /// </summary>
        /// <returns></returns>
        public IQueryable<DataTypeModel> GetDataTypes()
        {
            var dataTypes = this.dataTypeRepository.GetDataTypes();

            IList<DataTypeModel> dataTypeModels = new List<DataTypeModel>();
            dataTypes.ForEach((user) => dataTypeModels.Add(user.ToModel()));

            return dataTypeModels.AsQueryable();
        }

        /// <summary>
        /// Get list of DataTypes by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DataTypeModel> GetDataTypesByName(string dataTypeName)
        {
            var dataTypes = this.dataTypeRepository.GetDataTypesByName(dataTypeName);

            IList<DataTypeModel> dataTypeModels = new List<DataTypeModel>();
            dataTypes.ForEach((user) => dataTypeModels.Add(user.ToModel()));

            return dataTypeModels.AsQueryable();
        }

        /// <summary>
        /// Get DataType by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public DataTypeModel GetDataTypeById(int dataTypeId)
        {
            DataType dataType = this.dataTypeRepository.GetDataTypeById(dataTypeId);

            return (dataType != null) ? dataType.ToModel() : null;
        }

        /// <summary>
        /// Get DataType by DataTypeId using async
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public async Task<DataTypeModel> GetDataTypeByIdAsync(int dataTypeId)
        {
            var dataType = await this.dataTypeRepository.GetDataTypeByIdAsync(dataTypeId);

            return (dataType != null) ? dataType.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update DataType entity
        /// </summary>
        /// <param name="dataTypeModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateDataTypeDetails(DataTypeModel dataTypeModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            DataType dataType = null;

            //Validate model data
            dataTypeModel = dataTypeModel.GetModelValidationErrors<DataTypeModel>();

            if (dataTypeModel.ErrorMessages.Count > 0)
            {
                response.Errors = dataTypeModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (dataTypeModel.RequestType == RequestType.Edit)
            {
                dataType = this.dataTypeRepository.GetDataTypeById(dataTypeModel.DataTypeId);
            }
            else
            {
                dataType = new DataType();

                //var dataTypeItem = this.dataTypeRepository.GetLastDataType();
                //int intDataTypeId = 0;

                //if (dataTypeItem != null)
                //    int.TryParse(dataTypeItem.DataTypeId, out intDataTypeId);

                //dataType.DataTypeId = (intDataTypeId + 1).ToString();
            }

            if (dataTypeModel.ErrorMessages.Count == 0 && (dataTypeModel.Name.StringVal().ToLower() != dataType.Name.StringVal().ToLower()))
            {
                bool isDataTypeExists = this.dataTypeRepository.CheckDataTypeExistByName(dataTypeModel);

                if (isDataTypeExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                dataType.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                dataType = dataTypeModel.FromModel(dataType);

                //Save Or Update dataType details
                bool isSaved = this.dataTypeRepository.SaveOrUpdateDataTypeDetails(dataType);

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
        /// Delete a record by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public bool DeleteById(int dataTypeId)
        {
            return this.dataTypeRepository.DeleteById(dataTypeId);
        }

        /// <summary>
        /// Check duplicate DataType by Name
        /// </summary>
        /// <param name="dataTypeModel"></param>
        /// <returns></returns>
        public bool CheckDataTypeExistByName(DataTypeModel dataTypeModel)
        {
            return this.dataTypeRepository.CheckDataTypeExistByName(dataTypeModel);
        }

        /// <summary>}
        /// List of DataType with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DataTypeViewModel> GetDataTypesByCriteria(SearchDataTypeModel searchModel)
        {
            return this.dataTypeRepository.GetDataTypesByCriteria(searchModel);
        }
    }
}