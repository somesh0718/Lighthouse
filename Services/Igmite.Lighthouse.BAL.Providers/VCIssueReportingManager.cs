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
    /// Manager of the VCIssueReporting entity
    /// </summary>
    public class VCIssueReportingManager : GenericManager<VCIssueReportingModel>, IVCIssueReportingManager
    {
        private readonly IVCIssueReportingRepository vcIssueReportingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;
        private readonly IIssueMappingRepository issueMappingRepository;

        /// <summary>
        /// Initializes the vcIssueReporting manager.
        /// </summary>
        /// <param name="vcIssueReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        /// <param name="_issueMappingRepository"></param>
        public VCIssueReportingManager(IVCIssueReportingRepository _vcIssueReportingRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository, IIssueMappingRepository _issueMappingRepository)
        {
            this.vcIssueReportingRepository = _vcIssueReportingRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
            this.issueMappingRepository = _issueMappingRepository;
        }

        /// <summary>
        /// Get list of VCIssueReportings
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCIssueReportingModel> GetVCIssueReportings()
        {
            var vcIssueReportings = this.vcIssueReportingRepository.GetVCIssueReportings();

            IList<VCIssueReportingModel> vcIssueReportingModels = new List<VCIssueReportingModel>();
            vcIssueReportings.ForEach((user) => vcIssueReportingModels.Add(user.ToModel()));

            return vcIssueReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VCIssueReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCIssueReportingModel> GetVCIssueReportingsByName(string vcIssueReportingName)
        {
            var vcIssueReportings = this.vcIssueReportingRepository.GetVCIssueReportingsByName(vcIssueReportingName);

            IList<VCIssueReportingModel> vcIssueReportingModels = new List<VCIssueReportingModel>();
            vcIssueReportings.ForEach((user) => vcIssueReportingModels.Add(user.ToModel()));

            return vcIssueReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        public VCIssueReportingModel GetVCIssueReportingById(Guid vcIssueReportingId)
        {
            VCIssueReporting vcIssueReporting = this.vcIssueReportingRepository.GetVCIssueReportingById(vcIssueReportingId);

            return (vcIssueReporting != null) ? vcIssueReporting.ToModel() : null;
        }

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId using async
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        public async Task<VCIssueReportingModel> GetVCIssueReportingByIdAsync(Guid vcIssueReportingId)
        {
            var vcIssueReporting = await this.vcIssueReportingRepository.GetVCIssueReportingByIdAsync(vcIssueReportingId);

            return (vcIssueReporting != null) ? vcIssueReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VCIssueReporting entity
        /// </summary>
        /// <param name="vcIssueReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVCIssueReportingDetails(VCIssueReportingModel vcIssueReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VCIssueReporting vcIssueReporting = null;

                //Validate model data
                vcIssueReportingModel = vcIssueReportingModel.GetModelValidationErrors<VCIssueReportingModel>();

                if (vcIssueReportingModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vcIssueReportingModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vcIssueReportingModel.RequestType == RequestType.Edit)
                {
                    vcIssueReporting = this.vcIssueReportingRepository.GetVCIssueReportingById(vcIssueReportingModel.VCIssueReportingId);
                }
                else
                {
                    vcIssueReporting = new VCIssueReporting();
                    vcIssueReporting.VCIssueReportingId = Guid.NewGuid();
                    vcIssueReporting.ApprovalStatus = "219"; // Open
                }

                if (vcIssueReportingModel.ErrorMessages.Count == 0)
                {
                    List<string> validationMessages = this.vcIssueReportingRepository.CheckVCIssueReportingExistByName(vcIssueReportingModel);

                    if (validationMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", validationMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vcIssueReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    vcIssueReporting = vcIssueReportingModel.FromModel(vcIssueReporting);

                    //Save Or Update vcIssueReporting details
                    bool isSaved = this.vcIssueReportingRepository.SaveOrUpdateVCIssueReportingDetails(vcIssueReporting);

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
                throw new Exception("BAL > SaveOrUpdateVCIssueReportingDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcIssueReportingId)
        {
            return this.vcIssueReportingRepository.DeleteById(vcIssueReportingId);
        }

        /// <summary>
        /// Check duplicate VCIssueReporting by Name
        /// </summary>
        /// <param name="vcIssueReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckVCIssueReportingExistByName(VCIssueReportingModel vcIssueReportingModel)
        {
            return this.vcIssueReportingRepository.CheckVCIssueReportingExistByName(vcIssueReportingModel);
        }

        /// <summary>}
        /// List of VCIssueReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCIssueReportingViewModel> GetVCIssueReportingsByCriteria(SearchVCIssueReportingModel searchModel)
        {
            return this.vcIssueReportingRepository.GetVCIssueReportingsByCriteria(searchModel);
        }

        /// <summary>
        /// Approved VC Issue Reporting
        /// </summary>
        /// <param name="vcApprovalRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> ApprovedVCIssueReporting(VCIssueReportingApprovalRequest vcApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            bool results = false;

            VCIssueReporting vcIssueReporting = this.vcIssueReportingRepository.GetVCIssueReportingById(vcApprovalRequest.VCIssueReportingId);

            if (vcIssueReporting != null)
            {
                IssueApprovalHistory issueApprovalHistory = new IssueApprovalHistory
                {
                    IssueApprovalHistoryId = Guid.NewGuid(),
                    IssueId = vcIssueReporting.VCIssueReportingId,
                    IssueType = "VC",
                    ApprovedBy = vcIssueReporting.VCId,
                    ApprovedDate = Constants.GetCurrentDateTime,
                    ApprovalStatus = vcIssueReporting.ApprovalStatus,
                    Remarks = vcIssueReporting.Remarks
                };

                vcIssueReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vcIssueReporting.ApprovalStatus = vcApprovalRequest.ApprovalStatus;
                vcIssueReporting.ApprovedDate = Constants.GetCurrentDateTime;
                vcIssueReporting.Remarks = string.Format("{0}\n\n{1}", vcIssueReporting.Remarks, vcApprovalRequest.Remarks);
                vcIssueReporting.RequestType = RequestType.Edit;
                vcIssueReporting.SetAuditValues(RequestType.Edit);

                results = this.vcIssueReportingRepository.SaveOrUpdateVCIssueReportingDetails(vcIssueReporting, issueApprovalHistory);
            }

            if (results)
            {
                response.Result = "Success";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
            }

            return response;
        }
    }
}