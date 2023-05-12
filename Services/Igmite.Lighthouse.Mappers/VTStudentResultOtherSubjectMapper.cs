using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTStudentResultOtherSubjectMapper
    {
        public static VTStudentResultOtherSubjectModel ToModel(this VTStudentResultOtherSubject vtStudentResultOtherSubject)
        {
            if (vtStudentResultOtherSubject == null)
                return null;

            VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel = new VTStudentResultOtherSubjectModel
            {
                VTStudentResultOtherSubjectId = vtStudentResultOtherSubject.VTStudentResultOtherSubjectId,
                VTClassId = vtStudentResultOtherSubject.VTClassId,
                StudentId = vtStudentResultOtherSubject.StudentId,
                SubjectName = vtStudentResultOtherSubject.SubjectName,
                SubjectNumber = vtStudentResultOtherSubject.SubjectNumber,
                SubjectMarks = vtStudentResultOtherSubject.SubjectMarks,
                CreatedBy = vtStudentResultOtherSubject.CreatedBy,
                CreatedOn = vtStudentResultOtherSubject.CreatedOn,
                UpdatedBy = vtStudentResultOtherSubject.UpdatedBy,
                UpdatedOn = vtStudentResultOtherSubject.UpdatedOn,
                IsActive = vtStudentResultOtherSubject.IsActive
            };

            return vtStudentResultOtherSubjectModel;
        }
        public static VTStudentResultOtherSubject FromModel(this VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel, VTStudentResultOtherSubject vtStudentResultOtherSubject)
        {
            vtStudentResultOtherSubject.VTStudentResultOtherSubjectId = vtStudentResultOtherSubjectModel.VTStudentResultOtherSubjectId;
            vtStudentResultOtherSubject.VTClassId = vtStudentResultOtherSubjectModel.VTClassId;
            vtStudentResultOtherSubject.StudentId = vtStudentResultOtherSubjectModel.StudentId;
            vtStudentResultOtherSubject.SubjectName = vtStudentResultOtherSubjectModel.SubjectName;
            vtStudentResultOtherSubject.SubjectNumber = vtStudentResultOtherSubjectModel.SubjectNumber;
            vtStudentResultOtherSubject.SubjectMarks = vtStudentResultOtherSubjectModel.SubjectMarks;
            vtStudentResultOtherSubject.IsActive = vtStudentResultOtherSubjectModel.IsActive;
            vtStudentResultOtherSubject.RequestType = vtStudentResultOtherSubjectModel.RequestType;
            vtStudentResultOtherSubject.SetAuditValues(vtStudentResultOtherSubjectModel.RequestType);

            return vtStudentResultOtherSubject;
        }
    }
}
