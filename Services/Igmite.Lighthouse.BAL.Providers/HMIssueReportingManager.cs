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
    /// Manager of the HMIssueReporting entity
    /// </summary>
    public class HMIssueReportingManager : GenericManager<HMIssueReportingModel>, IHMIssueReportingManager
    {
        private readonly IHMIssueReportingRepository hmIssueReportingRepository;
        private readonly IIssueMappingRepository issueMappingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the hmIssueReporting manager.
        /// </summary>
        /// <param name="hmIssueReportingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_issueMappingRepository"></param>
        public HMIssueReportingManager(IHMIssueReportingRepository _hmIssueReportingRepository, IHttpContextAccessor _httpContextAccessor, IIssueMappingRepository _issueMappingRepository)
        {
            this.hmIssueReportingRepository = _hmIssueReportingRepository;
            this.issueMappingRepository = _issueMappingRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of HMIssueReportings
        /// </summary>
        /// <returns></returns>
        public IQueryable<HMIssueReportingModel> GetHMIssueReportings()
        {
            var hmIssueReportings = this.hmIssueReportingRepository.GetHMIssueReportings();

            IList<HMIssueReportingModel> hmIssueReportingModels = new List<HMIssueReportingModel>();
            hmIssueReportings.ForEach((user) => hmIssueReportingModels.Add(user.ToModel()));

            return hmIssueReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of HMIssueReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<HMIssueReportingModel> GetHMIssueReportingsByName(string hmIssueReportingName)
        {
            var hmIssueReportings = this.hmIssueReportingRepository.GetHMIssueReportingsByName(hmIssueReportingName);

            IList<HMIssueReportingModel> hmIssueReportingModels = new List<HMIssueReportingModel>();
            hmIssueReportings.ForEach((user) => hmIssueReportingModels.Add(user.ToModel()));

            return hmIssueReportingModels.AsQueryable();
        }

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        public HMIssueReportingModel GetHMIssueReportingById(Guid hmIssueReportingId)
        {
            HMIssueReporting hmIssueReporting = this.hmIssueReportingRepository.GetHMIssueReportingById(hmIssueReportingId);

            return (hmIssueReporting != null) ? hmIssueReporting.ToModel() : null;
        }

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId using async
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        public async Task<HMIssueReportingModel> GetHMIssueReportingByIdAsync(Guid hmIssueReportingId)
        {
            var hmIssueReporting = await this.hmIssueReportingRepository.GetHMIssueReportingByIdAsync(hmIssueReportingId);

            return (hmIssueReporting != null) ? hmIssueReporting.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update HMIssueReporting entity
        /// </summary>
        /// <param name="hmIssueReportingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateHMIssueReportingDetails(HMIssueReportingModel hmIssueReportingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                HMIssueReporting hmIssueReporting = null;

                //Validate model data
                hmIssueReportingModel = hmIssueReportingModel.GetModelValidationErrors<HMIssueReportingModel>();

                if (hmIssueReportingModel.ErrorMessages.Count > 0)
                {
                    response.Errors = hmIssueReportingModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (hmIssueReportingModel.RequestType == RequestType.Edit)
                {
                    hmIssueReporting = this.hmIssueReportingRepository.GetHMIssueReportingById(hmIssueReportingModel.HMIssueReportingId);
                }
                else
                {
                    hmIssueReporting = new HMIssueReporting();
                    hmIssueReporting.HMIssueReportingId = Guid.NewGuid();
                    hmIssueReporting.ApprovalStatus = "219"; // Open
                }

                if (hmIssueReportingModel.ErrorMessages.Count == 0)
                {
                    List<string> validationMessages = this.hmIssueReportingRepository.CheckHMIssueReportingExistByName(hmIssueReportingModel);

                    if (validationMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", validationMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    hmIssueReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    hmIssueReporting = hmIssueReportingModel.FromModel(hmIssueReporting);

                    //Save Or Update hmIssueReporting details
                    bool isSaved = this.hmIssueReportingRepository.SaveOrUpdateHMIssueReportingDetails(hmIssueReporting);

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
                throw new Exception("BAL > SaveOrUpdateHMIssueReportingDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid hmIssueReportingId)
        {
            return this.hmIssueReportingRepository.DeleteById(hmIssueReportingId);
        }

        /// <summary>
        /// Check duplicate HMIssueReporting by Name
        /// </summary>
        /// <param name="hmIssueReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckHMIssueReportingExistByName(HMIssueReportingModel hmIssueReportingModel)
        {
            return this.hmIssueReportingRepository.CheckHMIssueReportingExistByName(hmIssueReportingModel);
        }

        /// <summary>}
        /// List of HMIssueReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<HMIssueReportingViewModel> GetHMIssueReportingsByCriteria(SearchHMIssueReportingModel searchModel)
        {
            return this.hmIssueReportingRepository.GetHMIssueReportingsByCriteria(searchModel);
        }

        /// <summary>
        /// Approved HM Issue Reporting
        /// </summary>
        /// <param name="hmApprovalRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> ApprovedHMIssueReporting(HMIssueReportingApprovalRequest hmApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            bool results = false;

            HMIssueReporting hmIssueReporting = this.hmIssueReportingRepository.GetHMIssueReportingById(hmApprovalRequest.HMIssueReportingId);

            if (hmIssueReporting != null)
            {
                IssueApprovalHistory issueApprovalHistory = new IssueApprovalHistory
                {
                    IssueApprovalHistoryId = Guid.NewGuid(),
                    IssueId = hmIssueReporting.HMIssueReportingId,
                    IssueType = "HM",
                    ApprovedBy = hmIssueReporting.HMId,
                    ApprovedDate = Constants.GetCurrentDateTime,
                    ApprovalStatus = hmIssueReporting.ApprovalStatus,
                    Remarks = hmIssueReporting.Remarks
                };

                hmIssueReporting.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                hmIssueReporting.ApprovalStatus = hmApprovalRequest.ApprovalStatus;
                hmIssueReporting.ApprovedDate = Constants.GetCurrentDateTime;
                hmIssueReporting.Remarks = string.Format("{0}\n\n{1}", hmIssueReporting.Remarks, hmApprovalRequest.Remarks);
                hmIssueReporting.RequestType = RequestType.Edit;
                hmIssueReporting.SetAuditValues(RequestType.Edit);

                results = this.hmIssueReportingRepository.SaveOrUpdateHMIssueReportingDetails(hmIssueReporting, issueApprovalHistory);
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