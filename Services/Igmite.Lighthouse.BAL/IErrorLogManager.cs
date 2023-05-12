using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the ErrorLog entity
    /// </summary>
    public interface IErrorLogManager : IGenericManager<ErrorLogModel>
    {
        /// <summary>
        /// Get list of ErrorLogs
        /// </summary>
        /// <returns></returns>
        IQueryable<ErrorLogModel> GetErrorLogs();

        /// <summary>
        /// Get list of ErrorLogs by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<ErrorLogModel> GetErrorLogsByName(string errorLogName);

        /// <summary>
        /// Get ErrorLog by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        ErrorLogModel GetErrorLogById(Guid errorLogId);

        /// <summary>
        /// Get ErrorLog by ErrorLogId using async
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        Task<ErrorLogModel> GetErrorLogByIdAsync(Guid errorLogId);

        /// <summary>
        /// Insert/Update ErrorLog entity
        /// </summary>
        /// <param name="errorLogModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateErrorLogDetails(ErrorLogModel errorLogModel);

        /// <summary>
        /// Delete a record by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        bool DeleteById(Guid errorLogId);

        /// <summary>
        /// Check duplicate ErrorLog by Name
        /// </summary>
        /// <param name="errorLogModel"></param>
        /// <returns></returns>
        bool CheckErrorLogExistByName(ErrorLogModel errorLogModel);

        /// <summary>
        /// List of ErrorLog with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<ErrorLogViewModel> GetErrorLogsByCriteria(SearchErrorLogModel searchModel);
    }
}
