using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class TermsConditionMapper
    {
        public static TermsConditionModel ToModel(this TermsCondition termsCondition)
        {
            if (termsCondition == null)
                return null;

            TermsConditionModel termsConditionModel = new TermsConditionModel
            {
                TermsConditionId = termsCondition.TermsConditionId,
                Name = termsCondition.Name,
                Description = termsCondition.Description,
                ApplicableFrom = termsCondition.ApplicableFrom,
                CreatedBy = termsCondition.CreatedBy,
                CreatedOn = termsCondition.CreatedOn,
                IsActive = termsCondition.IsActive
            };

            //termsCondition.AccountUserTerms.ForEach((accountUserTerm) => termsConditionModel.AccountUserTermModels.Add(accountUserTerm.ToModel()));
            //termsCondition.UserAcceptances.ForEach((userAcceptance) => termsConditionModel.UserAcceptanceModels.Add(userAcceptance.ToModel()));

            return termsConditionModel;
        }
        public static TermsCondition FromModel(this TermsConditionModel termsConditionModel, TermsCondition termsCondition)
        {
            termsCondition.TermsConditionId = termsConditionModel.TermsConditionId;
            termsCondition.Name = termsConditionModel.Name;
            termsCondition.Description = termsConditionModel.Description;
            termsCondition.ApplicableFrom = termsConditionModel.ApplicableFrom;
            termsCondition.IsActive = termsConditionModel.IsActive;
            termsCondition.RequestType = termsConditionModel.RequestType;
            termsCondition.SetAuditValues(termsConditionModel.RequestType);

            //// Handling multiple termsCondition terms
            //foreach (var termModel in termsConditionModel.AccountUserTermModels)
            //{
            //    AccountUserTerm term = termsCondition.AccountUserTerms.FirstOrDefault(f => f.AccountTermsId == termModel.AccountTermsId);
            //    if (term == null || termsConditionModel.RequestType == RequestType.New)
            //    {
            //        term = new AccountUserTerm();
            //        term.AccountTermsId = Guid.NewGuid();
            //        term.TermsConditionId = termsCondition.TermsConditionId;
            //    }
            //    term = termModel.FromModel(term);
            //    term.SetAuditValues(termsConditionModel.RequestType);

            //    termsCondition.AccountUserTerms.Add(term);
            //}

            //// Handling multiple termsCondition acceptances
            //foreach (var acceptanceModel in termsConditionModel.UserAcceptanceModels)
            //{
            //    UserAcceptance acceptance = termsCondition.UserAcceptances.FirstOrDefault(f => f.UserAcceptanceId == acceptanceModel.UserAcceptanceId);
            //    if (acceptance == null || termsConditionModel.RequestType == RequestType.New)
            //    {
            //        acceptance = new UserAcceptance();
            //        acceptance.UserAcceptanceId = Guid.NewGuid();
            //        acceptance.TermsConditionId = termsCondition.TermsConditionId;
            //    }
            //    acceptance = acceptanceModel.FromModel(acceptance);
            //    acceptance.SetAuditValues(termsConditionModel.RequestType);

            //    termsCondition.UserAcceptances.Add(acceptance);
            //}

            return termsCondition;
        }
    }
}
