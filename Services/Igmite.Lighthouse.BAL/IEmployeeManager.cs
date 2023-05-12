using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Employee entity
    /// </summary>
    public interface IEmployeeManager : IGenericManager<EmployeeModel>
    {
        /// <summary>
        /// Get list of Employees
        /// </summary>
        /// <returns></returns>
        IQueryable<EmployeeModel> GetEmployees();

        /// <summary>
        /// Get list of Employees by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<EmployeeModel> GetEmployeesByName(string employeeName);

        /// <summary>
        /// Get Employee by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        EmployeeModel GetEmployeeById(Guid accountId);

        /// <summary>
        /// Get Employee by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<EmployeeModel> GetEmployeeByIdAsync(Guid accountId);

        /// <summary>
        /// Insert/Update Employee entity
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateEmployeeDetails(EmployeeModel employeeModel);

        /// <summary>
        /// Delete a record by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        bool DeleteById(Guid accountId);

        /// <summary>
        /// Check duplicate Employee by Name
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        bool CheckEmployeeExistByName(EmployeeModel employeeModel);

        /// <summary>
        /// List of Employee with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<EmployeeViewModel> GetEmployeesByCriteria(SearchEmployeeModel searchModel);
    }
}
