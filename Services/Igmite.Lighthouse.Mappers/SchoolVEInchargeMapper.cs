using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;

namespace Igmite.Lighthouse.Mappers
{
    public static class SchoolVEInchargeMapper
    {
        public static SchoolVEInchargeModel ToModel(this SchoolVEIncharge schoolVEIncharge)
        {
            if (schoolVEIncharge == null)
                return null;

            SchoolVEInchargeModel schoolVEInchargeModel = new SchoolVEInchargeModel
            {
                VEIId = schoolVEIncharge.VEIId,
                VTId = schoolVEIncharge.VTId,
                SchoolId = schoolVEIncharge.SchoolId,
                FirstName = schoolVEIncharge.FirstName,
                MiddleName = schoolVEIncharge.MiddleName,
                LastName = schoolVEIncharge.LastName,
                FullName = string.Format("{0} {1} {2}", schoolVEIncharge.FirstName, schoolVEIncharge.MiddleName, schoolVEIncharge.LastName).TrimSpaces(),
                Mobile = schoolVEIncharge.Mobile,
                Mobile1 = schoolVEIncharge.Mobile1,
                Email = schoolVEIncharge.Email,
                Gender = schoolVEIncharge.Gender,
                DateOfJoining = schoolVEIncharge.DateOfJoining,
                DateOfResignationFromRoleSchool = schoolVEIncharge.DateOfResignationFromRoleSchool,
                CreatedBy = schoolVEIncharge.CreatedBy,
                CreatedOn = schoolVEIncharge.CreatedOn,
                UpdatedBy = schoolVEIncharge.UpdatedBy,
                UpdatedOn = schoolVEIncharge.UpdatedOn,
                IsActive = schoolVEIncharge.IsActive
            };

            return schoolVEInchargeModel;
        }

        public static SchoolVEIncharge FromModel(this SchoolVEInchargeModel schoolVEInchargeModel, SchoolVEIncharge schoolVEIncharge)
        {
            schoolVEIncharge.VTId = schoolVEInchargeModel.VTId;
            schoolVEIncharge.SchoolId = schoolVEInchargeModel.SchoolId;
            schoolVEIncharge.FirstName = schoolVEInchargeModel.FirstName;
            schoolVEIncharge.MiddleName = schoolVEInchargeModel.MiddleName;
            schoolVEIncharge.LastName = schoolVEInchargeModel.LastName;
            schoolVEIncharge.FullName = string.Format("{0} {1} {2}", schoolVEInchargeModel.FirstName, schoolVEInchargeModel.MiddleName, schoolVEInchargeModel.LastName).TrimSpaces();
            schoolVEIncharge.Mobile = schoolVEInchargeModel.Mobile;
            schoolVEIncharge.Mobile1 = schoolVEInchargeModel.Mobile1;
            schoolVEIncharge.Email = schoolVEInchargeModel.Email;
            schoolVEIncharge.Gender = schoolVEInchargeModel.Gender;
            schoolVEIncharge.DateOfJoining = schoolVEInchargeModel.DateOfJoining;
            schoolVEIncharge.DateOfResignationFromRoleSchool = schoolVEInchargeModel.DateOfResignationFromRoleSchool;
            schoolVEIncharge.IsActive = (schoolVEInchargeModel.DateOfResignationFromRoleSchool != null) ? false : schoolVEInchargeModel.IsActive;
            schoolVEIncharge.RequestType = schoolVEInchargeModel.RequestType;
            schoolVEIncharge.SetAuditValues(schoolVEInchargeModel.RequestType);

            return schoolVEIncharge;
        }
    }
}