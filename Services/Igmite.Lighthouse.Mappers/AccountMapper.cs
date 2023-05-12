using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class AccountMapper
    {
        public static AccountModel ToModel(this Account account)
        {
            if (account == null)
                return null;

            AccountModel accountModel = new AccountModel
            {
                AccountId = account.AccountId,
                LoginId = account.LoginId,
                Password = account.Password,
                UserId = account.UserId,
                UserName = account.UserName,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Designation = account.Designation,
                EmailId = account.EmailId,
                Mobile = account.Mobile,
                AccountType = account.AccountType,
                StateId = account.StateId,
                DivisionId = account.DivisionId,
                DistrictId = account.DistrictId,
                BlockId = account.BlockId,
                ClusterId = account.ClusterId,
                PasswordUpdateDate = account.PasswordUpdateDate,
                PasswordExpiredOn = account.PasswordExpiredOn,
                LastLoginDate = account.LastLoginDate,
                InvalidAttempt = account.InvalidAttempt,
                IsPasswordReset = account.IsPasswordReset,
                PasswordResetToken = account.PasswordResetToken,
                AuthToken = account.AuthToken,
                TokenExpiredOn = account.TokenExpiredOn,
                IsLocked = account.IsLocked,
                CreatedBy = account.CreatedBy,
                CreatedOn = account.CreatedOn,
                UpdatedBy = account.UpdatedBy,
                UpdatedOn = account.UpdatedOn,
                IsActive = account.IsActive
            };

            return accountModel;
        }

        public static Account FromModel(this AccountModel accountModel, Account account)
        {
            account.LoginId = accountModel.LoginId;
            account.UserName = accountModel.UserName;
            account.FirstName = accountModel.FirstName;
            account.LastName = accountModel.LastName;
            account.Designation = accountModel.Designation;
            account.EmailId = accountModel.EmailId;
            account.Mobile = accountModel.Mobile;
            account.AccountType = accountModel.AccountType;
            account.StateId = accountModel.StateId;
            account.DivisionId = accountModel.DivisionId;
            account.DistrictId = accountModel.DistrictId;
            account.BlockId = accountModel.BlockId;
            account.ClusterId = accountModel.ClusterId;
            account.PasswordUpdateDate = accountModel.PasswordUpdateDate;
            account.PasswordExpiredOn = accountModel.PasswordExpiredOn;
            account.LastLoginDate = accountModel.LastLoginDate;
            account.InvalidAttempt = accountModel.InvalidAttempt;
            account.IsPasswordReset = accountModel.IsPasswordReset;
            account.PasswordResetToken = accountModel.PasswordResetToken;
            account.AuthToken = accountModel.AuthToken;
            account.TokenExpiredOn = accountModel.TokenExpiredOn;
            account.IsLocked = accountModel.IsLocked;
            account.IsActive = accountModel.IsActive;
            account.RequestType = accountModel.RequestType;
            account.SetAuditValues(accountModel.RequestType);

            return account;
        }

        public static AccountWorkLocationModel ToModel(this AccountWorkLocation workLocation)
        {
            if (workLocation == null)
                return null;

            AccountWorkLocationModel workLocationModel = new AccountWorkLocationModel
            {
                AccountWorkLocationId = workLocation.AccountWorkLocationId,
                AccountId = workLocation.AccountId,
                StateCode = workLocation.StateCode,
                DivisionId = workLocation.DivisionId,
                DistrictId = workLocation.DistrictId,
                BlockId = workLocation.BlockId,
                ClusterId = workLocation.ClusterId,
                Remarks = workLocation.Remarks,
                IsActive = true
            };

            return workLocationModel;
        }

        public static AccountWorkLocation FromModel(this AccountWorkLocationModel workLocationModel, Account account, AccountWorkLocation workLocation)
        {
            workLocation.StateCode = workLocationModel.StateCode;
            workLocation.DivisionId = workLocationModel.DivisionId;
            workLocation.DistrictId = workLocationModel.DistrictId;
            workLocation.BlockId = workLocationModel.BlockId;
            workLocation.ClusterId = workLocationModel.ClusterId;
            workLocation.RequestType = account.RequestType;
            workLocation.IsActive = true;
            workLocation.SetAuditValues(account.RequestType);

            return workLocation;
        }
    }
}