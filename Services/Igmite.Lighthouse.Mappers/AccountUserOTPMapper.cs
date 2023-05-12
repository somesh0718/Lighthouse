using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class AccountUserOTPMapper
    {
        public static AccountUserOTPModel ToModel(this AccountUserOTP accountUserOTP)
        {
            if (accountUserOTP == null)
                return null;

            AccountUserOTPModel accountUserOTPModel = new AccountUserOTPModel
            {
                AccountOTPId = accountUserOTP.AccountOTPId,
                AccountId = accountUserOTP.AccountId,
                OTPId = accountUserOTP.OTPId,
                CreatedBy = accountUserOTP.CreatedBy,
                CreatedOn = accountUserOTP.CreatedOn,
                IsActive = accountUserOTP.IsActive
            };

            return accountUserOTPModel;
        }
        public static AccountUserOTP FromModel(this AccountUserOTPModel accountUserOTPModel, AccountUserOTP accountUserOTP)
        {
            accountUserOTP.AccountOTPId = accountUserOTPModel.AccountOTPId;
            accountUserOTP.AccountId = accountUserOTPModel.AccountId;
            accountUserOTP.OTPId = accountUserOTPModel.OTPId;
            accountUserOTP.IsActive = accountUserOTPModel.IsActive;
            accountUserOTP.RequestType = accountUserOTPModel.RequestType;
            accountUserOTP.SetAuditValues(accountUserOTPModel.RequestType);

            return accountUserOTP;
        }
    }
}
