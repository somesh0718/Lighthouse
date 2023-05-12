using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the VTFieldIndustryVisitConducted entity
    /// </summary>
    public class VTFieldIndustryVisitConductedRepository : GenericRepository<VTFieldIndustryVisitConducted>, IVTFieldIndustryVisitConductedRepository
    {
        /// <summary>
        /// Get list of VTFieldIndustryVisitConducted
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTFieldIndustryVisitConducted> GetVTFieldIndustryVisitConducteds()
        {
            return this.Context.VTFieldIndustryVisitConducteds.AsQueryable();
        }

        /// <summary>
        /// Get list of VTFieldIndustryVisitConducted by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTFieldIndustryVisitConducted> GetVTFieldIndustryVisitConductedsByName(string name)
        {
            var vtFieldIndustryVisitConducteds = (from v in this.Context.VTFieldIndustryVisitConducteds
                                                  select v).AsQueryable();

            return vtFieldIndustryVisitConducteds;
        }

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        public VTFieldIndustryVisitConducted GetVTFieldIndustryVisitConductedById(Guid vtFieldIndustryVisitConductedId)
        {
            return this.Context.VTFieldIndustryVisitConducteds.FirstOrDefault(v => v.VTFieldIndustryVisitConductedId == vtFieldIndustryVisitConductedId);
        }

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId using async
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        public async Task<VTFieldIndustryVisitConducted> GetVTFieldIndustryVisitConductedByIdAsync(Guid vtFieldIndustryVisitConductedId)
        {
            var vtFieldIndustryVisitConducted = await (from v in this.Context.VTFieldIndustryVisitConducteds
                                                       where v.VTFieldIndustryVisitConductedId == vtFieldIndustryVisitConductedId
                                                       select v).FirstOrDefaultAsync();

            return vtFieldIndustryVisitConducted;
        }

        /// <summary>
        /// Insert/Update VTFieldIndustryVisitConducted entity
        /// </summary>
        /// <param name="fieldIndustryVisit"></param>
        /// <param name="fieldIndustryVisitModel"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTFieldIndustryVisitConductedDetails(VTFieldIndustryVisitConducted fieldIndustryVisit, VTFieldIndustryVisitConductedModel fieldIndustryVisitModel)
        {
            try
            {
                if (RequestType.New == fieldIndustryVisit.RequestType)
                    Context.VTFieldIndustryVisitConducteds.Add(fieldIndustryVisit);
                else
                {
                    Context.Entry<VTFieldIndustryVisitConducted>(fieldIndustryVisit).State = EntityState.Modified;
                }

                Context.SaveChanges();

                if (fieldIndustryVisitModel != null)
                {
                    #region Sections

                    if (fieldIndustryVisitModel.SectionIds != null)
                    {
                        Context.VTFSections.Add(new VTFSection
                        {
                            VTFSectionId = Guid.NewGuid(),
                            VTFieldIndustryVisitConductedId = fieldIndustryVisit.VTFieldIndustryVisitConductedId,
                            SectionId = fieldIndustryVisitModel.SectionIds,
                            CreatedBy = fieldIndustryVisit.CreatedBy,
                            CreatedOn = fieldIndustryVisit.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion Sections

                    #region Unit Sessions Taught

                    if (fieldIndustryVisitModel.UnitSessionsModels != null && fieldIndustryVisitModel.UnitSessionsModels.Count > 0)
                    {
                        foreach (var unitSessionsModel in fieldIndustryVisitModel.UnitSessionsModels)
                        {
                            foreach (var unitSessionId in unitSessionsModel.SessionIds)
                            {
                                Context.VTFUnitSessionsTaughts.Add(new VTFUnitSessionsTaught
                                {
                                    VTFUnitSessionsTaughtId = Guid.NewGuid(),
                                    VTFieldIndustryVisitConductedId = fieldIndustryVisit.VTFieldIndustryVisitConductedId,
                                    ModuleId = unitSessionsModel.ModuleId,
                                    UnitId = unitSessionsModel.UnitId,
                                    SessionId = unitSessionId,
                                    CreatedBy = fieldIndustryVisit.CreatedBy,
                                    CreatedOn = fieldIndustryVisit.CreatedOn,
                                    IsActive = true
                                });
                            }
                        }
                    }

                    #endregion Unit Sessions Taught

                    #region Student Attendance

                    if (fieldIndustryVisitModel.StudentAttendances != null && fieldIndustryVisitModel.StudentAttendances.Count > 0)
                    {
                        foreach (var attendanceModel in fieldIndustryVisitModel.StudentAttendances)
                        {
                            Context.VTFStudentAttendances.Add(new VTFStudentAttendance
                            {
                                VTFStudentAttendanceId = Guid.NewGuid(),
                                VTFieldIndustryVisitConductedId = fieldIndustryVisit.VTFieldIndustryVisitConductedId,
                                VTId = fieldIndustryVisitModel.VTId,
                                ClassId = fieldIndustryVisit.ClassTaughtId,
                                StudentId = attendanceModel.StudentId,
                                IsPresent = attendanceModel.IsPresent,
                                CreatedBy = fieldIndustryVisit.CreatedBy,
                                CreatedOn = fieldIndustryVisit.CreatedOn,
                                IsActive = true
                            });
                        }
                    }

                    #endregion Student Attendance

                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVTFieldIndustryVisitConductedDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtFieldIndustryVisitConductedId)
        {
            VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted = this.Context.VTFieldIndustryVisitConducteds.FirstOrDefault(v => v.VTFieldIndustryVisitConductedId == vtFieldIndustryVisitConductedId);

            if (vtFieldIndustryVisitConducted != null)
            {
                Context.Entry<VTFieldIndustryVisitConducted>(vtFieldIndustryVisitConducted).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTFieldIndustryVisitConducted by Name
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        public List<string> CheckVTFieldIndustryVisitConductedExistByName(VTFieldIndustryVisitConductedModel fieldIndustryVisitModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                var fieldIndustryVisits = this.Context.VTFieldIndustryVisitConducteds.Where(v => v.VTId == fieldIndustryVisitModel.VTId && v.ReportingDate.Date == fieldIndustryVisitModel.ReportingDate.Date).ToList();

                if (fieldIndustryVisitModel.RequestType == RequestType.New && fieldIndustryVisits.Count > 0)
                {
                    var fieldIndustryVisitItem = fieldIndustryVisits.FirstOrDefault(f => f.ClassTaughtId == fieldIndustryVisitModel.ClassTaughtId && f.SectionTaughtId == fieldIndustryVisitModel.SectionIds);

                    if (fieldIndustryVisitItem != null)
                    {
                        errorMessageList.Add("Selected class & sesction is already submitted");
                    }
                }

                //2. Allowed 7 days Back Dated Reporting from Today
                //if (!(fieldIndustryVisitModel.ReportingDate.Date >= Constants.GetCurrentDateTime.AddDays(-1 * Constants.BackDatedReportingDays).Date))
                //{
                //    errorMessageList.Add(string.Format("Only allowed {0} days Back Dated Reporting from Today", Constants.BackDatedReportingDays));
                //}

                //3. Avoid VT Field Visit Reporting on Sunday
                if (string.Equals(fieldIndustryVisitModel.ReportingDate.Date.DayOfWeek.ToString(), "Sunday"))
                {
                    errorMessageList.Add("User cannot submit the VT Field Visit Conduced on Sunday");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVTFieldIndustryVisitConductedExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of VTFieldIndustryVisitConducted with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTFieldIndustryVisitConductedViewModel> GetVTFieldIndustryVisitConductedsByCriteria(SearchVTFieldIndustryVisitConductedModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTFieldIndustryVisitConductedViewModels.FromSql<VTFieldIndustryVisitConductedViewModel>("CALL GetVTFieldIndustryVisitConductedsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of VTFSections by fieldIndustryVisitId
        /// </summary>
        /// <param name="fieldIndustryVisitId"></param>
        /// <returns></returns>
        public Guid GetVTFSectionsByFieldIndustryVisitId(Guid fieldIndustryVisitId)
        {
            var vtfSections = this.Context.VTFSections.Where(s => s.VTFieldIndustryVisitConductedId == fieldIndustryVisitId).Select(cs => cs.SectionId).FirstOrDefault();

            return vtfSections;
        }

        /// <summary>
        /// Get list of UnitSessionsTaughts by fieldIndustryVisitId
        /// </summary>
        /// <param name="fieldIndustryVisitId"></param>
        /// <returns></returns>
        public IList<UnitSessionsModel> GetVTFUnitSessionsTaughtsByFieldIndustryVisitId(Guid fieldIndustryVisitId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "fieldIndustryVisitId", MySqlDbType = MySqlDbType.VarChar, Value = fieldIndustryVisitId };

            return Context.UnitSessionsModels.FromSql<UnitSessionsModel>("CALL GetUnitSessionsTaughtsByFVId (@fieldIndustryVisitId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of VTFStudents by fieldIndustryVisitId
        /// </summary>
        /// <param name="fieldIndustryVisitId"></param>
        /// <returns></returns>
        public IList<StudentAttendanceModel> GetVTFStudentsByFieldIndustryVisitId(Guid fieldIndustryVisitId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "fieldIndustryVisitId", MySqlDbType = MySqlDbType.VarChar, Value = fieldIndustryVisitId };

            return Context.StudentAttendanceModels.FromSql<StudentAttendanceModel>("CALL GetStudentAttendancesByFVId (@fieldIndustryVisitId)", sqlParams).ToList();
        }
    }
}