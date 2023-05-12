using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igmite.Lighthouse.Mappers
{
    public static class IssueMappingMapper
    {
        public static IssueMappingModel ToModel(this IssueMapping IssueMapping)
        {
            if (IssueMapping == null)
                return null;

            IssueMappingModel IssueMappingModel = new IssueMappingModel
            {
                IssueMappingId = IssueMapping.IssueMappingId,
                MainIssueId = IssueMapping.MainIssueId,
                SubIssueId = IssueMapping.SubIssueId,
                IssueCategoryId = IssueMapping.IssueCategoryId,
                IssuePriority = IssueMapping.IssuePriority,
                IsApplicableForVC = IssueMapping.IsApplicableForVC,
                IsApplicableForVT = IssueMapping.IsApplicableForVT,
                IsApplicableForHM = IssueMapping.IsApplicableForHM,
                CreatedBy = IssueMapping.CreatedBy,
                CreatedOn = IssueMapping.CreatedOn,
                UpdatedBy = IssueMapping.UpdatedBy,
                UpdatedOn = IssueMapping.UpdatedOn,
                IsActive = IssueMapping.IsActive
            };

            return IssueMappingModel;
        }

        public static IssueMapping FromModel(this IssueMappingModel IssueMappingModel, IssueMapping IssueMapping)
        {
            IssueMapping.IssueMappingId = IssueMappingModel.IssueMappingId;
            IssueMapping.MainIssueId = IssueMappingModel.MainIssueId;
            IssueMapping.SubIssueId = IssueMappingModel.SubIssueId;
            IssueMapping.IssueCategoryId = IssueMappingModel.IssueCategoryId;
            IssueMapping.IssuePriority = IssueMappingModel.IssuePriority;
            IssueMapping.IsApplicableForVC = IssueMappingModel.IsApplicableForVC;
            IssueMapping.IsApplicableForVT = IssueMappingModel.IsApplicableForVT;
            IssueMapping.IsApplicableForHM = IssueMappingModel.IsApplicableForHM;
            IssueMapping.IsActive = IssueMappingModel.IsActive;
            IssueMapping.RequestType = IssueMappingModel.RequestType;
            IssueMapping.SetAuditValues(IssueMappingModel.RequestType);

            return IssueMapping;
        }
    }
}
