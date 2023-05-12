using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the JobRole entity
    /// </summary>
    public interface IJobRoleRepository : IGenericRepository<JobRole>
    {
        /// <summary>
        /// Get list of JobRole
        /// </summary>
        /// <returns></returns>
        IQueryable<JobRole> GetJobRoles();

        /// <summary>
        /// Get list of JobRole by jobRoleName
        /// </summary>
        /// <param name="jobRoleName"></param>
        /// <returns></returns>
        IQueryable<JobRole> GetJobRolesByName(string jobRoleName);

        /// <summary>
        /// Get JobRole by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        JobRole GetJobRoleById(Guid jobRoleId);

        /// <summary>
        /// Get JobRole by JobRoleId using async
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        Task<JobRole> GetJobRoleByIdAsync(Guid jobRoleId);

        /// <summary>
        /// Insert/Update JobRole entity
        /// </summary>
        /// <param name="jobRole"></param>
        /// <returns></returns>
        bool SaveOrUpdateJobRoleDetails(JobRole jobRole);

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