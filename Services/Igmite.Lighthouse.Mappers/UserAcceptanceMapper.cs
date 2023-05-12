using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class UserAcceptanceMapper
    {
        public static UserAcceptanceModel ToModel(this UserAcceptance userAcceptance)
        {
            if (userAcceptance == null)
                return null;

            UserAcceptanceModel userAcceptanceModel = new UserAcceptanceModel
            {
                UserAcceptanceId = userAcceptance.UserAcceptanceId,
                TermsConditionId = userAcceptance.TermsConditionId,
                UserMachineId = userAcceptance.UserMachineId,
                CreatedOn = userAcceptance.CreatedOn
            };

            return userAcceptanceModel;
        }
        public static UserAcceptance FromModel(this UserAcceptanceModel userAcceptanceModel, UserAcceptance userAcceptance)
        {
            userAcceptance.UserAcceptanceId = userAcceptanceModel.UserAcceptanceId;
            userAcceptance.TermsConditionId = userAcceptanceModel.TermsConditionId;
            userAcceptance.UserMachineId = userAcceptanceModel.UserMachineId;
            userAcceptance.RequestType = userAcceptanceModel.RequestType;
            userAcceptance.SetAuditValues(userAcceptanceModel.RequestType);

            return userAcceptance;
        }
    }
}
