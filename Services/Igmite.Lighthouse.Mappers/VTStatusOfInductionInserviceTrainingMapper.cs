using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTStatusOfInductionInserviceTrainingMapper
    {
        public static VTStatusOfInductionInserviceTrainingModel ToModel(this VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining)
        {
            if (vtStatusOfInductionInserviceTraining == null)
                return null;

            VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel = new VTStatusOfInductionInserviceTrainingModel
            {
                VTStatusOfInductionInserviceTrainingId = vtStatusOfInductionInserviceTraining.VTStatusOfInductionInserviceTrainingId,
                VTSchoolSectorId = vtStatusOfInductionInserviceTraining.VTSchoolSectorId,
                IndustryTrainingStatus = vtStatusOfInductionInserviceTraining.IndustryTrainingStatus,
                InserviceTrainingStatus = vtStatusOfInductionInserviceTraining.InserviceTrainingStatus,
                CreatedBy = vtStatusOfInductionInserviceTraining.CreatedBy,
                CreatedOn = vtStatusOfInductionInserviceTraining.CreatedOn,
                UpdatedBy = vtStatusOfInductionInserviceTraining.UpdatedBy,
                UpdatedOn = vtStatusOfInductionInserviceTraining.UpdatedOn,
                IsActive = vtStatusOfInductionInserviceTraining.IsActive
            };

            return vtStatusOfInductionInserviceTrainingModel;
        }
        public static VTStatusOfInductionInserviceTraining FromModel(this VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel, VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining)
        {
            vtStatusOfInductionInserviceTraining.VTStatusOfInductionInserviceTrainingId = vtStatusOfInductionInserviceTrainingModel.VTStatusOfInductionInserviceTrainingId;
            vtStatusOfInductionInserviceTraining.VTSchoolSectorId = vtStatusOfInductionInserviceTrainingModel.VTSchoolSectorId;
            vtStatusOfInductionInserviceTraining.IndustryTrainingStatus = vtStatusOfInductionInserviceTrainingModel.IndustryTrainingStatus;
            vtStatusOfInductionInserviceTraining.InserviceTrainingStatus = vtStatusOfInductionInserviceTrainingModel.InserviceTrainingStatus;
            vtStatusOfInductionInserviceTraining.IsActive = vtStatusOfInductionInserviceTrainingModel.IsActive;
            vtStatusOfInductionInserviceTraining.RequestType = vtStatusOfInductionInserviceTrainingModel.RequestType;
            vtStatusOfInductionInserviceTraining.SetAuditValues(vtStatusOfInductionInserviceTrainingModel.RequestType);

            return vtStatusOfInductionInserviceTraining;
        }
    }
}
