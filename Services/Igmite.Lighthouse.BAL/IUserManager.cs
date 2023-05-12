using Igmite.Lighthouse.Models;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    public interface IUserManager : IGenericManager<AccountModel>
    {
        AccountModel GetAccountByLoginId(string loginId);

        AccountModel GetAccountByUserId(string userId);

        bool CheckExistAccountByUserId(string userId);

        AccountModel GetAccountByEmailId(string emailId);

        bool CheckExistAccountByEmailId(string emailId);

        Task<string> GeneratePasswordResetToken(string emailId);

        ResetPasswordModel GetAccountByVerifyCode(string verifyCode);

        ForgotPasswordModel SetForgotPasswordAdmin(string emailIdOrMobile);

        bool InActiveByID(string mobile);

        Task<bool> ResetPasswordAsync(ResetPasswordModel accountModel);

        SingularResponse<string> ForgotPassword(string mobile);

        bool SaveUserRegistrationDetails(UserRegistrationModel userRegistrationModel);

        SingularResponse<OTPResponseModel> SendOTPNumber(OTPRequestModel otpRequest);

        SingularResponse<bool> ValidateOTPNumber(OTPRequestModel otpRequest);
    }
}