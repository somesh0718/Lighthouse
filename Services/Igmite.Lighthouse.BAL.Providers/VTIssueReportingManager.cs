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
    /// Manager of the VTIssueReporting entity
    /// </summary>
    public class VTIssueReportingManager : GenericManager<VTIssueReportingModel>, IVTIssueReportingManager
    {
        private readonly IVTIssueReportingRepository vtIssueReportingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;
        private readonly IIssueMappingRepository issueMappingRepository;

        /// <summary>
        /// Initializes the vtIssueReporting manager.
        /// </summary>
        /// <param name="vtIssueReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        /// <param name="_issueMappingRepository"></param>
        public VTIssueReportingManager(IVTIssueReportingRepository _vtIssueReportingRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository, IIssueMappingRepository _issueMappingRepository)
        {
            this.vtIssueReportingRepository = _vtIssueReportingRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
            this.issueMappingRepository = _issueMappingRepository;
        }

        /// <summary>
        /// Get list of VTIssueReportings
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTIssueReportingModel> GetVTIssueReportings()
        {
            var vtIssueReportings = this.vtIssueReportingRepository.GetVTIssueReportings();

            IList<VTIssueReportingModel> vtIssueReportingModels = new List<VTIssueReportingModel>();
            vtIssueReportings.ForEach((user) => vtIssueReportingModels.Add(user.ToModel()));

            return vtIssueReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTIssueReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTIssueReportingModel> GetVTIssueReportingsByName(string vtIssueReportingName)
        {
            var vtIssueReportings = this.vtIssueReportingRepository.GetVTIssueReportingsByName(vtIssueReportingName);

            IList<VTIssueReportingModel> vtIssueReportingModels = new List<VTIssueReportingModel>();
            vtIssueReportings.ForEach((user) => vtIssueReportingModels.Add(user.ToModel()));

            return vtIssueReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        public VTIssueReportingModel GetVTIssueReportingById(Guid vtIssueReportingId)
        {
            VTIssueReporting vtIssueReporting = this.vtIssueReportingRepository.GetVTIssueReportingById(vtIssueReportingId);

            return (vtIssueReporting != null) ? vtIssueReporting.ToModel() : null;
        }

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId using async
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        public async Task<VTIssueReportingModel> GetVTIssueReportingByIdAsync(Guid vtIssueReportingId)
        {
            var vtIssueReporting = await this.vtIssueReportingRepository.GetVTIssueReportingByIdAsync(vtIssueReportingId);

            return (vtIssueReporting != null) ? vtIssueReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTIssueReporting entity
        /// </summary>
        /// <param name="vtIssueReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTIssueReportingDetails(VTIssueReportingModel vtIssueReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VTIssueReporting vtIssueReporting = null;

                //Validate model data
                vtIssueReportingModel = vtIssueReportingModel.GetModelValidationErrors<VTIssueReportingModel>();

                if (vtIssueReportingModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vtIssueReportingModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                string authUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtIssueReportingModel.RequestType == RequestType.Edit)
                {
                    vtIssueReporting = this.vtIssueReportingRepository.GetVTIssueReportingById(vtIssueReportingModel.VTIssueReportingId);
                }
                else
                {
                    vtIssueReporting = new VTIssueReporting();
                    vtIssueReporting.VTIssueReportingId = Guid.NewGuid();
                    vtIssueReporting.ApprovalStatus = "219"; // Open
                }

                if (vtIssueReportingModel.ErrorMessages.Count == 0)
                {
                    List<string> validationMessages = this.vtIssueReportingRepository.CheckVTIssueReportingExistByName(vtIssueReportingModel);

                    if (validationMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", validationMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vtIssueReporting.AuthUserId = authUserId;
                    vtIssueReporting = vtIssueReportingModel.FromModel(vtIssueReporting);

                    //Save Or Update vtIssueReporting details
                    bool isSaved = this.vtIssueReportingRepository.SaveOrUpdateVTIssueReportingDetails(vtIssueReporting);

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
                throw new Exception("BAL > SaveOrUpdateVTIssueReportingDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtIssueReportingId)
        {
            return this.vtIssueReportingRepository.DeleteById(vtIssueReportingId);
        }

        /// <summary>
        /// Check duplicate VTIssueReporting by Name
        /// </summary>
        /// <param name="vtIssueReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckVTIssueReportingExistByName(VTIssueReportingModel vtIssueReportingModel)
        {
            return this.vtIssueReportingRepository.CheckVTIssueReportingExistByName(vtIssueReportingModel);
        }

        /// <summary>}
        /// List of VTIssueReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTIssueReportingViewModel> GetVTIssueReportingsByCriteria(SearchVTIssueReportingModel searchModel)
        {
            return this.vtIssueReportingRepository.GetVTIssueReportingsByCriteria(searchModel);
        }

        /// <summary>
        /// Approved VT Issue Reporting
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> ApprovedVTIssueReporting(VTIssueReportingApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            bool results = false;

            VTIssueReporting vtIssueReporting = this.vtIssueReportingRepository.GetVTIssueReportingById(vtApprovalRequest.VTIssueReportingId);

            if (vtIssueReporting != null)
            {
                IssueApprovalHistory issueApprovalHistory = new IssueApprovalHistory
                {
                    IssueApprovalHistoryId = Guid.NewGuid(),
                    IssueId = vtIssueReporting.VTIssueReportingId,
                    IssueType = "VT",
                    ApprovedBy = vtIssueReporting.VTId,
                    ApprovedDate = Constants.GetCurrentDateTime,
                    ApprovalStatus = vtIssueReporting.ApprovalStatus,
                    Remarks = vtIssueReporting.Remarks
                };

                vtIssueReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vtIssueReporting.ApprovalStatus = vtApprovalRequest.ApprovalStatus;
                vtIssueReporting.ApprovedDate = Constants.GetCurrentDateTime;
                vtIssueReporting.Remarks = string.Format("{0}\n\n{1}", vtIssueReporting.Remarks, vtApprovalRequest.Remarks);
                vtIssueReporting.RequestType = RequestType.Edit;
                vtIssueReporting.SetAuditValues(RequestType.Edit);

                results = this.vtIssueReportingRepository.SaveOrUpdateVTIssueReportingDetails(vtIssueReporting, issueApprovalHistory);
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