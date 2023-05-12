using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class DistrictMapper
    {
        public static DistrictModel ToModel(this District district)
        {
            if (district == null)
                return null;

            DistrictModel districtModel = new DistrictModel
            {
                DistrictCode = district.DistrictCode,
                StateCode = district.StateCode,
                DivisionId = district.DivisionId,
                DistrictName = district.DistrictName,
                Description = district.Description,
                CreatedBy = district.CreatedBy,
                CreatedOn = district.CreatedOn,
                UpdatedBy = district.UpdatedBy,
                UpdatedOn = district.UpdatedOn,
                IsActive = district.IsActive
            };

            return districtModel;
        }

        public static District FromModel(this DistrictModel districtModel, District district)
        {
            district.DistrictCode = districtModel.DistrictCode;
            district.StateCode = districtModel.StateCode;
            district.DivisionId = districtModel.DivisionId;
            district.DistrictName = districtModel.DistrictName;
            district.Description = districtModel.Description;
            district.IsActive = districtModel.IsActive;
            district.RequestType = districtModel.RequestType;
            district.SetAuditValues(districtModel.RequestType);

            return district;
        }
    }
}