using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VCSchoolVisit entity
    /// </summary>
    public interface IVCSchoolVisitManager : IGenericManager<VCSchoolVisitModel>
    {
        /// <summary>
        /// Get list of VCSchoolVisits
        /// </summary>
        /// <returns></returns>
        IQueryable<VCSchoolVisitModel> GetVCSchoolVisits();

        /// <summary>
        /// Get list of VCSchoolVisits by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VCSchoolVisitModel> GetVCSchoolVisitsByName(string vcSchoolVisitName);

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        VCSchoolVisitModel GetVCSchoolVisitById(Guid vcSchoolVisitId);

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId using async
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        Task<VCSchoolVisitModel> GetVCSchoolVisitByIdAsync(Guid vcSchoolVisitId);

        /// <summary>
        /// Insert/Update VCSchoolVisit entity
        /// </summary>
        /// <param name="vcSchoolVisitModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVCSchoolVisitDetails(VCSchoolVisitModel vcSchoolVisitModel);

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