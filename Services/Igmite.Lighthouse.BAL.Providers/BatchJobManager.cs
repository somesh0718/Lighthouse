using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Repository of the Batch Job Works
    /// </summary>
    public class BatchJobManager : GenericManager<RoleModel>, IBatchJobManager
    {
        private readonly IBatchJobRepository batchJobRepository;

        /// <summary>
        /// Initializes the Batch Job manager.
        /// </summary>
        /// <param name="batchJobRepository"></param>
        public BatchJobManager(IBatchJobRepository _batchJobRepository)
        {
            this.batchJobRepository = _batchJobRepository;
        }

        /// <summary>
        /// Generate VT not submitted daily reporting data
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GenerateVTNotSubmittedDailyReportingData(DateTime startDate, DateTime endDate)
        {
            return this.batchJobRepository.GenerateVTNotSubmittedDailyReportingData(startDate, endDate);
        }
    }
}