using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.Cryptography;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.EmailServices;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the HeadMaster entity
    /// </summary>
    public class HeadMasterManager : GenericManager<HeadMasterModel>, IHeadMasterManager
    {
        private readonly IHeadMasterRepository headMasterRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ICommonRepository commonRepository;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the headMaster manager.
        /// </summary>
        /// <param name="headMasterRepository"></param>
        /// <param name="_accountRepository"></param>
        /// <param name="_emailSender"></param>
        /// <param name="_httpContextAccessor"></param>
        public HeadMasterManager(IHeadMasterRepository _headMasterRepository, IAccountRepository _accountRepository, ICommonRepository _commonRepository, IEmailSender _emailSender, IHttpContextAccessor _httpContextAccessor)
        {
            this.headMasterRepository = _headMasterRepository;
            this.accountRepository = _accountRepository;
            this.commonRepository = _commonRepository;
            this.emailSender = _emailSender;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of HeadMasters
        /// </summary>
        /// <returns></returns>
        public IQueryable<HeadMasterModel> GetHeadMasters()
        {
            var headMasters = this.headMasterRepository.GetHeadMasters();

            IList<HeadMasterModel> headMasterModels = new List<HeadMasterModel>();
            headMasters.ForEach((user) => headMasterModels.Add(user.ToModel()));

            return headMasterModels.AsQueryable();
        }

        /// <summary>
        /// Get list of HeadMasters by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<HeadMasterModel> GetHeadMastersByName(string headMasterName)
        {
            var headMasters = this.headMasterRepository.GetHeadMastersByName(headMasterName);

            IList<HeadMasterModel> headMasterModels = new List<HeadMasterModel>();
            headMasters.ForEach((user) => headMasterModels.Add(user.ToModel()));

            return headMasterModels.AsQueryable();
        }

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="HMId"></param>
        /// <returns></returns>
        public HeadMasterModel GetHeadMasterById(Guid hmId)
        {
            HeadMasterModel headMasterModel = null;
            HeadMaster headMaster = this.headMasterRepository.GetHeadMasterById(hmId);

            if (headMaster != null)
            {
                headMasterModel = headMaster.ToModel();
            }

            return headMasterModel;
        }

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="HMId"></param>
        /// <returns></returns>
        public HeadMasterModel GetHeadMasterById(DataRequest hmRequest)
        {
            HeadMasterModel headMasterModel = null;
            HeadMaster headMaster = this.headMasterRepository.GetHeadMasterById(hmRequest);

            if (headMaster != null)
            {
                headMasterModel = headMaster.ToModel();
                LhUserModel userModel = this.commonRepository.GetVTPandVCIdBySchoolId(headMaster.HMSchool.SchoolId);

                if (userModel != null)
                {
                    headMasterModel.VTPId = userModel.VTPId;
                    headMasterModel.VCId = userModel.VCId;
                }
            }

            return headMasterModel;
        }

        /// <summary>
        /// Get HeadMaster by HMId using async
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public async Task<HeadMasterModel> GetHeadMasterByIdAsync(Guid hmId)
        {
            var headMaster = await this.headMasterRepository.GetHeadMasterByIdAsync(hmId);

            return (headMaster != null) ? headMaster.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update HeadMaster entity
        /// </summary>
        /// <param name="headMasterModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateHeadMasterDetails(HeadMasterModel headMasterModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                HeadMaster headMaster = null;

                //Validate model data
                headMasterModel = headMasterModel.GetModelValidationErrors<HeadMasterModel>();

                if (headMasterModel.ErrorMessages.Count > 0)
                {
                    response.Errors = headMasterModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (headMasterModel.RequestType == RequestType.Edit)
                {
                    headMaster = this.headMasterRepository.GetHeadMasterById(new DataRequest { DataId = headMasterModel.AcademicYearId.ToString(), DataId1 = headMasterModel.SchoolId.ToString(), DataId2 = headMasterModel.HMId.ToString() });
                }
                else
                {
                    headMaster = new HeadMaster();
                    headMasterModel.HMId = Guid.NewGuid();
                    headMaster.HMSchool.HMSchoolId = Guid.NewGuid();
                }

                if (!headMasterModel.DateOfResignation.HasValue)
                {
                    var errorMessages = this.headMasterRepository.CheckHeadMasterExistByName(headMaster, headMasterModel);

                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    headMaster.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    Account account = null;
                    bool isVTActivated = headMaster.HMSchool.DateOfResignation.HasValue && !headMasterModel.DateOfResignation.HasValue;
                    bool isNameChanged = (!string.Equals(headMasterModel.FirstName, headMaster.FirstName) || !string.Equals(headMasterModel.LastName, headMaster.LastName));

                    headMaster = headMasterModel.FromModel(headMaster);

                    //Save Or Update headMaster details
                    bool isSaved = this.headMasterRepository.SaveOrUpdateHeadMasterDetails(headMaster);

                    if (isSaved && !string.IsNullOrEmpty(headMasterModel.Email))
                    {
                        if (headMasterModel.RequestType == RequestType.New)
                        {
                            string userId = headMasterModel.Email.Substring(0, headMasterModel.Email.IndexOf("@"));

                            Role roleItem = this.accountRepository.GetRoleByCode("HM");

                            account = new Account();
                            account.AccountId = headMasterModel.HMId;
                            account.LoginId = headMasterModel.Email;
                            account.Password = CryptographyManager.Encrypt(Constants.DefaultAppPwd, true);
                            account.UserId = this.accountRepository.GetUniqueUserId(userId);
                            account.UserName = string.Format("{0} {1}", headMasterModel.FirstName, headMasterModel.LastName);
                            account.FirstName = headMasterModel.FirstName;
                            account.LastName = headMasterModel.LastName;
                            account.Designation = roleItem.Name;
                            account.EmailId = headMasterModel.Email;
                            account.Mobile = headMasterModel.Mobile;
                            account.AccountType = roleItem.Name;
                            account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);
                            account.InvalidAttempt = 0;
                            account.IsPasswordReset = false;
                            account.IsLocked = false;
                            account.IsActive = headMaster.IsActive;
                            account.RequestType = headMasterModel.RequestType;
                            account.AuthUserId = headMaster.AuthUserId;

                            account.SetAuditValues(headMasterModel.RequestType);
                            this.accountRepository.SaveOrUpdateAccountDetails(account);

                            AccountRole accountRole = new AccountRole();
                            accountRole.AccountRoleId = Guid.NewGuid();
                            accountRole.AccountId = account.AccountId;
                            accountRole.RoleId = roleItem.RoleId;
                            accountRole.RequestType = RequestType.New;
                            accountRole.AuthUserId = headMaster.AuthUserId;
                            accountRole.IsActive = account.IsActive;

                            accountRole.SetAuditValues(headMasterModel.RequestType);
                            this.accountRepository.SaveOrUpdateAccountRoleDetails(accountRole);

                            try
                            {
                                string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : account.LoginId;
                                string subject = string.Format("Created {0} account - {1} for Lighthouse {2}", roleItem.Name, account.LoginId, Constants.StateCode);

                                StringBuilder sbNewUserTemplate = this.GetNewUserCreationTemplate(account);
                                Message message = new Message(new string[] { toEmailId }, subject, sbNewUserTemplate.ToString(), null);

                                this.emailSender.SendEmailAsync(message);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("BAL > SaveOrUpdateHeadMasterDetails>Sending Email", ex);
                            }
                        }
                        else if (headMasterModel.RequestType == RequestType.Edit)
                        {
                            if (isNameChanged || headMasterModel.DateOfResignation.HasValue || isVTActivated)
                            {
                                account = this.accountRepository.GetAccountById(headMaster.HMId);
                                if (account != null)
                                {
                                    account.FirstName = headMasterModel.FirstName;
                                    account.LastName = headMasterModel.LastName;
                                    account.UserName = string.Format("{0} {1}", headMasterModel.FirstName, headMasterModel.LastName);

                                    account.AuthUserId = headMaster.AuthUserId;
                                    account.IsActive = headMaster.IsActive;

                                    if (headMasterModel.DateOfResignation.HasValue)
                                    {
                                        account.IsLocked = true;
                                        account.PasswordExpiredOn = Constants.GetCurrentDateTime;
                                    }
                                    else
                                    {
                                        account.IsLocked = false;
                                        account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);
                                    }

                                    account.RequestType = RequestType.Edit;
                                    account.SetAuditValues(RequestType.Edit);

                                    this.accountRepository.SaveOrUpdateAccountDetails(account);
                                }
                            }
                        }
                    }

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
                throw new Exception("BAL > SaveOrUpdateHeadMasterDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid hmId)
        {
            return this.headMasterRepository.DeleteById(hmId);
        }

        /// <summary>}
        /// List of HeadMaster with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<HeadMasterViewModel> GetHeadMastersByCriteria(SearchHeadMasterModel searchModel)
        {
            return this.headMasterRepository.GetHeadMastersByCriteria(searchModel);
        }
    }
}