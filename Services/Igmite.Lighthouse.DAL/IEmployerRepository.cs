using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Employer entity
    /// </summary>
    public interface IEmployerRepository : IGenericRepository<Employer>
    {
        /// <summary>
        /// Get list of Employer
        /// </summary>
        /// <returns></returns>
        IQueryable<Employer> GetEmployers();

        /// <summary>
        /// Get list of Employer by employerName
        /// </summary>
        /// <param name="employerName"></param>
        /// <returns></returns>
        IQueryable<Employer> GetEmployersByName(string employerName);

        /// <summary>
        /// Get Employer by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Employer GetEmployerById(Guid employerId);

        /// <summary>
        /// Get Employer by EmployerId using async
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task<Employer> GetEmployerByIdAsync(Guid employerId);

        /// <summary>
        /// Insert/Update Employer entity
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        bool SaveOrUpdateEmployerDetails(Employer employer);

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