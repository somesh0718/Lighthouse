using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VocationalTrainingProvider entity
    /// </summary>
    public interface IVocationalTrainingProviderRepository : IGenericRepository<VocationalTrainingProvider>
    {
        /// <summary>
        /// Get list of VocationalTrainingProvider
        /// </summary>
        /// <returns></returns>
        IQueryable<VocationalTrainingProvider> GetVocationalTrainingProviders();

        /// <summary>
        /// Get list of VocationalTrainingProvider by vocationalTrainingProviderName
        /// </summary>
        /// <param name="vocationalTrainingProviderName"></param>
        /// <returns></returns>
        IQueryable<VocationalTrainingProvider> GetVocationalTrainingProvidersByName(string vocationalTrainingProviderName);

        /// <summary>
        /// Get VocationalTrainingProvider by VTPId
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        VocationalTrainingProvider GetVocationalTrainingProviderById(Guid vtpId);

        /// <summary>
        /// Get VocationalTrainingProvider by VTPId using async
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        Task<VocationalTrainingProvider> GetVocationalTrainingProviderByIdAsync(Guid vtpId);

        /// <summary>
        /// Insert/Update VocationalTrainingProvider entity
        /// </summary>
        /// <param name="vocationalTrainingProvider"></param>
        /// <returns></returns>
        bool SaveOrUpdateVocationalTrainingProviderDetails(VocationalTrainingProvider vocationalTrainingProvider);

        /// <summary>
        /// Delete a record by VTPId
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtpId);

        /// <summary>
        /// Check duplicate VocationalTrainingProvider by Name
        /// </summary>
        /// <param name="vocationalTrainingProviderModel"></param>
        /// <returns></returns>
        bool CheckVocationalTrainingProviderExistByName(VocationalTrainingProviderModel vocationalTrainingProviderModel);

        /// <summary>
        /// Get list of VocationalTrainingProvider
        /// </summary>
        /// <returns></returns>
        IList<DropdownModel<Guid>> GetVTPList();

        /// <summary>
        /// List of VocationalTrainingProvider with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VocationalTrainingProviderViewModel> GetVocationalTrainingProvidersByCriteria(SearchVocationalTrainingProviderModel searchModel);

        /// <summary>
        /// List of School by VTP
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        IList<VTPSchoolModel> GetSchoolByVTPIdSectorId(Guid academicYearId, Guid vtpId,Guid sectorId);

        /// <summary>
        /// Save VTP Transfers data
        /// </summary>
        /// <param name="vtpSchoolTransferRequest"></param>
        /// <returns></returns>
        VTPSchoolTransferModel SaveVTPTransfers(VTPSchoolTransferModel vtpSchoolTransferRequest);

    }
}