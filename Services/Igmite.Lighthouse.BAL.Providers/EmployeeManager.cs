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
    /// Manager of the Employee entity
    /// </summary>
    public class EmployeeManager : GenericManager<EmployeeModel>, IEmployeeManager
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the employee manager.
        /// </summary>
        /// <param name="employeeRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public EmployeeManager(IEmployeeRepository _employeeRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.employeeRepository = _employeeRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Employees
        /// </summary>
        /// <returns></returns>
        public IQueryable<EmployeeModel> GetEmployees()
        {
            var employees = this.employeeRepository.GetEmployees();

            IList<EmployeeModel> employeeModels = new List<EmployeeModel>();
            employees.ForEach((user) => employeeModels.Add(user.ToModel()));

            return employeeModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Employees by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<EmployeeModel> GetEmployeesByName(string employeeName)
        {
            var employees = this.employeeRepository.GetEmployeesByName(employeeName);

            IList<EmployeeModel> employeeModels = new List<EmployeeModel>();
            employees.ForEach((user) => employeeModels.Add(user.ToModel()));

            return employeeModels.AsQueryable();
        }

        /// <summary>
        /// Get Employee by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public EmployeeModel GetEmployeeById(Guid accountId)
        {
            Employee employee = this.employeeRepository.GetEmployeeById(accountId);

            return (employee != null) ? employee.ToModel() : null;
        }

        /// <summary>
        /// Get Employee by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<EmployeeModel> GetEmployeeByIdAsync(Guid accountId)
        {
            var employee = await this.employeeRepository.GetEmployeeByIdAsync(accountId);

            return (employee != null) ? employee.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Employee entity
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateEmployeeDetails(EmployeeModel employeeModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Employee employee = null;

            //Validate model data
            employeeModel = employeeModel.GetModelValidationErrors<EmployeeModel>();

            if (employeeModel.ErrorMessages.Count > 0)
            {
                response.Errors = employeeModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (employeeModel.RequestType == RequestType.Edit)
            {
                employee = this.employeeRepository.GetEmployeeById(employeeModel.AccountId);
            }
            else
            {
                employee = new Employee();
                employeeModel.AccountId = Guid.NewGuid();
            }

            if (employeeModel.ErrorMessages.Count == 0 && (employeeModel.FirstName.StringVal().ToLower() != employee.FirstName.StringVal().ToLower()))
            {
                bool isEmployeeExists = this.employeeRepository.CheckEmployeeExistByName(employeeModel);

                if (isEmployeeExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                employee.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                employee = employeeModel.FromModel(employee);

                //Save Or Update employee details
                bool isSaved = this.employeeRepository.SaveOrUpdateEmployeeDetails(employee);

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
        /// Delete a record by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid accountId)
        {
            return this.employeeRepository.DeleteById(accountId);
        }

        /// <summary>
        /// Check duplicate Employee by Name
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public bool CheckEmployeeExistByName(EmployeeModel employeeModel)
        {
            return this.employeeRepository.CheckEmployeeExistByName(employeeModel);
        }

        /// <summary>}
        /// List of Employee with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<EmployeeViewModel> GetEmployeesByCriteria(SearchEmployeeModel searchModel)
        {
            return this.employeeRepository.GetEmployeesByCriteria(searchModel);
        }
    }
}