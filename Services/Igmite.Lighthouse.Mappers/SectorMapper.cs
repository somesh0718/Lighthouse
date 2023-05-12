using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class SectorMapper
    {
        public static SectorModel ToModel(this Sector sector)
        {
            if (sector == null)
                return null;

            SectorModel sectorModel = new SectorModel
            {
                SectorId = sector.SectorId,
                SectorName = sector.SectorName,
                Description = sector.Description,
                DisplayOrder = sector.DisplayOrder,
                CreatedBy = sector.CreatedBy,
                CreatedOn = sector.CreatedOn,
                UpdatedBy = sector.UpdatedBy,
                UpdatedOn = sector.UpdatedOn,
                IsActive = sector.IsActive
            };

            return sectorModel;
        }

        public static Sector FromModel(this SectorModel sectorModel, Sector sector)
        {
            sector.SectorId = sectorModel.SectorId;
            sector.SectorName = sectorModel.SectorName;
            sector.Description = sectorModel.Description;
            sector.DisplayOrder = sectorModel.DisplayOrder;
            sector.IsActive = sectorModel.IsActive;
            sector.RequestType = sectorModel.RequestType;
            sector.SetAuditValues(sectorModel.RequestType);

            return sector;
        }
    }
}