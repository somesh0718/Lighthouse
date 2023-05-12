using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTPMonthlyBillSubmissionStatusMapper
    {
        public static VTPMonthlyBillSubmissionStatusModel ToModel(this VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus)
        {
            if (vtpMonthlyBillSubmissionStatus == null)
                return null;

            VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel = new VTPMonthlyBillSubmissionStatusModel
            {
                VTPMonthlyBillSubmissionStatusId = vtpMonthlyBillSubmissionStatus.VTPMonthlyBillSubmissionStatusId,
                VCId = vtpMonthlyBillSubmissionStatus.VCId,
                Month = vtpMonthlyBillSubmissionStatus.Month,
                DateSubmission = vtpMonthlyBillSubmissionStatus.DateSubmission,
                Incorrect = vtpMonthlyBillSubmissionStatus.Incorrect,
                IncorrectDetails = vtpMonthlyBillSubmissionStatus.IncorrectDetails,
                Final = vtpMonthlyBillSubmissionStatus.Final,
                ApprovedPMU = vtpMonthlyBillSubmissionStatus.ApprovedPMU,
                Amount = vtpMonthlyBillSubmissionStatus.Amount,
                DiaryentryDone = vtpMonthlyBillSubmissionStatus.DiaryentryDone,
                DiaryentryNumber = vtpMonthlyBillSubmissionStatus.DiaryentryNumber,
                Details = vtpMonthlyBillSubmissionStatus.Details,
                CreatedBy = vtpMonthlyBillSubmissionStatus.CreatedBy,
                CreatedOn = vtpMonthlyBillSubmissionStatus.CreatedOn,
                UpdatedBy = vtpMonthlyBillSubmissionStatus.UpdatedBy,
                UpdatedOn = vtpMonthlyBillSubmissionStatus.UpdatedOn,
                IsActive = vtpMonthlyBillSubmissionStatus.IsActive
            };

            return vtpMonthlyBillSubmissionStatusModel;
        }
        public static VTPMonthlyBillSubmissionStatus FromModel(this VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel, VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus)
        {
            vtpMonthlyBillSubmissionStatus.VTPMonthlyBillSubmissionStatusId = vtpMonthlyBillSubmissionStatusModel.VTPMonthlyBillSubmissionStatusId;
            vtpMonthlyBillSubmissionStatus.VCId = vtpMonthlyBillSubmissionStatusModel.VCId;
            vtpMonthlyBillSubmissionStatus.Month = vtpMonthlyBillSubmissionStatusModel.Month;
            vtpMonthlyBillSubmissionStatus.DateSubmission = vtpMonthlyBillSubmissionStatusModel.DateSubmission;
            vtpMonthlyBillSubmissionStatus.Incorrect = vtpMonthlyBillSubmissionStatusModel.Incorrect;
            vtpMonthlyBillSubmissionStatus.IncorrectDetails = vtpMonthlyBillSubmissionStatusModel.IncorrectDetails;
            vtpMonthlyBillSubmissionStatus.Final = vtpMonthlyBillSubmissionStatusModel.Final;
            vtpMonthlyBillSubmissionStatus.ApprovedPMU = vtpMonthlyBillSubmissionStatusModel.ApprovedPMU;
            vtpMonthlyBillSubmissionStatus.Amount = vtpMonthlyBillSubmissionStatusModel.Amount;
            vtpMonthlyBillSubmissionStatus.DiaryentryDone = vtpMonthlyBillSubmissionStatusModel.DiaryentryDone;
            vtpMonthlyBillSubmissionStatus.DiaryentryNumber = vtpMonthlyBillSubmissionStatusModel.DiaryentryNumber;
            vtpMonthlyBillSubmissionStatus.Details = vtpMonthlyBillSubmissionStatusModel.Details;
            vtpMonthlyBillSubmissionStatus.IsActive = vtpMonthlyBillSubmissionStatusModel.IsActive;
            vtpMonthlyBillSubmissionStatus.RequestType = vtpMonthlyBillSubmissionStatusModel.RequestType;
            vtpMonthlyBillSubmissionStatus.SetAuditValues(vtpMonthlyBillSubmissionStatusModel.RequestType);

            return vtpMonthlyBillSubmissionStatus;
        }
    }
}
