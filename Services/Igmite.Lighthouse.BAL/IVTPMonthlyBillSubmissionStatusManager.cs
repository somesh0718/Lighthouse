using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTPMonthlyBillSubmissionStatus entity
    /// </summary>
    public interface IVTPMonthlyBillSubmissionStatusManager : IGenericManager<VTPMonthlyBillSubmissionStatusModel>
    {
        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPMonthlyBillSubmissionStatusModel> GetVTPMonthlyBillSubmissionStatus();

        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTPMonthlyBillSubmissionStatusModel> GetVTPMonthlyBillSubmissionStatusByName(string vtpMonthlyBillSubmissionStatusName);

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        VTPMonthlyBillSubmissionStatusModel GetVTPMonthlyBillSubmissionStatusById(Guid vtpMonthlyBillSubmissionStatusId);

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId using async
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        Task<VTPMonthlyBillSubmissionStatusModel> GetVTPMonthlyBillSubmissionStatusByIdAsync(Guid vtpMonthlyBillSubmissionStatusId);

        /// <summary>
        /// Insert/Update VTPMonthlyBillSubmissionStatus entity
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel);

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
