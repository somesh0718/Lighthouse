using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class SchoolClassMapper
    {
        public static SchoolClassModel ToModel(this SchoolClass schoolClass)
        {
            if (schoolClass == null)
                return null;

            SchoolClassModel schoolClassModel = new SchoolClassModel
            {
                ClassId = schoolClass.ClassId,
                Name = schoolClass.Name,
                Description = schoolClass.Description,
                Remarks = schoolClass.Remarks,
                CreatedBy = schoolClass.CreatedBy,
                CreatedOn = schoolClass.CreatedOn,
                UpdatedBy = schoolClass.UpdatedBy,
                UpdatedOn = schoolClass.UpdatedOn,
                IsActive = schoolClass.IsActive
            };

            return schoolClassModel;
        }
        public static SchoolClass FromModel(this SchoolClassModel schoolClassModel, SchoolClass schoolClass)
        {
            schoolClass.ClassId = schoolClassModel.ClassId;
            schoolClass.Name = schoolClassModel.Name;
            schoolClass.Description = schoolClassModel.Description;
            schoolClass.Remarks = schoolClassModel.Remarks;
            schoolClass.IsActive = schoolClassModel.IsActive;
            schoolClass.RequestType = schoolClassModel.RequestType;
            schoolClass.SetAuditValues(schoolClassModel.RequestType);

            return schoolClass;
        }
    }
}
