using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTPracticalAssessment entity
    /// </summary>
    public interface IVTPracticalAssessmentManager : IGenericManager<VTPracticalAssessmentModel>
    {
        /// <summary>
        /// Get list of VTPracticalAssessments
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPracticalAssessmentModel> GetVTPracticalAssessments();

        /// <summary>
        /// Get list of VTPracticalAssessments by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTPracticalAssessmentModel> GetVTPracticalAssessmentsByName(string vtPracticalAssessmentName);

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        VTPracticalAssessmentModel GetVTPracticalAssessmentById(Guid vtPracticalAssessmentId);

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId using async
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        Task<VTPracticalAssessmentModel> GetVTPracticalAssessmentByIdAsync(Guid vtPracticalAssessmentId);

        /// <summary>
        /// Insert/Update VTPracticalAssessment entity
        /// </summary>
        /// <param name="vtPracticalAssessmentModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTPracticalAssessmentDetails(VTPracticalAssessmentModel vtPracticalAssessmentModel);

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
