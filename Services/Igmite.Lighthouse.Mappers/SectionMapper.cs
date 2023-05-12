using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class SectionMapper
    {
        public static SectionModel ToModel(this Section section)
        {
            if (section == null)
                return null;

            SectionModel sectionModel = new SectionModel
            {
                SectionId = section.SectionId,
                Name = section.Name,
                Description = section.Description,
                Remarks = section.Remarks,
                CreatedBy = section.CreatedBy,
                CreatedOn = section.CreatedOn,
                UpdatedBy = section.UpdatedBy,
                UpdatedOn = section.UpdatedOn,
                IsActive = section.IsActive
            };

            return sectionModel;
        }
        public static Section FromModel(this SectionModel sectionModel, Section section)
        {
            section.SectionId = sectionModel.SectionId;
            section.Name = sectionModel.Name;
            section.Description = sectionModel.Description;
            section.Remarks = sectionModel.Remarks;
            section.IsActive = sectionModel.IsActive;
            section.RequestType = sectionModel.RequestType;
            section.SetAuditValues(sectionModel.RequestType);

            return section;
        }
    }
}
