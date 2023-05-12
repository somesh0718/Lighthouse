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
    /// Repository of the VTSchoolSector entity
    /// </summary>
    public class VTSchoolSectorRepository : GenericRepository<VTSchoolSector>, IVTSchoolSectorRepository
    {
        /// <summary>
        /// Get list of VTSchoolSector
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTSchoolSector> GetVTSchoolSectors()
        {
            return this.Context.VTSchoolSectors.AsQueryable();
        }

        /// <summary>
        /// Get list of VTSchoolSector by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTSchoolSector> GetVTSchoolSectorsByName(string name)
        {
            var vtSchoolSectors = (from v in this.Context.VTSchoolSectors
                                   select v).AsQueryable();

            return vtSchoolSectors;
        }

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        public VTSchoolSector GetVTSchoolSectorById(Guid vtSchoolSectorId)
        {
            return this.Context.VTSchoolSectors.FirstOrDefault(v => v.VTSchoolSectorId == vtSchoolSectorId);
        }

        /// <summary>
        /// Get VTSchoolSector by SchoolId & SectorId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="SectorId"></param>
        /// <returns></returns>
        public VTSchoolSector GetVTSchoolSectorBySchoolIdAndSectorId(Guid schoolId, Guid sectorId)
        {
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(y => y.IsCurrentAcademicYear == true);
            return this.Context.VTSchoolSectors.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.SchoolId == schoolId && v.SectorId == sectorId && v.IsActive == true);
        }

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId using async
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        public async Task<VTSchoolSector> GetVTSchoolSectorByIdAsync(Guid vtSchoolSectorId)
        {
            var vtSchoolSector = await (from v in this.Context.VTSchoolSectors
                                        where v.VTSchoolSectorId == vtSchoolSectorId
                                        select v).FirstOrDefaultAsync();

            return vtSchoolSector;
        }

        /// <summary>
        /// Insert/Update VTSchoolSector entity
        /// </summary>
        /// <param name="schoolSector"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTSchoolSectorDetails(VTSchoolSector schoolSector, VTSchoolSectorModel schoolSectorModel, bool isChangeStatus)
        {
            try
            {
                if (RequestType.New == schoolSector.RequestType)
                {
                    Context.VTSchoolSectors.Add(schoolSector);
                    Context.Entry<VTSchoolSector>(schoolSector).State = EntityState.Added;
                    Context.SaveChanges();

                    #region Generate VT Not Submitted Daily Reporting Data

                    DateTime startDate = Constants.GetCurrentDateTime;
                    int lastDayOfMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

                    DateTime endDate = Convert.ToDateTime(startDate.ToString("yyyy/MM/") + lastDayOfMonth.ToString());

                    MySqlParameter[] sqlParams = new MySqlParameter[3];
                    sqlParams[0] = new MySqlParameter { ParameterName = "StartDate", MySqlDbType = MySqlDbType.VarChar, Value = startDate };
                    sqlParams[1] = new MySqlParameter { ParameterName = "EndDate", MySqlDbType = MySqlDbType.VarChar, Value = endDate };
                    sqlParams[2] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.Guid, Value = schoolSector.VTId };

                    Context.Database.ExecuteSqlCommand("CALL GenerateVTNotSubmittedDailyReportingData (@StartDate, @EndDate, @VTId)", sqlParams);

                    #endregion Generate VT Not Submitted Daily Reporting Data
                }
                else
                {
                    if (isChangeStatus)
                    {
                        List<VTClass> vtClasses = this.Context.VTClasses.Where(v => v.AcademicYearId == schoolSector.AcademicYearId && v.SchoolId == schoolSector.SchoolId && v.VTId == schoolSector.VTId && v.IsActive == true).ToList();

                        for (int classIndex = 0; classIndex < vtClasses.Count; classIndex++)
                        {
                            vtClasses[classIndex].UpdatedBy = schoolSector.UpdatedBy;
                            vtClasses[classIndex].UpdatedOn = Constants.GetCurrentDateTime;
                            vtClasses[classIndex].IsActive = schoolSectorModel.IsActive;
                            Context.Entry<VTClass>(vtClasses[classIndex]).State = EntityState.Modified;

                            List<StudentClassMapping> students = this.Context.StudentClassMapping.Where(v => v.AcademicYearId == schoolSector.AcademicYearId && v.SchoolId == schoolSector.SchoolId && v.VTId == schoolSector.VTId && v.ClassId == vtClasses[classIndex].ClassId && v.IsActive == true).ToList();

                            for (int studentIndex = 0; studentIndex < students.Count; studentIndex++)
                            {
                                students[studentIndex].UpdatedBy = schoolSector.UpdatedBy;
                                students[studentIndex].UpdatedOn = Constants.GetCurrentDateTime;
                                students[studentIndex].IsActive = schoolSectorModel.IsActive;
                                Context.Entry<StudentClassMapping>(students[studentIndex]).State = EntityState.Modified;
                            }
                        }
                    }

                    Context.Entry<VTSchoolSector>(schoolSector).State = EntityState.Modified;
                    Context.SaveChanges();
                }

                #region JobRoles - VT School Sector

                if (schoolSectorModel.JobRoleIds != null && schoolSectorModel.JobRoleIds.Count > 0)
                {
                    IList<VTSchoolSectorJobRole> schoolSectorJobRoles = Context.VTSchoolSectorJobRoles.Where(v => v.VTSchoolSectorId == schoolSector.VTSchoolSectorId).ToList();

                    schoolSectorJobRoles.ForEach((schoolSectorJobRoleItem) =>
                    {
                        Context.Entry<VTSchoolSectorJobRole>(schoolSectorJobRoleItem).State = EntityState.Deleted;
                    });

                    foreach (Guid jobRoleId in schoolSectorModel.JobRoleIds)
                    {
                        Context.VTSchoolSectorJobRoles.Add(new VTSchoolSectorJobRole
                        {
                            VTSchoolSectorJobRoleId = Guid.NewGuid(),
                            VTSchoolSectorId = schoolSector.VTSchoolSectorId,
                            JobRoleId = jobRoleId,
                            CreatedBy = schoolSector.CreatedBy,
                            CreatedOn = schoolSector.CreatedOn,
                            IsActive = true
                        });
                    }

                    Context.SaveChanges();
                }

                #endregion JobRoles - VT School Sector
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVTSchoolSectorDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtSchoolSectorId)
        {
            VTSchoolSector vtSchoolSector = this.Context.VTSchoolSectors.FirstOrDefault(v => v.VTSchoolSectorId == vtSchoolSectorId);

            if (vtSchoolSector != null)
            {
                List<VTClass> vtClasses = this.Context.VTClasses.Where(v => v.AcademicYearId == vtSchoolSector.AcademicYearId && v.SchoolId == vtSchoolSector.SchoolId && v.VTId == vtSchoolSector.VTId).ToList();

                if (vtClasses.Count > 0)
                {
                    for (int classIndex = 0; classIndex < vtClasses.Count; classIndex++)
                    {
                        vtClasses[classIndex].UpdatedOn = Constants.GetCurrentDateTime;
                        vtClasses[classIndex].IsActive = false;
                        Context.Entry<VTClass>(vtClasses[classIndex]).State = EntityState.Modified;
                    }

                    List<StudentClassMapping> students = this.Context.StudentClassMapping.Where(v => v.VTId == vtSchoolSector.VTId).ToList();
                    for (int studentIndex = 0; studentIndex < students.Count; studentIndex++)
                    {
                        students[studentIndex].IsActive = false;
                        students[studentIndex].UpdatedOn = Constants.GetCurrentDateTime;

                        Context.Entry<StudentClassMapping>(students[studentIndex]).State = EntityState.Modified;
                    }

                    vtSchoolSector.UpdatedOn = Constants.GetCurrentDateTime;
                    vtSchoolSector.IsActive = false;
                    Context.Entry<VTSchoolSector>(vtSchoolSector).State = EntityState.Modified;
                }
                else
                {
                    Context.Entry<VTSchoolSector>(vtSchoolSector).State = EntityState.Deleted;
                }

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTSchoolSector by Name
        /// </summary>
        /// <param name="schoolSector"></param>
        /// <param name="schoolSectorModel"></param>
        /// <returns></returns>
        public string CheckVTSchoolSectorExistByName(VTSchoolSector schoolSector, VTSchoolSectorModel schoolSectorModel)
        {
            try
            {
                // Get list of all asigned VT against Sector & Job Roles in the School
                List<VTSchoolSector> schoolSectors = this.Context.VTSchoolSectors.Where(v => v.AcademicYearId == schoolSectorModel.AcademicYearId && v.SchoolId == schoolSectorModel.SchoolId && v.IsActive == true).ToList();

                if (schoolSectorModel.RequestType == RequestType.New)
                {
                    // Find the already mapped VT with schhol
                    VTSchoolSector vtSchoolSector = schoolSectors.FirstOrDefault(v => v.AcademicYearId == schoolSectorModel.AcademicYearId && v.SchoolId == schoolSectorModel.SchoolId && v.SectorId == schoolSectorModel.SectorId && v.IsActive == true);

                    if (vtSchoolSector != null)
                    {
                        return string.Format("Other VT is already mapped with this School");
                    }
                }

                if (schoolSectors.Count > 0)
                {
                    var existVTClasses = this.Context.VTClasses.Where(v => v.AcademicYearId == schoolSectorModel.AcademicYearId && v.SchoolId == schoolSector.SchoolId && v.VTId == schoolSector.VTId && v.IsActive == true).ToList();

                    if (existVTClasses.Count > 0 && Guid.Equals(schoolSector.VTId, schoolSectorModel.VTId) && !Guid.Equals(schoolSector.SchoolId, schoolSectorModel.SchoolId))
                    {
                        return string.Format("School cannot be changed beacause VT Classes are already mapped with current mapped school.");
                    }

                    VCTrainerMap vocationalTrainerItem = this.Context.VCTrainersMap.FirstOrDefault(s => s.AcademicYearId == schoolSectorModel.AcademicYearId && s.VTId == schoolSectorModel.VTId && s.IsActive == true);

                    SchoolVTPSector schoolVTPSectorItem = this.Context.SchoolVTPSectors.FirstOrDefault(s => s.AcademicYearId == schoolSectorModel.AcademicYearId && s.SchoolId == schoolSectorModel.SchoolId && s.VTPId == vocationalTrainerItem.VTPId && s.SectorId == schoolSectorModel.SectorId && s.IsActive == true);

                    if (schoolVTPSectorItem != null)
                    {
                        VCSchoolSector vcSchoolSectorItem = this.Context.VCSchoolSectors.FirstOrDefault(s => s.AcademicYearId == schoolSectorModel.AcademicYearId && s.SchoolVTPSectorId == schoolVTPSectorItem.SchoolVTPSectorId && s.VCId == vocationalTrainerItem.VCId && s.IsActive == true);

                        if (vcSchoolSectorItem == null)
                        {
                            return string.Format("Trainer's coordinator doesn't have VC School Sector mapping.");
                        }
                    }
                    else if (schoolVTPSectorItem == null)
                    {
                        return string.Format("Selected school and sector doesn't have School VTP Sector mapping.");
                    }

                    // Find the already mapped VT with selected Sector & Job Role
                    VTSchoolSector schoolSectorItem = schoolSectors.FirstOrDefault(v => v.AcademicYearId == schoolSectorModel.AcademicYearId && v.SchoolId == schoolSectorModel.SchoolId && v.SectorId == schoolSectorModel.SectorId && v.IsActive == true && v.DateOfRemoval == null);

                    // If both VT are not same, then check existing VT status
                    if (schoolSectorItem != null)
                    {
                        if (!Guid.Equals(schoolSectorItem.VTId, schoolSectorModel.VTId))
                        {
                            vocationalTrainerItem = this.Context.VCTrainersMap.FirstOrDefault(s => s.AcademicYearId == schoolSectorModel.AcademicYearId && s.VTId == schoolSectorItem.VTId && s.IsActive == true);

                            if (vocationalTrainerItem != null && vocationalTrainerItem.IsActive)
                            {
                                if (schoolSectorItem.DateOfRemoval.HasValue)
                                    return string.Format("First VT must be resigned from curent job role.");

                                return string.Format("Other VT is already mapped with this School, Sector & Job Role");
                            }
                            else
                            {
                                // Check new VT is not mapped with any Sector and JobRole in the School.
                                schoolSector = schoolSectors.FirstOrDefault(v => v.AcademicYearId == schoolSectorModel.AcademicYearId && v.VTId == schoolSectorModel.VTId && v.IsActive == true);

                                if (schoolSector != null)
                                {
                                    return string.Format("Current VT is already mapped to this School with different Sector & Job Role");
                                }
                            }
                        }
                        else if (schoolSectorModel.RequestType == RequestType.New && Guid.Equals(schoolSectorItem.VTId, schoolSectorModel.VTId))
                        {
                            return string.Format("Current VT is already mapped with this School, Sector & Job Role");
                        }

                        if (schoolSectorModel.RequestType == RequestType.Edit && schoolSector.IsActive && !schoolSectorModel.IsActive)
                        {
                            var checkAnyVTClass = (from vtss in this.Context.VTSchoolSectors
                                                   join vtc in this.Context.VTClasses on new { a = vtss.AcademicYearId, b = vtss.SchoolId, c = vtss.VTId } equals new { a = vtc.AcademicYearId, b = vtc.SchoolId, c = vtc.VTId }
                                                   where vtss.IsActive == true && vtc.IsActive == true && vtss.VTSchoolSectorId == schoolSector.VTSchoolSectorId
                                                   select new { AcademicYearId = vtss.AcademicYearId, SchoolId = vtss.SchoolId, VTId = vtss.VTId, ClassId = vtc.ClassId }).FirstOrDefault();

                            if (checkAnyVTClass != null)
                            {
                                return string.Format("VT Classes are already active for this VT School Sector.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVTSchoolSectorExistByName", ex);
            }

            return string.Empty;
        }

        /// <summary>
        /// Check VT School Sector can be inactive
        /// </summary>
        /// <param name="vtSchoolSector"></param>
        /// <returns></returns>
        public bool CheckUserCanInactiveVTSchoolSectorById(VTSchoolSector vtSchoolSector)
        {
            VTClass vtClass = this.Context.VTClasses.FirstOrDefault(s => s.VTId == vtSchoolSector.VTId && s.SchoolId == vtSchoolSector.SchoolId && s.IsActive == true);

            return (vtClass == null);
        }

        /// <summary>}
        /// List of VTSchoolSector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTSchoolSectorViewModel> GetVTSchoolSectorsByCriteria(SearchVTSchoolSectorModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[13];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "jobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[6] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[7] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[8] = new MySqlParameter { ParameterName = "isRollover", MySqlDbType = MySqlDbType.Bool, Value = searchModel.IsRollover };
            sqlParams[9] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[10] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[11] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[12] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VTSchoolSectorViewModels.FromSql<VTSchoolSectorViewModel>("CALL GetVTSchoolSectorsByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @sectorId, @jobRoleId, @schoolId, @status, @isRollover, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of JobRoles by VT School Sector Id
        /// </summary>
        /// <param name="schoolSectorId"></param>
        /// <returns></returns>
        public IList<Guid> GetJobRolesByVTSchoolSectorId(Guid schoolSectorId)
        {
            var jobRoleIds = this.Context.VTSchoolSectorJobRoles.Where(v => v.VTSchoolSectorId == schoolSectorId).Select(j => j.JobRoleId).ToList();

            return jobRoleIds;
        }
    }
}