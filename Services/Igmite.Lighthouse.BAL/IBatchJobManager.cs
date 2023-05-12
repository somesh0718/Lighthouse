using Igmite.Lighthouse.Models;
using System;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Repository of the Batch Job Works
    /// </summary>
    public interface IBatchJobManager : IGenericManager<RoleModel>
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