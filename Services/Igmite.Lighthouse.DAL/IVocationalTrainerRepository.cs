using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VocationalTrainer entity
    /// </summary>
    public interface IVocationalTrainerRepository : IGenericRepository<VocationalTrainer>
    {
        /// <summary>
        /// Get list of VocationalTrainer
        /// </summary>
        /// <returns></returns>
        IQueryable<VocationalTrainer> GetVocationalTrainers();

        /// <summary>
        /// Get list of VocationalTrainer by vocationalTrainerName
        /// </summary>
        /// <param name="vocationalTrainerName"></param>
        /// <returns></returns>
        IQueryable<VocationalTrainer> GetVocationalTrainersByName(string vocationalTrainerName);

        /// <summary>
        /// Get list of VocationalTrainer by vtNames
        /// </summary>
        /// <param name="vtNames"></param>
        /// <returns></returns>
        IQueryable<VocationalTrainer> GetVocationalTrainersByNames(List<string> vtNames);

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
        VocationalTrainer GetVocationalTrainerById(Guid vtId);

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <param name="VTId"></param>
        /// <returns></returns>
        VocationalTrainer GetVocationalTrainerById(DataRequest vtRequest);

        /// <summary>
        /// Get VocationalTrainer by VTId using async
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        Task<VocationalTrainer> GetVocationalTrainerByIdAsync(Guid vtId);

        /// <summary>
        /// Insert/Update VocationalTrainer entity
        /// </summary>
        /// <param name="vocationalTrainer"></param>
        /// <returns></returns>
        bool SaveOrUpdateVocationalTrainerDetails(VocationalTrainer vocationalTrainer);

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
        /// Check duplicate Vocational Trainer by Aadhaar Number
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        VocationalTrainer CheckVocationalTrainerExistByAadhaarNumber(string aadhaarNumber);

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

        /// <summary>
        /// Inactive VT Related Data When Resigned
        /// </summary>
        /// <param name="vocationalTrainer"></param>
        /// <returns></returns>
        bool InactiveVTRelatedDataWhenResigned(VocationalTrainer vocationalTrainer);

        /// <summary>
        /// List of School Students by VT
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolStudentModel> GetSchoolStudentsByVTId(Guid academicYearId, Guid vtId);

        /// <summary>
        /// List of School Student Details by VT
        /// </summary>
        /// <param name="vtTransferRequest"></param>
        /// <returns></returns>
        string TransferOldVTToNewVT(VTTransferRequest vtTransferRequest);

        /// <summary>
        /// Get VCTrainerMap by VTId
        /// </summary>
        /// <param name="vTTransferRequest"></param>
        /// <returns></returns>
        VCTrainerMap SaveOrUpdateVCTrainerMapById(VTTransferRequest vtTransferRequest);
    }
}