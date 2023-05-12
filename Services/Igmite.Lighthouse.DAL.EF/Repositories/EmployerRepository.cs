using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Employer entity
    /// </summary>
    public class EmployerRepository : GenericRepository<Employer>, IEmployerRepository
    {
        /// <summary>
        /// Get list of Employer
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employer> GetEmployers()
        {
            return this.Context.Employers.AsQueryable();
        }

        /// <summary>
        /// Get list of Employer by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Employer> GetEmployersByName(string name)
        {
            var employers = (from e in this.Context.Employers
                             where e.BlockName.Contains(name)
                             select e).AsQueryable();

            return employers;
        }

        /// <summary>
        /// Get Employer by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public Employer GetEmployerById(Guid employerId)
        {
            return this.Context.Employers.FirstOrDefault(e => e.EmployerId == employerId);
        }

        /// <summary>
        /// Get Employer by EmployerId using async
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task<Employer> GetEmployerByIdAsync(Guid employerId)
        {
            var employer = await (from e in this.Context.Employers
                                  where e.EmployerId == employerId
                                  select e).FirstOrDefaultAsync();

            return employer;
        }

        /// <summary>
        /// Insert/Update Employer entity
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        public bool SaveOrUpdateEmployerDetails(Employer employer)
        {
            if (RequestType.New == employer.RequestType)
                Context.Employers.Add(employer);
            else
            {
                Context.Entry<Employer>(employer).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid employerId)
        {
            Employer employer = this.Context.Employers.FirstOrDefault(e => e.EmployerId == employerId);

            if (employer != null)
            {
                Context.Entry<Employer>(employer).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Employer by Name
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        public bool CheckEmployerExistByName(EmployerModel employerModel)
        {
            Employer employer = this.Context.Employers.FirstOrDefault(e => e.StateCode == employerModel.StateCode && e.DivisionId == employerModel.DivisionId && e.DistrictCode == employerModel.DistrictCode && e.BusinessType == employerModel.BusinessType);

            return employer != null;
        }

        /// <summary>}
        /// List of Employer with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<EmployerViewModel> GetEmployersByCriteria(SearchEmployerModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.EmployerViewModels.FromSql<EmployerViewModel>("CALL GetEmployersByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}