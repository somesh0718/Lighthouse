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
    /// Expose all employer WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class EmployerController : BaseController
    {
        private readonly IEmployerManager employerManager;

        /// <summary>
        /// Initializes the Employer controller class.
        /// </summary>
        /// <param name="_employerManager"></param>
        public EmployerController(IEmployerManager _employerManager)
        {
            this.employerManager = _employerManager;
        }

        /// <summary>
        /// Get list of employer data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetEmployers")]
        public async Task<ListResponse<EmployerModel>> GetEmployers()
        {
            ListResponse<EmployerModel> response = new ListResponse<EmployerModel>();

            try
            {
                IQueryable<EmployerModel> employerModels = await Task.Run(() =>
                {
                    return this.employerManager.GetEmployers();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = employerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Employer with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetEmployersByCriteria")]
        public async Task<ListResponse<EmployerViewModel>> GetEmployersByCriteria([FromBody] SearchEmployerModel searchModel)
        {
            ListResponse<EmployerViewModel> response = new ListResponse<EmployerViewModel>();

            try
            {
                var employerModels = await Task.Run(() =>
                {
                    return this.employerManager.GetEmployersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = employerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of employer data by name
        /// </summary>
        /// <param name="employerName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetEmployersByName")]
        public async Task<ListResponse<EmployerModel>> GetEmployersByName([FromQuery] string employerName)
        {
            ListResponse<EmployerModel> response = new ListResponse<EmployerModel>();

            try
            {
                var employerModels = await Task.Run(() =>
                {
                    return this.employerManager.GetEmployersByName(employerName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = employerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get employer data by Id
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetEmployerById")]
        public async Task<SingularResponse<EmployerModel>> GetEmployerById([FromBody] DataRequest employerRequest)
        {
            SingularResponse<EmployerModel> response = new SingularResponse<EmployerModel>();

            try
            {
                var employerModel = await Task.Run(() =>
                {
                    return this.employerManager.GetEmployerById(Guid.Parse(employerRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = employerModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new employer
        /// </summary>
        /// <param name="employerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateEmployer"), Route("CreateOrUpdateEmployerDetails")]
        public async Task<SingularResponse<string>> CreateEmployer([FromBody] EmployerRequest employerRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //employerRequest.RequestType = RequestType.New;
                    return this.employerManager.SaveOrUpdateEmployerDetails(employerRequest);
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
        /// Update employer by Id
        /// </summary>
        /// <param name="employerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateEmployer")]
        public async Task<SingularResponse<string>> UpdateEmployer([FromBody] EmployerRequest employerRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    employerRequest.RequestType = RequestType.Edit;
                    return this.employerManager.SaveOrUpdateEmployerDetails(employerRequest);
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
        /// Delete employer by Id
        /// </summary>
        /// <param name="employerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteEmployerById")]
        public async Task<SingularResponse<bool>> DeleteEmployerById([FromBody] DeleteRequest<string> employerRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.employerManager.DeleteById(Guid.Parse(employerRequest.DataId));
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