using Igmite.Lighthouse.Entities;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    public interface IUserRepository : IGenericRepository<Account>
    {
        Account GetAccountByLoginId(string loginId);

        Account GetAccountByMobile(string mobile);

        Account GetAccountByEmailId(string emailId);

        Account GetAccountByEmailOrMobileId(string emailIdOrMobile);

        bool CheckExistAccountByEmailId(string emailId);

        Account GetAccountByUserId(string userId);

        bool CheckExistAccountByUserId(string userId);

        bool InActiveByID(string mobile);

        bool ResetPassword(Account account);

        Task<bool> SetAccountWmAuthTokenAsync(Account account);

        bool GenerateOTP(UserOTPDetail otpDetails);

        Task<int> ResetPasswordAsync(Account account);

        UserOTPDetail GetOTPDetailByID(string mobile, string otpToken);

        bool UpdateOTPDetails(UserOTPDetail otpDetail);

        Employee GetEmployeeByEmailId(string emailId);

        bool UpdateEmployee(Employee employee);
    }
}