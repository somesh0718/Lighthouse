using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTStudentVEResultMapper
    {
        public static VTStudentVEResultModel ToModel(this VTStudentVEResult vtStudentVEResult)
        {
            if (vtStudentVEResult == null)
                return null;

            VTStudentVEResultModel vtStudentVEResultModel = new VTStudentVEResultModel
            {
                VTStudentVEResultId = vtStudentVEResult.VTStudentVEResultId,
                VTClassId = vtStudentVEResult.VTClassId,
                StudentId = vtStudentVEResult.StudentId,
                DateIssuence = vtStudentVEResult.DateIssuence,
                ExternalMarks = vtStudentVEResult.ExternalMarks,
                TheoryMarks = vtStudentVEResult.TheoryMarks,
                InternalMarks = vtStudentVEResult.InternalMarks,
                TotalMarks = vtStudentVEResult.TotalMarks,
                Grade = vtStudentVEResult.Grade,
                CreatedBy = vtStudentVEResult.CreatedBy,
                CreatedOn = vtStudentVEResult.CreatedOn,
                UpdatedBy = vtStudentVEResult.UpdatedBy,
                UpdatedOn = vtStudentVEResult.UpdatedOn,
                IsActive = vtStudentVEResult.IsActive
            };

            return vtStudentVEResultModel;
        }
        public static VTStudentVEResult FromModel(this VTStudentVEResultModel vtStudentVEResultModel, VTStudentVEResult vtStudentVEResult)
        {
            vtStudentVEResult.VTStudentVEResultId = vtStudentVEResultModel.VTStudentVEResultId;
            vtStudentVEResult.VTClassId = vtStudentVEResultModel.VTClassId;
            vtStudentVEResult.StudentId = vtStudentVEResultModel.StudentId;
            vtStudentVEResult.DateIssuence = vtStudentVEResultModel.DateIssuence;
            vtStudentVEResult.ExternalMarks = vtStudentVEResultModel.ExternalMarks;
            vtStudentVEResult.TheoryMarks = vtStudentVEResultModel.TheoryMarks;
            vtStudentVEResult.InternalMarks = vtStudentVEResultModel.InternalMarks;
            vtStudentVEResult.TotalMarks = vtStudentVEResultModel.TotalMarks;
            vtStudentVEResult.Grade = vtStudentVEResultModel.Grade;
            vtStudentVEResult.IsActive = vtStudentVEResultModel.IsActive;
            vtStudentVEResult.RequestType = vtStudentVEResultModel.RequestType;
            vtStudentVEResult.SetAuditValues(vtStudentVEResultModel.RequestType);

            return vtStudentVEResult;
        }
    }
}
