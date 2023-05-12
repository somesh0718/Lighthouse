using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class SiteHeaderMapper
    {
        public static SiteHeaderModel ToModel(this SiteHeader siteHeader)
        {
            if (siteHeader == null)
                return null;

            SiteHeaderModel siteHeaderModel = new SiteHeaderModel
            {
                SiteHeaderId = siteHeader.SiteHeaderId,
                ShortName = siteHeader.ShortName,
                LongName = siteHeader.LongName,
                Description = siteHeader.Description,
                DisplayOrder = siteHeader.DisplayOrder,
                Remarks = siteHeader.Remarks,
                CreatedBy = siteHeader.CreatedBy,
                CreatedOn = siteHeader.CreatedOn,
                UpdatedBy = siteHeader.UpdatedBy,
                UpdatedOn = siteHeader.UpdatedOn,
                IsActive = siteHeader.IsActive
            };

            //siteHeader.SubHeaders.ForEach((subHeader) => siteHeaderModel.SubHeaderModels.Add(subHeader.ToModel()));

            return siteHeaderModel;
        }
        public static SiteHeader FromModel(this SiteHeaderModel siteHeaderModel, SiteHeader siteHeader)
        {
            siteHeader.SiteHeaderId = siteHeaderModel.SiteHeaderId;
            siteHeader.ShortName = siteHeaderModel.ShortName;
            siteHeader.LongName = siteHeaderModel.LongName;
            siteHeader.Description = siteHeaderModel.Description;
            siteHeader.DisplayOrder = siteHeaderModel.DisplayOrder;
            siteHeader.Remarks = siteHeaderModel.Remarks;
            siteHeader.IsActive = siteHeaderModel.IsActive;
            siteHeader.RequestType = siteHeaderModel.RequestType;
            siteHeader.SetAuditValues(siteHeaderModel.RequestType);

            //// Handling multiple siteHeader headers
            //foreach (var headerModel in siteHeaderModel.SubHeaderModels)
            //{
            //    SiteSubHeader header = siteHeader.SubHeaders.FirstOrDefault(f => f.SiteSubHeaderId == headerModel.SiteSubHeaderId);
            //    if (header == null || siteHeaderModel.RequestType == RequestType.New)
            //    {
            //        header = new SiteSubHeader();
            //        header.SiteSubHeaderId = Guid.NewGuid();
            //        header.SiteHeaderId = siteHeader.SiteHeaderId;
            //    }
            //    header = headerModel.FromModel(header);
            //    header.SetAuditValues(siteHeaderModel.RequestType);

            //    siteHeader.SubHeaders.Add(header);
            //}

            return siteHeader;
        }
    }
}
