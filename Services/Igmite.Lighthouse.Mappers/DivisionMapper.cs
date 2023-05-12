using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class DivisionMapper
    {
        public static DivisionModel ToModel(this Division division)
        {
            if (division == null)
                return null;

            DivisionModel divisionModel = new DivisionModel
            {
                DivisionId = division.DivisionId,
                StateCode = division.StateCode,
                DivisionName = division.DivisionName,
                Description = division.Description,
                CreatedBy = division.CreatedBy,
                CreatedOn = division.CreatedOn,
                UpdatedBy = division.UpdatedBy,
                UpdatedOn = division.UpdatedOn,
                IsActive = division.IsActive
            };

            return divisionModel;
        }

        public static Division FromModel(this DivisionModel divisionModel, Division division)
        {
            division.DivisionId = divisionModel.DivisionId;
            division.StateCode = divisionModel.StateCode;
            division.DivisionName = divisionModel.DivisionName;
            division.Description = divisionModel.Description;
            division.IsActive = divisionModel.IsActive;
            division.RequestType = divisionModel.RequestType;
            division.SetAuditValues(divisionModel.RequestType);

            return division;
        }
    }
}