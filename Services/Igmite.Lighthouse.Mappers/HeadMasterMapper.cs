using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;

namespace Igmite.Lighthouse.Mappers
{
    public static class HeadMasterMapper
    {
        public static HeadMasterModel ToModel(this HeadMaster headMaster)
        {
            if (headMaster == null)
                return null;

            HeadMasterModel headMasterModel = new HeadMasterModel
            {
                HMId = headMaster.HMId,
                FirstName = headMaster.FirstName,
                MiddleName = headMaster.MiddleName,
                LastName = headMaster.LastName,
                FullName = string.Format("{0} {1} {2}", headMaster.FirstName, headMaster.MiddleName, headMaster.LastName).TrimSpaces(),
                Mobile = headMaster.Mobile,
                Mobile1 = headMaster.Mobile1,
                Email = headMaster.Email,
                Gender = headMaster.Gender,
                YearsInSchool = headMaster.YearsInSchool,

                CreatedBy = headMaster.CreatedBy,
                CreatedOn = headMaster.CreatedOn,
                UpdatedBy = headMaster.UpdatedBy,
                UpdatedOn = headMaster.UpdatedOn,
                IsActive = headMaster.IsActive
            };

            if (headMaster.HMSchool != null)
            {
                headMasterModel.AcademicYearId = headMaster.HMSchool.AcademicYearId;
                headMasterModel.SchoolId = headMaster.HMSchool.SchoolId;
                headMasterModel.DateOfJoining = headMaster.HMSchool.DateOfJoining;
                headMasterModel.DateOfResignation = headMaster.HMSchool.DateOfResignation;
            }

            return headMasterModel;
        }

        public static HeadMaster FromModel(this HeadMasterModel headMasterModel, HeadMaster headMaster)
        {
            headMaster.HMId = headMasterModel.HMId;
            headMaster.FirstName = headMasterModel.FirstName;
            headMaster.MiddleName = headMasterModel.MiddleName;
            headMaster.LastName = headMasterModel.LastName;
            headMaster.FullName = string.Format("{0} {1} {2}", headMasterModel.FirstName, headMasterModel.MiddleName, headMasterModel.LastName).TrimSpaces();
            headMaster.Mobile = headMasterModel.Mobile;
            headMaster.Mobile1 = headMasterModel.Mobile1;
            headMaster.Email = headMasterModel.Email;
            headMaster.Gender = headMasterModel.Gender;
            headMaster.YearsInSchool = headMasterModel.YearsInSchool;
            headMaster.IsActive = headMasterModel.IsActive;
            headMaster.RequestType = headMasterModel.RequestType;
            headMaster.SetAuditValues(headMasterModel.RequestType);

            headMaster.HMSchool.AuthUserId = headMaster.AuthUserId;
            headMaster.HMSchool.AcademicYearId = headMasterModel.AcademicYearId;
            headMaster.HMSchool.SchoolId = headMasterModel.SchoolId;
            headMaster.HMSchool.HMId = headMasterModel.HMId;
            headMaster.HMSchool.DateOfJoining = headMasterModel.DateOfJoining;
            headMaster.HMSchool.DateOfResignation = headMasterModel.DateOfResignation;
            headMaster.HMSchool.IsActive = headMasterModel.IsActive;
            headMaster.HMSchool.SetAuditValues(headMasterModel.RequestType);

            return headMaster;
        }
    }
}