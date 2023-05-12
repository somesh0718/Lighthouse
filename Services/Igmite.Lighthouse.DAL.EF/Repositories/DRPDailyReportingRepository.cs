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
    /// Repository of the DRPDailyReporting entity
    /// </summary>
    public class DRPDailyReportingRepository : GenericRepository<DRPDailyReporting>, IDRPDailyReportingRepository
    {
        /// <summary>
        /// Get list of DRPDailyReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<DRPDailyReporting> GetDRPDailyReportings()
        {
            return this.Context.DRPDailyReportings.AsQueryable();
        }

        /// <summary>
        /// Get list of DRPDailyReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DRPDailyReporting> GetDRPDailyReportingsByName(string name)
        {
            var dailyReportings = (from v in this.Context.DRPDailyReportings
                                   select v).AsQueryable();

            return dailyReportings;
        }

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public DRPDailyReporting GetDRPDailyReportingById(Guid dailyReportingId)
        {
            return this.Context.DRPDailyReportings.FirstOrDefault(v => v.DRPDailyReportingId == dailyReportingId);
        }

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId using async
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public async Task<DRPDailyReporting> GetDRPDailyReportingByIdAsync(Guid dailyReportingId)
        {
            var dailyReporting = await (from v in this.Context.DRPDailyReportings
                                        where v.DRPDailyReportingId == dailyReportingId
                                        select v).FirstOrDefaultAsync();

            return dailyReporting;
        }

        /// <summary>
        /// Insert/Update DRPDailyReporting entity
        /// </summary>
        /// <param name="dailyReporting"></param>
        /// <returns></returns>
        public bool SaveOrUpdateDRPDailyReportingDetails(DRPDailyReporting dailyReporting, DRPDailyReportingModel dailyReportingModel)
        {
            try
            {
                if (RequestType.New == dailyReporting.RequestType)
                    Context.DRPDailyReportings.Add(dailyReporting);
                else
                {
                    Context.Entry<DRPDailyReporting>(dailyReporting).State = EntityState.Modified;
                }

                Context.SaveChanges();

                if (dailyReportingModel != null)
                {
                    #region 1. Working Day Type

                    if (dailyReportingModel.WorkingDayTypeIds != null && dailyReportingModel.WorkingDayTypeIds.Count > 0)
                    {
                        IList<DRPRWorkingDayType> workingDayTypes = Context.DRPRWorkingDayTypes.Where(v => v.DRPDailyReportingId == dailyReporting.DRPDailyReportingId).ToList();

                        workingDayTypes.ForEach((workingDayTypeItem) =>
                        {
                            Context.Entry<DRPRWorkingDayType>(workingDayTypeItem).State = EntityState.Deleted;
                        });

                        foreach (string workingTypeId in dailyReportingModel.WorkingDayTypeIds)
                        {
                            Context.DRPRWorkingDayTypes.Add(new DRPRWorkingDayType
                            {
                                DRPRWorkingDayTypeId = Guid.NewGuid(),
                                DRPDailyReportingId = dailyReporting.DRPDailyReportingId,
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
                        IList<DRPRIndustryExposureVisit> industryExposureVisits = Context.DRPRIndustryExposureVisits.Where(v => v.DRPDailyReportingId == dailyReporting.DRPDailyReportingId).ToList();

                        industryExposureVisits.ForEach((industryExposureVisitItem) =>
                        {
                            Context.Entry<DRPRIndustryExposureVisit>(industryExposureVisitItem).State = EntityState.Deleted;
                        });

                        var industryExposureVisitModel = dailyReportingModel.IndustryExposureVisit;

                        var industryExposureVisit = new DRPRIndustryExposureVisit
                        {
                            DRPRIndustryExposureVisitId = Guid.NewGuid(),
                            DRPDailyReportingId = dailyReporting.DRPDailyReportingId,
                            TypeOfIndustryLinkage = industryExposureVisitModel.TypeOfIndustryLinkage,
                            ContactPersonName = industryExposureVisitModel.ContactPersonName,
                            ContactPersonMobile = industryExposureVisitModel.ContactPersonMobile,
                            ContactPersonEmail = industryExposureVisitModel.ContactPersonEmail,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        };

                        Context.DRPRIndustryExposureVisits.Add(industryExposureVisit);
                    }

                    #endregion 2. Industry Exposure Visit

                    #region 3. On Leave

                    if (dailyReportingModel.Leave != null)
                    {
                        IList<DRPRLeave> leaves = Context.DRPRLeaves.Where(v => v.DRPDailyReportingId == dailyReporting.DRPDailyReportingId).ToList();

                        leaves.ForEach((leaveItem) =>
                        {
                            Context.Entry<DRPRLeave>(leaveItem).State = EntityState.Deleted;
                        });

                        var leaveModel = dailyReportingModel.Leave;

                        Context.DRPRLeaves.Add(new DRPRLeave
                        {
                            DRPRLeaveId = Guid.NewGuid(),
                            DRPDailyReportingId = dailyReporting.DRPDailyReportingId,
                            LeaveTypeId = leaveModel.LeaveTypeId,
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
                        IList<DRPRHoliday> holidays = Context.DRPRHolidays.Where(v => v.DRPDailyReportingId == dailyReporting.DRPDailyReportingId).ToList();

                        holidays.ForEach((holidayItem) =>
                        {
                            Context.Entry<DRPRHoliday>(holidayItem).State = EntityState.Deleted;
                        });

                        var holidayModel = dailyReportingModel.Holiday;

                        Context.DRPRHolidays.Add(new DRPRHoliday
                        {
                            DRPRHolidayId = Guid.NewGuid(),
                            DRPDailyReportingId = dailyReporting.DRPDailyReportingId,
                            HolidayTypeId = holidayModel.HolidayTypeId,
                            HolidayDetails = holidayModel.HolidayDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 4. Holiday/School Off

                    #region 5. School Visits

                    if (dailyReportingModel.WorkingDayTypeIds.Contains("313"))
                    {
                        Context.DRPRSchoolVisits.Add(new DRPRSchoolVisit
                        {
                            DRPRSchoolVisitId = Guid.NewGuid(),
                            DRPDailyReportingId = dailyReporting.DRPDailyReportingId,
                            SchoolId = dailyReportingModel.SchoolId.Value,
                            WorkDetails = dailyReportingModel.WorkTypeDetails,
                            CreatedBy = dailyReporting.CreatedBy,
                            CreatedOn = dailyReporting.CreatedOn,
                            IsActive = true
                        });
                    }

                    #endregion 5. School Visits
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateDRPDailyReportingDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid dailyReportingId)
        {
            DRPDailyReporting dailyReporting = this.Context.DRPDailyReportings.FirstOrDefault(v => v.DRPDailyReportingId == dailyReportingId);

            if (dailyReporting != null)
            {
                Context.Entry<DRPDailyReporting>(dailyReporting).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate DRPDailyReporting by Name
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckDRPDailyReportingExistByName(DRPDailyReportingModel dailyReportingModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                var dailyReporting = this.Context.DRPDailyReportings.FirstOrDefault(v => v.DRPId == dailyReportingModel.DRPId && v.ReportDate.Date == dailyReportingModel.ReportDate.Date);

                if (dailyReportingModel.RequestType == RequestType.New && dailyReporting != null)
                {
                    if (dailyReporting.ReportType == "305")
                    {
                        errorMessageList.Add("DRP Daily Reporting is already submitted for On Leave");
                    }
                    else if (dailyReporting.ReportType == "306")
                    {
                        errorMessageList.Add("DRP Daily Reporting is already submitted for Holiday / School Off");
                    }
                    else
                    {
                        var workingDayTypes = this.Context.DRPRWorkingDayTypes.Where(v => v.DRPDailyReportingId == dailyReporting.DRPDailyReportingId).ToList();

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
                                else if (workTypeId == "313")
                                    {
                                    //errorMessageList.Add("'School' is already submitted");
                                }

                                //Work details
                                else if (!(workTypeId == "160" || workTypeId == "313") && !string.IsNullOrEmpty(dailyReporting.WorkTypeDetails))
                                    {
                                    //errorMessageList.Add("'Work details' is already submitted");
                                }
                                }
                            });
                        }
                        else
                        {
                            errorMessageList.Add("DRP Daily Reporting is already submitted for Working days");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckDRPDailyReportingExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of DRPDailyReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DRPDailyReportingViewModel> GetDRPDailyReportingsByCriteria(SearchDRPDailyReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.DRPDailyReportingViewModels.FromSql<DRPDailyReportingViewModel>("CALL GetDRPDailyReportingsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        public IList<string> GetWorkTypesByDailyReportingId(Guid dailyReportingId)
        {
            return Context.DRPRWorkingDayTypes.Where(v => v.DRPDailyReportingId == dailyReportingId).Select(w => w.WorkingTypeId).ToList();
        }

        public DRPRIndustryExposureVisitModel GetIndustryExposureVisitByDailyReportingId(Guid dailyReportingId)
        {
            return Context.DRPRIndustryExposureVisits.Where(v => v.DRPDailyReportingId == dailyReportingId)
                .Select(w => new DRPRIndustryExposureVisitModel
                {
                    TypeOfIndustryLinkage = w.TypeOfIndustryLinkage,
                    ContactPersonName = w.ContactPersonName,
                    ContactPersonMobile = w.ContactPersonMobile,
                    ContactPersonEmail = w.ContactPersonEmail,
                }).FirstOrDefault();
        }

        public LeaveModel GetLeaveByDailyReportingId(Guid dailyReportingId)
        {
            return Context.DRPRLeaves.Where(v => v.DRPDailyReportingId == dailyReportingId)
                .Select(l => new LeaveModel
                {
                    LeaveTypeId = l.LeaveTypeId,
                    LeaveApprovalStatus = l.LeaveApprovalStatus,
                    LeaveApprover = l.LeaveApprover,
                    LeaveReason = l.LeaveReason,
                }).FirstOrDefault();
        }

        public HolidayModel GetHolidayByDailyReportingId(Guid dailyReportingId)
        {
            return Context.DRPRHolidays.Where(v => v.DRPDailyReportingId == dailyReportingId)
                .Select(h => new HolidayModel
                {
                    HolidayTypeId = h.HolidayTypeId,
                    HolidayDetails = h.HolidayDetails
                }).FirstOrDefault();
        }
    }
}