using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class CourseModuleMapper
    {
        public static CourseModuleModel ToModel(this CourseModule courseModule)
        {
            if (courseModule == null)
                return null;

            CourseModuleModel courseModuleModel = new CourseModuleModel
            {
                CourseModuleId = courseModule.CourseModuleId,
                ClassId = courseModule.ClassId,
                ModuleTypeId = courseModule.ModuleTypeId,
                SectorId = courseModule.SectorId,
                JobRoleId = courseModule.JobRoleId,
                UnitName = courseModule.UnitName,
                DisplayOrder = courseModule.DisplayOrder,
                Remarks = courseModule.Remarks,
                CreatedBy = courseModule.CreatedBy,
                CreatedOn = courseModule.CreatedOn,
                UpdatedBy = courseModule.UpdatedBy,
                UpdatedOn = courseModule.UpdatedOn,
                IsActive = courseModule.IsActive
            };

            return courseModuleModel;
        }

        public static CourseModule FromModel(this CourseModuleModel courseModuleModel, CourseModule courseModule)
        {
            courseModule.CourseModuleId = courseModuleModel.CourseModuleId;
            courseModule.ClassId = courseModuleModel.ClassId;
            courseModule.ModuleTypeId = courseModuleModel.ModuleTypeId;
            courseModule.SectorId = courseModuleModel.SectorId;
            courseModule.JobRoleId = courseModuleModel.JobRoleId;
            courseModule.UnitName = courseModuleModel.UnitName;
            courseModule.DisplayOrder = courseModuleModel.DisplayOrder;
            courseModule.Remarks = courseModuleModel.Remarks;
            courseModule.IsActive = courseModuleModel.IsActive;
            courseModule.RequestType = courseModuleModel.RequestType;
            courseModule.SetAuditValues(courseModuleModel.RequestType);

            return courseModule;
        }
    }
}