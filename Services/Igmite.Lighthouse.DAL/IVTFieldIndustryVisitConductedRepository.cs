using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTFieldIndustryVisitConducted entity
    /// </summary>
    public interface IVTFieldIndustryVisitConductedRepository : IGenericRepository<VTFieldIndustryVisitConducted>
    {
        /// <summary>
        /// Get list of VTFieldIndustryVisitConducted
        /// </summary>
        /// <returns></returns>
        IQueryable<VTFieldIndustryVisitConducted> GetVTFieldIndustryVisitConducteds();

        /// <summary>
        /// Get list of VTFieldIndustryVisitConducted by vtFieldIndustryVisitConductedName
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedName"></param>
        /// <returns></returns>
        IQueryable<VTFieldIndustryVisitConducted> GetVTFieldIndustryVisitConductedsByName(string vtFieldIndustryVisitConductedName);

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        VTFieldIndustryVisitConducted GetVTFieldIndustryVisitConductedById(Guid vtFieldIndustryVisitConductedId);

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId using async
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        Task<VTFieldIndustryVisitConducted> GetVTFieldIndustryVisitConductedByIdAsync(Guid vtFieldIndustryVisitConductedId);

        /// <summary>
        /// Insert/Update VTFieldIndustryVisitConducted entity
        /// </summary>
        /// <param name="vtFieldIndustryVisitConducted"></param>
        /// <param name="fieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTFieldIndustryVisitConductedDetails(VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted, VTFieldIndustryVisitConductedModel fieldIndustryVisitConductedModel);

        /// <summary>
        /// Delete a record by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtFieldIndustryVisitConductedId);

        /// <summary>
        /// Check duplicate VTFieldIndustryVisitConducted by Name
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        List<string> CheckVTFieldIndustryVisitConductedExistByName(VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel);

        /// <summary>
        /// List of VTFieldIndustryVisitConducted with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTFieldIndustryVisitConductedViewModel> GetVTFieldIndustryVisitConductedsByCriteria(SearchVTFieldIndustryVisitConductedModel searchModel);

        /// <summary>
        /// Get list of VTFSections by fieldIndustryVisitId
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Guid GetVTFSectionsByFieldIndustryVisitId(Guid fieldIndustryVisitId);

        /// <summary>
        /// Get list of UnitSessionsTaughts by fieldIndustryVisitId
        /// </summary>
        /// <param name="fieldIndustryVisitId"></param>
        /// <returns></returns>
        IList<UnitSessionsModel> GetVTFUnitSessionsTaughtsByFieldIndustryVisitId(Guid fieldIndustryVisitId);

        /// <summary>
        /// Get list of VTFStudents by fieldIndustryVisitId
        /// </summary>
        /// <param name="fieldIndustryVisitId"></param>
        /// <returns></returns>
        IList<StudentAttendanceModel> GetVTFStudentsByFieldIndustryVisitId(Guid fieldIndustryVisitId);
    }
}