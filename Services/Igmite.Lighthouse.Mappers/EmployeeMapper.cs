using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeModel ToModel(this Employee employee)
        {
            if (employee == null)
                return null;

            EmployeeModel employeeModel = new EmployeeModel
            {
                AccountId = employee.AccountId,
                EmployeeCode = employee.EmployeeCode,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                Department = employee.Department,
                Telephone = employee.Telephone,
                Mobile = employee.Mobile,
                EmailId = employee.EmailId,
                CreatedBy = employee.CreatedBy,
                CreatedOn = employee.CreatedOn,
                UpdatedBy = employee.UpdatedBy,
                UpdatedOn = employee.UpdatedOn,
                IsActive = employee.IsActive
            };

            return employeeModel;
        }
        public static Employee FromModel(this EmployeeModel employeeModel, Employee employee)
        {
            employee.AccountId = employeeModel.AccountId;
            employee.EmployeeCode = employeeModel.EmployeeCode;
            employee.FirstName = employeeModel.FirstName;
            employee.MiddleName = employeeModel.MiddleName;
            employee.LastName = employeeModel.LastName;
            employee.Gender = employeeModel.Gender;
            employee.DateOfBirth = employeeModel.DateOfBirth;
            employee.Department = employeeModel.Department;
            employee.Telephone = employeeModel.Telephone;
            employee.Mobile = employeeModel.Mobile;
            employee.EmailId = employeeModel.EmailId;
            employee.IsActive = employeeModel.IsActive;
            employee.RequestType = employeeModel.RequestType;
            employee.SetAuditValues(employeeModel.RequestType);

            return employee;
        }
    }
}
