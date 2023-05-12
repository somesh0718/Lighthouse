using Igmite.Lighthouse.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Batch Job Works
    /// </summary>
    public class BatchJobRepository : GenericRepository<ExecuteScriptQuery>, IBatchJobRepository
    {
        /// <summary>
        /// Generate VT not submitted daily reporting data
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GenerateVTNotSubmittedDailyReportingData(DateTime startDate, DateTime endDate)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "StartDate", MySqlDbType = MySqlDbType.VarChar, Value = startDate };
            sqlParams[1] = new MySqlParameter { ParameterName = "EndDate", MySqlDbType = MySqlDbType.VarChar, Value = endDate };
            sqlParams[2] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.Guid, Value = null };

            var executionResults = Context.ExecuteScriptQuery.FromSql<ExecuteScriptQuery>("CALL GenerateVTNotSubmittedDailyReportingData (@StartDate, @EndDate, @VTId)", sqlParams);

            return true;
        }
    }
}