using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTPMonthlyBillSubmissionStatus entity
    /// </summary>
    public interface IVTPMonthlyBillSubmissionStatusRepository : IGenericRepository<VTPMonthlyBillSubmissionStatus>
    {
        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPMonthlyBillSubmissionStatus> GetVTPMonthlyBillSubmissionStatus();

        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus by vtpMonthlyBillSubmissionStatusName
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusName"></param>
        /// <returns></returns>
        IQueryable<VTPMonthlyBillSubmissionStatus> GetVTPMonthlyBillSubmissionStatusByName(string vtpMonthlyBillSubmissionStatusName);

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        VTPMonthlyBillSubmissionStatus GetVTPMonthlyBillSubmissionStatusById(Guid vtpMonthlyBillSubmissionStatusId);

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId using async
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        Task<VTPMonthlyBillSubmissionStatus> GetVTPMonthlyBillSubmissionStatusByIdAsync(Guid vtpMonthlyBillSubmissionStatusId);

        /// <summary>
        /// Insert/Update VTPMonthlyBillSubmissionStatus entity
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatus"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus);

        /// <summary>
        /// Delete a record by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtpMonthlyBillSubmissionStatusId);

        /// <summary>
        /// Check duplicate VTPMonthlyBillSubmissionStatus by Name
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusModel"></param>
        /// <returns></returns>
        bool CheckVTPMonthlyBillSubmissionStatusExistByName(VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel);

        /// <summary>
        /// List of VTPMonthlyBillSubmissionStatus with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTPMonthlyBillSubmissionStatusViewModel> GetVTPMonthlyBillSubmissionStatusByCriteria(SearchVTPMonthlyBillSubmissionStatusModel searchModel);
    }
}
