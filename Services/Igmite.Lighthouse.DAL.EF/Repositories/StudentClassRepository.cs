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
    /// Repository of the StudentClass entity
    /// </summary>
    public class StudentClassRepository : GenericRepository<StudentClass>, IStudentClassRepository
    {
        /// <summary>
        /// Get list of StudentClass
        /// </summary>
        /// <returns></returns>
        public IQueryable<StudentClass> GetStudentClasses()
        {
            return this.Context.StudentClasses.AsQueryable();
        }

        public IQueryable<StudentClass> GetStudentClassesByYear(ExitSurveyRequestModel exitSurveyDetailsModel)
        {
            return this.Context.StudentClasses.Where(x => x.AcademicYearId == exitSurveyDetailsModel.AcademicYearId).AsQueryable();
        }

        /// <summary>
        /// Get list of StudentClass by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StudentClass> GetStudentClassesByName(string name)
        {
            var studentClasses = (from s in this.Context.StudentClasses
                                  where s.FirstName.Contains(name)
                                  select s).AsQueryable();

            return studentClasses;
        }

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        public IQueryable<StudentClass> GetStudentsByNames(List<string> studentNames)
        {
            var studentClasses = (from s in this.Context.StudentClasses
                                  where studentNames.Contains(s.FullName)
                                  select s).AsQueryable();

            return studentClasses;
        }

        /// <summary>
        /// Get StudentClass by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentClass GetStudentClassById(Guid studentId)
        {
            return this.Context.StudentClasses.FirstOrDefault(s => s.StudentId == studentId);
        }

        /// <summary>
        /// Get StudentClass by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentClass> GetStudentClassByIdAsync(Guid studentId)
        {
            var studentClass = await (from s in this.Context.StudentClasses
                                      where s.StudentId == studentId
                                      select s).FirstOrDefaultAsync();

            return studentClass;
        }

        /// <summary>
        /// Insert/Update StudentClass entity
        /// </summary>
        /// <param name="studentClass"></param>
        /// <returns></returns>
        public bool SaveOrUpdateStudentClassDetails(StudentClass studentClass)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    if (RequestType.New == studentClass.RequestType)
                    {
                        Context.StudentClasses.Add(studentClass);

                        Context.StudentClassMapping.Add(
                            new StudentClassMapping
                            {
                                StudentClassMappingId = Guid.NewGuid(),
                                AcademicYearId = studentClass.AcademicYearId,
                                SchoolId = studentClass.SchoolId,
                                ClassId = studentClass.ClassId,
                                SectionId = studentClass.SectionId,
                                VTId = studentClass.VTId.Value,
                                StudentId = studentClass.StudentId,
                                CreatedBy = studentClass.CreatedBy,
                                CreatedOn = studentClass.CreatedOn,
                                IsActive = studentClass.IsActive
                            });
                    }
                    else
                    {
                        StudentClassMapping studentMappedWithClass = this.Context.StudentClassMapping.FirstOrDefault(s => s.AcademicYearId == studentClass.AcademicYearId && s.SchoolId == studentClass.SchoolId && s.VTId == studentClass.VTId && s.StudentId == studentClass.StudentId);

                        if (studentMappedWithClass != null)
                        {
                            studentMappedWithClass.AcademicYearId = studentClass.AcademicYearId;
                            studentMappedWithClass.SchoolId = studentClass.SchoolId;
                            studentMappedWithClass.ClassId = studentClass.ClassId;
                            studentMappedWithClass.SectionId = studentClass.SectionId;
                            studentMappedWithClass.VTId = studentClass.VTId.Value;
                            studentMappedWithClass.IsActive = studentClass.IsActive;
                            studentMappedWithClass.UpdatedBy = studentClass.UpdatedBy;
                            studentMappedWithClass.UpdatedOn = Constants.GetCurrentDateTime;

                            Context.Entry<StudentClassMapping>(studentMappedWithClass).State = EntityState.Modified;
                        }

                        StudentClassDetail studentClassDetail = this.Context.StudentClassDetails.FirstOrDefault(s => s.StudentId == studentClass.StudentId);
                        if (studentClassDetail != null)
                        {
                            studentClassDetail.UpdatedBy = studentClass.UpdatedBy;
                            studentClassDetail.UpdatedOn = Constants.GetCurrentDateTime;
                            studentClassDetail.IsActive = studentClass.IsActive;
                            Context.Entry<StudentClassDetail>(studentClassDetail).State = EntityState.Modified;
                        }

                        Context.Entry<StudentClass>(studentClass).State = EntityState.Modified;
                    }

                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("DAL > SaveOrUpdateStudentClassDetails", ex);
                }
            }

            return true;
        }

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid studentId, string userId)
        {
            StudentClass studentClass = this.Context.StudentClasses.FirstOrDefault(s => s.StudentId == studentId);

            if (studentClass != null)
            {
                VTRStudentAttendance vtStudentAttendance = this.Context.VTRStudentAttendances.FirstOrDefault(s => s.StudentId == studentId);

                VTGStudentAttendance glStudentAttendance = this.Context.VTGStudentAttendances.FirstOrDefault(s => s.StudentId == studentId);

                VTFStudentAttendance fvStudentAttendance = this.Context.VTFStudentAttendances.FirstOrDefault(s => s.StudentId == studentId);

                StudentClassMapping studentMappedWithClass = this.Context.StudentClassMapping.FirstOrDefault(s => s.StudentId == studentId);
                StudentClassDetail studentClassDetails = this.Context.StudentClassDetails.FirstOrDefault(s => s.StudentId == studentId);

                if (vtStudentAttendance == null && glStudentAttendance == null && fvStudentAttendance == null)
                {
                    if (studentMappedWithClass != null)
                    {
                        Context.Entry<StudentClassMapping>(studentMappedWithClass).State = EntityState.Deleted;
                        Context.SaveChanges();
                    }
                    if (studentClassDetails != null)
                    {
                        Context.Entry<StudentClassDetail>(studentClassDetails).State = EntityState.Deleted;
                        Context.SaveChanges();
                    }

                    Context.Entry<StudentClass>(studentClass).State = EntityState.Deleted;
                }
                else
                {
                    if (studentMappedWithClass != null)
                    {
                        studentMappedWithClass.IsActive = false;
                        Context.Entry<StudentClassMapping>(studentMappedWithClass).State = EntityState.Modified;
                    }

                    if (studentClassDetails != null)
                    {
                        studentClassDetails.IsActive = false;
                        Context.Entry<StudentClassDetail>(studentClassDetails).State = EntityState.Modified;
                    }

                    studentClass.DeletedBy = userId;
                    studentClass.DeletedOn = Constants.GetCurrentDateTime;
                    studentClass.IsActive = false;
                    Context.Entry<StudentClass>(studentClass).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate StudentClass by Name
        /// </summary>
        /// <param name="studentClass"></param>
        /// <param name="studentClassModel"></param>
        /// <returns></returns>
        public string CheckStudentClassExistByName(StudentClass studentClass, StudentClassModel studentClassModel)
        {
            StudentClass studentClassItem = this.Context.StudentClasses.FirstOrDefault(s => s.AcademicYearId == studentClassModel.AcademicYearId && s.SchoolId == studentClassModel.SchoolId && s.ClassId == studentClassModel.ClassId && s.SectionId == studentClassModel.SectionId && s.FullName == studentClassModel.FullName && s.IsActive == true);

            if (studentClassModel.RequestType == RequestType.New)
            {
                if (studentClassItem != null)
                {
                    //return string.Format("Student is already exists");
                }
            }
            else if (studentClassModel.RequestType == RequestType.Edit)
            {
                VTClass vtClassItem = (from vtc in this.Context.VTClasses
                                       join vtcs in this.Context.VTClassSections on vtc.VTClassId equals vtcs.VTClassId
                                       where vtc.AcademicYearId == studentClassModel.AcademicYearId && vtc.SchoolId == studentClassModel.SchoolId && vtc.ClassId == studentClassModel.ClassId && vtcs.SectionId == studentClassModel.SectionId && vtc.VTId == studentClassModel.VTId
                                       select vtc).FirstOrDefault();

                if (vtClassItem == null)
                {
                    return string.Format("Student's Class cannot be changed because trainer class does not exists");
                }
            }

            return string.Empty;
        }

        /// <summary>}
        /// List of StudentClass with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<StudentClassViewModel> GetStudentClassesByCriteria(SearchStudentClassModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[15];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "jobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[6] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[7] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
            sqlParams[8] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Int32, Value = searchModel.HMId };
            sqlParams[9] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[10] = new MySqlParameter { ParameterName = "isRollover", MySqlDbType = MySqlDbType.Bool, Value = searchModel.IsRollover };
            sqlParams[11] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[12] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[13] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[14] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.StudentClassViewModels.FromSql<StudentClassViewModel>("CALL GetStudentClassesByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @sectorId, @jobRoleId, @schoolId, @classId, @hmId, @status, @isRollover, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}