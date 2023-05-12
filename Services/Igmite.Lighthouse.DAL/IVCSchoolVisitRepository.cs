using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VCSchoolVisit entity
    /// </summary>
    public interface IVCSchoolVisitRepository : IGenericRepository<VCSchoolVisit>
    {
        /// <summary>
        /// Get list of VCSchoolVisit
        /// </summary>
        /// <returns></returns>
        IQueryable<VCSchoolVisit> GetVCSchoolVisits();

        /// <summary>
        /// Get list of VCSchoolVisit by vcSchoolVisitName
        /// </summary>
        /// <param name="vcSchoolVisitName"></param>
        /// <returns></returns>
        IQueryable<VCSchoolVisit> GetVCSchoolVisitsByName(string vcSchoolVisitName);

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        VCSchoolVisit GetVCSchoolVisitById(Guid vcSchoolVisitId);

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId using async
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        Task<VCSchoolVisit> GetVCSchoolVisitByIdAsync(Guid vcSchoolVisitId);

        /// <summary>
        /// Insert/Update VCSchoolVisit entity
        /// </summary>
        /// <param name="vcSchoolVisit"></param>
        /// <returns></returns>
        bool SaveOrUpdateVCSchoolVisitDetails(VCSchoolVisit vcSchoolVisit);

        /// <summary>
        /// Delete a record by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcSchoolVisitId);

        /// <summary>
        /// Check duplicate VCSchoolVisit by Name
        /// </summary>
        /// <param name="vcSchoolVisitModel"></param>
        /// <returns></returns>
        string CheckVCSchoolVisitExistByName(VCSchoolVisitModel vcSchoolVisitModel);

        /// <summary>
        /// List of VCSchoolVisit with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VCSchoolVisitViewModel> GetVCSchoolVisitsByCriteria(SearchVCSchoolVisitModel searchModel);
    }
}
