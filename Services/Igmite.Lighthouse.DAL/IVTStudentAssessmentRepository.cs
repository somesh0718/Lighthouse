using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTStudentAssessment entity
    /// </summary>
    public interface IVTStudentAssessmentRepository : IGenericRepository<VTStudentAssessment>
    {
        /// <summary>
        /// Get list of VTStudentAssessment
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentAssessment> GetVTStudentAssessments();

        /// <summary>
        /// Get list of VTStudentAssessment by vtStudentAssessmentName
        /// </summary>
        /// <param name="vtStudentAssessmentName"></param>
        /// <returns></returns>
        IQueryable<VTStudentAssessment> GetVTStudentAssessmentsByName(string vtStudentAssessmentName);

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        VTStudentAssessment GetVTStudentAssessmentById(Guid vtStudentAssessmentId);

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId using async
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        Task<VTStudentAssessment> GetVTStudentAssessmentByIdAsync(Guid vtStudentAssessmentId);

        /// <summary>
        /// Insert/Update VTStudentAssessment entity
        /// </summary>
        /// <param name="vtStudentAssessment"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTStudentAssessmentDetails(VTStudentAssessment vtStudentAssessment);

        /// <summary>
        /// Delete a record by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtStudentAssessmentId);

        /// <summary>
        /// Check duplicate VTStudentAssessment by Name
        /// </summary>
        /// <param name="vtStudentAssessmentModel"></param>
        /// <returns></returns>
        bool CheckVTStudentAssessmentExistByName(VTStudentAssessmentModel vtStudentAssessmentModel);

        /// <summary>
        /// List of VTStudentAssessment with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTStudentAssessmentViewModel> GetVTStudentAssessmentsByCriteria(SearchVTStudentAssessmentModel searchModel);
    }
}
