using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VocationalTrainer entity
    /// </summary>
    public interface IVocationalTrainerManager : IGenericManager<VocationalTrainerModel>
    {
        /// <summary>
        /// Get list of VocationalTrainers
        /// </summary>
        /// <returns></returns>
        IQueryable<VocationalTrainerModel> GetVocationalTrainers();

        /// <summary>
        /// Get list of VocationalTrainers by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VocationalTrainerModel> GetVocationalTrainersByName(string vocationalTrainerName);

        /// <summary>
        /// Get list of VocationalTrainer by vtNames
        /// </summary>
        /// <param name="vtNames"></param>
        /// <returns></returns>
        IQueryable<VocationalTrainerModel> GetVocationalTrainersByNames(List<string> vtNames);

        /// <summary>
        /// Get list of VocationalTrainer by EmailIds
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        IQueryable<VocationalTrainerModel> GetVocationalTrainersByEmails(Guid academicYearId, List<string> emailIds);

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="VTId"></param>
        /// <returns></returns>
        VocationalTrainerModel GetVocationalTrainerById(Guid vtId);

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <param name="VTId"></param>
        /// <returns></returns>
        VocationalTrainerModel GetVocationalTrainerById(DataRequest vtRequest);

        /// <summary>
        /// Get VocationalTrainer by VTId using async
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        Task<VocationalTrainerModel> GetVocationalTrainerByIdAsync(Guid vtId);

        /// <summary>
        /// Insert/Update VocationalTrainer entity
        /// </summary>
        /// <param name="vocationalTrainerModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVocationalTrainerDetails(VocationalTrainerModel vocationalTrainerModel);

        /// <summary>
        /// Delete a record by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtId);

        /// <summary>
        /// Check duplicate VocationalTrainer by Name
        /// </summary>
        /// <param name="vocationalTrainerModel"></param>
        /// <returns></returns>
        List<string> CheckVocationalTrainerExistByName(VocationalTrainerModel vocationalTrainerModel);

        /// <summary>
        /// List of VocationalTrainer with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VocationalTrainerViewModel> GetVocationalTrainersByCriteria(SearchVocationalTrainerModel searchModel);

        /// <summary>
        /// Get list of VT Ids by School Ids
        /// </summary>
        /// <param name="schoolIds"></param>
        /// <returns></returns>
        Dictionary<Guid, Guid> GetVTIdsBySchoolIds(List<Guid> schoolIds);

        /// <summary>}
        /// Transfer a VT to another School
        /// </summary>}
        /// <param name="vtTransferRequest"></param>}
        /// <returns></returns>}
        SingularResponse<string> VTTransfer(VTTransferRequest vtTransferRequest);

        /// <summary>
        /// List of School Students by VT
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolStudentModel> GetSchoolStudentsByVTId(Guid academicYearId, Guid vtId);
    }
}