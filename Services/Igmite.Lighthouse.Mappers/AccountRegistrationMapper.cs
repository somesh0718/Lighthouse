using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class AccountRegistrationMapper
    {        
       public static AccountModel ToUserRegistrationModel(this UserRegistrationModel userRegistrationModel)
        {
            AccountModel accountModel = new AccountModel
            {
                AccountId = Guid.NewGuid(),
                UserId = StringUtility.GetUniqueCharKey(8),
                FirstName = userRegistrationModel.UserName,
                EmailId = userRegistrationModel.EmailId,
                Password = userRegistrationModel.Password,
                Mobile = userRegistrationModel.Mobile,
                AccountType = userRegistrationModel.AccountType,
                InvalidAttempt = 0,
                IsPasswordReset = false,
                TokenExpiredOn = Constants.GetCurrentDateTime.AddYears(2),
                IsLocked = false,
                IsActive = true,
                RoleId = Constants.UserRoleId
            };

            return accountModel;
        }
    }
}
