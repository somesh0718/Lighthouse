using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTPracticalAssessment entity
    /// </summary>
    public interface IVTPracticalAssessmentRepository : IGenericRepository<VTPracticalAssessment>
    {
        /// <summary>
        /// Get list of VTPracticalAssessment
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPracticalAssessment> GetVTPracticalAssessments();

        /// <summary>
        /// Get list of VTPracticalAssessment by vtPracticalAssessmentName
        /// </summary>
        /// <param name="vtPracticalAssessmentName"></param>
        /// <returns></returns>
        IQueryable<VTPracticalAssessment> GetVTPracticalAssessmentsByName(string vtPracticalAssessmentName);

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        VTPracticalAssessment GetVTPracticalAssessmentById(Guid vtPracticalAssessmentId);

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId using async
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        Task<VTPracticalAssessment> GetVTPracticalAssessmentByIdAsync(Guid vtPracticalAssessmentId);

        /// <summary>
        /// Insert/Update VTPracticalAssessment entity
        /// </summary>
        /// <param name="vtPracticalAssessment"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTPracticalAssessmentDetails(VTPracticalAssessment vtPracticalAssessment);

        /// <summary>
        /// Delete a record by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtPracticalAssessmentId);

        /// <summary>
        /// Check duplicate VTPracticalAssessment by Name
        /// </summary>
        /// <param name="vtPracticalAssessmentModel"></param>
        /// <returns></returns>
        bool CheckVTPracticalAssessmentExistByName(VTPracticalAssessmentModel vtPracticalAssessmentModel);

        /// <summary>
        /// List of VTPracticalAssessment with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTPracticalAssessmentViewModel> GetVTPracticalAssessmentsByCriteria(SearchVTPracticalAssessmentModel searchModel);
    }
}
