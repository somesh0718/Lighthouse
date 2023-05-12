using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Employer entity
    /// </summary>
    public interface IEmployerManager : IGenericManager<EmployerModel>
    {
        /// <summary>
        /// Get list of Employers
        /// </summary>
        /// <returns></returns>
        IQueryable<EmployerModel> GetEmployers();

        /// <summary>
        /// Get list of Employers by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<EmployerModel> GetEmployersByName(string employerName);

        /// <summary>
        /// Get Employer by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        EmployerModel GetEmployerById(Guid employerId);

        /// <summary>
        /// Get Employer by EmployerId using async
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task<EmployerModel> GetEmployerByIdAsync(Guid employerId);

        /// <summary>
        /// Insert/Update Employer entity
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateEmployerDetails(EmployerModel employerModel);

        /// <summary>
        /// Delete a record by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        bool DeleteById(Guid employerId);

        /// <summary>
        /// Check duplicate Employer by Name
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        bool CheckEmployerExistByName(EmployerModel employerModel);

        /// <summary>
        /// List of Employer with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<EmployerViewModel> GetEmployersByCriteria(SearchEmployerModel searchModel);
    }
}
