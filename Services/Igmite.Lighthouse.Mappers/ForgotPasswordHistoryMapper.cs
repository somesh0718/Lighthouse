using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class ForgotPasswordHistoryMapper
    {
        public static ForgotPasswordHistoryModel ToModel(this ForgotPasswordHistory forgotPasswordHistory)
        {
            if (forgotPasswordHistory == null)
                return null;

            ForgotPasswordHistoryModel forgotPasswordHistoryModel = new ForgotPasswordHistoryModel
            {
                ForgotPasswordId = forgotPasswordHistory.ForgotPasswordId,
                EmailId = forgotPasswordHistory.EmailId,
                PasswordResetUrl = forgotPasswordHistory.PasswordResetUrl,
                UserIPAddress = forgotPasswordHistory.UserIPAddress,
                RequestDate = forgotPasswordHistory.RequestDate,
                ResetPasswordDate = forgotPasswordHistory.ResetPasswordDate
            };

            return forgotPasswordHistoryModel;
        }
        public static ForgotPasswordHistory FromModel(this ForgotPasswordHistoryModel forgotPasswordHistoryModel, ForgotPasswordHistory forgotPasswordHistory)
        {
            forgotPasswordHistory.ForgotPasswordId = forgotPasswordHistoryModel.ForgotPasswordId;
            forgotPasswordHistory.EmailId = forgotPasswordHistoryModel.EmailId;
            forgotPasswordHistory.PasswordResetUrl = forgotPasswordHistoryModel.PasswordResetUrl;
            forgotPasswordHistory.UserIPAddress = forgotPasswordHistoryModel.UserIPAddress;
            forgotPasswordHistory.RequestDate = forgotPasswordHistoryModel.RequestDate;
            forgotPasswordHistory.ResetPasswordDate = forgotPasswordHistoryModel.ResetPasswordDate;
            forgotPasswordHistory.RequestType = forgotPasswordHistoryModel.RequestType;
            forgotPasswordHistory.SetAuditValues(forgotPasswordHistoryModel.RequestType);

            return forgotPasswordHistory;
        }
    }
}
