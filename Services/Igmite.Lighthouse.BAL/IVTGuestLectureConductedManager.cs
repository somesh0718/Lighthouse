using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTGuestLectureConducted entity
    /// </summary>
    public interface IVTGuestLectureConductedManager : IGenericManager<VTGuestLectureConductedModel>
    {
        /// <summary>
        /// Get list of VTGuestLectureConducteds
        /// </summary>
        /// <returns></returns>
        IQueryable<VTGuestLectureConductedModel> GetVTGuestLectureConducteds();

        /// <summary>
        /// Get list of VTGuestLectureConducteds by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTGuestLectureConductedModel> GetVTGuestLectureConductedsByName(string vtGuestLectureConductedName);

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        VTGuestLectureConductedModel GetVTGuestLectureConductedById(Guid vtGuestLectureId);

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId using async
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        Task<VTGuestLectureConductedModel> GetVTGuestLectureConductedByIdAsync(Guid vtGuestLectureId);

        /// <summary>
        /// Insert/Update VTGuestLectureConducted entity
        /// </summary>
        /// <param name="vtGuestLectureConductedModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTGuestLectureConductedDetails(VTGuestLectureConductedModel vtGuestLectureConductedModel);

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
        /// Approved VT Guest Lecture Conducted
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        SingularResponse<string> ApprovedVTGuestLectureConducted(VTGuestLectureApprovalRequest vtApprovalRequest);
    }
}