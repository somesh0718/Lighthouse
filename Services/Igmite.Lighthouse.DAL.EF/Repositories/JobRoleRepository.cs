using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the JobRole entity
    /// </summary>
    public class JobRoleRepository : GenericRepository<JobRole>, IJobRoleRepository
    {
        /// <summary>
        /// Get list of JobRole
        /// </summary>
        /// <returns></returns>
        public IQueryable<JobRole> GetJobRoles()
        {
            return this.Context.JobRoles.AsQueryable();
        }

        /// <summary>
        /// Get list of JobRole by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<JobRole> GetJobRolesByName(string name)
        {
            var jobRoles = (from j in this.Context.JobRoles
                            where j.JobRoleName.Contains(name)
                            select j).AsQueryable();

            return jobRoles;
        }

        /// <summary>
        /// Get JobRole by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        public JobRole GetJobRoleById(Guid jobRoleId)
        {
            return this.Context.JobRoles.FirstOrDefault(j => j.JobRoleId == jobRoleId);
        }

        /// <summary>
        /// Get JobRole by JobRoleId using async
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        public async Task<JobRole> GetJobRoleByIdAsync(Guid jobRoleId)
        {
            var jobRole = await (from j in this.Context.JobRoles
                                 where j.JobRoleId == jobRoleId
                                 select j).FirstOrDefaultAsync();

            return jobRole;
        }

        /// <summary>
        /// Insert/Update JobRole entity
        /// </summary>
        /// <param name="jobRole"></param>
        /// <returns></returns>
        public bool SaveOrUpdateJobRoleDetails(JobRole jobRole)
        {
            if (RequestType.New == jobRole.RequestType)
                Context.JobRoles.Add(jobRole);
            else
            {
                Context.Entry<JobRole>(jobRole).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid jobRoleId)
        {
            JobRole jobRole = this.Context.JobRoles.FirstOrDefault(j => j.JobRoleId == jobRoleId);

            if (jobRole != null)
            {
                Context.Entry<JobRole>(jobRole).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate JobRole by Name
        /// </summary>
        /// <param name="jobRoleModel"></param>
        /// <returns></returns>
        public List<string> CheckJobRoleExistByName(JobRoleModel jobRoleModel)
        {
            var errorMessageList = new List<string>();

            JobRole jobRoleName = this.Context.JobRoles.FirstOrDefault(j => j.JobRoleName == jobRoleModel.JobRoleName);
            JobRole jobRoleQPCode = this.Context.JobRoles.FirstOrDefault(j => j.QPCode == jobRoleModel.QPCode);

            if (jobRoleModel.RequestType == RequestType.New)
            {
                if (jobRoleName != null)
                    errorMessageList.Add("Sector and Job Role are already exists");

                if (jobRoleQPCode != null)
                    errorMessageList.Add("QPCode is already exists");
            }

            if (jobRoleModel.RequestType == RequestType.Edit)
            {
                JobRole jobRole = this.Context.JobRoles.FirstOrDefault(j => j.JobRoleId == jobRoleModel.JobRoleId);

                if (jobRole != null)
                {
                    if (jobRoleName != null && !string.Equals(jobRole.JobRoleName, jobRoleName.JobRoleName))
                        errorMessageList.Add("Sector and Job Role are already exists");

                    if (jobRoleQPCode != null && !string.Equals(jobRole.QPCode, jobRoleQPCode.QPCode))
                        errorMessageList.Add("QPCode is already exists");
                }
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of JobRole with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<JobRoleViewModel> GetJobRolesByCriteria(SearchJobRoleModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.JobRoleViewModels.FromSql<JobRoleViewModel>("CALL GetJobRolesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}