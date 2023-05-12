using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the ExitSurveyDetails entity
    /// </summary>
    public class ExitSurveyDetailsRepository : GenericRepository<ExitSurveyDetails>, IExitSurveyDetailsRepository
    {
        /// <summary>
        /// Get list of ExitSurveyDetails
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExitSurveyDetails> GetExitSurveyDetails()
        {
            return this.Context.ExitSurveyDetails.AsQueryable();
        }

        /// <summary>
        /// Get list of ExitSurveyDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ExitSurveyDetails GetExitSurveyDetailsByStudentId(Guid exitStudentId)
        {
            var exitSurveyDetails = (from s in this.Context.ExitSurveyDetails
                                     where s.ExitStudentId.Equals(exitStudentId)
                                     select s).FirstOrDefault();

            return exitSurveyDetails;
        }

        /// <summary>
        /// Get ExitSurveyDetails by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ExitSurveyDetails GetExitSurveyDetailsById(int id)
        {
            return this.Context.ExitSurveyDetails.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Get ExitSurveyDetails by AcademicYear & StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ExitSurveyDetails GetExitSurveyDetailsById(Guid? academicYearId, Guid studentId)
        {
            return this.Context.ExitSurveyDetails.FirstOrDefault(s => s.AcademicYearId == academicYearId && s.ExitStudentId == studentId);
        }

        /// <summary>
        /// Get ExitSurveyDetails by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<ExitSurveyDetails> GetExitSurveyDetailsByIdAsync(int id)
        {
            var exitSurveyDetails = await (from s in this.Context.ExitSurveyDetails
                                           where s.Id == id
                                           select s).FirstOrDefaultAsync();

            return exitSurveyDetails;
        }

        /// <summary>
        /// Insert/Update ExitSurveyDetails entity
        /// </summary>
        /// <param name="exitSurveyDetails"></param>
        /// <returns></returns>
        public bool SaveOrUpdateExitSurveyDetailsDetails(ExitSurveyDetails exitSurveyDetails)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    if (RequestType.New == exitSurveyDetails.RequestType)
                    {
                        Context.ExitSurveyDetails.Add(exitSurveyDetails);
                    }
                    else
                    {
                        Context.Entry<ExitSurveyDetails>(exitSurveyDetails).State = EntityState.Modified;
                    }

                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("DAL > SaveOrUpdateExitSurveyDetailsDetails", ex);
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
            ExitSurveyDetails exitSurveyDetails = this.Context.ExitSurveyDetails.FirstOrDefault(s => s.ExitStudentId == studentId);

            if (exitSurveyDetails != null)
            {
                exitSurveyDetails.IsActive = false;

                Context.Entry<ExitSurveyDetails>(exitSurveyDetails).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate ExitSurveyDetails by Name
        /// </summary>
        /// <param name="exitSurveyDetailsModel"></param>
        /// <returns></returns>
        public bool CheckExitSurveyDetailsExistByName(ExitSurveyDetailsModel exitSurveyDetailsModel)
        {
            bool valResult = false;
            if (exitSurveyDetailsModel.RequestType == RequestType.New)
            {
                ExitSurveyDetails exitSurveyDetails = this.Context.ExitSurveyDetails.FirstOrDefault(s => s.ExitStudentId == exitSurveyDetailsModel.ExitStudentId);

                valResult = (exitSurveyDetails != null);
            }

            return valResult;
        }

        /// <summary>
        /// Get Student Exit Survey Details By Id
        /// </summary>
        /// <param name="exitSurveyModel"></param>
        /// <returns></returns>
        public ExitSurveyReportModel GetStudentExitSurveyById(ExitSurveyRequestModel exitSurveyModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[8];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserType", MySqlDbType = MySqlDbType.VarChar, Value = exitSurveyModel.UserType };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyModel.UserId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyModel.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyModel.ClassId };
            sqlParams[4] = new MySqlParameter { ParameterName = "StudentId", MySqlDbType = MySqlDbType.Guid, Value = exitSurveyModel.StudentId };
            sqlParams[5] = new MySqlParameter { ParameterName = "StudentUniqueId", MySqlDbType = MySqlDbType.VarChar, Value = exitSurveyModel.StudentUniqueId };
            sqlParams[6] = new MySqlParameter { ParameterName = "PageIndex", MySqlDbType = MySqlDbType.Int32, Value = 0 };
            sqlParams[7] = new MySqlParameter { ParameterName = "PageSize", MySqlDbType = MySqlDbType.Int32, Value = 1 };

            return this.Context.ExitSurveyReports.FromSql("CALL GetExitSurveyReportV2 (@UserType, @UserId, @AcademicYearId, @ClassId, @StudentId, @StudentUniqueId, @PageIndex, @PageSize)", sqlParams).FirstOrDefault();
        }
    }
}