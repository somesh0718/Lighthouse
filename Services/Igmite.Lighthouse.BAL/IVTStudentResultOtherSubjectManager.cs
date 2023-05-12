using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTStudentResultOtherSubject entity
    /// </summary>
    public interface IVTStudentResultOtherSubjectManager : IGenericManager<VTStudentResultOtherSubjectModel>
    {
        /// <summary>
        /// Get list of VTStudentResultOtherSubjects
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentResultOtherSubjectModel> GetVTStudentResultOtherSubjects();

        /// <summary>
        /// Get list of VTStudentResultOtherSubjects by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTStudentResultOtherSubjectModel> GetVTStudentResultOtherSubjectsByName(string vtStudentResultOtherSubjectName);

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        VTStudentResultOtherSubjectModel GetVTStudentResultOtherSubjectById(Guid vtStudentResultOtherSubjectId);

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId using async
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        Task<VTStudentResultOtherSubjectModel> GetVTStudentResultOtherSubjectByIdAsync(Guid vtStudentResultOtherSubjectId);

        /// <summary>
        /// Insert/Update VTStudentResultOtherSubject entity
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTStudentResultOtherSubjectDetails(VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel);

        /// <summary>
        /// Delete a record by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtStudentResultOtherSubjectId);

        /// <summary>
        /// Check duplicate VTStudentResultOtherSubject by Name
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectModel"></param>
        /// <returns></returns>
        bool CheckVTStudentResultOtherSubjectExistByName(VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel);

        /// <summary>
        /// List of VTStudentResultOtherSubject with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTStudentResultOtherSubjectViewModel> GetVTStudentResultOtherSubjectsByCriteria(SearchVTStudentResultOtherSubjectModel searchModel);
    }
}
