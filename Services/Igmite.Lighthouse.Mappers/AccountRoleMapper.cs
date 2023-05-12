using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class AccountRoleMapper
    {
        public static AccountRoleModel ToModel(this AccountRole accountRole)
        {
            if (accountRole == null)
                return null;

            AccountRoleModel accountRoleModel = new AccountRoleModel
            {
                AccountRoleId = accountRole.AccountRoleId,
                AccountId = accountRole.AccountId,
                RoleId = accountRole.RoleId,
                Remarks = accountRole.Remarks,
                CreatedBy = accountRole.CreatedBy,
                CreatedOn = accountRole.CreatedOn,
                UpdatedBy = accountRole.UpdatedBy,
                UpdatedOn = accountRole.UpdatedOn,
                IsActive = accountRole.IsActive
            };

            return accountRoleModel;
        }
        public static AccountRole FromModel(this AccountRoleModel accountRoleModel, AccountRole accountRole)
        {
            accountRole.AccountRoleId = accountRoleModel.AccountRoleId;
            accountRole.AccountId = accountRoleModel.AccountId;
            accountRole.RoleId = accountRoleModel.RoleId;
            accountRole.Remarks = accountRoleModel.Remarks;
            accountRole.IsActive = accountRoleModel.IsActive;
            accountRole.RequestType = accountRoleModel.RequestType;
            accountRole.SetAuditValues(accountRoleModel.RequestType);

            return accountRole;
        }
    }
}
