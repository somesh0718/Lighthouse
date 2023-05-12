using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igmite.Lighthouse.Mappers
{
    public static class StudentsForExitFormMapper
    {
        public static StudentsForExitFormModel ToModel(this StudentsForExitForm studentsForExitForm)
        {
            if (studentsForExitForm == null)
                return null;

            StudentsForExitFormModel studentsForExitFormModel = new StudentsForExitFormModel
            {
                ExitStudentId = studentsForExitForm.ExitStudentId,
                FirstName = studentsForExitForm.FirstName,
                MiddleName = studentsForExitForm.MiddleName,
                LastName = studentsForExitForm.LastName,
                AcademicYear = studentsForExitForm.AcademicYear,
                StudentFullName = studentsForExitForm.StudentFullName,
                FatherName = studentsForExitForm.FatherName,
                MotherName = studentsForExitForm.MotherName,
                NameOfSchool = studentsForExitForm.NameOfSchool,
                StudentUniqueId = studentsForExitForm.StudentUniqueId,
                UdiseCode = studentsForExitForm.UdiseCode,
                District = studentsForExitForm.District,
                Class = studentsForExitForm.Class,
                Gender = studentsForExitForm.Gender,
                DOB = studentsForExitForm.DOB,
                Category = studentsForExitForm.Category,
                Sector = studentsForExitForm.Sector,
                JobRole = studentsForExitForm.JobRole,
                VTPId = studentsForExitForm.VTPId,
                VTPName = studentsForExitForm.VTPName,
                VTId = studentsForExitForm.VTId,
                VTName = studentsForExitForm.VTName,
                VTMobile = studentsForExitForm.VTMobile,
                VCId = studentsForExitForm.VCId,
                VCName = studentsForExitForm.VCName,
                CreatedBy = studentsForExitForm.CreatedBy,
                CreatedOn = studentsForExitForm.CreatedOn,
                UpdatedBy = studentsForExitForm.UpdatedBy,
                UpdatedOn = studentsForExitForm.UpdatedOn,
                IsActive = studentsForExitForm.IsActive
            };

            return studentsForExitFormModel;
        }

        public static StudentsForExitForm FromModel(this StudentsForExitFormModel studentsForExitFormModel, StudentsForExitForm studentsForExitForm)
        {
            studentsForExitForm.ExitStudentId = studentsForExitFormModel.ExitStudentId;
            studentsForExitForm.FirstName = studentsForExitFormModel.FirstName;
            studentsForExitForm.MiddleName = studentsForExitFormModel.MiddleName;
            studentsForExitForm.LastName = studentsForExitFormModel.LastName;
            studentsForExitForm.AcademicYear = studentsForExitFormModel.AcademicYear;
            studentsForExitForm.StudentFullName = studentsForExitFormModel.StudentFullName;
            studentsForExitForm.FatherName = studentsForExitFormModel.FatherName;
            studentsForExitForm.MotherName = studentsForExitFormModel.MotherName;
            studentsForExitForm.StudentUniqueId = studentsForExitFormModel.StudentUniqueId;
            studentsForExitForm.NameOfSchool = studentsForExitFormModel.NameOfSchool;
            studentsForExitForm.UdiseCode = studentsForExitFormModel.UdiseCode;
            studentsForExitForm.District = studentsForExitFormModel.District;
            studentsForExitForm.Class = studentsForExitFormModel.Class;
            studentsForExitForm.Gender = studentsForExitFormModel.Gender;
            studentsForExitForm.DOB = studentsForExitFormModel.DOB;
            studentsForExitForm.Category = studentsForExitFormModel.Category;
            studentsForExitForm.Sector = studentsForExitFormModel.Sector;
            studentsForExitForm.JobRole = studentsForExitFormModel.JobRole;
            studentsForExitForm.VTPId = studentsForExitFormModel.VTPId;
            studentsForExitForm.VTPName = studentsForExitFormModel.VTPName;
            studentsForExitForm.VTId = studentsForExitFormModel.VTId;
            studentsForExitForm.VTName = studentsForExitFormModel.VTName;
            studentsForExitForm.VTMobile = studentsForExitFormModel.VTMobile;
            studentsForExitForm.VCId = studentsForExitFormModel.VCId;
            studentsForExitForm.VCName = studentsForExitFormModel.VCName;
            studentsForExitForm.CreatedBy = studentsForExitFormModel.CreatedBy;
            studentsForExitForm.CreatedOn = studentsForExitFormModel.CreatedOn;
            studentsForExitForm.UpdatedBy = studentsForExitFormModel.UpdatedBy;
            studentsForExitForm.UpdatedOn = studentsForExitFormModel.UpdatedOn;
            studentsForExitForm.RequestType = studentsForExitFormModel.RequestType;
            // studentsForExitForm.SetAuditValues(studentsForExitFormModel.RequestType);

            return studentsForExitForm;
        }
    }
}
