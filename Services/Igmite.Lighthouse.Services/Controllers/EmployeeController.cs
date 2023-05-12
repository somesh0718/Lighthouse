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
    /// Expose all employee WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeManager employeeManager;

        /// <summary>
        /// Initializes the Employee controller class.
        /// </summary>
        /// <param name="_employeeManager"></param>
        public EmployeeController(IEmployeeManager _employeeManager)
        {
            this.employeeManager = _employeeManager;
        }

        /// <summary>
        /// Get list of employee data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetEmployees")]
        public async Task<ListResponse<EmployeeModel>> GetEmployees()
        {
            ListResponse<EmployeeModel> response = new ListResponse<EmployeeModel>();

            try
            {
                IQueryable<EmployeeModel> employeeModels = await Task.Run(() =>
                {
                    return this.employeeManager.GetEmployees();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = employeeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Employee with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetEmployeesByCriteria")]
        public async Task<ListResponse<EmployeeViewModel>> GetEmployeesByCriteria([FromBody] SearchEmployeeModel searchModel)
        {
            ListResponse<EmployeeViewModel> response = new ListResponse<EmployeeViewModel>();

            try
            {
                var employeeModels = await Task.Run(() =>
                {
                    return this.employeeManager.GetEmployeesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = employeeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of employee data by name
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetEmployeesByName")]
        public async Task<ListResponse<EmployeeModel>> GetEmployeesByName([FromQuery] string employeeName)
        {
            ListResponse<EmployeeModel> response = new ListResponse<EmployeeModel>();

            try
            {
                var employeeModels = await Task.Run(() =>
                {
                    return this.employeeManager.GetEmployeesByName(employeeName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = employeeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get employee data by Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetEmployeeById")]
        public async Task<SingularResponse<EmployeeModel>> GetEmployeeById([FromBody] DataRequest employeeRequest)
        {
            SingularResponse<EmployeeModel> response = new SingularResponse<EmployeeModel>();

            try
            {
                var employeeModel = await Task.Run(() =>
                {
                    return this.employeeManager.GetEmployeeById(Guid.Parse(employeeRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = employeeModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateEmployee"), Route("CreateOrUpdateEmployeeDetails")]
        public async Task<SingularResponse<string>> CreateEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //employeeRequest.RequestType = RequestType.New;
                    return this.employeeManager.SaveOrUpdateEmployeeDetails(employeeRequest);
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
        /// Update employee by Id
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateEmployee")]
        public async Task<SingularResponse<string>> UpdateEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    employeeRequest.RequestType = RequestType.Edit;
                    return this.employeeManager.SaveOrUpdateEmployeeDetails(employeeRequest);
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
        /// Delete employee by Id
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteEmployeeById")]
        public async Task<SingularResponse<bool>> DeleteEmployeeById([FromBody] DeleteRequest<Guid> employeeRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.employeeManager.DeleteById(employeeRequest.DataId);
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