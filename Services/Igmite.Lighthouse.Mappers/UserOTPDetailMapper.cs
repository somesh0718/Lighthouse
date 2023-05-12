using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class UserOTPDetailMapper
    {
        public static UserOTPDetailModel ToModel(this UserOTPDetail userOTPDetail)
        {
            if (userOTPDetail == null)
                return null;

            UserOTPDetailModel userOTPDetailModel = new UserOTPDetailModel
            {
                OTPId = userOTPDetail.OTPId,
                Mobile = userOTPDetail.Mobile,
                OTPToken = userOTPDetail.OTPToken,
                ExpireOn = userOTPDetail.ExpireOn,
                IsRedeemed = userOTPDetail.IsRedeemed,
                CreatedBy = userOTPDetail.CreatedBy,
                CreatedOn = userOTPDetail.CreatedOn,
                UpdatedBy = userOTPDetail.UpdatedBy,
                UpdatedOn = userOTPDetail.UpdatedOn,
                IsActive = userOTPDetail.IsActive
            };

            //userOTPDetail.AccountOTPs.ForEach((accountOTP) => userOTPDetailModel.AccountOTPModels.Add(accountOTP.ToModel()));

            return userOTPDetailModel;
        }
        public static UserOTPDetail FromModel(this UserOTPDetailModel userOTPDetailModel, UserOTPDetail userOTPDetail)
        {
            userOTPDetail.OTPId = userOTPDetailModel.OTPId;
            userOTPDetail.Mobile = userOTPDetailModel.Mobile;
            userOTPDetail.OTPToken = userOTPDetailModel.OTPToken;
            userOTPDetail.ExpireOn = userOTPDetailModel.ExpireOn;
            userOTPDetail.IsRedeemed = userOTPDetailModel.IsRedeemed;
            userOTPDetail.IsActive = userOTPDetailModel.IsActive;
            userOTPDetail.RequestType = userOTPDetailModel.RequestType;
            userOTPDetail.SetAuditValues(userOTPDetailModel.RequestType);

            //// Handling multiple userOTPDetail otP
            //foreach (var otPModel in userOTPDetailModel.AccountOTPModels)
            //{
            //    AccountUserOTP otP = userOTPDetail.AccountOTPs.FirstOrDefault(f => f.AccountOTPId == otPModel.AccountOTPId);
            //    if (otP == null || userOTPDetailModel.RequestType == RequestType.New)
            //    {
            //        otP = new AccountUserOTP();
            //        otP.AccountOTPId = Guid.NewGuid();
            //        otP.OTPId = userOTPDetail.OTPId;
            //    }
            //    otP = otPModel.FromModel(otP);
            //    otP.SetAuditValues(userOTPDetailModel.RequestType);

            //    userOTPDetail.AccountOTPs.Add(otP);
            //}

            return userOTPDetail;
        }
    }
}
