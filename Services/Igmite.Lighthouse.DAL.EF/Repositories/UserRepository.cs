using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    public class UserRepository : GenericRepository<Account>, IUserRepository
    {
        public IQueryable<Account> GetAccounts(bool isActive)
        {
            return Context.Accounts
                .Where(a => a.AccountId != Constants.DefaultAccountId)
                .OrderBy(f => f.FirstName)
                .AsQueryable();
        }

        public Account GetAccountByLoginId(string loginId)
        {
            Account account = Context.Accounts.FirstOrDefault(r => r.LoginId == loginId);

            return account;
        }

        public Account GetAccountByMobile(string mobile)
        {
            Account account = Context.Accounts
                //.Include(ur => ur.Roles.Select(r => r.Role))
                //.Include(ut => ut.Transactions.Select(t => t.Transaction))
                .FirstOrDefault(r => r.Mobile == mobile && r.IsActive == true);

            return account;
        }

        public Account GetAccountByEmailId(string emailId)
        {
            Account user = Context.Accounts
                  .FirstOrDefault(r => r.EmailId == emailId);

            return user;
        }

        public Account GetAccountByEmailOrMobileId(string emailIdOrMobile)
        {
            return Context.Accounts.FirstOrDefault(a => a.EmailId == emailIdOrMobile || a.Mobile == emailIdOrMobile);
        }

        public bool CheckExistAccountByEmailId(string emailId)
        {
            Account user = Context.Accounts
                  .FirstOrDefault(r => r.EmailId == emailId);

            return (user != null);
        }

        public Account GetAccountByUserId(string userId)
        {
            return Context.Accounts.FirstOrDefault(r => r.UserId == userId);
        }

        public bool CheckExistAccountByUserId(string userId)
        {
            Account user = Context.Accounts
                  .FirstOrDefault(r => r.UserId == userId);

            return (user != null);
        }

        public bool InActiveByID(string mobile)
        {
            var user = Context.Accounts.FirstOrDefault(r => r.Mobile == mobile && r.IsActive == true);

            if (user != null)
            {
                user.IsActive = false;

                //for (int userRoleIndex = 0; userRoleIndex < user.Roles.Count; userRoleIndex++)
                //    user.Roles[userRoleIndex].IsActive = false;

                //for (int userTransactionIndex = 0; userTransactionIndex < user.Transactions.Count; userTransactionIndex++)
                //    user.Transactions[userTransactionIndex].IsActive = false;

                Context.Entry<Account>(user).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return true;
        }

        public bool ResetPassword(Account account)
        {
            Context.Entry<Account>(account).State = EntityState.Modified;
            int rowCount = Context.SaveChanges();

            return rowCount > 0;
        }

        public async Task<bool> SetAccountWmAuthTokenAsync(Account account)
        {
            Context.Entry<Account>(account).State = EntityState.Modified;
            Task<int> resultTask = Context.SaveChangesAsync();

            int rowCount = await resultTask;
            return rowCount > 0;
        }

        public bool GenerateOTP(UserOTPDetail otpDetails)
        {
            Context.UserOTPDetails.Add(otpDetails);
            int rowCount = Context.SaveChanges();

            return rowCount > 0;
        }

        public async Task<int> ResetPasswordAsync(Account account)
        {
            Context.Entry<Account>(account).State = EntityState.Modified;
            Task<int> resultTask = Context.SaveChangesAsync();

            int rowCount = await resultTask;

            return rowCount;
        }

        public UserOTPDetail GetOTPDetailByID(string mobile, string otpToken)
        {
            return this.Context.UserOTPDetails.FirstOrDefault(o => o.Mobile == mobile && o.OTPToken == otpToken);
        }

        public bool UpdateOTPDetails(UserOTPDetail otpDetail)
        {
            this.Context.Entry<UserOTPDetail>(otpDetail).State = EntityState.Modified;
            this.Context.SaveChanges();

            return true;
        }

        public Employee GetEmployeeByEmailId(string emailId)
        {
            Employee employee = Context.Employees.FirstOrDefault(r => r.EmailId == emailId);

            return employee;
        }

        public bool UpdateEmployee(Employee employee)
        {
            this.Context.Entry<Employee>(employee).State = EntityState.Modified;
            this.Context.SaveChanges();

            return true;
        }
    }
}