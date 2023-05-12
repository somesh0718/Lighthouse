using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTStudentPlacementDetailMapper
    {
        public static VTStudentPlacementDetailModel ToModel(this VTStudentPlacementDetail vtStudentPlacementDetail)
        {
            if (vtStudentPlacementDetail == null)
                return null;

            VTStudentPlacementDetailModel vtStudentPlacementDetailModel = new VTStudentPlacementDetailModel
            {
                VTStudentPlacementDetailId = vtStudentPlacementDetail.VTStudentPlacementDetailId,
                VTClassId = vtStudentPlacementDetail.VTClassId,
                StudentId = vtStudentPlacementDetail.StudentId,
                PlacementApplyStatus = vtStudentPlacementDetail.PlacementApplyStatus,
                PlacementStatus = vtStudentPlacementDetail.PlacementStatus,
                ApprenticeshipApplyStatus = vtStudentPlacementDetail.ApprenticeshipApplyStatus,
                ApprenticeshipStatus = vtStudentPlacementDetail.ApprenticeshipStatus,
                HigherEducationVE = vtStudentPlacementDetail.HigherEducationVE,
                HigherEductaionOther = vtStudentPlacementDetail.HigherEductaionOther,
                StudentPlacementStatus = vtStudentPlacementDetail.StudentPlacementStatus,
                CreatedBy = vtStudentPlacementDetail.CreatedBy,
                CreatedOn = vtStudentPlacementDetail.CreatedOn,
                UpdatedBy = vtStudentPlacementDetail.UpdatedBy,
                UpdatedOn = vtStudentPlacementDetail.UpdatedOn,
                IsActive = vtStudentPlacementDetail.IsActive
            };

            return vtStudentPlacementDetailModel;
        }
        public static VTStudentPlacementDetail FromModel(this VTStudentPlacementDetailModel vtStudentPlacementDetailModel, VTStudentPlacementDetail vtStudentPlacementDetail)
        {
            vtStudentPlacementDetail.VTStudentPlacementDetailId = vtStudentPlacementDetailModel.VTStudentPlacementDetailId;
            vtStudentPlacementDetail.VTClassId = vtStudentPlacementDetailModel.VTClassId;
            vtStudentPlacementDetail.StudentId = vtStudentPlacementDetailModel.StudentId;
            vtStudentPlacementDetail.PlacementApplyStatus = vtStudentPlacementDetailModel.PlacementApplyStatus;
            vtStudentPlacementDetail.PlacementStatus = vtStudentPlacementDetailModel.PlacementStatus;
            vtStudentPlacementDetail.ApprenticeshipApplyStatus = vtStudentPlacementDetailModel.ApprenticeshipApplyStatus;
            vtStudentPlacementDetail.ApprenticeshipStatus = vtStudentPlacementDetailModel.ApprenticeshipStatus;
            vtStudentPlacementDetail.HigherEducationVE = vtStudentPlacementDetailModel.HigherEducationVE;
            vtStudentPlacementDetail.HigherEductaionOther = vtStudentPlacementDetailModel.HigherEductaionOther;
            vtStudentPlacementDetail.StudentPlacementStatus = vtStudentPlacementDetailModel.StudentPlacementStatus;
            vtStudentPlacementDetail.IsActive = vtStudentPlacementDetailModel.IsActive;
            vtStudentPlacementDetail.RequestType = vtStudentPlacementDetailModel.RequestType;
            vtStudentPlacementDetail.SetAuditValues(vtStudentPlacementDetailModel.RequestType);

            return vtStudentPlacementDetail;
        }
    }
}
