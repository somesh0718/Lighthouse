using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTStudentResultOtherSubject entity
    /// </summary>
    public interface IVTStudentResultOtherSubjectRepository : IGenericRepository<VTStudentResultOtherSubject>
    {
        /// <summary>
        /// Get list of VTStudentResultOtherSubject
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStudentResultOtherSubject> GetVTStudentResultOtherSubjects();

        /// <summary>
        /// Get list of VTStudentResultOtherSubject by vtStudentResultOtherSubjectName
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectName"></param>
        /// <returns></returns>
        IQueryable<VTStudentResultOtherSubject> GetVTStudentResultOtherSubjectsByName(string vtStudentResultOtherSubjectName);

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        VTStudentResultOtherSubject GetVTStudentResultOtherSubjectById(Guid vtStudentResultOtherSubjectId);

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId using async
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        Task<VTStudentResultOtherSubject> GetVTStudentResultOtherSubjectByIdAsync(Guid vtStudentResultOtherSubjectId);

        /// <summary>
        /// Insert/Update VTStudentResultOtherSubject entity
        /// </summary>
        /// <param name="vtStudentResultOtherSubject"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTStudentResultOtherSubjectDetails(VTStudentResultOtherSubject vtStudentResultOtherSubject);

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
