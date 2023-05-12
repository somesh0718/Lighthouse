using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTGuestLectureConducted entity
    /// </summary>
    public interface IVTGuestLectureConductedRepository : IGenericRepository<VTGuestLectureConducted>
    {
        /// <summary>
        /// Get list of VTGuestLectureConducted
        /// </summary>
        /// <returns></returns>
        IQueryable<VTGuestLectureConducted> GetVTGuestLectureConducteds();

        /// <summary>
        /// Get list of VTGuestLectureConducted by vtGuestLectureConductedName
        /// </summary>
        /// <param name="vtGuestLectureConductedName"></param>
        /// <returns></returns>
        IQueryable<VTGuestLectureConducted> GetVTGuestLectureConductedsByName(string vtGuestLectureConductedName);

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        VTGuestLectureConducted GetVTGuestLectureConductedById(Guid vtGuestLectureId);

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId using async
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        Task<VTGuestLectureConducted> GetVTGuestLectureConductedByIdAsync(Guid vtGuestLectureId);

        /// <summary>
        /// Insert/Update VTGuestLectureConducted entity
        /// </summary>
        /// <param name="guestLecture"></param>
        /// <param name="guestLectureModel"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTGuestLectureConductedDetails(VTGuestLectureConducted guestLecture, VTGuestLectureConductedModel guestLectureModel);

        /// <summary>
        /// Delete a record by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtGuestLectureId);

        /// <summary>
        /// Check duplicate VTGuestLectureConducted by Name
        /// </summary>
        /// <param name="vtGuestLectureConductedModel"></param>
        /// <returns></returns>
        List<string> CheckVTGuestLectureConductedExistByName(VTGuestLectureConductedModel vtGuestLectureConductedModel);

        /// <summary>
        /// List of VTGuestLectureConducted with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTGuestLectureConductedViewModel> GetVTGuestLectureConductedsByCriteria(SearchVTGuestLectureConductedModel searchModel);

        /// <summary>
        /// Get list of Sections by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        IList<Guid> GetVTGSectionsByGuestLectureId(Guid guestLectureId);

        /// <summary>
        /// Get list of Methodologies by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        IList<string> GetVTGMethodologiesByGuestLectureId(Guid guestLectureId);

        /// <summary>
        /// Get list of UnitSessionsTaughts by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        IList<UnitSessionsModel> GetVTFUnitSessionsTaughtsByGuestLectureId(Guid guestLectureId);

        /// <summary>
        /// Get list of Students by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        IList<StudentAttendanceModel> GetVTFStudentsByGuestLectureId(Guid guestLectureId);
    }
}