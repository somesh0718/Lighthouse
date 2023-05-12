using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Employee entity
    /// </summary>
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        /// <summary>
        /// Get list of Employee
        /// </summary>
        /// <returns></returns>
        IQueryable<Employee> GetEmployees();

        /// <summary>
        /// Get list of Employee by employeeName
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        IQueryable<Employee> GetEmployeesByName(string employeeName);

        /// <summary>
        /// Get Employee by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Employee GetEmployeeById(Guid accountId);

        /// <summary>
        /// Get Employee by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByIdAsync(Guid accountId);

        /// <summary>
        /// Insert/Update Employee entity
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        bool SaveOrUpdateEmployeeDetails(Employee employee);

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
