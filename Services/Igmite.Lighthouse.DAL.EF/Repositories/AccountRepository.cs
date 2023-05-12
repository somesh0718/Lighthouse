using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Account entity
    /// </summary>
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private int AutoIncrement = 1;

        /// <summary>
        /// Get list of Account
        /// </summary>
        /// <returns></returns>
        public IQueryable<Account> GetAccounts()
        {
            return this.Context.Accounts.AsQueryable();
        }

        /// <summary>
        /// Get list of Account by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Account> GetAccountsByName(string name)
        {
            var accounts = (from a in this.Context.Accounts
                            where a.UserName.Contains(name)
                            select a).AsQueryable();

            return accounts;
        }

        /// <summary>
        /// Get Account by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetAccountById(Guid accountId)
        {
            return this.Context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        /// <summary>
        /// User Login by Id
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public Account GetAccountByLoginId(string loginId)
        {
            Account user = Context.Accounts
                //.Include(ur => ur.Roles.Select(r => r.Role))
                //.Include(ut => ut.Transactions.Select(t => t.Transaction))
                .FirstOrDefault(r => r.LoginId == loginId || r.Mobile == loginId || r.EmailId == loginId || r.UserId == loginId & r.IsLocked == false & r.IsActive == true);

            return user;
        }

        /// <summary>
        /// Get Account by LoginId using async
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<Account> GetAccountByLoginIdAsync(string loginId)
        {
            var account = await (from a in this.Context.Accounts
                                 where a.LoginId == loginId
                                 select a).FirstOrDefaultAsync();

            return account;
        }

        /// <summary>
        /// Get Account by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Account> GetAccountByIdAsync(Guid accountId)
        {
            var account = await (from a in this.Context.Accounts
                                 where a.AccountId == accountId
                                 select a).FirstOrDefaultAsync();

            return account;
        }

        /// <summary>
        /// Insert/Update Account entity
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool SaveOrUpdateAccountDetails(Account account)
        {
            try
            {
                if (RequestType.New == account.RequestType)
                {
                    Context.Accounts.Add(account);

                    // Add User Work Locations from Models
                    account.AccountWorkLocations.ForEach(workLocation =>
                    {
                        Context.AccountWorkLocations.Add(workLocation);
                    });
                }
                else
                {
                    AccountRole accountRole = this.Context.AccountRoles.FirstOrDefault(a => a.AccountId == account.AccountId);
                    if (accountRole != null)
                    {
                        accountRole.IsActive = account.IsActive;
                        Context.Entry<AccountRole>(accountRole).State = EntityState.Modified;
                    }

                    Context.Entry<Account>(account).State = EntityState.Modified;

                    // Get list of User Work Locations from Database
                    IList<AccountWorkLocation> workLocations = this.Context.AccountWorkLocations.Where(a => a.AccountId == account.AccountId).ToList();

                    // Add/Update User Work Location from Models
                    account.AccountWorkLocations.ForEach(workLocation =>
                    {
                        if (workLocation.RequestType == RequestType.Edit)
                        {
                            // Update new User Work Location
                            Context.Entry<AccountWorkLocation>(workLocation).State = EntityState.Modified;
                        }
                        else
                        {
                            // Add new User Work Location
                            Context.AccountWorkLocations.Add(workLocation);
                        }
                    });

                    // Delete User Work Location from Database if any User Work Location removed from Models
                    account.DeletedWorkLocationIds.ForEach(workLocationId =>
                    {
                        var workLocationModel = account.AccountWorkLocations.FirstOrDefault(awl => awl.AccountWorkLocationId == workLocationId);

                        if (workLocationModel != null)
                            Context.Entry<AccountWorkLocation>(workLocationModel).State = EntityState.Deleted;
                    });
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateAccountDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Insert/Update Account Async entity
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> SaveOrUpdateAccountAsync(Account account)
        {
            try
            {
                if (RequestType.New == account.RequestType)
                    Context.Accounts.Add(account);
                else
                {
                    Context.Entry<Account>(account).State = EntityState.Modified;
                }

                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateAccountAsync", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid accountId)
        {
            Account account = this.Context.Accounts.FirstOrDefault(a => a.AccountId == accountId);

            if (account != null)
            {
                Context.Entry<Account>(account).State = EntityState.Deleted;

                AccountRole accountRole = this.Context.AccountRoles.FirstOrDefault(a => a.AccountId == accountId);
                if (accountRole != null)
                {
                    Context.Entry<AccountRole>(accountRole).State = EntityState.Deleted;
                }

                AccountTransaction accountTransaction = this.Context.AccountTransactions.FirstOrDefault(a => a.AccountId == accountId);
                if (accountTransaction != null)
                {
                    Context.Entry<AccountTransaction>(accountTransaction).State = EntityState.Deleted;
                }

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Account by Name
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        public List<string> CheckAccountExistByName(AccountModel accountModel)
        {
            var errorMessages = new List<string>();

            try
            {
                if (accountModel.RequestType == RequestType.New)
                {
                    Account account = this.Context.Accounts.FirstOrDefault(a => a.LoginId == accountModel.LoginId);
                    if (account != null)
                    {
                        errorMessages.Add(string.Format("{0} - User already exists.", account.UserName));
                    }

                    account = this.Context.Accounts.FirstOrDefault(a => a.Mobile == accountModel.Mobile);
                    if (account != null)
                    {
                        errorMessages.Add(string.Format("Mobile No is already exists for another User ({0})", account.UserName));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckAccountExistByName", ex);
            }

            return errorMessages;
        }

        /// <summary>
        /// Get Role by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Role GetRoleByCode(string code)
        {
            return this.Context.Roles.FirstOrDefault(r => r.Code == code);
        }

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetRoleByRoleId(Guid roleId)
        {
            return this.Context.Roles.FirstOrDefault(r => r.RoleId == roleId);
        }

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetRoleByAccountId(Guid accountId)
        {
            Role roleItem = (from ar in this.Context.AccountRoles
                             join r in this.Context.Roles on ar.RoleId equals r.RoleId
                             where ar.AccountId == accountId
                             select r).FirstOrDefault();

            return roleItem;
        }

        /// <summary>}
        /// List of Account with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<AccountViewModel> GetAccountsByCriteria(SearchAccountModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.AccountViewModels.FromSql<AccountViewModel>("CALL GetAccountsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Validate login by User Id in Igmite Application
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public LoginResponce ValidateUserLogin(LoginRequest loginRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "loginId", MySqlDbType = MySqlDbType.VarChar, Value = loginRequest.UserId.StringVal() };

            //return Context.Query<LoginResponce>().FromSql("Call ValidateUserLogin (@loginId)", sqlParams).FirstOrDefault();

            return Context.LoginResponces.FromSql<LoginResponce>("Call ValidateUserLogin (@loginId)", sqlParams).FirstOrDefault();
        }

        /// <summary>
        /// Get user transaction details with rights
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public LoginResponce GetUserWithTransactionsById(LoginRequest loginRequest)
        {
            LoginResponce loginResponce = new LoginResponce();

            using (var sqlConn = new MySqlConnection((new MySqlConnectionStringBuilder(Constants.SQLConnectionString)).ConnectionString))
            {
                sqlConn.Open();

                using (var sqlCmd = new MySqlCommand(ProcedureConstants.GetUserWithTransactionsById, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@userId", MySqlDbType.VarChar).Value = loginRequest.UserId;

                    //using (var reader = sqlCmd.ExecuteXmlReader())
                    //{
                    //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(LoginResponce));
                    //    loginResponce = (LoginResponce)xmlSerializer.Deserialize(reader);
                    //}
                }

                sqlConn.Close();
            }

            return loginResponce;
        }

        /// <summary>
        /// Get Role Transactions By User Id
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public IList<RoleTransactionResponce> GetRoleTransactionsById(LoginRequest loginRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "loginId", MySqlDbType = MySqlDbType.VarChar, Value = loginRequest.UserId.StringVal() };

            return Context.RoleTransactionsBy.FromSql<RoleTransactionResponce>("Call GetRoleTransactionsByUserId (@loginId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get User Transactions By User Id
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public IList<UserTransactionResponce> GetUserTransactionsById(LoginRequest loginRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "loginId", MySqlDbType = MySqlDbType.VarChar, Value = loginRequest.UserId.StringVal() };

            return Context.UserTransactionsBy.FromSql<UserTransactionResponce>("Call GetUserTransactionsByUserId (@loginId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get AccountRole by Id
        /// </summary>
        /// <param name="accountRoleId"></param>
        /// <returns></returns>
        public AccountRole GetAccountRoleById(Guid accountId)
        {
            return this.Context.AccountRoles.FirstOrDefault(ar => ar.AccountId == accountId);
        }

        /// <summary>
        /// Insert/Update Account Role entity
        /// </summary>
        /// <param name="accountRole"></param>
        /// <returns></returns>
        public bool SaveOrUpdateAccountRoleDetails(AccountRole accountRole)
        {
            if (RequestType.New == accountRole.RequestType)
                Context.AccountRoles.Add(accountRole);
            else
            {
                Context.Entry<AccountRole>(accountRole).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get Unique User Id by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUniqueUserId(string userId)
        {
            Account user = Context.Accounts.FirstOrDefault(r => r.UserId == userId);

            if (user != null)
            {
                userId = string.Format("{0}{1}", userId, AutoIncrement);
                AutoIncrement += 1;

                this.GetUniqueUserId(userId);
            }

            return userId;
        }

        /// <summary>
        /// Insert/Update LogoutHistory entity
        /// </summary>
        /// <param name="logoutHistory"></param>
        /// <returns></returns>
        public bool SaveOrUpdateLogoutHistoryDetails(LogoutHistory logoutHistory, Account account)
        {
            try
            {
                Context.Entry<Account>(account).State = EntityState.Modified;
                Context.LogoutHistories.Add(logoutHistory);

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateAccountDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Update AuthToken Async after user logged-in application
        /// </summary>
        /// <param name="loginResponce"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAuthTokenAsync(LoginResponce loginResponce, string loginType)
        {
            try
            {
                MySqlParameter[] sqlParams = new MySqlParameter[8];
                sqlParams[0] = new MySqlParameter { ParameterName = "LoginUniqueId", MySqlDbType = MySqlDbType.Guid, Value = loginResponce.LoginUniqueId };
                sqlParams[1] = new MySqlParameter { ParameterName = "AccountId", MySqlDbType = MySqlDbType.Guid, Value = loginResponce.UserTypeId };
                sqlParams[2] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = loginResponce.UserId };
                sqlParams[3] = new MySqlParameter { ParameterName = "LoginDateTime", MySqlDbType = MySqlDbType.DateTime, Value = Constants.GetCurrentDateTime };
                sqlParams[4] = new MySqlParameter { ParameterName = "LogoutDateTime", MySqlDbType = MySqlDbType.DateTime, Value = Constants.GetCurrentDateTime };
                sqlParams[5] = new MySqlParameter { ParameterName = "AuthToken", MySqlDbType = MySqlDbType.VarChar, Value = loginResponce.AuthToken };
                sqlParams[6] = new MySqlParameter { ParameterName = "IsMobile", MySqlDbType = MySqlDbType.Bit, Value = loginResponce.IsMobile };
                sqlParams[7] = new MySqlParameter { ParameterName = "LoginType", MySqlDbType = MySqlDbType.VarChar, Value = loginType };

                return await Context.Database.ExecuteSqlCommandAsync("Call UpdateUserLoginDetails (@LoginUniqueId, @AccountId, @UserId, @LoginDateTime, @LogoutDateTime, @AuthToken, @IsMobile, @LoginType)", sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > UpdateUserAuthToken", ex);
            }
        }

        /// <summary>}
        /// Change User LoginId by Admin
        /// </summary>}
        /// <param name="changeLoginModel"></param>}
        /// <returns></returns>}
        public bool ChangeUserLoginId(ChangeLoginModel changeLoginModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "roleIdValue", MySqlDbType = MySqlDbType.Guid, Value = changeLoginModel.RoleId };
            sqlParams[1] = new MySqlParameter { ParameterName = "loginId", MySqlDbType = MySqlDbType.Guid, Value = changeLoginModel.AccountId };
            sqlParams[2] = new MySqlParameter { ParameterName = "newLoginId", MySqlDbType = MySqlDbType.VarChar, Value = changeLoginModel.NewLoginId };

            Context.Database.ExecuteSqlCommand("Call ChangeUserLoginId (@roleIdValue, @loginId, @newLoginId)", sqlParams);
            return true;
        }

        /// <summary>
        /// Get Account Work Locations By User Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AccountWorkLocationModel> GetAccountWorkLocationsByCriteria(Guid accountId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "AccountId", MySqlDbType = MySqlDbType.Guid, Value = accountId };

            return Context.AccountWorkLocationModels.FromSql<AccountWorkLocationModel>("Call GetAccountWorkLocationsByCriteria (@AccountId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get Account Work Locations By User Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AccountWorkLocation> GetAccountWorkLocationsById(Guid accountId)
        {
            return Context.AccountWorkLocations.Where(a => a.AccountId == accountId).ToList();
        }
    }
}