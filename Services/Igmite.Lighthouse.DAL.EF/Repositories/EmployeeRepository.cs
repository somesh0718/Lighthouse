using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Employee entity
    /// </summary>
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Get list of Employee
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> GetEmployees()
        {
            return this.Context.Employees.AsQueryable();
        }

        /// <summary>
        /// Get list of Employee by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Employee> GetEmployeesByName(string name)
        {
            var employees = (from e in this.Context.Employees
                         where e.FirstName.Contains(name)
                         select e).AsQueryable();

            return employees;
        }

        /// <summary>
        /// Get Employee by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Employee GetEmployeeById(Guid accountId)
        {
            return this.Context.Employees.FirstOrDefault(e => e.AccountId == accountId);
        }

        /// <summary>
        /// Get Employee by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByIdAsync(Guid accountId)
        {
            var employee = await (from e in this.Context.Employees
                              where e.AccountId == accountId
                              select e).FirstOrDefaultAsync();

            return employee;
        }

        /// <summary>
        /// Insert/Update Employee entity
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool SaveOrUpdateEmployeeDetails(Employee employee)
        {
            if (RequestType.New == employee.RequestType)
                Context.Employees.Add(employee);
            else
            {
                Context.Entry<Employee>(employee).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid accountId)
        {
            Employee employee = this.Context.Employees.FirstOrDefault(e => e.AccountId == accountId);

            if (employee != null)
            {
                Context.Entry<Employee>(employee).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Employee by Name
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public bool CheckEmployeeExistByName(EmployeeModel employeeModel)
        {
            Employee employee = this.Context.Employees.FirstOrDefault(e => e.FirstName == employeeModel.FirstName);

            return employee != null;
        }

        /// <summary>}
        /// List of Employee with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<EmployeeViewModel> GetEmployeesByCriteria(SearchEmployeeModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.EmployeeViewModels.FromSql<EmployeeViewModel>("CALL GetEmployeesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
