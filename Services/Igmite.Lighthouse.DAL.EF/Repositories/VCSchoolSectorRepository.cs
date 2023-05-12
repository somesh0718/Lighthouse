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
    /// Repository of the VCSchoolSector entity
    /// </summary>
    public class VCSchoolSectorRepository : GenericRepository<VCSchoolSector>, IVCSchoolSectorRepository
    {
        /// <summary>
        /// Get list of VCSchoolSector
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCSchoolSector> GetVCSchoolSectors()
        {
            return this.Context.VCSchoolSectors.AsQueryable();
        }

        /// <summary>
        /// Get list of VCSchoolSector by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCSchoolSector> GetVCSchoolSectorsByName(string name)
        {
            var vcSchoolSectors = (from v in this.Context.VCSchoolSectors
                                   select v).AsQueryable();

            return vcSchoolSectors;
        }

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        public VCSchoolSector GetVCSchoolSectorById(Guid vcSchoolSectorId)
        {
            return this.Context.VCSchoolSectors.FirstOrDefault(v => v.VCSchoolSectorId == vcSchoolSectorId);
        }

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId using async
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        public async Task<VCSchoolSector> GetVCSchoolSectorByIdAsync(Guid vcSchoolSectorId)
        {
            var vcSchoolSector = await (from v in this.Context.VCSchoolSectors
                                        where v.VCSchoolSectorId == vcSchoolSectorId
                                        select v).FirstOrDefaultAsync();

            return vcSchoolSector;
        }

        /// <summary>
        /// Get SchoolSector by VCId
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public Task<VCSchoolSector> GetSchoolSectorByVCId(Guid vcId)
        {
            var vcSchoolSectors = (from vsc in this.Context.VCSchoolSectors
                                   join vtpc in this.Context.VTPCoordinatorMap on new { ay = vsc.AcademicYearId, vc = vsc.VCId } equals new { ay = vtpc.AcademicYearId, vc = vtpc.VCId }
                                   join vc in Context.VocationalCoordinators on vtpc.VCId equals vc.VCId
                                   where vc.IsActive && vc.VCId == vcId
                                   select vsc).FirstOrDefault();

            return Task.FromResult(vcSchoolSectors);
        }

        /// <summary>
        /// Insert/Update VCSchoolSector entity
        /// </summary>
        /// <param name="vcSchoolSector"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVCSchoolSectorDetails(VCSchoolSector vcSchoolSector)
        {
            try
            {
                if (RequestType.New == vcSchoolSector.RequestType)
                    Context.VCSchoolSectors.Add(vcSchoolSector);
                else
                {
                    Context.Entry<VCSchoolSector>(vcSchoolSector).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVCSchoolSectorDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcSchoolSectorId)
        {
            VCSchoolSector vcSchoolSector = this.Context.VCSchoolSectors.FirstOrDefault(v => v.VCSchoolSectorId == vcSchoolSectorId);

            if (vcSchoolSector != null)
            {
                Context.Entry<VCSchoolSector>(vcSchoolSector).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VCSchoolSector by Name
        /// </summary>
        /// <param name="vcSchoolSector"></param>
        /// <param name="vcSchoolSectorModel"></param>
        /// <returns></returns>
        public string CheckVCSchoolSectorExistByName(VCSchoolSector vcSchoolSector, VCSchoolSectorModel schoolSectorModel)
        {
            // Get list of all asigned VC against School VTP Sector in the School
            List<VCSchoolSector> schoolSectors = this.Context.VCSchoolSectors.Where(v => v.AcademicYearId == schoolSectorModel.AcademicYearId && v.SchoolVTPSectorId == schoolSectorModel.SchoolVTPSectorId && v.IsActive == true).ToList();

            if (schoolSectors.Count > 0)
            {
                if (schoolSectorModel.RequestType == RequestType.New)
                {
                    var schoolSectorItem = schoolSectors.FirstOrDefault();
                    if (schoolSectorItem != null)
                    {
                        return string.Format("Current School VTP Sector is already mapped to this VC");
                    }

                    if (schoolSectorItem != null && schoolSectorItem.IsActive && !Guid.Equals(schoolSectorItem.VCId, vcSchoolSector.VCId))
                    {
                        return string.Format("Current School VTP Sector is already mapped to another VC");
                    }
                }
                else if (schoolSectorModel.RequestType == RequestType.Edit)
                {
                    VCSchoolSector schoolSectorItem = schoolSectors.FirstOrDefault(c => c.AcademicYearId == schoolSectorModel.AcademicYearId && c.VCId == schoolSectorModel.VCId);

                    if (schoolSectorItem != null && schoolSectorItem.IsActive && !Guid.Equals(schoolSectorItem.SchoolVTPSectorId, vcSchoolSector.SchoolVTPSectorId) && Guid.Equals(schoolSectorItem.VCId, vcSchoolSector.VCId))
                    {
                        return string.Format("Current School VTP Sector is already mapped to this VC");
                    }

                    schoolSectorItem = schoolSectors.FirstOrDefault();
                    if (schoolSectorItem != null && schoolSectorItem.IsActive && !Guid.Equals(schoolSectorItem.SchoolVTPSectorId, vcSchoolSector.SchoolVTPSectorId) && !Guid.Equals(schoolSectorItem.VCId, vcSchoolSector.VCId))
                    {
                        return string.Format("Current School VTP Sector is already mapped to another VC");
                    }

                    if (vcSchoolSector.IsActive && !schoolSectorModel.IsActive)
                    {
                        var checkAnyVTSchoolSector = (from vcss in this.Context.VCSchoolSectors
                                                      join svs in this.Context.SchoolVTPSectors on new { a = vcss.AcademicYearId, b = vcss.SchoolVTPSectorId } equals new { a = svs.AcademicYearId, b = svs.SchoolVTPSectorId }
                                                      join vtss in this.Context.VTSchoolSectors on new { a = svs.AcademicYearId, b = svs.SchoolId, c = svs.SectorId } equals new { a = vtss.AcademicYearId, b = vtss.SchoolId, c = vtss.SectorId }
                                                      where svs.IsActive == true && vcss.IsActive == true && vtss.IsActive == true && vcss.VCSchoolSectorId == vcSchoolSector.VCSchoolSectorId
                                                      select new { AcademicYearId = vcss.AcademicYearId, VTPId = svs.VTPId, VCId = vcss.VCId, VTId = vtss.VTId, SchoolId = svs.SchoolId, SectorId = svs.SectorId, JobRoleId = vtss.JobRoleId }).FirstOrDefault();

                        if (checkAnyVTSchoolSector != null)
                        {
                            List<VTClass> vtClasses = this.Context.VTClasses.Where(v => v.AcademicYearId == vcSchoolSector.AcademicYearId && v.SchoolId == checkAnyVTSchoolSector.SchoolId && v.VTId == checkAnyVTSchoolSector.VTId && v.IsActive == true).ToList();

                            if (vtClasses.Count > 0)
                            {
                                return string.Format("VT School Sector & VT Classes are already active for this VC School Sector.");
                            }
                            else
                            {
                                return string.Format("VT School Sector are already active for this VC School Sector.");
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// List of VCSchoolSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<VCSchoolSectorViewModel> GetVCSchoolSectorsByCriteria(SearchVCSchoolSectorModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[4] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[6] = new MySqlParameter { ParameterName = "isRollover", MySqlDbType = MySqlDbType.Bool, Value = searchModel.IsRollover };
            sqlParams[7] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[8] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[9] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[10] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VCSchoolSectorViewModels.FromSql<VCSchoolSectorViewModel>("CALL GetVCSchoolSectorsByCriteria (@academicYearId, @vtpId, @vcId, @schoolId, @sectorId, @status, @isRollover, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}