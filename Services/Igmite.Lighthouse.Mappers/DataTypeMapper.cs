using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class DataTypeMapper
    {
        public static DataTypeModel ToModel(this DataType dataType)
        {
            if (dataType == null)
                return null;

            DataTypeModel dataTypeModel = new DataTypeModel
            {
                DataTypeId = dataType.DataTypeId,
                Name = dataType.Name,
                Description = dataType.Description,
                CreatedBy = dataType.CreatedBy,
                CreatedOn = dataType.CreatedOn,
                UpdatedBy = dataType.UpdatedBy,
                UpdatedOn = dataType.UpdatedOn,
                IsActive = dataType.IsActive
            };

            return dataTypeModel;
        }
        public static DataType FromModel(this DataTypeModel dataTypeModel, DataType dataType)
        {
            dataType.Name = dataTypeModel.Name;
            dataType.Description = dataTypeModel.Description;
            dataType.IsActive = dataTypeModel.IsActive;
            dataType.RequestType = dataTypeModel.RequestType;
            dataType.SetAuditValues(dataTypeModel.RequestType);

            return dataType;
        }
    }
}
