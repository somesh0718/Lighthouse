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
    /// Manager of the VocationalTrainer entity
    /// </summary>
    public class VocationalTrainerManager : GenericManager<VocationalTrainerModel>, IVocationalTrainerManager
    {
        private readonly IVocationalTrainerRepository vocationalTrainerRepository;
        private readonly IVCSchoolSectorRepository vcSchoolSectorRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vocationalTrainer manager.
        /// </summary>
        /// <param name="_vocationalTrainerRepository"></param>
        /// <param name="_vcSchoolSectorRepository"></param>
        /// <param name="_accountRepository"></param>
        /// <param name="_emailSender"></param>
        /// <param name="_httpContextAccessor"></param>
        public VocationalTrainerManager(IVocationalTrainerRepository _vocationalTrainerRepository, IAccountRepository _accountRepository, IEmailSender _emailSender, IHttpContextAccessor _httpContextAccessor, IVCSchoolSectorRepository _vcSchoolSectorRepository)
        {
            this.vocationalTrainerRepository = _vocationalTrainerRepository;
            this.vcSchoolSectorRepository = _vcSchoolSectorRepository;
            this.accountRepository = _accountRepository;
            this.emailSender = _emailSender;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VocationalTrainers
        /// </summary>
        /// <returns></returns>
        public IQueryable<VocationalTrainerModel> GetVocationalTrainers()
        {
            var vocationalTrainers = this.vocationalTrainerRepository.GetVocationalTrainers();

            IList<VocationalTrainerModel> vocationalTrainerModels = new List<VocationalTrainerModel>();
            vocationalTrainers.ForEach((user) => vocationalTrainerModels.Add(user.ToModel()));

            return vocationalTrainerModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalTrainers by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainerModel> GetVocationalTrainersByName(string vocationalTrainerName)
        {
            var vocationalTrainers = this.vocationalTrainerRepository.GetVocationalTrainersByName(vocationalTrainerName);

            IList<VocationalTrainerModel> vocationalTrainerModels = new List<VocationalTrainerModel>();
            vocationalTrainers.ForEach((user) => vocationalTrainerModels.Add(user.ToModel()));

            return vocationalTrainerModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalTrainer by vtNames
        /// </summary>
        /// <param name="vtNames"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainerModel> GetVocationalTrainersByNames(List<string> vtNames)
        {
            var vocationalTrainers = this.vocationalTrainerRepository.GetVocationalTrainersByNames(vtNames);

            IList<VocationalTrainerModel> vocationalTrainerModels = new List<VocationalTrainerModel>();
            vocationalTrainers.ForEach((user) => vocationalTrainerModels.Add(user.ToModel()));

            return vocationalTrainerModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalTrainer by EmailIds
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainerModel> GetVocationalTrainersByEmails(Guid academicYearId, List<string> emailIds)
        {
            return this.vocationalTrainerRepository.GetVocationalTrainersByEmails(academicYearId, emailIds);
        }

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="VTId"></param>
        /// <returns></returns>
        public VocationalTrainerModel GetVocationalTrainerById(Guid vtId)
        {
            VocationalTrainer vocationalTrainer = this.vocationalTrainerRepository.GetVocationalTrainerById(vtId);

            return (vocationalTrainer != null) ? vocationalTrainer.ToModel() : null;
        }

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <param name="VTId"></param>
        /// <returns></returns>
        public VocationalTrainerModel GetVocationalTrainerById(DataRequest vtRequest)
        {
            VocationalTrainer vocationalTrainer = this.vocationalTrainerRepository.GetVocationalTrainerById(vtRequest);

            return (vocationalTrainer != null) ? vocationalTrainer.ToModel() : null;
        }

        /// <summary>
        /// Get VocationalTrainer by VTId using async
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public async Task<VocationalTrainerModel> GetVocationalTrainerByIdAsync(Guid vtId)
        {
            var vocationalTrainer = await this.vocationalTrainerRepository.GetVocationalTrainerByIdAsync(vtId);

            return (vocationalTrainer != null) ? vocationalTrainer.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VocationalTrainer entity
        /// </summary>
        /// <param name="vocationalTrainerModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVocationalTrainerDetails(VocationalTrainerModel vocationalTrainerModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VocationalTrainer vocationalTrainer = null;

                //Validate model data
                vocationalTrainerModel = vocationalTrainerModel.GetModelValidationErrors<VocationalTrainerModel>();

                if (vocationalTrainerModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vocationalTrainerModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vocationalTrainerModel.RequestType == RequestType.Edit)
                {
                    vocationalTrainer = this.vocationalTrainerRepository.GetVocationalTrainerById(new DataRequest { DataId = vocationalTrainerModel.AcademicYearId.ToString(), DataId1 = vocationalTrainerModel.VTPId.ToString(), DataId2 = vocationalTrainerModel.VCId.ToString(), DataId3 = vocationalTrainerModel.VTId.ToString() });
                }
                else
                {
                    vocationalTrainer = new VocationalTrainer();
                    vocationalTrainerModel.VTId = Guid.NewGuid();
                    vocationalTrainer.VCTrainer.VCTrainerId = Guid.NewGuid();
                }

                if (!vocationalTrainerModel.DateOfResignation.HasValue)
                {
                    var errorMessages = this.vocationalTrainerRepository.CheckVocationalTrainerExistByName(vocationalTrainerModel);
                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    vocationalTrainer.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    Account account = null;
                    bool isVTActivated = vocationalTrainer.VCTrainer.DateOfResignation.HasValue && !vocationalTrainerModel.DateOfResignation.HasValue;
                    bool isNameChanged = (!string.Equals(vocationalTrainerModel.FirstName, vocationalTrainer.FirstName) || !string.Equals(vocationalTrainerModel.LastName, vocationalTrainer.LastName));

                    vocationalTrainer = vocationalTrainerModel.FromModel(vocationalTrainer);

                    //Save Or Update vocationalTrainer details
                    bool isSaved = this.vocationalTrainerRepository.SaveOrUpdateVocationalTrainerDetails(vocationalTrainer);

                    if (isSaved && !string.IsNullOrEmpty(vocationalTrainerModel.Email))
                    {
                        if (vocationalTrainerModel.RequestType == RequestType.New)
                        {
                            string userId = vocationalTrainerModel.Email.Substring(0, vocationalTrainerModel.Email.IndexOf("@"));

                            Role roleItem = this.accountRepository.GetRoleByCode("VT");

                            account = new Account();
                            account.AccountId = vocationalTrainerModel.VTId;
                            account.LoginId = vocationalTrainerModel.Email;
                            account.Password = CryptographyManager.Encrypt(Constants.DefaultAppPwd, true);
                            account.UserId = this.accountRepository.GetUniqueUserId(userId);
                            account.UserName = string.Format("{0} {1}", vocationalTrainerModel.FirstName, vocationalTrainerModel.LastName);
                            account.FirstName = vocationalTrainerModel.FirstName;
                            account.LastName = vocationalTrainerModel.LastName;
                            account.Designation = roleItem.Name;
                            account.EmailId = vocationalTrainerModel.Email;
                            account.Mobile = vocationalTrainerModel.Mobile;
                            account.AccountType = roleItem.Name;
                            account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);
                            account.InvalidAttempt = 0;
                            account.IsPasswordReset = false;
                            account.IsLocked = false;
                            account.IsActive = true;
                            account.RequestType = vocationalTrainerModel.RequestType;
                            account.AuthUserId = vocationalTrainer.AuthUserId;

                            account.SetAuditValues(vocationalTrainerModel.RequestType);
                            this.accountRepository.SaveOrUpdateAccountDetails(account);

                            AccountRole accountRole = new AccountRole();
                            accountRole.AccountRoleId = Guid.NewGuid();
                            accountRole.AccountId = account.AccountId;
                            accountRole.RoleId = roleItem.RoleId;
                            accountRole.RequestType = RequestType.New;
                            accountRole.AuthUserId = vocationalTrainer.AuthUserId;
                            accountRole.IsActive = account.IsActive;

                            accountRole.SetAuditValues(vocationalTrainerModel.RequestType);
                            this.accountRepository.SaveOrUpdateAccountRoleDetails(accountRole);

                            try
                            {
                                string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : account.LoginId;
                                string subject = string.Format("Created {0} account - {1} for Lighthouse {2}", roleItem.Name, account.LoginId, Constants.StateCode);

                                StringBuilder sbNewUserTemplate = this.GetNewUserCreationTemplate(account);
                                Message message = new Message(new string[] { toEmailId }, subject, sbNewUserTemplate.ToString(), null);

                                this.emailSender.SendEmail(message);
                            }
                            catch (Exception ex)
                            {
                                Logging.ErrorManager.Instance.WriteErrorLogsInFile(ex, "BAL > SaveOrUpdateVocationalTrainerDetails>Sending Email");
                            }
                        }
                        else if (vocationalTrainerModel.RequestType == RequestType.Edit)
                        {
                            if (isNameChanged || vocationalTrainerModel.DateOfResignation.HasValue || isVTActivated)
                            {
                                account = this.accountRepository.GetAccountById(vocationalTrainer.VTId);
                                if (account != null)
                                {
                                    account.FirstName = vocationalTrainerModel.FirstName;
                                    account.LastName = vocationalTrainerModel.LastName;
                                    account.UserName = string.Format("{0} {1}", vocationalTrainerModel.FirstName, vocationalTrainerModel.LastName);

                                    account.AuthUserId = vocationalTrainer.AuthUserId;
                                    account.IsActive = vocationalTrainer.IsActive;

                                    if (vocationalTrainerModel.DateOfResignation.HasValue)
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

                            if (!bool.Equals(vocationalTrainer.IsActive, vocationalTrainerModel.IsActive) || vocationalTrainerModel.DateOfResignation.HasValue || isVTActivated)
                            {
                                this.vocationalTrainerRepository.InactiveVTRelatedDataWhenResigned(vocationalTrainer);
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
                throw new Exception("BAL > SaveOrUpdateVocationalTrainerDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtId)
        {
            return this.vocationalTrainerRepository.DeleteById(vtId);
        }

        /// <summary>
        /// Check duplicate VocationalTrainer by Name
        /// </summary>
        /// <param name="vocationalTrainerModel"></param>
        /// <returns></returns>
        public List<string> CheckVocationalTrainerExistByName(VocationalTrainerModel vocationalTrainerModel)
        {
            return this.vocationalTrainerRepository.CheckVocationalTrainerExistByName(vocationalTrainerModel);
        }

        /// <summary>}
        /// List of VocationalTrainer with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VocationalTrainerViewModel> GetVocationalTrainersByCriteria(SearchVocationalTrainerModel searchModel)
        {
            return this.vocationalTrainerRepository.GetVocationalTrainersByCriteria(searchModel);
        }

        /// <summary>
        /// Get list of VT Ids by School Ids
        /// </summary>
        /// <param name="schoolIds"></param>
        /// <returns></returns>
        public Dictionary<Guid, Guid> GetVTIdsBySchoolIds(List<Guid> schoolIds)
        {
            return this.vocationalTrainerRepository.GetVTIdsBySchoolIds(schoolIds);
        }

        /// <summary>}
        /// Transfer a VT to another School
        /// </summary>}
        /// <param name="vtTransferRequest"></param>}
        /// <returns></returns>}
        public SingularResponse<string> VTTransfer(VTTransferRequest vtTransferRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            var vcschoolSector = this.vcSchoolSectorRepository.GetSchoolSectorByVCId(vtTransferRequest.ToVCId);
            if (vcschoolSector != null)
            {
                this.vocationalTrainerRepository.SaveOrUpdateVCTrainerMapById(vtTransferRequest);

                string res = this.vocationalTrainerRepository.TransferOldVTToNewVT(vtTransferRequest);
                response.Messages.Add(res);
                Console.WriteLine(res);
            }
            else
            {
                response.Errors.Add(string.Format("VC does not map with this selected school"));
            }

            return response;
        }

        /// <summary>
        /// List of School Students by VT
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<SchoolStudentModel> GetSchoolStudentsByVTId(Guid academicYearId, Guid vtId)
        {
            return this.vocationalTrainerRepository.GetSchoolStudentsByVTId(academicYearId, vtId);
        }
    }
}