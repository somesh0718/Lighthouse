using Igmite.Lighthouse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the DataValue entity
    /// </summary>
    public interface IDataValueManager : IGenericManager<DataValueModel>
    {
        /// <summary>
        /// Get list of DataValues
        /// </summary>
        /// <returns></returns>
        IQueryable<DataValueModel> GetDataValues();

        /// <summary>
        /// Get list of DataValues by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<DataValueModel> GetDataValuesByName(string dataValueName);

        /// <summary>
        /// Get DataValue by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        DataValueModel GetDataValueById(string dataValueId);

        /// <summary>
        /// Get DataValue by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        DataValueModel GetDataValueByCode(string code);

        /// <summary>
        /// Get DataValue by DataValueId using async
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        Task<DataValueModel> GetDataValueByIdAsync(string dataValueId);

        /// <summary>
        /// Insert/Update DataValue entity
        /// </summary>
        /// <param name="dataValueModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateDataValueDetails(DataValueModel dataValueModel);

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
        IQueryable<DataValueModel> GetDataValuesByType(string dataTypeId);
    }
}