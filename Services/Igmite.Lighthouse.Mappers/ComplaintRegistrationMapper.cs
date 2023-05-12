using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class ComplaintRegistrationMapper
    {
        public static ComplaintRegistrationModel ToModel(this ComplaintRegistration complaintRegistration)
        {
            if (complaintRegistration == null)
                return null;

            ComplaintRegistrationModel complaintRegistrationModel = new ComplaintRegistrationModel
            {
                ComplaintRegistrationId = complaintRegistration.ComplaintRegistrationId,
                UserType = complaintRegistration.UserType,
                UserName = complaintRegistration.UserName,
                EmailId = complaintRegistration.EmailId,
                Subject = complaintRegistration.Subject,
                IssueDetails = complaintRegistration.IssueDetails,
                IssueStatus = complaintRegistration.IssueStatus,
                Attachment = complaintRegistration.Attachment,
                CreatedOn = complaintRegistration.CreatedOn,
                UpdatedBy = complaintRegistration.UpdatedBy,
                UpdatedOn = complaintRegistration.UpdatedOn,
                ResolvedBy = complaintRegistration.ResolvedBy,
                ResolvedOn = complaintRegistration.ResolvedOn,
                IsActive = complaintRegistration.IsActive
            };

            return complaintRegistrationModel;
        }

        public static ComplaintRegistration FromModel(this ComplaintRegistrationModel complaintRegistrationModel, ComplaintRegistration complaintRegistration)
        {
            complaintRegistration.UserType = complaintRegistrationModel.UserType;
            complaintRegistration.UserName = complaintRegistrationModel.UserName;
            complaintRegistration.EmailId = complaintRegistrationModel.EmailId;
            complaintRegistration.Subject = complaintRegistrationModel.Subject;
            complaintRegistration.IssueDetails = complaintRegistrationModel.IssueDetails;
            complaintRegistration.IssueStatus = complaintRegistrationModel.IssueStatus;
            complaintRegistration.Attachment = complaintRegistrationModel.Attachment;
            complaintRegistration.CreatedOn = Constants.GetCurrentDateTime;
            complaintRegistration.IsActive = true;

            return complaintRegistration;
        }
    }
}