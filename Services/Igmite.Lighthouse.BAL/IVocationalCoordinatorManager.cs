using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VocationalCoordinator entity
    /// </summary>
    public interface IVocationalCoordinatorManager : IGenericManager<VocationalCoordinatorModel>
    {
        /// <summary>
        /// Get list of VocationalCoordinators
        /// </summary>
        /// <returns></returns>
        IQueryable<VocationalCoordinatorModel> GetVocationalCoordinators();

        /// <summary>
        /// Get list of VocationalCoordinators by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByName(string vocationalCoordinatorName);

        /// <summary>
        /// Get list of VocationalCoordinators by VC Names
        /// </summary>
        /// <param name="vcNames"></param>
        /// <returns></returns>
        IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByNames(List<string> vcNames);

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
        VocationalCoordinatorModel GetVocationalCoordinatorById(Guid vcId);

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        VocationalCoordinatorModel GetVocationalCoordinatorById(DataRequest vcRequest);

        /// <summary>
        /// Get VocationalCoordinator by VCId using async
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        Task<VocationalCoordinatorModel> GetVocationalCoordinatorByIdAsync(Guid vcId);

        /// <summary>
        /// Insert/Update VocationalCoordinator entity
        /// </summary>
        /// <param name="vocationalCoordinatorModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVocationalCoordinatorDetails(VocationalCoordinatorModel vocationalCoordinatorModel);

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