using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SchoolVEIncharge entity
    /// </summary>
    public interface ISchoolVEInchargeManager : IGenericManager<SchoolVEInchargeModel>
    {
        /// <summary>
        /// Get list of SchoolVEIncharges
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolVEInchargeModel> GetSchoolVEIncharges();

        /// <summary>
        /// Get list of SchoolVEIncharges by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SchoolVEInchargeModel> GetSchoolVEInchargesByName(string schoolVEInchargeName);

        /// <summary>
        /// Get SchoolVEIncharge by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        SchoolVEInchargeModel GetSchoolVEInchargeById(Guid veiId);

        /// <summary>
        /// Get SchoolVEIncharge by VEIId using async
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        Task<SchoolVEInchargeModel> GetSchoolVEInchargeByIdAsync(Guid veiId);

        /// <summary>
        /// Insert/Update SchoolVEIncharge entity
        /// </summary>
        /// <param name="schoolVEInchargeModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSchoolVEInchargeDetails(SchoolVEInchargeModel schoolVEInchargeModel);

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
