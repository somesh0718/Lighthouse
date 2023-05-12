using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;

namespace Igmite.Lighthouse.Mappers
{
    public static class VocationalCoordinatorMapper
    {
        public static VocationalCoordinatorModel ToModel(this VocationalCoordinator vocationalCoordinator)
        {
            if (vocationalCoordinator == null)
                return null;

            VocationalCoordinatorModel vocationalCoordinatorModel = new VocationalCoordinatorModel
            {
                VCId = vocationalCoordinator.VCId,
                FirstName = vocationalCoordinator.FirstName,
                MiddleName = vocationalCoordinator.MiddleName,
                LastName = vocationalCoordinator.LastName,
                FullName = string.Format("{0} {1} {2}", vocationalCoordinator.FirstName, vocationalCoordinator.MiddleName, vocationalCoordinator.LastName).TrimSpaces(),
                Mobile = vocationalCoordinator.Mobile,
                Mobile1 = vocationalCoordinator.Mobile1,
                EmailId = vocationalCoordinator.EmailId,
                Gender = vocationalCoordinator.Gender,

                CreatedBy = vocationalCoordinator.CreatedBy,
                CreatedOn = vocationalCoordinator.CreatedOn,
                UpdatedBy = vocationalCoordinator.UpdatedBy,
                UpdatedOn = vocationalCoordinator.UpdatedOn,
                IsActive = vocationalCoordinator.IsActive
            };

            if (vocationalCoordinator.VTPCoordinator != null)
            {
                vocationalCoordinatorModel.AcademicYearId = vocationalCoordinator.VTPCoordinator.AcademicYearId;
                vocationalCoordinatorModel.VTPId = vocationalCoordinator.VTPCoordinator.VTPId;
                vocationalCoordinatorModel.DateOfJoining = vocationalCoordinator.VTPCoordinator.DateOfJoining;
                vocationalCoordinatorModel.DateOfResignation = vocationalCoordinator.VTPCoordinator.DateOfResignation;
                vocationalCoordinatorModel.NatureOfAppointment = vocationalCoordinator.VTPCoordinator.NatureOfAppointment;
            }

            return vocationalCoordinatorModel;
        }

        public static VocationalCoordinator FromModel(this VocationalCoordinatorModel vocationalCoordinatorModel, VocationalCoordinator vocationalCoordinator)
        {
            vocationalCoordinator.VCId = vocationalCoordinatorModel.VCId;
            vocationalCoordinator.FirstName = vocationalCoordinatorModel.FirstName;
            vocationalCoordinator.MiddleName = vocationalCoordinatorModel.MiddleName;
            vocationalCoordinator.LastName = vocationalCoordinatorModel.LastName;
            vocationalCoordinator.FullName = string.Format("{0} {1} {2}", vocationalCoordinatorModel.FirstName, vocationalCoordinatorModel.MiddleName, vocationalCoordinatorModel.LastName).TrimSpaces();
            vocationalCoordinator.Mobile = vocationalCoordinatorModel.Mobile;
            vocationalCoordinator.Mobile1 = vocationalCoordinatorModel.Mobile1;
            vocationalCoordinator.EmailId = vocationalCoordinatorModel.EmailId;
            vocationalCoordinator.Gender = vocationalCoordinatorModel.Gender;
            vocationalCoordinator.IsActive = vocationalCoordinatorModel.IsActive;
            vocationalCoordinator.RequestType = vocationalCoordinatorModel.RequestType;
            vocationalCoordinator.SetAuditValues(vocationalCoordinatorModel.RequestType);

            vocationalCoordinator.VTPCoordinator.AuthUserId = vocationalCoordinator.AuthUserId;
            vocationalCoordinator.VTPCoordinator.VTPId = vocationalCoordinatorModel.VTPId;
            vocationalCoordinator.VTPCoordinator.VCId = vocationalCoordinatorModel.VCId;
            vocationalCoordinator.VTPCoordinator.DateOfJoining = vocationalCoordinatorModel.DateOfJoining;
            vocationalCoordinator.VTPCoordinator.DateOfResignation = vocationalCoordinatorModel.DateOfResignation;
            vocationalCoordinator.VTPCoordinator.NatureOfAppointment = vocationalCoordinatorModel.NatureOfAppointment;
            vocationalCoordinator.VTPCoordinator.IsActive = vocationalCoordinatorModel.IsActive;
            vocationalCoordinator.VTPCoordinator.SetAuditValues(vocationalCoordinatorModel.RequestType);

            return vocationalCoordinator;
        }
    }
}