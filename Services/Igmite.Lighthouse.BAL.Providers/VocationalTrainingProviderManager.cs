using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the VocationalTrainingProvider entity
    /// </summary>
    public class VocationalTrainingProviderManager : GenericManager<VocationalTrainingProviderModel>, IVocationalTrainingProviderManager
    {
        private readonly IVocationalTrainingProviderRepository vocationalTrainingProviderRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vocationalTrainingProvider manager.
        /// </summary>
        /// <param name="vocationalTrainingProviderRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VocationalTrainingProviderManager(IVocationalTrainingProviderRepository _vocationalTrainingProviderRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vocationalTrainingProviderRepository = _vocationalTrainingProviderRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VocationalTrainingProviders
        /// </summary>
        /// <returns></returns>
        public IQueryable<VocationalTrainingProviderModel> GetVocationalTrainingProviders()
        {
            var vocationalTrainingProviders = this.vocationalTrainingProviderRepository.GetVocationalTrainingProviders();

            IList<VocationalTrainingProviderModel> vocationalTrainingProviderModels = new List<VocationalTrainingProviderModel>();
            vocationalTrainingProviders.ForEach((user) => vocationalTrainingProviderModels.Add(user.ToModel()));

            return vocationalTrainingProviderModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalTrainingProviders by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainingProviderModel> GetVocationalTrainingProvidersByName(string vocationalTrainingProviderName)
        {
            var vocationalTrainingProviders = this.vocationalTrainingProviderRepository.GetVocationalTrainingProvidersByName(vocationalTrainingProviderName);

            IList<VocationalTrainingProviderModel> vocationalTrainingProviderModels = new List<VocationalTrainingProviderModel>();
            vocationalTrainingProviders.ForEach((user) => vocationalTrainingProviderModels.Add(user.ToModel()));

            return vocationalTrainingProviderModels.AsQueryable();
        }

        /// <summary>
        /// Get VocationalTrainingProvider by VTPId
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public VocationalTrainingProviderModel GetVocationalTrainingProviderById(Guid vtpId)
        {
            VocationalTrainingProvider vocationalTrainingProvider = this.vocationalTrainingProviderRepository.GetVocationalTrainingProviderById(vtpId);

            return (vocationalTrainingProvider != null) ? vocationalTrainingProvider.ToModel() : null;
        }

        /// <summary>
        /// Get VocationalTrainingProvider by VTPId using async
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public async Task<VocationalTrainingProviderModel> GetVocationalTrainingProviderByIdAsync(Guid vtpId)
        {
            var vocationalTrainingProvider = await this.vocationalTrainingProviderRepository.GetVocationalTrainingProviderByIdAsync(vtpId);

            return (vocationalTrainingProvider != null) ? vocationalTrainingProvider.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VocationalTrainingProvider entity
        /// </summary>
        /// <param name="vocationalTrainingProviderModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVocationalTrainingProviderDetails(VocationalTrainingProviderModel vocationalTrainingProviderModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VocationalTrainingProvider vocationalTrainingProvider = null;

                //Validate model data
                vocationalTrainingProviderModel = vocationalTrainingProviderModel.GetModelValidationErrors<VocationalTrainingProviderModel>();

                if (vocationalTrainingProviderModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vocationalTrainingProviderModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vocationalTrainingProviderModel.RequestType == RequestType.Edit)
                {
                    vocationalTrainingProvider = this.vocationalTrainingProviderRepository.GetVocationalTrainingProviderById(vocationalTrainingProviderModel.VTPId);
                }
                else
                {
                    vocationalTrainingProvider = new VocationalTrainingProvider();
                    vocationalTrainingProviderModel.VTPId = Guid.NewGuid();
                    vocationalTrainingProviderModel.VTPAcademicYear.VTPAcademicYearId = Guid.NewGuid();
                }

                if (vocationalTrainingProviderModel.ErrorMessages.Count == 0 && !(string.Equals(vocationalTrainingProviderModel.VTPShortName.ToLower(), vocationalTrainingProvider.VTPShortName.StringVal().ToLower())))
                {
                    bool isVocationalTrainingProviderExists = this.vocationalTrainingProviderRepository.CheckVocationalTrainingProviderExistByName(vocationalTrainingProviderModel);

                    if (isVocationalTrainingProviderExists)
                    {
                        response.Errors.Add(Constants.ExistMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vocationalTrainingProvider.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    vocationalTrainingProvider = vocationalTrainingProviderModel.FromModel(vocationalTrainingProvider);

                    if (vocationalTrainingProviderModel.MOUDocumentFile != null)
                    {
                        vocationalTrainingProviderModel.MOUDocumentFile.ContentId = vocationalTrainingProviderModel.VTPId;
                        var uploadedFile = UtilityManager.UploadFileInPostByMobile(vocationalTrainingProviderModel.MOUDocumentFile);

                        vocationalTrainingProvider.MOUDocUpload = uploadedFile.FilePath;

                        if (uploadedFile.Exception != null)
                            Logging.ErrorManager.Instance.WriteErrorLogsInFile(uploadedFile.Exception);
                    }

                    //Save Or Update vocationalTrainingProvider details
                    bool isSaved = this.vocationalTrainingProviderRepository.SaveOrUpdateVocationalTrainingProviderDetails(vocationalTrainingProvider);

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateVocationalTrainingProviderDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTPId
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpId)
        {
            return this.vocationalTrainingProviderRepository.DeleteById(vtpId);
        }

        /// <summary>
        /// Check duplicate VocationalTrainingProvider by Name
        /// </summary>
        /// <param name="vocationalTrainingProviderModel"></param>
        /// <returns></returns>
        public bool CheckVocationalTrainingProviderExistByName(VocationalTrainingProviderModel vocationalTrainingProviderModel)
        {
            return this.vocationalTrainingProviderRepository.CheckVocationalTrainingProviderExistByName(vocationalTrainingProviderModel);
        }

        /// <summary>
        /// Get list of VocationalTrainingProvider
        /// </summary>
        /// <returns></returns>
        public IList<DropdownModel<Guid>> GetVTPList()
        {
           return this.vocationalTrainingProviderRepository.GetVTPList();
        }

        /// <summary>}
        /// List of VocationalTrainingProvider with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VocationalTrainingProviderViewModel> GetVocationalTrainingProvidersByCriteria(SearchVocationalTrainingProviderModel searchModel)
        {
            return this.vocationalTrainingProviderRepository.GetVocationalTrainingProvidersByCriteria(searchModel);
        }
    
        /// <summary>
        /// List of School by VTP
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<VTPSchoolModel> GetSchoolByVTPIdSectorId(Guid academicYearId, Guid vtpId,Guid sectorId)
        {
            return this.vocationalTrainingProviderRepository.GetSchoolByVTPIdSectorId(academicYearId, vtpId, sectorId);
        }

        /// <summary>
        /// Save VTP Transfers data
        /// </summary>
        /// <param name="vtpSchoolTransferRequest"></param>
        /// <returns></returns>
        public VTPSchoolTransferModel SaveVTPTransfers(VTPSchoolTransferModel vtpSchoolTransferRequest)
        {
            return this.vocationalTrainingProviderRepository.SaveVTPTransfers(vtpSchoolTransferRequest);
        }
    }
}