using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SchoolVEIncharge entity
    /// </summary>
    public interface ISchoolVEInchargeRepository : IGenericRepository<SchoolVEIncharge>
    {
        /// <summary>
        /// Get list of SchoolVEIncharge
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolVEIncharge> GetSchoolVEIncharges();

        /// <summary>
        /// Get list of SchoolVEIncharge by schoolVEInchargeName
        /// </summary>
        /// <param name="schoolVEInchargeName"></param>
        /// <returns></returns>
        IQueryable<SchoolVEIncharge> GetSchoolVEInchargesByName(string schoolVEInchargeName);

        /// <summary>
        /// Get SchoolVEIncharge by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        SchoolVEIncharge GetSchoolVEInchargeById(Guid veiId);

        /// <summary>
        /// Get SchoolVEIncharge by VEIId using async
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        Task<SchoolVEIncharge> GetSchoolVEInchargeByIdAsync(Guid veiId);

        /// <summary>
        /// Insert/Update SchoolVEIncharge entity
        /// </summary>
        /// <param name="schoolVEIncharge"></param>
        /// <returns></returns>
        bool SaveOrUpdateSchoolVEInchargeDetails(SchoolVEIncharge schoolVEIncharge);

        /// <summary>
        /// Delete a record by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        bool DeleteById(Guid veiId);

        /// <summary>
        /// Check duplicate SchoolVEIncharge by Name
        /// </summary>
        /// <param name="schoolVEInchargeModel"></param>
        /// <returns></returns>
        string CheckSchoolVEInchargeExistByName(SchoolVEInchargeModel schoolVEInchargeModel);

        /// <summary>
        /// List of SchoolVEIncharge with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolVEInchargeViewModel> GetSchoolVEInchargesByCriteria(SearchSchoolVEInchargeModel searchModel);
    }
}
