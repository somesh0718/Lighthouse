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
    /// Repository of the VTGuestLectureConducted entity
    /// </summary>
    public class VTGuestLectureConductedRepository : GenericRepository<VTGuestLectureConducted>, IVTGuestLectureConductedRepository
    {
        /// <summary>
        /// Get list of VTGuestLectureConducted
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTGuestLectureConducted> GetVTGuestLectureConducteds()
        {
            return this.Context.VTGuestLectureConducteds.AsQueryable();
        }

        /// <summary>
        /// Get list of VTGuestLectureConducted by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTGuestLectureConducted> GetVTGuestLectureConductedsByName(string name)
        {
            var vtGuestLectureConducteds = (from v in this.Context.VTGuestLectureConducteds
                                            select v).AsQueryable();

            return vtGuestLectureConducteds;
        }

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        public VTGuestLectureConducted GetVTGuestLectureConductedById(Guid vtGuestLectureId)
        {
            return this.Context.VTGuestLectureConducteds.FirstOrDefault(v => v.VTGuestLectureId == vtGuestLectureId);
        }

        /// <summary>
        /// Get VTGuestLectureConducted by VTGuestLectureId using async
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        public async Task<VTGuestLectureConducted> GetVTGuestLectureConductedByIdAsync(Guid vtGuestLectureId)
        {
            var vtGuestLectureConducted = await (from v in this.Context.VTGuestLectureConducteds
                                                 where v.VTGuestLectureId == vtGuestLectureId
                                                 select v).FirstOrDefaultAsync();

            return vtGuestLectureConducted;
        }

        /// <summary>
        /// Insert/Update VTGuestLectureConducted entity
        /// </summary>
        /// <param name="guestLecture"></param>
        /// <param name="guestLectureModel"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTGuestLectureConductedDetails(VTGuestLectureConducted guestLecture, VTGuestLectureConductedModel guestLectureModel)
        {
            try
            {
                if (RequestType.New == guestLecture.RequestType)
                    Context.VTGuestLectureConducteds.Add(guestLecture);
                else
                {
                    Context.Entry<VTGuestLectureConducted>(guestLecture).State = EntityState.Modified;
                }

                Context.SaveChanges();

                if (guestLectureModel != null)
                {
                    #region Sections

                    if (guestLectureModel.SectionIds != null)
                    {
                        Context.VTGSections.Add(new VTGSection
                        {
                            VTGSectionId = Guid.NewGuid(),
                            VTGuestLectureId = guestLecture.VTGuestLectureId,
                            SectionId = guestLectureModel.SectionIds,
                            CreatedBy = guestLecture.CreatedBy,
                            CreatedOn = guestLecture.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion Sections

                    #region Methodology

                    if (guestLectureModel.MethodologyIds != null && guestLectureModel.MethodologyIds.Count > 0)
                    {
                        foreach (var methodologyId in guestLectureModel.MethodologyIds)
                        {
                            Context.VTGMethodologies.Add(new VTGMethodology
                            {
                                VTGMethodologyId = Guid.NewGuid(),
                                VTGuestLectureId = guestLecture.VTGuestLectureId,
                                MethodologyId = methodologyId,
                                CreatedBy = guestLecture.CreatedBy,
                                CreatedOn = guestLecture.CreatedOn,
                                IsActive = true
                            });
                        }
                    }

                    #endregion Methodology

                    #region Unit Sessions Taught

                    if (guestLectureModel.UnitSessionsModels != null && guestLectureModel.UnitSessionsModels.Count > 0)
                    {
                        foreach (var unitSessionsModel in guestLectureModel.UnitSessionsModels)
                        {
                            foreach (var unitSessionId in unitSessionsModel.SessionIds)
                            {
                                Context.VTGUnitSessionsTaughts.Add(new VTGUnitSessionsTaught
                                {
                                    VTGUnitSessionsTaughtId = Guid.NewGuid(),
                                    VTGuestLectureId = guestLecture.VTGuestLectureId,
                                    ModuleId = unitSessionsModel.ModuleId,
                                    UnitId = unitSessionsModel.UnitId,
                                    SessionId = unitSessionId,
                                    CreatedBy = guestLecture.CreatedBy,
                                    CreatedOn = guestLecture.CreatedOn,
                                    IsActive = true
                                });
                            }
                        }
                    }

                    #endregion Unit Sessions Taught

                    #region Student Attendance

                    if (guestLectureModel.StudentAttendances != null && guestLectureModel.StudentAttendances.Count > 0)
                    {
                        foreach (var attendanceModel in guestLectureModel.StudentAttendances)
                        {
                            Context.VTGStudentAttendances.Add(new VTGStudentAttendance
                            {
                                VTGStudentAttendanceId = Guid.NewGuid(),
                                VTGuestLectureId = guestLecture.VTGuestLectureId,
                                VTId = guestLectureModel.VTId,
                                ClassId = guestLecture.ClassTaughtId,
                                StudentId = attendanceModel.StudentId,
                                IsPresent = attendanceModel.IsPresent,
                                CreatedBy = guestLecture.CreatedBy,
                                CreatedOn = guestLecture.CreatedOn,
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
                throw new Exception("DAL > SaveOrUpdateVTGuestLectureConductedDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTGuestLectureId
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtGuestLectureId)
        {
            VTGuestLectureConducted vtGuestLectureConducted = this.Context.VTGuestLectureConducteds.FirstOrDefault(v => v.VTGuestLectureId == vtGuestLectureId);

            if (vtGuestLectureConducted != null)
            {
                Context.Entry<VTGuestLectureConducted>(vtGuestLectureConducted).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTGuestLectureConducted by Name
        /// </summary>
        /// <param name="vtGuestLectureConductedModel"></param>
        /// <returns></returns>
        public List<string> CheckVTGuestLectureConductedExistByName(VTGuestLectureConductedModel guestLectureModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                var guestLectures = this.Context.VTGuestLectureConducteds.Where(v => v.VTId == guestLectureModel.VTId && v.ReportingDate.Date == guestLectureModel.ReportingDate.Date).ToList();

                if (guestLectureModel.RequestType == RequestType.New && guestLectures.Count > 0)
                {
                    var fieldIndustryVisitItem = guestLectures.FirstOrDefault(f => f.ClassTaughtId == guestLectureModel.ClassTaughtId && f.SectionTaughtId == guestLectureModel.SectionIds);

                    if (fieldIndustryVisitItem != null)
                    {
                        errorMessageList.Add("Selected class & sesction is already submitted");
                    }
                }

                //2. Allowed 7 days Back Dated Reporting from Today
                //if (!(guestLectureModel.ReportingDate.Date >= Constants.GetCurrentDateTime.AddDays(-1 * Constants.BackDatedReportingDays).Date))
                //{
                //    errorMessageList.Add(string.Format("Only allowed {0} days Back Dated Reporting from Today", Constants.BackDatedReportingDays));
                //}

                //3. Avoid VT Guest Lecture Conducted Reporting on Sunday
                if (string.Equals(guestLectureModel.ReportingDate.Date.DayOfWeek.ToString(), "Sunday"))
                {
                    errorMessageList.Add("User cannot submit the VT Guest Lecture on Sunday");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVTGuestLectureConductedExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of VTGuestLectureConducted with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTGuestLectureConductedViewModel> GetVTGuestLectureConductedsByCriteria(SearchVTGuestLectureConductedModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTGuestLectureConductedViewModels.FromSql<VTGuestLectureConductedViewModel>("CALL GetVTGuestLectureConductedsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of Sections by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        public IList<Guid> GetVTGSectionsByGuestLectureId(Guid guestLectureId)
        {
            var vtfSections = this.Context.VTGSections.Where(s => s.VTGuestLectureId == guestLectureId).Select(cs => cs.SectionId).ToList();

            return vtfSections;
        }

        /// <summary>
        /// Get list of Methodologies by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        public IList<string> GetVTGMethodologiesByGuestLectureId(Guid guestLectureId)
        {
            var methodologies = this.Context.VTGMethodologies.Where(s => s.VTGuestLectureId == guestLectureId).Select(cs => cs.MethodologyId).ToList();

            return methodologies;
        }

        /// <summary>
        /// Get list of UnitSessionsTaughts by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        public IList<UnitSessionsModel> GetVTFUnitSessionsTaughtsByGuestLectureId(Guid guestLectureId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "guestLectureId", MySqlDbType = MySqlDbType.VarChar, Value = guestLectureId };

            return Context.UnitSessionsModels.FromSql<UnitSessionsModel>("CALL GetUnitSessionsTaughtsByGLId (@guestLectureId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of Students by guestLectureId
        /// </summary>
        /// <param name="guestLectureId"></param>
        /// <returns></returns>
        public IList<StudentAttendanceModel> GetVTFStudentsByGuestLectureId(Guid guestLectureId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "guestLectureId", MySqlDbType = MySqlDbType.VarChar, Value = guestLectureId };

            return Context.StudentAttendanceModels.FromSql<StudentAttendanceModel>("CALL GetStudentAttendancesByGLId (@guestLectureId)", sqlParams).ToList();
        }
    }
}