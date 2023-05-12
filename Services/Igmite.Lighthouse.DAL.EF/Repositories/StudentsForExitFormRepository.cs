using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the StudentsForExitForm entity
    /// </summary>
    public class StudentsForExitFormRepository : GenericRepository<StudentsForExitForm>, IStudentsForExitFormRepository
    {
        /// <summary>
        /// Get list of StudentsForExitForm
        /// </summary>
        /// <returns></returns>
        public IList<StudentsForExitSurveyViewModel> GetStudentsForExitForm(ExitSurveyRequestModel exitSurveyDetailsModel)
        {
            List<StudentsForExitSurveyViewModel> studentsForExitForm = new List<StudentsForExitSurveyViewModel>();

            //AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(ay => ay.AcademicYearId == exitSurveyDetailsModel.AcademicYearId);
            //SchoolClass schoolClass = this.Context.SchoolClasses.FirstOrDefault(sc => sc.ClassId == exitSurveyDetailsModel.ClassId);

            //if (exitSurveyDetailsModel != null && exitSurveyDetailsModel.UserType == "VT")
            //{
            //    studentsForExitForm = this.Context.StudentsForExitForm.Where(x => x.VTId == exitSurveyDetailsModel.UserId && x.AcademicYear == academicYear.YearName && x.Class == schoolClass.Name).ToList();
            //}
            //else if (exitSurveyDetailsModel != null && exitSurveyDetailsModel.UserType == "VC")
            //{
            //    studentsForExitForm = this.Context.StudentsForExitForm.Where(x => x.VCId == exitSurveyDetailsModel.UserId && x.AcademicYear == academicYear.YearName && x.Class == schoolClass.Name).ToList();
            //}
            //else
            //{
            //    studentsForExitForm = this.Context.StudentsForExitForm.Where(x => x.AcademicYear == academicYear.YearName && x.Class == schoolClass.Name).ToList();
            //}

            //if (academicYear.YearName == "2020-2021" && studentsForExitForm != null && studentsForExitForm.Count > 0)
            //{
            //    return studentsForExitForm.AsQueryable();
            //}
            //else
            //{
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserType", MySqlDbType = MySqlDbType.VarChar, Value = exitSurveyDetailsModel.UserType };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.UserId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.ClassId };
            sqlParams[4] = new MySqlParameter { ParameterName = "StudentId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.StudentId };

            List<StudentsForExitSurveyViewModel> studentsForExitForms = Context.StudentsForExitSurveyViewModels.FromSql<StudentsForExitSurveyViewModel>("CALL GetStudentsByClassAndAcademicYearV2 (@UserType, @UserId, @AcademicYearId, @ClassId, @StudentId)", sqlParams).ToList();

            //if (studentsForExitForm != null && studentsForExitForm.Count > 0)
            //{
            //    studentsForExitForms.AddRange(studentsForExitForm);
            //    studentsForExitForms = studentsForExitForms.Distinct().ToList();
            //}

            return studentsForExitForms;
            //}
        }

        public IQueryable<ExitSurveyReportModel> GetExitSurveyReport(ExitSurveyRequestModel exitSurveyDetailsModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[8];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserType", MySqlDbType = MySqlDbType.VarChar, Value = exitSurveyDetailsModel.UserType };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.UserId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.ClassId };
            sqlParams[4] = new MySqlParameter { ParameterName = "StudentId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyDetailsModel.StudentId };
            sqlParams[5] = new MySqlParameter { ParameterName = "StudentUniqueId", MySqlDbType = MySqlDbType.VarChar, Value = exitSurveyDetailsModel.StudentUniqueId };
            sqlParams[6] = new MySqlParameter { ParameterName = "PageIndex", MySqlDbType = MySqlDbType.Int32, Value = exitSurveyDetailsModel.PageIndex };
            sqlParams[7] = new MySqlParameter { ParameterName = "PageSize", MySqlDbType = MySqlDbType.Int32, Value = exitSurveyDetailsModel.PageSize };

            return this.Context.ExitSurveyReports.FromSql("CALL GetExitSurveyReportV2 (@UserType, @UserId, @AcademicYearId, @ClassId, @StudentId, @StudentUniqueId, @PageIndex, @PageSize)", sqlParams).AsQueryable();
        }

        /// <summary>
        /// Get list of StudentsForExitForm by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StudentsForExitForm> GetStudentsForExitFormByName(string name)
        {
            var studentsForExitForm = (from s in this.Context.StudentsForExitForm
                                       where s.StudentFullName.Contains(name)
                                       select s).AsQueryable();

            return studentsForExitForm;
        }

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        public IQueryable<StudentsForExitForm> GetStudentsByNames(List<string> studentNames)
        {
            var studentsForExitForm = (from s in this.Context.StudentsForExitForm
                                       where studentNames.Contains(s.StudentFullName)
                                       select s).AsQueryable();

            return studentsForExitForm;
        }

        /// <summary>
        /// Get StudentsForExitForm by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentsForExitForm GetStudentsForExitFormById(Guid studentId)
        {
            return this.Context.StudentsForExitForm.FirstOrDefault(s => s.ExitStudentId == studentId);
        }

        /// <summary>
        /// Get StudentsForExitForm by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentsForExitForm> GetStudentsForExitFormByIdAsync(Guid studentId)
        {
            var studentsForExitForm = await (from s in this.Context.StudentsForExitForm
                                             where s.ExitStudentId == studentId
                                             select s).FirstOrDefaultAsync();

            return studentsForExitForm;
        }

        /// <summary>
        /// Insert/Update StudentsForExitForm entity
        /// </summary>
        /// <param name="studentsForExitForm"></param>
        /// <returns></returns>
        public bool SaveOrUpdateStudentsForExitFormDetails(StudentsForExitForm studentsForExitForm)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    VocationalTrainer VTDetails = new VocationalTrainer();
                    VocationalTrainingProvider VTPDetails = new VocationalTrainingProvider();
                    if (studentsForExitForm != null && studentsForExitForm.VTId != null)
                    {
                        VTDetails = this.Context.VocationalTrainers.FirstOrDefault(x => x.VTId == studentsForExitForm.VTId);
                        if (VTDetails != null)
                        {
                            studentsForExitForm.VTName = VTDetails.FullName;
                            studentsForExitForm.VTMobile = VTDetails.Mobile;
                            var vcdata = this.Context.VocationalCoordinators.FirstOrDefault(x => x.VCId == VTDetails.VCTrainer.VCId);
                            if (vcdata != null)
                            {
                                studentsForExitForm.VCId = vcdata.VCId;
                                studentsForExitForm.VCName = vcdata.FullName;
                            }
                        }
                    }
                    if (studentsForExitForm != null && studentsForExitForm.VTPId != null)
                    {
                        VTPDetails = this.Context.VocationalTrainingProviders.FirstOrDefault(x => x.VTPId == studentsForExitForm.VTPId);
                        if (VTPDetails != null)
                        {
                            studentsForExitForm.VTPName = VTPDetails.VTPName;
                        }
                    }
                    if (studentsForExitForm != null && !string.IsNullOrEmpty(studentsForExitForm.NameOfSchool))
                    {
                        var schoolDetails = this.Context.Schools.FirstOrDefault(x => x.SchoolId.ToString() == studentsForExitForm.NameOfSchool);
                        if (schoolDetails != null)
                            studentsForExitForm.NameOfSchool = schoolDetails.SchoolName;
                    }

                    if (RequestType.New == studentsForExitForm.RequestType)
                    {
                        Context.StudentsForExitForm.Add(studentsForExitForm);
                    }
                    else
                    {
                        Context.Entry<StudentsForExitForm>(studentsForExitForm).State = EntityState.Modified;
                    }

                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("DAL > SaveOrUpdateStudentsForExitFormDetails", ex);
                }
            }

            return true;
        }

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid studentId)
        {
            StudentsForExitForm studentsForExitForm = this.Context.StudentsForExitForm.FirstOrDefault(s => s.ExitStudentId == studentId);

            if (studentsForExitForm != null)
            {
                studentsForExitForm.IsActive = false;
                Context.Entry<StudentsForExitForm>(studentsForExitForm).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate StudentsForExitForm by Name
        /// </summary>
        /// <param name="studentsForExitFormModel"></param>
        /// <returns></returns>
        public bool CheckStudentsForExitFormExistByName(StudentsForExitFormModel studentsForExitFormModel)
        {
            bool valResult = false;
            if (studentsForExitFormModel.RequestType == RequestType.New)
            {
                StudentsForExitForm studentsForExitForm = this.Context.StudentsForExitForm.FirstOrDefault(s => s.StudentFullName == studentsForExitFormModel.StudentFullName && s.UdiseCode == studentsForExitFormModel.UdiseCode && s.Class == studentsForExitFormModel.Class);

                valResult = (studentsForExitForm != null);
            }

            return valResult;
        }

        ///// <summary>}
        ///// List of StudentsForExitForm with filter criteria}
        ///// </summary>}
        ///// <param name="searchModel"></param>}
        ///// <returns></returns>}
        //public IList<StudentsForExitFormViewModel> GetStudentsForExitFormByCriteria(SearchStudentsForExitFormModel searchModel)
        //{
        //    MySqlParameter[] sqlParams = new MySqlParameter[5];
        //    sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
        //    sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
        //    sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
        //    sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
        //    sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

        //    return Context.StudentsForExitFormViewModels.FromSql<StudentsForExitFormViewModel>("CALL GetStudentsForExitFormByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        //}
    }
}