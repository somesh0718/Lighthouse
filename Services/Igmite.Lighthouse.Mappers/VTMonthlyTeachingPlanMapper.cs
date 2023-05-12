using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTMonthlyTeachingPlanMapper
    {
        public static VTMonthlyTeachingPlanModel ToModel(this VTMonthlyTeachingPlan vtMonthlyTeachingPlan)
        {
            if (vtMonthlyTeachingPlan == null)
                return null;

            VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel = new VTMonthlyTeachingPlanModel
            {
                VTMonthlyTeachingPlanId = vtMonthlyTeachingPlan.VTMonthlyTeachingPlanId,
                VTClassId = vtMonthlyTeachingPlan.VTClassId,
                Month = vtMonthlyTeachingPlan.Month,
                WeekStartDate = vtMonthlyTeachingPlan.WeekStartDate,
                WeekendDate = vtMonthlyTeachingPlan.WeekendDate,
                ModulesPlanned = vtMonthlyTeachingPlan.ModulesPlanned,
                IVPlannedDate = vtMonthlyTeachingPlan.IVPlannedDate,
                IVVCAttend = vtMonthlyTeachingPlan.IVVCAttend,
                FVPlannedDate = vtMonthlyTeachingPlan.FVPlannedDate,
                FVPurpose = vtMonthlyTeachingPlan.FVPurpose,
                FVLocation = vtMonthlyTeachingPlan.FVLocation,
                GLPlannedDate = vtMonthlyTeachingPlan.GLPlannedDate,
                OtherDetails = vtMonthlyTeachingPlan.OtherDetails,
                CreatedBy = vtMonthlyTeachingPlan.CreatedBy,
                CreatedOn = vtMonthlyTeachingPlan.CreatedOn,
                UpdatedBy = vtMonthlyTeachingPlan.UpdatedBy,
                UpdatedOn = vtMonthlyTeachingPlan.UpdatedOn,
                IsActive = vtMonthlyTeachingPlan.IsActive
            };

            return vtMonthlyTeachingPlanModel;
        }
        public static VTMonthlyTeachingPlan FromModel(this VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel, VTMonthlyTeachingPlan vtMonthlyTeachingPlan)
        {
            vtMonthlyTeachingPlan.VTMonthlyTeachingPlanId = vtMonthlyTeachingPlanModel.VTMonthlyTeachingPlanId;
            vtMonthlyTeachingPlan.VTClassId = vtMonthlyTeachingPlanModel.VTClassId;
            vtMonthlyTeachingPlan.Month = vtMonthlyTeachingPlanModel.Month;
            vtMonthlyTeachingPlan.WeekStartDate = vtMonthlyTeachingPlanModel.WeekStartDate;
            vtMonthlyTeachingPlan.WeekendDate = vtMonthlyTeachingPlanModel.WeekendDate;
            vtMonthlyTeachingPlan.ModulesPlanned = vtMonthlyTeachingPlanModel.ModulesPlanned;
            vtMonthlyTeachingPlan.IVPlannedDate = vtMonthlyTeachingPlanModel.IVPlannedDate;
            vtMonthlyTeachingPlan.IVVCAttend = vtMonthlyTeachingPlanModel.IVVCAttend;
            vtMonthlyTeachingPlan.FVPlannedDate = vtMonthlyTeachingPlanModel.FVPlannedDate;
            vtMonthlyTeachingPlan.FVPurpose = vtMonthlyTeachingPlanModel.FVPurpose;
            vtMonthlyTeachingPlan.FVLocation = vtMonthlyTeachingPlanModel.FVLocation;
            vtMonthlyTeachingPlan.GLPlannedDate = vtMonthlyTeachingPlanModel.GLPlannedDate;
            vtMonthlyTeachingPlan.OtherDetails = vtMonthlyTeachingPlanModel.OtherDetails;
            vtMonthlyTeachingPlan.IsActive = vtMonthlyTeachingPlanModel.IsActive;
            vtMonthlyTeachingPlan.RequestType = vtMonthlyTeachingPlanModel.RequestType;
            vtMonthlyTeachingPlan.SetAuditValues(vtMonthlyTeachingPlanModel.RequestType);

            return vtMonthlyTeachingPlan;
        }
    }
}
