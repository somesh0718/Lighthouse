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
    /// Manager of the VocationalCoordinator entity
    /// </summary>
    public class VocationalCoordinatorManager : GenericManager<VocationalCoordinatorModel>, IVocationalCoordinatorManager
    {
        private readonly IVocationalCoordinatorRepository vocationalCoordinatorRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vocationalCoordinator manager.
        /// </summary>
        /// <param name="vocationalCoordinatorRepository"></param>
        /// <param name="_accountRepository"></param>
        /// <param name="_emailSender"></param>
        /// <param name="_httpContextAccessor"></param>
        public VocationalCoordinatorManager(IVocationalCoordinatorRepository _vocationalCoordinatorRepository, IAccountRepository _accountRepository, IEmailSender _emailSender, IHttpContextAccessor _httpContextAccessor)
        {
            this.vocationalCoordinatorRepository = _vocationalCoordinatorRepository;
            this.accountRepository = _accountRepository;
            this.emailSender = _emailSender;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VocationalCoordinators
        /// </summary>
        /// <returns></returns>
        public IQueryable<VocationalCoordinatorModel> GetVocationalCoordinators()
        {
            var vocationalCoordinators = this.vocationalCoordinatorRepository.GetVocationalCoordinators();

            IList<VocationalCoordinatorModel> vocationalCoordinatorModels = new List<VocationalCoordinatorModel>();
            vocationalCoordinators.ForEach((user) => vocationalCoordinatorModels.Add(user.ToModel()));

            return vocationalCoordinatorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalCoordinators by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByName(string vocationalCoordinatorName)
        {
            var vocationalCoordinators = this.vocationalCoordinatorRepository.GetVocationalCoordinatorsByName(vocationalCoordinatorName);

            IList<VocationalCoordinatorModel> vocationalCoordinatorModels = new List<VocationalCoordinatorModel>();
            vocationalCoordinators.ForEach((user) => vocationalCoordinatorModels.Add(user.ToModel()));

            return vocationalCoordinatorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalCoordinators by VC Names
        /// </summary>
        /// <param name="vcNames"></param>
        /// <returns></returns>
        public IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByNames(List<string> vcNames)
        {
            var vocationalCoordinators = this.vocationalCoordinatorRepository.GetVocationalCoordinatorsByNames(vcNames);

            IList<VocationalCoordinatorModel> vocationalCoordinatorModels = new List<VocationalCoordinatorModel>();
            vocationalCoordinators.ForEach((user) => vocationalCoordinatorModels.Add(user.ToModel()));

            return vocationalCoordinatorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalCoordinators by EmailIds
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        public IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByEmails(Guid academicYearId, List<string> emailIds)
        {
            return this.vocationalCoordinatorRepository.GetVocationalCoordinatorsByEmails(academicYearId, emailIds);
        }

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="VCId"></param>
        /// <returns></returns>
        public VocationalCoordinatorModel GetVocationalCoordinatorById(Guid vcId)
        {
            VocationalCoordinator vocationalCoordinator = this.vocationalCoordinatorRepository.GetVocationalCoordinatorById(vcId);

            return (vocationalCoordinator != null) ? vocationalCoordinator.ToModel() : null;
        }

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        public VocationalCoordinatorModel GetVocationalCoordinatorById(DataRequest vcRequest)
        {
            VocationalCoordinator vocationalCoordinator = this.vocationalCoordinatorRepository.GetVocationalCoordinatorById(vcRequest);

            return (vocationalCoordinator != null) ? vocationalCoordinator.ToModel() : null;
        }

        /// <summary>
        /// Get VocationalCoordinator by VCId using async
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public async Task<VocationalCoordinatorModel> GetVocationalCoordinatorByIdAsync(Guid vcId)
        {
            var vocationalCoordinator = await this.vocationalCoordinatorRepository.GetVocationalCoordinatorByIdAsync(vcId);

            return (vocationalCoordinator != null) ? vocationalCoordinator.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VocationalCoordinator entity
        /// </summary>
        /// <param name="vocationalCoordinatorModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVocationalCoordinatorDetails(VocationalCoordinatorModel vocationalCoordinatorModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VocationalCoordinator vocationalCoordinator = null;

                //Validate model data
                vocationalCoordinatorModel = vocationalCoordinatorModel.GetModelValidationErrors<VocationalCoordinatorModel>();

                if (vocationalCoordinatorModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vocationalCoordinatorModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vocationalCoordinatorModel.RequestType == RequestType.Edit)
                {
                    vocationalCoordinator = this.vocationalCoordinatorRepository.GetVocationalCoordinatorById(new DataRequest { DataId = vocationalCoordinatorModel.AcademicYearId.ToString(), DataId1 = vocationalCoordinatorModel.VTPId.ToString(), DataId2 = vocationalCoordinatorModel.VCId.ToString() });
                }
                else
                {
                    vocationalCoordinator = new VocationalCoordinator();
                    vocationalCoordinatorModel.VCId = Guid.NewGuid();
                    vocationalCoordinator.VTPCoordinator.VTPCoordinatorId = Guid.NewGuid();
                }

                if (!vocationalCoordinatorModel.DateOfResignation.HasValue)
                {
                    var errorMessages = this.vocationalCoordinatorRepository.CheckVocationalCoordinatorExistByName(vocationalCoordinatorModel);
                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vocationalCoordinator.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    Account account = null;
                    bool isVCActivated = vocationalCoordinator.VTPCoordinator.DateOfResignation.HasValue && !vocationalCoordinatorModel.DateOfResignation.HasValue;
                    bool isNameChanged = (!string.Equals(vocationalCoordinatorModel.FirstName, vocationalCoordinator.FirstName) || !string.Equals(vocationalCoordinatorModel.LastName, vocationalCoordinator.LastName));

                    vocationalCoordinator = vocationalCoordinatorModel.FromModel(vocationalCoordinator);

                    //Save Or Update vocationalCoordinator details
                    bool isSaved = this.vocationalCoordinatorRepository.SaveOrUpdateVocationalCoordinatorDetails(vocationalCoordinator);

                    if (isSaved)
                    {
                        if (vocationalCoordinatorModel.RequestType == RequestType.New)
                        {
                            string userId = vocationalCoordinatorModel.EmailId.Substring(0, vocationalCoordinatorModel.EmailId.IndexOf("@"));
                            Role roleItem = this.accountRepository.GetRoleByCode("VC");

                            account = new Account();
                            account.AccountId = vocationalCoordinator.VCId;
                            account.LoginId = vocationalCoordinatorModel.EmailId;
                            account.Password = CryptographyManager.Encrypt(Constants.DefaultAppPwd, true);
                            account.UserId = this.accountRepository.GetUniqueUserId(userId);
                            account.UserName = string.Format("{0} {1}", vocationalCoordinatorModel.FirstName, vocationalCoordinatorModel.LastName);
                            account.FirstName = vocationalCoordinatorModel.FirstName;
                            account.LastName = vocationalCoordinatorModel.LastName;
                            account.Designation = roleItem.Name;
                            account.EmailId = vocationalCoordinatorModel.EmailId;
                            account.Mobile = vocationalCoordinatorModel.Mobile;
                            account.AccountType = roleItem.Name;
                            account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);
                            account.InvalidAttempt = 0;
                            account.IsPasswordReset = false;
                            account.IsLocked = false;
                            account.IsActive = vocationalCoordinator.IsActive;
                            account.RequestType = vocationalCoordinatorModel.RequestType;
                            account.AuthUserId = vocationalCoordinator.AuthUserId;

                            account.SetAuditValues(vocationalCoordinatorModel.RequestType);
                            this.accountRepository.SaveOrUpdateAccountDetails(account);

                            AccountRole accountRole = new AccountRole();
                            accountRole.AccountRoleId = Guid.NewGuid();
                            accountRole.AccountId = account.AccountId;
                            accountRole.RoleId = roleItem.RoleId;
                            accountRole.RequestType = RequestType.New;
                            accountRole.AuthUserId = vocationalCoordinator.AuthUserId;
                            accountRole.IsActive = account.IsActive;

                            accountRole.SetAuditValues(vocationalCoordinatorModel.RequestType);
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
                                throw new Exception("BAL > SaveOrUpdateVocationalCoordinatorDetails>Sending Email", ex);
                            }
                        }
                        else if (vocationalCoordinatorModel.RequestType == RequestType.Edit)
                        {
                            if (isNameChanged || vocationalCoordinatorModel.DateOfResignation.HasValue || isVCActivated)
                            {
                                account = this.accountRepository.GetAccountById(vocationalCoordinator.VCId);
                                if (account != null)
                                {
                                    account.FirstName = vocationalCoordinatorModel.FirstName;
                                    account.LastName = vocationalCoordinatorModel.LastName;
                                    account.UserName = string.Format("{0} {1}", vocationalCoordinatorModel.FirstName, vocationalCoordinatorModel.LastName);

                                    account.AuthUserId = vocationalCoordinator.AuthUserId;
                                    account.IsActive = vocationalCoordinator.IsActive;

                                    if (vocationalCoordinatorModel.DateOfResignation.HasValue)
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

                            if (!bool.Equals(vocationalCoordinator.IsActive, vocationalCoordinatorModel.IsActive) || vocationalCoordinatorModel.DateOfResignation.HasValue || isVCActivated)
                            {
                                this.vocationalCoordinatorRepository.InactiveVCRelatedDataWhenResigned(vocationalCoordinator);
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
                throw new Exception("BAL > SaveOrUpdateVocationalCoordinatorDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VCId
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcId)
        {
            return this.vocationalCoordinatorRepository.DeleteById(vcId);
        }

        /// <summary>
        /// Check duplicate VocationalCoordinator by Name
        /// </summary>
        /// <param name="vocationalCoordinatorModel"></param>
        /// <returns></returns>
        public List<string> CheckVocationalCoordinatorExistByName(VocationalCoordinatorModel vocationalCoordinatorModel)
        {
            return this.vocationalCoordinatorRepository.CheckVocationalCoordinatorExistByName(vocationalCoordinatorModel);
        }

        /// <summary>}
        /// List of VocationalCoordinator with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VocationalCoordinatorViewModel> GetVocationalCoordinatorsByCriteria(SearchVocationalCoordinatorModel searchModel)
        {
            return this.vocationalCoordinatorRepository.GetVocationalCoordinatorsByCriteria(searchModel);
        }

        /// <summary>
        /// Get list of VocationalCoordinator
        /// </summary>
        /// <returns></returns>
        public IList<VocationalCoordinatorModel> GetVCList()
        {
            return this.vocationalCoordinatorRepository.GetVCList();
        }

        /// <summary>
        /// Get VC Schools By VTP And VC Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        public IList<VCSchoolModel> GetVCSchoolsByVTPAndVCId(DataRequest schoolRequest)
        {
            return this.vocationalCoordinatorRepository.GetVCSchoolsByVTPAndVCId(schoolRequest);
        }

        /// <summary>
        /// Save VC Transfers data
        /// </summary>
        /// <param name="vcSchoolTransferRequest"></param>
        /// <returns></returns>
        public VCSchoolTransferModel SaveVCTransfers(VCSchoolTransferModel vcSchoolTransferRequest)
        {
            return this.vocationalCoordinatorRepository.SaveVCTransfers(vcSchoolTransferRequest);
        }
    }
}