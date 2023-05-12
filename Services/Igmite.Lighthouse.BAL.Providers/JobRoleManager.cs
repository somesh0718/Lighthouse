using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the JobRole entity
    /// </summary>
    public class JobRoleManager : GenericManager<JobRoleModel>, IJobRoleManager
    {
        private readonly IJobRoleRepository jobRoleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the jobRole manager.
        /// </summary>
        /// <param name="jobRoleRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public JobRoleManager(IJobRoleRepository _jobRoleRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.jobRoleRepository = _jobRoleRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of JobRoles
        /// </summary>
        /// <returns></returns>
        public IQueryable<JobRoleModel> GetJobRoles()
        {
            var jobRoles = this.jobRoleRepository.GetJobRoles();

            IList<JobRoleModel> jobRoleModels = new List<JobRoleModel>();
            jobRoles.ForEach((user) => jobRoleModels.Add(user.ToModel()));

            return jobRoleModels.AsQueryable();
        }

        /// <summary>
        /// Get list of JobRoles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<JobRoleModel> GetJobRolesByName(string jobRoleName)
        {
            var jobRoles = this.jobRoleRepository.GetJobRolesByName(jobRoleName);

            IList<JobRoleModel> jobRoleModels = new List<JobRoleModel>();
            jobRoles.ForEach((user) => jobRoleModels.Add(user.ToModel()));

            return jobRoleModels.AsQueryable();
        }

        /// <summary>
        /// Get JobRole by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        public JobRoleModel GetJobRoleById(Guid jobRoleId)
        {
            JobRole jobRole = this.jobRoleRepository.GetJobRoleById(jobRoleId);

            return (jobRole != null) ? jobRole.ToModel() : null;
        }

        /// <summary>
        /// Get JobRole by JobRoleId using async
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        public async Task<JobRoleModel> GetJobRoleByIdAsync(Guid jobRoleId)
        {
            var jobRole = await this.jobRoleRepository.GetJobRoleByIdAsync(jobRoleId);

            return (jobRole != null) ? jobRole.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update JobRole entity
        /// </summary>
        /// <param name="jobRoleModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateJobRoleDetails(JobRoleModel jobRoleModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            JobRole jobRole = null;

            //Validate model data
            jobRoleModel = jobRoleModel.GetModelValidationErrors<JobRoleModel>();

            if (jobRoleModel.ErrorMessages.Count > 0)
            {
                response.Errors = jobRoleModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (jobRoleModel.RequestType == RequestType.Edit)
            {
                jobRole = this.jobRoleRepository.GetJobRoleById(jobRoleModel.JobRoleId);
            }
            else
            {
                jobRole = new JobRole();
                jobRoleModel.JobRoleId = Guid.NewGuid();
            }

            if (jobRoleModel.ErrorMessages.Count == 0)
            {
                List<string> validationMessages = this.jobRoleRepository.CheckJobRoleExistByName(jobRoleModel);

                if (validationMessages.Count > 0)
                {
                    response.Errors.Add(string.Join(",", validationMessages));
                }
            }

            if (response.Errors.Count == 0)
            {
                jobRole.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                jobRole = jobRoleModel.FromModel(jobRole);

                //Save Or Update jobRole details
                bool isSaved = this.jobRoleRepository.SaveOrUpdateJobRoleDetails(jobRole);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by JobRoleId
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid jobRoleId)
        {
            return this.jobRoleRepository.DeleteById(jobRoleId);
        }

        /// <summary>
        /// Check duplicate JobRole by Name
        /// </summary>
        /// <param name="jobRoleModel"></param>
        /// <returns></returns>
        public List<string> CheckJobRoleExistByName(JobRoleModel jobRoleModel)
        {
            return this.jobRoleRepository.CheckJobRoleExistByName(jobRoleModel);
        }

        /// <summary>}
        /// List of JobRole with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<JobRoleViewModel> GetJobRolesByCriteria(SearchJobRoleModel searchModel)
        {
            return this.jobRoleRepository.GetJobRolesByCriteria(searchModel);
        }
    }
}