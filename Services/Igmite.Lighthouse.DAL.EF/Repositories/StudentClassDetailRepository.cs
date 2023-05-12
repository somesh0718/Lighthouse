using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the StudentClassDetail entity
    /// </summary>
    public class StudentClassDetailRepository : GenericRepository<StudentClassDetail>, IStudentClassDetailRepository
    {
        /// <summary>
        /// Get list of StudentClassDetail
        /// </summary>
        /// <returns></returns>
        public IQueryable<StudentClassDetail> GetStudentClassDetails()
        {
            return this.Context.StudentClassDetails.AsQueryable();
        }

        /// <summary>
        /// Get list of StudentClassDetail by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StudentClassDetail> GetStudentClassDetailsByName(string name)
        {
            var studentClassDetails = (from s in this.Context.StudentClassDetails
                                       where s.FatherName.Contains(name)
                                       select s).AsQueryable();

            return studentClassDetails;
        }

        /// <summary>
        /// Get StudentClassDetail by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentClassDetail GetStudentClassDetailById(Guid studentId)
        {
            return this.Context.StudentClassDetails.FirstOrDefault(s => s.StudentId == studentId);
        }

        /// <summary>
        /// Get StudentClassDetail by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentClassDetail> GetStudentClassDetailByIdAsync(Guid studentId)
        {
            var studentClassDetail = await (from s in this.Context.StudentClassDetails
                                            where s.StudentId == studentId
                                            select s).FirstOrDefaultAsync();

            return studentClassDetail;
        }

        /// <summary>
        /// Insert/Update StudentClassDetail entity
        /// </summary>
        /// <param name="studentClassDetail"></param>
        /// <returns></returns>
        public bool SaveOrUpdateStudentClassDetailDetails(StudentClassDetail studentClassDetail)
        {
            try
            {
                if (RequestType.New == studentClassDetail.RequestType)
                {
                    Context.StudentClassDetails.Add(studentClassDetail);
                }
                else
                {
                    Context.Entry<StudentClassDetail>(studentClassDetail).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateStudentClassDetailDetails", ex);
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
            StudentClassDetail studentClassDetail = this.Context.StudentClassDetails.FirstOrDefault(s => s.StudentId == studentId);

            if (studentClassDetail != null)
            {
                Context.Entry<StudentClassDetail>(studentClassDetail).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate StudentClassDetail by Name
        /// </summary>
        /// <param name="studentClassDetailModel"></param>
        /// <returns></returns>
        public IList<string> CheckStudentClassDetailExistByName(StudentClassDetailModel studentClassDetailModel)
        {
            var errorMessage = new List<string>();

            try
            {
                StudentClassDetail studentItem = this.Context.StudentClassDetails.FirstOrDefault(s => s.StudentId == studentClassDetailModel.StudentId && s.IsActive == true);

                if (studentClassDetailModel.RequestType == RequestType.New)
                {
                    if (studentItem != null)
                    {
                        errorMessage.Add("Selected student is already exist");
                    }

                    if (!string.IsNullOrEmpty(studentClassDetailModel.StudentRollNumber))
                    {
                        var valStudentUniqueId = this.Context.StudentClassDetails.FirstOrDefault(s => s.StudentRollNumber == studentClassDetailModel.StudentRollNumber && s.IsActive == true);
                        if (valStudentUniqueId != null)
                        {
                            StudentClass existStudentUID = (from sc in this.Context.StudentClasses
                                                            join ay in this.Context.AcademicYears on sc.AcademicYearId equals ay.AcademicYearId
                                                            where sc.IsActive == true && ay.IsCurrentAcademicYear == true && sc.StudentId == valStudentUniqueId.StudentId
                                                            select sc).FirstOrDefault();
                            if (existStudentUID != null)
                            {
                                errorMessage.Add(string.Format("Student Unique ID is already exist for another student ({0})", existStudentUID.FullName));
                            }
                        }
                    }
                }

                VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.Mobile == studentClassDetailModel.Mobile || v.Mobile == studentClassDetailModel.Mobile1 && v.IsActive == true);
                HeadMaster headMaster = this.Context.HeadMasters.FirstOrDefault(h => h.Mobile == studentClassDetailModel.Mobile || h.Mobile == studentClassDetailModel.Mobile1 && h.IsActive == true);

                if ((!string.IsNullOrEmpty(studentClassDetailModel.Mobile) || !string.IsNullOrEmpty(studentClassDetailModel.Mobile1)) && (vocationalTrainer != null || headMaster != null))
                {
                    if (vocationalTrainer != null && (string.Equals(vocationalTrainer.Mobile, studentClassDetailModel.Mobile) || string.Equals(vocationalTrainer.Mobile, studentClassDetailModel.Mobile1) || string.Equals(vocationalTrainer.Mobile1, studentClassDetailModel.Mobile) || string.Equals(vocationalTrainer.Mobile1, studentClassDetailModel.Mobile1)))
                    {
                        errorMessage.Add("Vocational Trainer's mobile number is not acceptable in the Student Class & Assessment");
                    }

                    if (headMaster != null && (string.Equals(headMaster.Mobile, studentClassDetailModel.Mobile) || string.Equals(headMaster.Mobile, studentClassDetailModel.Mobile1) || string.Equals(headMaster.Mobile1, studentClassDetailModel.Mobile) || string.Equals(headMaster.Mobile1, studentClassDetailModel.Mobile1)))
                    {
                        errorMessage.Add("Head Master's mobile number is not acceptable in the Student Class & Assessment");
                    }
                }

                if (Constants.StateCode != "MH" && !string.IsNullOrEmpty(studentClassDetailModel.AadhaarNumber) && ((studentItem != null && !string.Equals(studentItem.AadhaarNumber, studentClassDetailModel.AadhaarNumber))))
                {
                    var studentAadhaarItem = this.Context.StudentClassDetails.FirstOrDefault(s => s.AadhaarNumber == studentClassDetailModel.AadhaarNumber && s.IsActive == true);
                    if (studentAadhaarItem != null)
                    {
                        errorMessage.Add("Aadhaar Number is already exist for another student");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckStudentClassDetailExistByName", ex);
            }

            return errorMessage;
        }

        /// <summary>}
        /// List of StudentClassDetail with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<StudentClassDetailViewModel> GetStudentClassDetailsByCriteria(SearchStudentClassDetailModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[14];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "jobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[6] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[7] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
            sqlParams[8] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Int32, Value = searchModel.HMId };
            sqlParams[9] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[10] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[11] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[12] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[13] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.StudentClassDetailViewModels.FromSql<StudentClassDetailViewModel>("CALL GetStudentClassDetailsByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @sectorId, @jobRoleId, @schoolId, @classId, @hmId, @userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VocationalEducationAssessment with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public VocationalEducationAssessmentModel GetVocationalEducationAssessmentBySchoolAndVTId(SearchStudentClassDetailModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[2] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            VocationalEducationAssessmentModel vocationalEducationAssessmentModel = new VocationalEducationAssessmentModel();

            vocationalEducationAssessmentModel.VEAHeaderModels = Context.VEAHeaderModels.FromSql<VEAHeaderModel>("CALL GetVEAHeaderBySchoolAndVTId (@academicYearId,@vtId, @schoolId)", sqlParams).FirstOrDefault();

            vocationalEducationAssessmentModel.VEADetailsModels = Context.VEADetailsModels.FromSql<VEADetailsModel>("CALL GetVocationalEducationAssessmentBySchoolAndVTId (@academicYearId,@vtId, @schoolId)", sqlParams).ToList();
            return vocationalEducationAssessmentModel;
        }
    }
}