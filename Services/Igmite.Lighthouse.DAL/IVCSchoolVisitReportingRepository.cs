using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VCSchoolVisitReporting entity
    /// </summary>
    public interface IVCSchoolVisitReportingRepository : IGenericRepository<VCSchoolVisitReporting>
    {
        /// <summary>
        /// Get list of VCSchoolVisitReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<VCSchoolVisitReporting> GetVCSchoolVisitReporting();

        /// <summary>
        /// Get list of VCSchoolVisitReporting by vcSchoolVisitReportingName
        /// </summary>
        /// <param name="vcSchoolVisitReportingName"></param>
        /// <returns></returns>
        IQueryable<VCSchoolVisitReporting> GetVCSchoolVisitReportingByName(string vcSchoolVisitReportingName);

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        VCSchoolVisitReporting GetVCSchoolVisitReportingById(Guid vcSchoolVisitReportingId);

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId using async
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        Task<VCSchoolVisitReporting> GetVCSchoolVisitReportingByIdAsync(Guid vcSchoolVisitReportingId);

        /// <summary>
        /// Insert/Update VCSchoolVisitReporting entity
        /// </summary>
        /// <param name="vcSchoolVisitReporting"></param>
        /// <returns></returns>
        bool SaveOrUpdateVCSchoolVisitReportingDetails(VCSchoolVisitReporting vcSchoolVisitReporting);

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