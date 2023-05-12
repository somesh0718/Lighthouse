//using Igmite.Lighthouse.BAL.Mailer;
using Igmite.Lighthouse.Cryptography;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers.Providers
{
    public class UserManager : GenericManager<AccountModel>, IUserManager
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the person manager class.
        /// </summary>
        /// <param name="_accountRepository"></param>
        /// <param name="_userRepository"></param>
        /// <param name="_roleRepository"></param>
        public UserManager(IAccountRepository _accountRepository, IUserRepository _userRepository, IRoleRepository _roleRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.accountRepository = _accountRepository;
            this.userRepository = _userRepository;
            this.roleRepository = _roleRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        public AccountModel GetAccountByLoginId(string loginId)
        {
            AccountModel accountModel = null;
            Account account = this.accountRepository.GetAccountByLoginId(loginId);

            if (account != null)
            {
                accountModel = account.ToModel();
            }

            return accountModel;
        }

        public AccountModel GetAccountByUserId(string userId)
        {
            AccountModel accountModel = null;
            Account account = this.userRepository.GetAccountByUserId(userId);

            if (account != null)
            {
                accountModel = account.ToModel();
            }

            return accountModel;
        }

        public bool CheckExistAccountByUserId(string userId)
        {
            return this.userRepository.CheckExistAccountByUserId(userId);
        }

        public AccountModel GetAccountByEmailId(string emailId)
        {
            AccountModel accountModel = null;
            Account account = this.userRepository.GetAccountByEmailId(emailId);

            if (account != null)
            {
                accountModel = account.ToModel();
            }

            return accountModel;
        }

        public bool CheckExistAccountByEmailId(string emailId)
        {
            return this.userRepository.CheckExistAccountByEmailId(emailId);
        }

        public async Task<string> GeneratePasswordResetToken(string emailId)
        {
            Account account = this.userRepository.GetAccountByEmailId(emailId);

            string takenCode = StringUtility.GetUniqueKey(8);
            string tokenValue = CryptographyManager.ComputeHash(takenCode, string.Empty, CryptographyManager.HashName.SHA1);
            string encryptAccountId = CryptographyManager.Encrypt(account.Mobile, true);

            account.PasswordResetToken = string.Format("{0}-{1}", tokenValue, account.Mobile);
            account.TokenExpiredOn = Constants.GetCurrentDateTime.AddMinutes(120);

            account.SetAuditValues<Account>(RequestType.Edit);

            Task<int> resultTask = this.userRepository.ResetPasswordAsync(account);

            int rowCount = await resultTask;

            return rowCount > 0 ? account.PasswordResetToken : string.Empty;
        }

        public ResetPasswordModel GetAccountByVerifyCode(string verifyCode)
        {
            string token = verifyCode.Split('-')[0];
            string userId = verifyCode.Split('-')[1];

            var accountModel = new ResetPasswordModel();
            Account account = this.userRepository.GetAccountByUserId(userId);

            if (account != null)
            {
                int expireToken = DateTime.Compare(account.TokenExpiredOn.Value, Constants.GetCurrentDateTime);

                accountModel.EmailId = account.EmailId;
                accountModel.VerifyCode = verifyCode;
                accountModel.IsValidTokenCode = expireToken == 1;
            }

            return accountModel;
        }

        public bool InActiveByID(string mobile)
        {
            return this.userRepository.InActiveByID(mobile);
        }

        public ForgotPasswordModel SetForgotPasswordAdmin(string emailIdOrMobile)
        {
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();

            Account account = this.userRepository.GetAccountByEmailOrMobileId(emailIdOrMobile);

            if (account != null)
            {
                string uniquePassword = StringUtility.GetUniqueKey(8);
                uniquePassword = uniquePassword.Substring(0, 1).ToUpper() + uniquePassword.Substring(1, uniquePassword.Length - 1).Insert(3, "$");
                account.Password = CryptographyManager.Encrypt(uniquePassword, true);

                this.userRepository.ResetPassword(account);

                if (!string.IsNullOrEmpty(account.Mobile) && account.Mobile.Length >= 10)
                {
                    string smsMessage = string.Format("Your new password is {0} for {1}", uniquePassword, account.EmailId);
                    SmsServiceProvider smsServiceProvider = new SmsServiceProvider();
                    bool smsServiceUrl = smsServiceProvider.SendSMSToMobile(account.Mobile, smsMessage);
                }

                //Send error email in asynchronously.
                if (!string.IsNullOrEmpty(account.EmailId))
                {
                    BlogPasswordModel lighthousePasswordModel = new BlogPasswordModel();
                    lighthousePasswordModel.EmailModel = new EmailModel();
                    lighthousePasswordModel.EmailModel.ToEmailId = account.EmailId;
                    lighthousePasswordModel.EmailModel.Subject = "Reset your user portal login password";
                    lighthousePasswordModel.Password = uniquePassword;

                    //                    LighthouseMailer lighthouseMailer = new LighthouseMailer();
                    //                    lighthouseMailer.Email("SendResetPassword", lighthousePasswordModel).Deliver();
                    //Task.Run(() => lighthouseMailer.Email("DisplayError", lighthousePasswordModel).Deliver());
                }
            }
            else
            {
                forgotPasswordModel.ErrorMessages.Add("user login does not exist");
            }

            return forgotPasswordModel;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordModel accountModel)
        {
            Account account = this.userRepository.GetAccountByEmailId(accountModel.EmailId);
            account.Password = CryptographyManager.Encrypt(accountModel.Password, true);
            account.TokenExpiredOn = Constants.GetCurrentDateTime;
            account.IsPasswordReset = false;

            account.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
            account.SetAuditValues<Account>(RequestType.Edit);

            Task<int> resultTask = this.userRepository.ResetPasswordAsync(account);

            int rowCount = await resultTask;

            return rowCount > 0;
        }

        public SingularResponse<string> ForgotPassword(string mobile)
        {
            SingularResponse<string> forgotPasswordModel = new SingularResponse<string>();

            if (mobile.Length != 10)
            {
                forgotPasswordModel.Errors.Add("Please enter valid 10 digits Mobile Number");
                forgotPasswordModel.HttpStatus = HttpStatusCode.BadRequest;
                forgotPasswordModel.Success = false;
                return forgotPasswordModel;
            }
            else if (!Regex.IsMatch(mobile, "^[0-9]*$"))
            {
                forgotPasswordModel.Errors.Add("Mobile number accepts only numbers");
                forgotPasswordModel.HttpStatus = HttpStatusCode.BadRequest;
                forgotPasswordModel.Success = false;
                return forgotPasswordModel;
            }

            Account account = this.userRepository.GetAccountByMobile(mobile);

            if (account != null)
            {
                string uniquePassword = StringUtility.GetUniqueKey(8);
                uniquePassword = uniquePassword.Substring(0, 1).ToUpper() + uniquePassword.Substring(1, uniquePassword.Length - 1).Insert(3, "$");
                account.Password = CryptographyManager.Encrypt(uniquePassword, true);

                this.userRepository.ResetPassword(account);

                string smsMessage = string.Format("Your new password is {0}", uniquePassword);
                SmsServiceProvider smsServiceProvider = new SmsServiceProvider();
                bool smsServiceUrl = smsServiceProvider.SendSMSToMobile(mobile, smsMessage);

                forgotPasswordModel.Result = "Sent new password";
                forgotPasswordModel.Messages.Add(string.Format("New password: {0} has heen sent to your mobile", uniquePassword));
                forgotPasswordModel.HttpStatus = HttpStatusCode.OK;
                forgotPasswordModel.Success = true;
            }
            else
            {
                forgotPasswordModel.Errors.Add("user does not exist");
                forgotPasswordModel.HttpStatus = HttpStatusCode.BadRequest;
                forgotPasswordModel.Success = false;
            }

            return forgotPasswordModel;
        }

        private bool ValidateAccount()
        {
            return false;
        }

        public bool SaveUserRegistrationDetails(UserRegistrationModel userRegistrationModel)
        {
            AccountModel accountModel = userRegistrationModel.ToUserRegistrationModel();
            bool result = this.accountRepository.SaveOrUpdateAccountDetails(accountModel);

            if (result)
            {
                UserOTPDetail otpDetails = this.userRepository.GetOTPDetailByID(userRegistrationModel.Mobile, userRegistrationModel.VerifyCode);
                otpDetails.ExpireOn = Constants.GetCurrentDateTime;
                otpDetails.IsRedeemed = true;

                otpDetails.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                otpDetails.SetAuditValues(RequestType.Edit);

                this.userRepository.UpdateOTPDetails(otpDetails);

                // Send user id and password to new user
                if (!string.IsNullOrEmpty(userRegistrationModel.Mobile) && userRegistrationModel.Mobile.Length >= 10)
                {
                    string smsMessage = string.Format("Your password is {0} for {1} and {2}", userRegistrationModel.Password, userRegistrationModel.EmailId, userRegistrationModel.Mobile);
                    SmsServiceProvider smsServiceProvider = new SmsServiceProvider();
                    bool smsServiceUrl = smsServiceProvider.SendSMSToMobile(userRegistrationModel.Mobile, smsMessage);
                }

                //Send new account details in asynchronously.
                if (!string.IsNullOrEmpty(userRegistrationModel.EmailId))
                {
                    BlogPasswordModel blogPasswordModel = new BlogPasswordModel();
                    blogPasswordModel.EmailModel = new EmailModel();
                    blogPasswordModel.EmailModel.ToEmailId = userRegistrationModel.EmailId;
                    blogPasswordModel.EmailModel.Subject = "New Account created for " + userRegistrationModel.UserName;
                    blogPasswordModel.Password = userRegistrationModel.Password;

                    //                    LighthouseMailer lighthouseMailer = new LighthouseMailer();
                    //                    lighthouseMailer.Email("SendResetPassword", blogPasswordModel).Deliver();
                    //Task.Run(() => lighthouseMailer.Email("DisplayError", lighthousePasswordModel).Deliver());
                }
            }

            return result;
        }

        public SingularResponse<OTPResponseModel> SendOTPNumber(OTPRequestModel otpRequest)
        {
            SingularResponse<OTPResponseModel> otpModel = new SingularResponse<OTPResponseModel>();

            if (otpRequest.Mobile.Length != 10)
            {
                otpModel.Errors.Add("Please enter valid 10 digits Mobile Number");
                otpModel.HttpStatus = HttpStatusCode.BadRequest;
                otpModel.Success = false;
                return otpModel;
            }
            else if (!Regex.IsMatch(otpRequest.Mobile, "^[0-9]*$"))
            {
                otpModel.Errors.Add("Mobile number accepts only numbers");
                otpModel.HttpStatus = HttpStatusCode.BadRequest;
                otpModel.Success = false;
                return otpModel;
            }

            UserOTPDetail otpDetails = new UserOTPDetail
            {
                OTPId = Guid.NewGuid(),
                Mobile = otpRequest.Mobile,
                OTPToken = StringUtility.GetOTPToken(6),
                ExpireOn = Constants.GetCurrentDateTime.AddMinutes(Constants.OTPExpireInMinutes),
                IsRedeemed = false
            };

            otpDetails.SetAuditValues(RequestType.New);

            this.userRepository.GenerateOTP(otpDetails);

            string smsMessage = string.Format("Your OTP is {0}", otpDetails.OTPToken);
            SmsServiceProvider smsServiceProvider = new SmsServiceProvider();
            bool smsServiceUrl = smsServiceProvider.SendSMSToMobile(otpRequest.Mobile, smsMessage);

            otpModel.Result = new OTPResponseModel
            {
                Mobile = otpDetails.Mobile,
                OTPToken = otpDetails.OTPToken,
                ExpireOn = otpDetails.ExpireOn
            };

            otpModel.Messages.Add(string.Format("OTP ({0}) sent to given mobile number", otpDetails.OTPToken));
            otpModel.HttpStatus = HttpStatusCode.OK;
            otpModel.Success = true;

            return otpModel;
        }

        public SingularResponse<bool> ValidateOTPNumber(OTPRequestModel otpRequest)
        {
            SingularResponse<bool> otpResponse = new SingularResponse<bool>();

            if (otpRequest.Mobile.Length != 10)
            {
                otpResponse.Errors.Add("Please enter valid 10 digits Mobile Number");
                otpResponse.HttpStatus = HttpStatusCode.BadRequest;
                otpResponse.Success = false;
                return otpResponse;
            }
            else if (!Regex.IsMatch(otpRequest.Mobile, "^[0-9]*$"))
            {
                otpResponse.Errors.Add("Mobile number accepts only numbers");
                otpResponse.HttpStatus = HttpStatusCode.BadRequest;
                otpResponse.Success = false;
                return otpResponse;
            }

            UserOTPDetail otmDetail = this.userRepository.GetOTPDetailByID(otpRequest.Mobile, otpRequest.OTPToken);

            if (otmDetail != null && !otmDetail.IsRedeemed)
            {
                otpResponse.Messages.Add(string.Format("OTP ({0}) has been validated sucessfully.", otpRequest.OTPToken));
                otpResponse.HttpStatus = HttpStatusCode.OK;
                otpResponse.Success = true;
            }
            else
            {
                otpResponse.Messages.Add(string.Format("Invalid OTP ({0}) number", otpRequest.OTPToken));
                otpResponse.HttpStatus = HttpStatusCode.BadRequest;
                otpResponse.Success = false;
            }

            return otpResponse;
        }
    }
}