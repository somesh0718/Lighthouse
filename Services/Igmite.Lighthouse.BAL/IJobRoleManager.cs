using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the JobRole entity
    /// </summary>
    public interface IJobRoleManager : IGenericManager<JobRoleModel>
    {
        /// <summary>
        /// Get list of JobRoles
        /// </summary>
        /// <returns></returns>
        IQueryable<JobRoleModel> GetJobRoles();

        /// <summary>
        /// Get list of JobRoles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<JobRoleModel> GetJobRolesByName(string jobRoleName);

        /// <summary>
        /// Get JobRole by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        JobRoleModel GetJobRoleById(Guid jobRoleId);

        /// <summary>
        /// Get JobRole by JobRoleId using async
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        Task<JobRoleModel> GetJobRoleByIdAsync(Guid jobRoleId);

        /// <summary>
        /// Insert/Update JobRole entity
        /// </summary>
        /// <param name="jobRoleModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateJobRoleDetails(JobRoleModel jobRoleModel);

        /// <summary>
        /// Delete a record by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        bool DeleteById(Guid jobRoleId);

        /// <summary>
        /// Check duplicate JobRole by Name
        /// </summary>
        /// <param name="jobRoleModel"></param>
        /// <returns></returns>
        List<string> CheckJobRoleExistByName(JobRoleModel jobRoleModel);

        /// <summary>
        /// List of JobRole with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<JobRoleViewModel> GetJobRolesByCriteria(SearchJobRoleModel searchModel);
    }
}
