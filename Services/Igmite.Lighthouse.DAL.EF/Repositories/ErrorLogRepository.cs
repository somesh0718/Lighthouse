using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the ErrorLog entity
    /// </summary>
    public class ErrorLogRepository : GenericRepository<ErrorLog>, IErrorLogRepository
    {
        /// <summary>
        /// Get list of ErrorLog
        /// </summary>
        /// <returns></returns>
        public IQueryable<ErrorLog> GetErrorLogs()
        {
            return this.Context.ErrorLogs.AsQueryable();
        }

        /// <summary>
        /// Get list of ErrorLog by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ErrorLog> GetErrorLogsByName(string name)
        {
            var errorLogs = (from e in this.Context.ErrorLogs
                         where e.ModuleName.Contains(name)
                         select e).AsQueryable();

            return errorLogs;
        }

        /// <summary>
        /// Get ErrorLog by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public ErrorLog GetErrorLogById(Guid errorLogId)
        {
            return this.Context.ErrorLogs.FirstOrDefault(e => e.ErrorLogId == errorLogId);
        }

        /// <summary>
        /// Get ErrorLog by ErrorLogId using async
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public async Task<ErrorLog> GetErrorLogByIdAsync(Guid errorLogId)
        {
            var errorLog = await (from e in this.Context.ErrorLogs
                              where e.ErrorLogId == errorLogId
                              select e).FirstOrDefaultAsync();

            return errorLog;
        }

        /// <summary>
        /// Insert/Update ErrorLog entity
        /// </summary>
        /// <param name="errorLog"></param>
        /// <returns></returns>
        public bool SaveOrUpdateErrorLogDetails(ErrorLog errorLog)
        {
            if (RequestType.New == errorLog.RequestType)
                Context.ErrorLogs.Add(errorLog);
            else
            {
                Context.Entry<ErrorLog>(errorLog).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid errorLogId)
        {
            ErrorLog errorLog = this.Context.ErrorLogs.FirstOrDefault(e => e.ErrorLogId == errorLogId);

            if (errorLog != null)
            {
                Context.Entry<ErrorLog>(errorLog).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate ErrorLog by Name
        /// </summary>
        /// <param name="errorLogModel"></param>
        /// <returns></returns>
        public bool CheckErrorLogExistByName(ErrorLogModel errorLogModel)
        {
            ErrorLog errorLog = this.Context.ErrorLogs.FirstOrDefault(e => e.ModuleName == errorLogModel.ModuleName);

            return errorLog != null;
        }

        /// <summary>}
        /// List of ErrorLog with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ErrorLogViewModel> GetErrorLogsByCriteria(SearchErrorLogModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.ErrorLogViewModels.FromSql<ErrorLogViewModel>("CALL GetErrorLogsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
