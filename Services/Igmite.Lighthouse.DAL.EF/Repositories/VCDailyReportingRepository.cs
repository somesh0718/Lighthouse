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
    /// Repository of the VCDailyReporting entity
    /// </summary>
    public class VCDailyReportingRepository : GenericRepository<VCDailyReporting>, IVCDailyReportingRepository
    {
        /// <summary>
        /// Get list of VCDailyReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCDailyReporting> GetVCDailyReportings()
        {
            return this.Context.VCDailyReportings.AsQueryable();
        }

        /// <summary>
        /// Get list of VCDailyReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCDailyReporting> GetVCDailyReportingsByName(string name)
        {
            var vcDailyReportings = (from v in this.Context.VCDailyReportings
                                     select v).AsQueryable();

            return vcDailyReportings;
        }

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        public VCDailyReporting GetVCDailyReportingById(Guid vcDailyReportingId)
        {
            return this.Context.VCDailyReportings.FirstOrDefault(v => v.VCDailyReportingId == vcDailyReportingId);
        }

        /// <summary>
        /// Get VCDailyReporting by VC Id & Reporting Date
        /// </summary>
        /// <param name="vcId"></param>
        /// <param name="reportingDate"></param>
        /// <returns></returns>
        public VCDailyReporting GetVCDailyReportingById(Guid vcId, DateTime reportingDate)
        {
            return this.Context.VCDailyReportings.FirstOrDefault(v => v.VCId == vcId && v.ReportDate.Date == reportingDate.Date);
        }

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId using async
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        public async Task<VCDailyReporting> GetVCDailyReportingByIdAsync(Guid vcDailyReportingId)
        {
            var vcDailyReporting = await (from v in this.Context.VCDailyReportings
                                          where v.VCDailyReportingId == vcDailyReportingId
                                          select v).FirstOrDefaultAsync();

            return vcDailyReporting;
        }

        /// <summary>
        /// Insert/Update VCDailyReporting entity
        /// </summary>
        /// <param name="vcDailyReporting"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVCDailyReportingDetails(VCDailyReporting dailyReporting, VCDailyReportingModel dailyReportingModel)
        {
            try
            {
                if (RequestType.New == dailyReporting.RequestType)
                    Context.VCDailyReportings.Add(dailyReporting);
                else
                {
                    Context.Entry<VCDailyReporting>(dailyReporting).State = EntityState.Modified;
                }

                Context.SaveChanges();

                if (dailyReportingModel != null)
                {
                    #region 1. Working Day Type

                    if (dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Count > 0)
                    {
                        IList<VCRWorkingDayType> workingDayTypes = Context.VCRWorkingDayTypes.Where(v => v.VCDailyReportingId == dailyReporting.VCDailyReportingId).ToList();

                        workingDayTypes.ForEach((workingDayTypeItem) =>
                        {
                            Context.Entry<VCRWorkingDayType>(workingDayTypeItem).State = EntityState.Deleted;
                        });

                        foreach (string workingTypeId in dailyReportingModel.WorkingDayTypeIds)
                        {
                            Context.VCRWorkingDayTypes.Add(new VCRWorkingDayType
                            {
                                VCRWorkingDayTypeId = Guid.NewGuid(),
                                VCDailyReportingId = dailyReporting.VCDailyReportingId,
                                WorkingTypeId = workingTypeId,
                                CreatedBy = dailyReporting.CreatedBy,
                                CreatedOn = dailyReporting.CreatedOn,
                                IsActive = true
                            });
                        }
                    }

                    #endregion 1. Working Day Type

                    #region 2. Industry Exposure Visit

                    if (dailyReportingModel.IndustryExposureVisit != null)
                    {
                        IList<VCRIndustryExposureVisit> industryExposureVisits = Context.VCRIndustryExposureVisits.Where(v => v.VCDailyReportingId == dailyReporting.VCDailyReportingId).ToList();

                        industryExposureVisits.ForEach((industryExposureVisitItem) =>
                        {
                            Context.Entry<VCRIndustryExposureVisit>(industryExposureVisitItem).State = EntityState.Deleted;
                        });

                        var industryExposureVisitModel = dailyReportingModel.IndustryExposureVisit;

                        var industryExposureVisit = new VCRIndustryExposureVisit
                        {
                            VCRIndustryExposureVisitId = Guid.NewGuid(),
                            VCDailyReportingId = dailyReporting.VCDailyReportingId,
                            TypeOfIndustryLinkage = industryExposureVisitModel.TypeOfIndustryLinkage,
                            ContactPersonName = industryExposureVisitModel.ContactPersonName,
                            ContactPersonMobile = industryExposureVisitModel.ContactPersonMobile,
                            ContactPersonEmail = industryExposureVisitModel.ContactPersonEmail,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        };

                        Context.VCRIndustryExposureVisits.Add(industryExposureVisit);
                    }

                    #endregion 2. Industry Exposure Visit

                    #region 3. On Leave

                    if (dailyReportingModel.Leave != null)
                    {
                        IList<VCRLeave> leaves = Context.VCRLeaves.Where(v => v.VCDailyReportingId == dailyReporting.VCDailyReportingId).ToList();

                        leaves.ForEach((leaveItem) =>
                        {
                            Context.Entry<VCRLeave>(leaveItem).State = EntityState.Deleted;
                        });

                        IList<VCDailyReporting> vcReportingForLeaves = Context.VCDailyReportings.Where(v => v.VCId == dailyReporting.VCId && v.ReportType == "47" && v.ReportDate.Month == dailyReporting.ReportDate.Month).ToList();

                        var leaveModel = dailyReportingModel.Leave;
                        leaveModel.LeaveModeId = (vcReportingForLeaves.Count == 1) ? "2" : "3";

                        Context.VCRLeaves.Add(new VCRLeave
                        {
                            VCRLeaveId = Guid.NewGuid(),
                            VCDailyReportingId = dailyReporting.VCDailyReportingId,
                            LeaveTypeId = leaveModel.LeaveTypeId,
                            LeaveModeId = leaveModel.LeaveModeId,
                            LeaveApprovalStatus = leaveModel.LeaveApprovalStatus,
                            LeaveApprover = leaveModel.LeaveApprover,
                            LeaveReason = leaveModel.LeaveReason,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 3. On Leave

                    #region 4. Holiday/School Off

                    if (dailyReportingModel.Holiday != null)
                    {
                        IList<VCRHoliday> holidays = Context.VCRHolidays.Where(v => v.VCDailyReportingId == dailyReporting.VCDailyReportingId).ToList();

                        holidays.ForEach((holidayItem) =>
                        {
                            Context.Entry<VCRHoliday>(holidayItem).State = EntityState.Deleted;
                        });

                        var holidayModel = dailyReportingModel.Holiday;

                        Context.VCRHolidays.Add(new VCRHoliday
                        {
                            VCRHolidayId = Guid.NewGuid(),
                            VCDailyReportingId = dailyReporting.VCDailyReportingId,
                            HolidayTypeId = holidayModel.HolidayTypeId,
                            HolidayDetails = holidayModel.HolidayDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 4. Holiday/School Off

                    #region 5. School Visits

                    if (dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Contains("153"))
                    {
                        Context.VCRSchoolVisits.Add(new VCRSchoolVisit
                        {
                            VCRSchoolVisitId = Guid.NewGuid(),
                            VCDailyReportingId = dailyReporting.VCDailyReportingId,
                            SchoolId = dailyReportingModel.SchoolId.Value,
                            WorkDetails = dailyReportingModel.WorkTypeDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 5. School Visits

                    #region 6. Praticals

                    if (dailyReportingModel.Pratical != null)
                    {
                        var vcrPraticalsItem = Context.VCRPraticals.Where(v => v.AcademicYearId == dailyReportingModel.Pratical.AcademicYearId && v.VCId == dailyReportingModel.Pratical.VCId && v.SchoolId == dailyReportingModel.Pratical.SchoolId && v.VTId == dailyReportingModel.Pratical.VTId && v.ClassId == dailyReportingModel.Pratical.ClassId).FirstOrDefault();

                        if (vcrPraticalsItem != null)
                        {
                            Context.Entry<VCRPratical>(vcrPraticalsItem).State = EntityState.Deleted;
                        }

                        var vTRPratical = new VCRPratical
                        {
                            VTRPraticalId = Guid.NewGuid(),
                            VCDailyReportingId = dailyReporting.VCDailyReportingId,
                            AcademicYearId = dailyReportingModel.Pratical.AcademicYearId,
                            VCId = dailyReportingModel.Pratical.VCId,
                            SchoolId = dailyReportingModel.Pratical.SchoolId,
                            VTId = dailyReportingModel.Pratical.VTId,
                            SectorId = dailyReportingModel.Pratical.SectorId,
                            JobRoleId = dailyReportingModel.Pratical.JobRoleId,
                            ClassId = dailyReportingModel.Pratical.ClassId,
                            IsPratical = dailyReportingModel.Pratical.IsPratical,
                            StudentCount = dailyReportingModel.Pratical.StudentCount,
                            VTPresent = dailyReportingModel.Pratical.VTPresent,
                            PresentStudentCount = dailyReportingModel.Pratical.PresentStudentCount,
                            AssesorName = dailyReportingModel.Pratical.AssesorName,
                            AssesorMobileNo = dailyReportingModel.Pratical.AssesorMobileNo,
                            Remarks = dailyReportingModel.Pratical.Remarks,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        };

                        Context.VCRPraticals.Add(vTRPratical);
                    }

                    #endregion 6. Praticals
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVCDailyReportingDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcDailyReportingId)
        {
            VCDailyReporting vcDailyReporting = this.Context.VCDailyReportings.FirstOrDefault(v => v.VCDailyReportingId == vcDailyReportingId);

            if (vcDailyReporting != null)
            {
                Context.Entry<VCDailyReporting>(vcDailyReporting).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VCDailyReporting by Name
        /// </summary>
        /// <param name="vcDailyReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckVCDailyReportingExistByName(VCDailyReportingModel dailyReportingModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                var dailyReporting = this.Context.VCDailyReportings.FirstOrDefault(v => v.VCId == dailyReportingModel.VCId && v.ReportDate.Date == dailyReportingModel.ReportDate.Date);

                if (dailyReporting != null)
                {
                    var workingDayTypes = this.Context.VCRWorkingDayTypes.Where(v => v.VCDailyReportingId == dailyReporting.VCDailyReportingId).ToList();

                    if (workingDayTypes.Count > 0 && dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Count > 0)
                    {
                        dailyReportingModel.WorkingDayTypeIds.ForEach(workTypeId =>
                        {
                            var workTypeItem = workingDayTypes.FirstOrDefault(v => v.WorkingTypeId == workTypeId);
                            if (workTypeItem != null)
                            {
                                //1. Industry Exposure Visit
                                if (workTypeId == "160")
                                {
                                    //errorMessageList.Add("'Industry Exposure Visit' is already submitted");
                                }

                                //School
                                else if (workTypeId == "153")
                                {
                                    //errorMessageList.Add("'School' is already submitted");
                                }

                                //Work details
                                else if (!(workTypeId == "160" || workTypeId == "153") && !string.IsNullOrEmpty(dailyReporting.WorkTypeDetails))
                                {
                                    //errorMessageList.Add("'Work details' is already submitted");
                                }
                            }
                        });
                    }
                    else
                    {
                        errorMessageList.Add("VC Daily Reporting is already submitted for Working days");
                    }

                    if (dailyReporting.ReportType == "47")
                    {
                        errorMessageList.Add("VC Daily Reporting is already submitted for On Leave");
                    }
                    else if (dailyReporting.ReportType == "48")
                    {
                        errorMessageList.Add("VC Daily Reporting is already submitted for Holiday / School Off");
                    }
                }

                if (dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Count > 0)
                {
                    var paSchool = dailyReportingModel.WorkingDayTypeIds.FirstOrDefault(pa => pa == "10");
                    if (paSchool != null)
                    {
                        var vcrPraticalsItem = Context.VCRPraticals.Where(v => v.AcademicYearId == dailyReportingModel.Pratical.AcademicYearId && v.VCId == dailyReportingModel.Pratical.VCId && v.SchoolId == dailyReportingModel.Pratical.SchoolId && v.VTId == dailyReportingModel.Pratical.VTId && v.ClassId == dailyReportingModel.Pratical.ClassId).FirstOrDefault();

                        if (vcrPraticalsItem != null && dailyReportingModel.RequestType == RequestType.New)
                        {
                            errorMessageList.Add("'Practical Assessment' is already submitted by current Vocational Coordinator");
                        }
                    }
                }

                //Allowed 7 days Back Dated Reporting from Today
                if (!(dailyReportingModel.ReportDate.Date >= Constants.GetCurrentDateTime.AddDays(-1 * Constants.BackDatedReportingDays).Date))
                {
                    errorMessageList.Add(string.Format("Only allowed {0} days Back Dated Reporting from Today", Constants.BackDatedReportingDays));
                }

                //Avoid daily VC Reporting on Sunday
                if (string.Equals(dailyReportingModel.ReportDate.Date.DayOfWeek.ToString(), "Sunday"))
                {
                    errorMessageList.Add("User cannot submit the VC Daily Reporting on Sunday");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVCDailyReportingExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of VCDailyReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCDailyReportingViewModel> GetVCDailyReportingsByCriteria(SearchVCDailyReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VCDailyReportingViewModels.FromSql<VCDailyReportingViewModel>("CALL GetVCDailyReportingsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        public IList<string> GetWorkTypesByDailyReportingId(Guid dailyReportingId)
        {
            return Context.VCRWorkingDayTypes.Where(v => v.VCDailyReportingId == dailyReportingId).Select(w => w.WorkingTypeId).ToList();
        }

        public VCRIndustryExposureVisitModel GetIndustryExposureVisitByDailyReportingId(Guid dailyReportingId)
        {
            return Context.VCRIndustryExposureVisits.Where(v => v.VCDailyReportingId == dailyReportingId)
                .Select(w => new VCRIndustryExposureVisitModel
                {
                    TypeOfIndustryLinkage = w.TypeOfIndustryLinkage,
                    ContactPersonName = w.ContactPersonName,
                    ContactPersonMobile = w.ContactPersonMobile,
                    ContactPersonEmail = w.ContactPersonEmail,
                }).FirstOrDefault();
        }

        public LeaveModel GetLeaveByDailyReportingId(Guid dailyReportingId)
        {
            return Context.VCRLeaves.Where(v => v.VCDailyReportingId == dailyReportingId)
                .Select(l => new LeaveModel
                {
                    LeaveTypeId = l.LeaveTypeId,
                    LeaveModeId = l.LeaveModeId,
                    LeaveApprovalStatus = l.LeaveApprovalStatus,
                    LeaveApprover = l.LeaveApprover,
                    LeaveReason = l.LeaveReason,
                }).FirstOrDefault();
        }

        public HolidayModel GetHolidayByDailyReportingId(Guid dailyReportingId)
        {
            return Context.VCRHolidays.Where(v => v.VCDailyReportingId == dailyReportingId)
                .Select(h => new HolidayModel
                {
                    HolidayTypeId = h.HolidayTypeId,
                    HolidayDetails = h.HolidayDetails
                }).FirstOrDefault();
        }

        public VCRPraticalModel GetPraticalByDailyReportingId(Guid dailyReportingId)
        {
            return Context.VCRPraticals.Where(v => v.VCDailyReportingId == dailyReportingId)
                .Select(p => new VCRPraticalModel
                {
                    AcademicYearId = p.AcademicYearId,
                    VCId = p.VCId,
                    SchoolId = p.SchoolId,
                    VTId = p.VTId,
                    SectorId = p.SectorId,
                    JobRoleId = p.JobRoleId,
                    ClassId = p.ClassId,
                    IsPratical = p.IsPratical,
                    StudentCount = p.StudentCount,
                    VTPresent = p.VTPresent,
                    PresentStudentCount = p.PresentStudentCount,
                    AssesorName = p.AssesorName,
                    AssesorMobileNo = p.AssesorMobileNo,
                    Remarks = p.Remarks
                }).FirstOrDefault();
        }
    }
}