using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class SiteSubHeaderMapper
    {
        public static SiteSubHeaderModel ToModel(this SiteSubHeader siteSubHeader)
        {
            if (siteSubHeader == null)
                return null;

            SiteSubHeaderModel siteSubHeaderModel = new SiteSubHeaderModel
            {
                SiteSubHeaderId = siteSubHeader.SiteSubHeaderId,
                SiteHeaderId = siteSubHeader.SiteHeaderId,
                TransactionId = siteSubHeader.TransactionId,
                IsHeaderMenu = siteSubHeader.IsHeaderMenu,
                DisplayOrder = siteSubHeader.DisplayOrder,
                Remarks = siteSubHeader.Remarks,
                CreatedBy = siteSubHeader.CreatedBy,
                CreatedOn = siteSubHeader.CreatedOn,
                UpdatedBy = siteSubHeader.UpdatedBy,
                UpdatedOn = siteSubHeader.UpdatedOn,
                IsActive = siteSubHeader.IsActive
            };

            return siteSubHeaderModel;
        }
        public static SiteSubHeader FromModel(this SiteSubHeaderModel siteSubHeaderModel, SiteSubHeader siteSubHeader)
        {
            siteSubHeader.SiteSubHeaderId = siteSubHeaderModel.SiteSubHeaderId;
            siteSubHeader.SiteHeaderId = siteSubHeaderModel.SiteHeaderId;
            siteSubHeader.TransactionId = siteSubHeaderModel.TransactionId;
            siteSubHeader.IsHeaderMenu = siteSubHeaderModel.IsHeaderMenu;
            siteSubHeader.DisplayOrder = siteSubHeaderModel.DisplayOrder;
            siteSubHeader.Remarks = siteSubHeaderModel.Remarks;
            siteSubHeader.IsActive = siteSubHeaderModel.IsActive;
            siteSubHeader.RequestType = siteSubHeaderModel.RequestType;
            siteSubHeader.SetAuditValues(siteSubHeaderModel.RequestType);

            return siteSubHeader;
        }
    }
}
