using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all jobRole WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class JobRoleController : BaseController
    {
        private readonly IJobRoleManager jobRoleManager;

        /// <summary>
        /// Initializes the JobRole controller class.
        /// </summary>
        /// <param name="_jobRoleManager"></param>
        public JobRoleController(IJobRoleManager _jobRoleManager)
        {
            this.jobRoleManager = _jobRoleManager;
        }

        /// <summary>
        /// Get list of jobRole data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetJobRoles")]
        public async Task<ListResponse<JobRoleModel>> GetJobRoles()
        {
            ListResponse<JobRoleModel> response = new ListResponse<JobRoleModel>();

            try
            {
                IQueryable<JobRoleModel> jobRoleModels = await Task.Run(() =>
                {
                    return this.jobRoleManager.GetJobRoles();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = jobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of JobRole with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetJobRolesByCriteria")]
        public async Task<ListResponse<JobRoleViewModel>> GetJobRolesByCriteria([FromBody] SearchJobRoleModel searchModel)
        {
            ListResponse<JobRoleViewModel> response = new ListResponse<JobRoleViewModel>();

            try
            {
                var jobRoleModels = await Task.Run(() =>
                {
                    return this.jobRoleManager.GetJobRolesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = jobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of jobRole data by name
        /// </summary>
        /// <param name="jobRoleName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetJobRolesByName")]
        public async Task<ListResponse<JobRoleModel>> GetJobRolesByName([FromQuery] string jobRoleName)
        {
            ListResponse<JobRoleModel> response = new ListResponse<JobRoleModel>();

            try
            {
                var jobRoleModels = await Task.Run(() =>
                {
                    return this.jobRoleManager.GetJobRolesByName(jobRoleName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = jobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get jobRole data by Id
        /// </summary>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetJobRoleById")]
        public async Task<SingularResponse<JobRoleModel>> GetJobRoleById([FromBody] DataRequest jobRoleRequest)
        {
            SingularResponse<JobRoleModel> response = new SingularResponse<JobRoleModel>();

            try
            {
                var jobRoleModel = await Task.Run(() =>
                {
                    return this.jobRoleManager.GetJobRoleById(Guid.Parse(jobRoleRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = jobRoleModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new jobRole
        /// </summary>
        /// <param name="jobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateJobRole"), Route("CreateOrUpdateJobRoleDetails")]
        public async Task<SingularResponse<string>> CreateJobRole([FromBody] JobRoleRequest jobRoleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //jobRoleRequest.RequestType = RequestType.New;
                    return this.jobRoleManager.SaveOrUpdateJobRoleDetails(jobRoleRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update jobRole by Id
        /// </summary>
        /// <param name="jobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateJobRole")]
        public async Task<SingularResponse<string>> UpdateJobRole([FromBody] JobRoleRequest jobRoleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    jobRoleRequest.RequestType = RequestType.Edit;
                    return this.jobRoleManager.SaveOrUpdateJobRoleDetails(jobRoleRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete jobRole by Id
        /// </summary>
        /// <param name="jobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteJobRoleById")]
        public async Task<SingularResponse<bool>> DeleteJobRoleById([FromBody] DeleteRequest<Guid> jobRoleRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.jobRoleManager.DeleteById(jobRoleRequest.DataId);
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}