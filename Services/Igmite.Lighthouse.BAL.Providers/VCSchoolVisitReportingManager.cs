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
    /// Manager of the VCSchoolVisitReporting entity
    /// </summary>
    public class VCSchoolVisitReportingManager : GenericManager<VCSchoolVisitReportingModel>, IVCSchoolVisitReportingManager
    {
        private readonly IVCSchoolVisitReportingRepository vcSchoolVisitReportingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vcSchoolVisitReporting manager.
        /// </summary>
        /// <param name="vcSchoolVisitReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VCSchoolVisitReportingManager(IVCSchoolVisitReportingRepository _vcSchoolVisitReportingRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vcSchoolVisitReportingRepository = _vcSchoolVisitReportingRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VCSchoolVisitReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCSchoolVisitReportingModel> GetVCSchoolVisitReporting()
        {
            var vcSchoolVisitReporting = this.vcSchoolVisitReportingRepository.GetVCSchoolVisitReporting();

            IList<VCSchoolVisitReportingModel> vcSchoolVisitReportingModels = new List<VCSchoolVisitReportingModel>();
            vcSchoolVisitReporting.ForEach((user) => vcSchoolVisitReportingModels.Add(user.ToModel()));

            return vcSchoolVisitReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VCSchoolVisitReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCSchoolVisitReportingModel> GetVCSchoolVisitReportingByName(string vcSchoolVisitReportingName)
        {
            var vcSchoolVisitReporting = this.vcSchoolVisitReportingRepository.GetVCSchoolVisitReportingByName(vcSchoolVisitReportingName);

            IList<VCSchoolVisitReportingModel> vcSchoolVisitReportingModels = new List<VCSchoolVisitReportingModel>();
            vcSchoolVisitReporting.ForEach((user) => vcSchoolVisitReportingModels.Add(user.ToModel()));

            return vcSchoolVisitReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        public VCSchoolVisitReportingModel GetVCSchoolVisitReportingById(Guid vcSchoolVisitReportingId)
        {
            VCSchoolVisitReporting vcSchoolVisitReporting = this.vcSchoolVisitReportingRepository.GetVCSchoolVisitReportingById(vcSchoolVisitReportingId);

            return (vcSchoolVisitReporting != null) ? vcSchoolVisitReporting.ToModel() : null;
        }

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId using async
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        public async Task<VCSchoolVisitReportingModel> GetVCSchoolVisitReportingByIdAsync(Guid vcSchoolVisitReportingId)
        {
            var vcSchoolVisitReporting = await this.vcSchoolVisitReportingRepository.GetVCSchoolVisitReportingByIdAsync(vcSchoolVisitReportingId);

            return (vcSchoolVisitReporting != null) ? vcSchoolVisitReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VCSchoolVisitReporting entity
        /// </summary>
        /// <param name="vcSchoolVisitReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVCSchoolVisitReportingDetails(VCSchoolVisitReportingModel vcSchoolVisitReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VCSchoolVisitReporting vcSchoolVisitReporting = null;

            //Validate model data
            vcSchoolVisitReportingModel = vcSchoolVisitReportingModel.GetModelValidationErrors<VCSchoolVisitReportingModel>();

            if (vcSchoolVisitReportingModel.ErrorMessages.Count > 0)
            {
                response.Errors = vcSchoolVisitReportingModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vcSchoolVisitReportingModel.RequestType == RequestType.Edit)
            {
                vcSchoolVisitReporting = this.vcSchoolVisitReportingRepository.GetVCSchoolVisitReportingById(vcSchoolVisitReportingModel.VCSchoolVisitReportingId);
            }
            else
            {
                vcSchoolVisitReporting = new VCSchoolVisitReporting();
                vcSchoolVisitReporting.VCSchoolVisitReportingId = Guid.NewGuid();
            }

            if (vcSchoolVisitReportingModel.ErrorMessages.Count == 0 && (vcSchoolVisitReportingModel.CompanyName.StringVal().ToLower() != vcSchoolVisitReporting.CompanyName.StringVal().ToLower()))
            {
                bool isVCSchoolVisitReportingExists = this.vcSchoolVisitReportingRepository.CheckVCSchoolVisitReportingExistByName(vcSchoolVisitReportingModel);

                if (isVCSchoolVisitReportingExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vcSchoolVisitReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vcSchoolVisitReporting = vcSchoolVisitReportingModel.FromModel(vcSchoolVisitReporting);

                if (vcSchoolVisitReportingModel.SVPhotoWithPrincipalFile != null)
                {
                    vcSchoolVisitReportingModel.SVPhotoWithPrincipalFile.ContentId = vcSchoolVisitReporting.VCSchoolVisitReportingId;
                    var svPhotoWithPrincipalFile = UtilityManager.UploadFileInPostByMobile(vcSchoolVisitReportingModel.SVPhotoWithPrincipalFile);

                    vcSchoolVisitReporting.SVPhotoWithPrincipal = svPhotoWithPrincipalFile.FilePath;

                    if (svPhotoWithPrincipalFile.Exception != null)
                        Logging.ErrorManager.Instance.WriteErrorLogsInFile(svPhotoWithPrincipalFile.Exception);
                }

                if (vcSchoolVisitReportingModel.SVPhotoWithStudentFile != null)
                {
                    vcSchoolVisitReportingModel.SVPhotoWithStudentFile.ContentId = vcSchoolVisitReporting.VCSchoolVisitReportingId;
                    var svPhotoWithStudentFile = UtilityManager.UploadFileInPostByMobile(vcSchoolVisitReportingModel.SVPhotoWithStudentFile);

                    vcSchoolVisitReporting.SVPhotoWithStudents = svPhotoWithStudentFile.FilePath;

                    if (svPhotoWithStudentFile.Exception != null)
                        Logging.ErrorManager.Instance.WriteErrorLogsInFile(svPhotoWithStudentFile.Exception);
                }

                //Save Or Update vcSchoolVisitReporting details
                bool isSaved = this.vcSchoolVisitReportingRepository.SaveOrUpdateVCSchoolVisitReportingDetails(vcSchoolVisitReporting);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcSchoolVisitReportingId)
        {
            return this.vcSchoolVisitReportingRepository.DeleteById(vcSchoolVisitReportingId);
        }

        /// <summary>
        /// Check duplicate VCSchoolVisitReporting by Name
        /// </summary>
        /// <param name="vcSchoolVisitReportingModel"></param>
        /// <returns></returns>
        public bool CheckVCSchoolVisitReportingExistByName(VCSchoolVisitReportingModel vcSchoolVisitReportingModel)
        {
            return this.vcSchoolVisitReportingRepository.CheckVCSchoolVisitReportingExistByName(vcSchoolVisitReportingModel);
        }

        /// <summary>}
        /// List of VCSchoolVisitReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCSchoolVisitReportingViewModel> GetVCSchoolVisitReportingByCriteria(SearchVCSchoolVisitReportingModel searchModel)
        {
            return this.vcSchoolVisitReportingRepository.GetVCSchoolVisitReportingByCriteria(searchModel);
        }
    }
}