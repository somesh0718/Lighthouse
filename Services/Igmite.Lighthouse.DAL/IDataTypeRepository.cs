using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the DataType entity
    /// </summary>
    public interface IDataTypeRepository : IGenericRepository<DataType>
    {
        /// <summary>
        /// Get list of DataType
        /// </summary>
        /// <returns></returns>
        IQueryable<DataType> GetDataTypes();

        /// <summary>
        /// Get last DataType value
        /// </summary>
        /// <returns></returns>
        DataType GetLastDataType();

        /// <summary>
        /// Get list of DataType by dataTypeName
        /// </summary>
        /// <param name="dataTypeName"></param>
        /// <returns></returns>
        IQueryable<DataType> GetDataTypesByName(string dataTypeName);

        /// <summary>
        /// Get DataType by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        DataType GetDataTypeById(int dataTypeId);

        /// <summary>
        /// Get DataType by DataTypeId using async
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        Task<DataType> GetDataTypeByIdAsync(int dataTypeId);

        /// <summary>
        /// Insert/Update DataType entity
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        bool SaveOrUpdateDataTypeDetails(DataType dataType);

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