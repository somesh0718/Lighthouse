using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class SchoolCategoryMapper
    {
        public static SchoolCategoryModel ToModel(this SchoolCategory schoolCategory)
        {
            if (schoolCategory == null)
                return null;

            SchoolCategoryModel schoolCategoryModel = new SchoolCategoryModel
            {
                SchoolCategoryId = schoolCategory.SchoolCategoryId,
                CategoryName = schoolCategory.CategoryName,
                Description = schoolCategory.Description,
                CreatedBy = schoolCategory.CreatedBy,
                CreatedOn = schoolCategory.CreatedOn,
                UpdatedBy = schoolCategory.UpdatedBy,
                UpdatedOn = schoolCategory.UpdatedOn,
                IsActive = schoolCategory.IsActive
            };

            //schoolCategory.Schools.ForEach((school) => schoolCategoryModel.SchoolModels.Add(school.ToModel()));

            return schoolCategoryModel;
        }
        public static SchoolCategory FromModel(this SchoolCategoryModel schoolCategoryModel, SchoolCategory schoolCategory)
        {
            schoolCategory.SchoolCategoryId = schoolCategoryModel.SchoolCategoryId;
            schoolCategory.CategoryName = schoolCategoryModel.CategoryName;
            schoolCategory.Description = schoolCategoryModel.Description;
            schoolCategory.IsActive = schoolCategoryModel.IsActive;
            schoolCategory.RequestType = schoolCategoryModel.RequestType;
            schoolCategory.SetAuditValues(schoolCategoryModel.RequestType);

            //// Handling multiple schoolCategory schools
            //foreach (var schoolModel in schoolCategoryModel.SchoolModels)
            //{
            //    School school = schoolCategory.Schools.FirstOrDefault(f => f.SchoolId == schoolModel.SchoolId);
            //    if (school == null || schoolCategoryModel.RequestType == RequestType.New)
            //    {
            //        school = new School();
            //        school.SchoolId = Guid.NewGuid();
            //        school.SchoolCategoryId = schoolCategory.SchoolCategoryId;
            //    }
            //    school = schoolModel.FromModel(school);
            //    school.SetAuditValues(schoolCategoryModel.RequestType);

            //    schoolCategory.Schools.Add(school);
            //}

            return schoolCategory;
        }
    }
}
