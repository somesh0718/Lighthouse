using Igmite.Lighthouse.Entities;
using System;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Batch Job Works
    /// </summary>
    public interface IBatchJobRepository : IGenericRepository<ExecuteScriptQuery>
    {
        /// <summary>
        /// Generate VT not submitted daily reporting data
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool GenerateVTNotSubmittedDailyReportingData(DateTime startDate, DateTime endDate);
    }
}