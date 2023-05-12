using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTStudentAssessment entity
    /// </summary>
    public interface IVTStudentAssessmentManager : IGenericManager<VTStudentAssessmentModel>
    {
        /// <summary>
        /// Get list of VTStudentAssessments
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentAssessmentModel> GetVTStudentAssessments();

        /// <summary>
        /// Get list of VTStudentAssessments by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTStudentAssessmentModel> GetVTStudentAssessmentsByName(string vtStudentAssessmentName);

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        VTStudentAssessmentModel GetVTStudentAssessmentById(Guid vtStudentAssessmentId);

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId using async
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        Task<VTStudentAssessmentModel> GetVTStudentAssessmentByIdAsync(Guid vtStudentAssessmentId);

        /// <summary>
        /// Insert/Update VTStudentAssessment entity
        /// </summary>
        /// <param name="vtStudentAssessmentModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTStudentAssessmentDetails(VTStudentAssessmentModel vtStudentAssessmentModel);

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
