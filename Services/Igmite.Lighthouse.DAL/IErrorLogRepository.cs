using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the ErrorLog entity
    /// </summary>
    public interface IErrorLogRepository : IGenericRepository<ErrorLog>
    {
        /// <summary>
        /// Get list of ErrorLog
        /// </summary>
        /// <returns></returns>
        IQueryable<ErrorLog> GetErrorLogs();

        /// <summary>
        /// Get list of ErrorLog by errorLogName
        /// </summary>
        /// <param name="errorLogName"></param>
        /// <returns></returns>
        IQueryable<ErrorLog> GetErrorLogsByName(string errorLogName);

        /// <summary>
        /// Get ErrorLog by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        ErrorLog GetErrorLogById(Guid errorLogId);

        /// <summary>
        /// Get ErrorLog by ErrorLogId using async
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        Task<ErrorLog> GetErrorLogByIdAsync(Guid errorLogId);

        /// <summary>
        /// Insert/Update ErrorLog entity
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        bool SaveOrUpdateErrorLogDetails(ErrorLog errorLog);

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
