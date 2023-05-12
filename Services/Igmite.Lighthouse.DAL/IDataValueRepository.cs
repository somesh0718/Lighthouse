using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the DataValue entity
    /// </summary>
    public interface IDataValueRepository : IGenericRepository<DataValue>
    {
        /// <summary>
        /// Get list of DataValue
        /// </summary>
        /// <returns></returns>
        IQueryable<DataValue> GetDataValues();

        /// <summary>
        /// Get last DataValue value
        /// </summary>
        /// <returns></returns>
        DataValue GetLastDataValue();

        /// <summary>
        /// Get list of DataValue by dataValueName
        /// </summary>
        /// <param name="dataValueName"></param>
        /// <returns></returns>
        IQueryable<DataValue> GetDataValuesByName(string dataValueName);

        /// <summary>
        /// Get DataValue by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        DataValue GetDataValueById(string dataValueId);

        /// <summary>
        /// Get DataValue by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        DataValue GetDataValueByCode(string code);

        /// <summary>
        /// Get DataValue by DataValueId using async
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        Task<DataValue> GetDataValueByIdAsync(string dataValueId);

        /// <summary>
        /// Insert/Update DataValue entity
        /// </summary>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        bool SaveOrUpdateDataValueDetails(DataValue dataValue);

        /// <summary>
        /// Delete a record by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        bool DeleteById(string dataValueId);

        /// <summary>
        /// Check duplicate DataValue by Name
        /// </summary>
        /// <param name="dataValueModel"></param>
        /// <returns></returns>
        bool CheckDataValueExistByName(DataValueModel dataValueModel);

        /// <summary>
        /// List of DataValue with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<DataValueViewModel> GetDataValuesByCriteria(SearchDataValueModel searchModel);

        /// <summary>
        /// Get list of DataValue by DataType
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        IQueryable<DataValue> GetDataValuesByType(string dataTypeId);
    }
}