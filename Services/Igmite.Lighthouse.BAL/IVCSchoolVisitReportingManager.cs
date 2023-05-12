 
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VCSchoolVisitReporting entity
    /// </summary>
    public interface IVCSchoolVisitReportingManager : IGenericManager<VCSchoolVisitReportingModel>
    {
        /// <summary>
        /// Get list of VCSchoolVisitReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<VCSchoolVisitReportingModel> GetVCSchoolVisitReporting();

        /// <summary>
        /// Get list of VCSchoolVisitReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VCSchoolVisitReportingModel> GetVCSchoolVisitReportingByName(string vcSchoolVisitReportingName);

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        VCSchoolVisitReportingModel GetVCSchoolVisitReportingById(Guid vcSchoolVisitReportingId);

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId using async
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        Task<VCSchoolVisitReportingModel> GetVCSchoolVisitReportingByIdAsync(Guid vcSchoolVisitReportingId);

        /// <summary>
        /// Insert/Update VCSchoolVisitReporting entity
        /// </summary>
        /// <param name="vcSchoolVisitReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVCSchoolVisitReportingDetails(VCSchoolVisitReportingModel vcSchoolVisitReportingModel);

        /// <summary>
        /// Delete a record by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcSchoolVisitReportingId);

        /// <summary>
        /// Check duplicate VCSchoolVisitReporting by Name
        /// </summary>
        /// <param name="vcSchoolVisitReportingModel"></param>
        /// <returns></returns>
        bool CheckVCSchoolVisitReportingExistByName(VCSchoolVisitReportingModel vcSchoolVisitReportingModel);

        /// <summary>
        /// List of VCSchoolVisitReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VCSchoolVisitReportingViewModel> GetVCSchoolVisitReportingByCriteria(SearchVCSchoolVisitReportingModel searchModel);
    }
}
