using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class AccountUserTermMapper
    {
        public static AccountUserTermModel ToModel(this AccountUserTerm accountUserTerm)
        {
            if (accountUserTerm == null)
                return null;

            AccountUserTermModel accountUserTermModel = new AccountUserTermModel
            {
                AccountTermsId = accountUserTerm.AccountTermsId,
                AccountId = accountUserTerm.AccountId,
                TermsConditionId = accountUserTerm.TermsConditionId,
                IsLatestTerms = accountUserTerm.IsLatestTerms,
                AcceptedDate = accountUserTerm.AcceptedDate,
                CreatedBy = accountUserTerm.CreatedBy,
                CreatedOn = accountUserTerm.CreatedOn,
                IsActive = accountUserTerm.IsActive
            };

            return accountUserTermModel;
        }
        public static AccountUserTerm FromModel(this AccountUserTermModel accountUserTermModel, AccountUserTerm accountUserTerm)
        {
            accountUserTerm.AccountTermsId = accountUserTermModel.AccountTermsId;
            accountUserTerm.AccountId = accountUserTermModel.AccountId;
            accountUserTerm.TermsConditionId = accountUserTermModel.TermsConditionId;
            accountUserTerm.IsLatestTerms = accountUserTermModel.IsLatestTerms;
            accountUserTerm.AcceptedDate = accountUserTermModel.AcceptedDate;
            accountUserTerm.IsActive = accountUserTermModel.IsActive;
            accountUserTerm.RequestType = accountUserTermModel.RequestType;
            accountUserTerm.SetAuditValues(accountUserTermModel.RequestType);

            return accountUserTerm;
        }
    }
}
