using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VocationalCoordinator entity
    /// </summary>
    public interface IVocationalCoordinatorRepository : IGenericRepository<VocationalCoordinator>
    {
        /// <summary>
        /// Get list of VocationalCoordinator
        /// </summary>
        /// <returns></returns>
        IQueryable<VocationalCoordinator> GetVocationalCoordinators();

        /// <summary>
        /// Get list of VocationalCoordinator by vocationalCoordinatorName
        /// </summary>
        /// <param name="vocationalCoordinatorName"></param>
        /// <returns></returns>
        IQueryable<VocationalCoordinator> GetVocationalCoordinatorsByName(string vocationalCoordinatorName);

        /// <summary>
        /// Get list of VocationalCoordinators by VC Names
        /// </summary>
        /// <param name="vcNames"></param>
        /// <returns></returns>
        IQueryable<VocationalCoordinator> GetVocationalCoordinatorsByNames(List<string> vcNames);

        /// <summary>
        /// Get list of VocationalCoordinators by EmailIds
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByEmails(Guid academicYearId, List<string> emailIds);

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="VCId"></param>
        /// <returns></returns>
        VocationalCoordinator GetVocationalCoordinatorById(Guid vcId);

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        VocationalCoordinator GetVocationalCoordinatorById(DataRequest vcRequest);

        /// <summary>
        /// Get VocationalCoordinator by VCId using async
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        Task<VocationalCoordinator> GetVocationalCoordinatorByIdAsync(Guid vcId);

        /// <summary>
        /// Insert/Update VocationalCoordinator entity
        /// </summary>
        /// <param name="vocationalCoordinator"></param>
        /// <returns></returns>
        bool SaveOrUpdateVocationalCoordinatorDetails(VocationalCoordinator vocationalCoordinator);

        /// <summary>
        /// Delete a record by VCId
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcId);

        /// <summary>
        /// Check duplicate VocationalCoordinator by Name
        /// </summary>
        /// <param name="vocationalCoordinatorModel"></param>
        /// <returns></returns>
        List<string> CheckVocationalCoordinatorExistByName(VocationalCoordinatorModel vocationalCoordinatorModel);

        /// <summary>
        /// List of VocationalCoordinator with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VocationalCoordinatorViewModel> GetVocationalCoordinatorsByCriteria(SearchVocationalCoordinatorModel searchModel);

        /// <summary>
        /// Inactive VC Related Data When Resigned
        /// </summary>
        /// <param name="vocationalCoordinator"></param>
        /// <returns></returns>
        bool InactiveVCRelatedDataWhenResigned(VocationalCoordinator vocationalCoordinator);

        /// <summary>
        /// Get list of VocationalCoordinator
        /// </summary>
        /// <returns></returns>
        IList<VocationalCoordinatorModel> GetVCList();
        
        /// <summary>
        /// Get VC Schools By VTP And VC Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        IList<VCSchoolModel> GetVCSchoolsByVTPAndVCId(DataRequest schoolRequest);

        /// <summary>
        /// Save VC Transfers data
        /// </summary>
        /// <param name="vcSchoolTransferRequest"></param>
        /// <returns></returns>
        VCSchoolTransferModel SaveVCTransfers(VCSchoolTransferModel vcSchoolTransferRequest);
    }
}