using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the School entity
    /// </summary>
    public interface ISchoolRepository : IGenericRepository<School>
    {
        /// <summary>
        /// Get list of School
        /// </summary>
        /// <returns></returns>
        IQueryable<School> GetSchools();

        /// <summary>
        /// Get list of School by schoolName
        /// </summary>
        /// <param name="schoolName"></param>
        /// <returns></returns>
        IQueryable<School> GetSchoolsByName(string schoolName);

        /// <summary>
        /// Get list of School by schoolNames
        /// </summary>
        /// <param name="schoolNames"></param>
        /// <returns></returns>
        IQueryable<School> GetSchoolsByNames(List<string> schoolNames);

        /// <summary>
        /// Get list of School by UDISE Codes
        /// </summary>
        /// <param name="udiseCodes"></param>
        /// <returns></returns>
        IQueryable<School> GetSchoolsByUDISECodes(List<string> udiseCodes);

        /// <summary>
        /// Get School by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        School GetSchoolById(Guid schoolId);

        /// <summary>
        /// Get School by SchoolId using async
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        Task<School> GetSchoolByIdAsync(Guid schoolId);

        /// <summary>
        /// Insert/Update School entity
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        bool SaveOrUpdateSchoolDetails(School school);

        /// <summary>
        /// Delete a record by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        bool DeleteById(Guid schoolId);

        /// <summary>
        /// Check duplicate School by Name
        /// </summary>
        /// <param name="schoolModel"></param>
        /// <returns></returns>
        bool CheckSchoolExistByName(SchoolModel schoolModel);

        /// <summary>
        /// Check school can be inactive
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        bool CheckUserCanInactiveSchoolById(Guid schoolId);

        /// <summary>
        /// List of School with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolViewModel> GetSchoolsByCriteria(SearchSchoolModel searchModel);
    }
}