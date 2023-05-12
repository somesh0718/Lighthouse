using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the DataType entity
    /// </summary>
    public interface IDataTypeManager : IGenericManager<DataTypeModel>
    {
        /// <summary>
        /// Get list of DataTypes
        /// </summary>
        /// <returns></returns>
        IQueryable<DataTypeModel> GetDataTypes();

        /// <summary>
        /// Get list of DataTypes by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<DataTypeModel> GetDataTypesByName(string dataTypeName);

        /// <summary>
        /// Get DataType by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        DataTypeModel GetDataTypeById(int dataTypeId);

        /// <summary>
        /// Get DataType by DataTypeId using async
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        Task<DataTypeModel> GetDataTypeByIdAsync(int dataTypeId);

        /// <summary>
        /// Insert/Update DataType entity
        /// </summary>
        /// <param name="dataTypeModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateDataTypeDetails(DataTypeModel dataTypeModel);

        /// <summary>
        /// Delete a record by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        bool DeleteById(int dataTypeId);

        /// <summary>
        /// Check duplicate DataType by Name
        /// </summary>
        /// <param name="dataTypeModel"></param>
        /// <returns></returns>
        bool CheckDataTypeExistByName(DataTypeModel dataTypeModel);

        /// <summary>
        /// List of DataType with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<DataTypeViewModel> GetDataTypesByCriteria(SearchDataTypeModel searchModel);
    }
}
