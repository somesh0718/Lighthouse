using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the School entity
    /// </summary>
    public interface ISchoolManager : IGenericManager<SchoolModel>
    {
        /// <summary>
        /// Get list of Schools
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolModel> GetSchools();

        /// <summary>
        /// Get list of Schools by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SchoolModel> GetSchoolsByName(string schoolName);

        /// <summary>
        /// Get list of School by schoolNames
        /// </summary>
        /// <param name="schoolNames"></param>
        /// <returns></returns>
        IQueryable<SchoolModel> GetSchoolsByNames(List<string> schoolNames);

        /// <summary>
        /// Get list of School by UDISE Codes
        /// </summary>
        /// <param name="udiseCodes"></param>
        /// <returns></returns>
        IQueryable<SchoolModel> GetSchoolsByUDISECodes(List<string> udiseCodes);

        /// <summary>
        /// Get School by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        SchoolModel GetSchoolById(Guid schoolId);

        /// <summary>
        /// Get School by SchoolId using async
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        Task<SchoolModel> GetSchoolByIdAsync(Guid schoolId);

        /// <summary>
        /// Insert/Update School entity
        /// </summary>
        /// <param name="schoolModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSchoolDetails(SchoolModel schoolModel);

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
        /// List of School with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolViewModel> GetSchoolsByCriteria(SearchSchoolModel searchModel);
    }
}