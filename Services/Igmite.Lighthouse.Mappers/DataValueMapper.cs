using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class DataValueMapper
    {
        public static DataValueModel ToModel(this DataValue dataValue)
        {
            if (dataValue == null)
                return null;

            DataValueModel dataValueModel = new DataValueModel
            {
                DataValueId = dataValue.DataValueId,
                DataTypeId = dataValue.DataTypeId,
                ParentId = dataValue.ParentId,
                Code = dataValue.Code,
                Name = dataValue.Name,
                Description = dataValue.Description,
                DisplayOrder = dataValue.DisplayOrder,
                CreatedBy = dataValue.CreatedBy,
                CreatedOn = dataValue.CreatedOn,
                UpdatedBy = dataValue.UpdatedBy,
                UpdatedOn = dataValue.UpdatedOn,
                IsActive = dataValue.IsActive
            };

            return dataValueModel;
        }

        public static DataValue FromModel(this DataValueModel dataValueModel, DataValue dataValue)
        {
            dataValue.DataTypeId = dataValueModel.DataTypeId;
            dataValue.ParentId = dataValueModel.ParentId;
            dataValue.Code = dataValueModel.Code;
            dataValue.Name = dataValueModel.Name;
            dataValue.Description = dataValueModel.Description;
            dataValue.DisplayOrder = dataValueModel.DisplayOrder;
            dataValue.IsActive = dataValueModel.IsActive;
            dataValue.RequestType = dataValueModel.RequestType;
            dataValue.SetAuditValues(dataValueModel.RequestType);

            return dataValue;
        }
    }
}