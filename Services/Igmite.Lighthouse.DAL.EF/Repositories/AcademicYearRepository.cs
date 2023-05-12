using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the AcademicYear entity
    /// </summary>
    public class AcademicYearRepository : GenericRepository<AcademicYear>, IAcademicYearRepository
    {
        /// <summary>
        /// Get list of AcademicYear
        /// </summary>
        /// <returns></returns>
        public IQueryable<AcademicYear> GetAcademicYears()
        {
            return this.Context.AcademicYears.AsQueryable();
        }

        /// <summary>
        /// Get list of AcademicYear by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<AcademicYear> GetAcademicYearsByName(string name)
        {
            var academicYears = (from a in this.Context.AcademicYears
                         where a.YearName.Contains(name)
                         select a).AsQueryable();

            return academicYears;
        }

        /// <summary>
        /// Get AcademicYear by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public AcademicYear GetAcademicYearById(Guid academicYearId)
        {
            return this.Context.AcademicYears.FirstOrDefault(a => a.AcademicYearId == academicYearId);
        }

        /// <summary>
        /// Get AcademicYear by AcademicYearId using async
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public async Task<AcademicYear> GetAcademicYearByIdAsync(Guid academicYearId)
        {
            var academicYear = await (from a in this.Context.AcademicYears
                              where a.AcademicYearId == academicYearId
                              select a).FirstOrDefaultAsync();

            return academicYear;
        }

        /// <summary>
        /// Insert/Update AcademicYear entity
        /// </summary>
        /// <param name="academicYear"></param>
        /// <returns></returns>
        public bool SaveOrUpdateAcademicYearDetails(AcademicYear academicYear)
        {
            if (RequestType.New == academicYear.RequestType)
                Context.AcademicYears.Add(academicYear);
            else
            {
                Context.Entry<AcademicYear>(academicYear).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid academicYearId)
        {
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(a => a.AcademicYearId == academicYearId);

            if (academicYear != null)
            {
                Context.Entry<AcademicYear>(academicYear).State = EntityState.Deleted;

                //IList<Guid> schoolVTPSectorJobRoleIds = academicYear.SchoolVTPSectorJobRoles.Select(a => a.AcademicYearSchoolVTPSectorJobRoleId).ToList();
                //foreach (Guid schoolVTPSectorJobRoleId in schoolVTPSectorJobRoleIds)
                //{
                //    AcademicYearSchoolVTPSectorJobRole schoolVTPSectorJobRole = academicYear.SchoolVTPSectorJobRoles.FirstOrDefault(a => a.AcademicYearSchoolVTPSectorJobRoleId == schoolVTPSectorJobRoleId);
                //    Context.Entry<AcademicYearSchoolVTPSectorJobRole>(schoolVTPSectorJobRole).State = EntityState.Deleted;
                //}

                //IList<Guid> headMasterIds = academicYear.HeadMasters.Select(a => a.HMId).ToList();
                //foreach (Guid hmId in headMasterIds)
                //{
                //    HeadMaster headMaster = academicYear.HeadMasters.FirstOrDefault(a => a.HMId == hmId);
                //    Context.Entry<HeadMaster>(headMaster).State = EntityState.Deleted;
                //}

                //IList<Guid> schoolIds = academicYear.Schools.Select(a => a.SchoolId).ToList();
                //foreach (Guid schoolId in schoolIds)
                //{
                //    School school = academicYear.Schools.FirstOrDefault(a => a.SchoolId == schoolId);
                //    Context.Entry<School>(school).State = EntityState.Deleted;
                //}

                //IList<Guid> schoolVEInchargeIds = academicYear.SchoolVEIncharges.Select(a => a.VEIId).ToList();
                //foreach (Guid veiId in schoolVEInchargeIds)
                //{
                //    SchoolVEIncharge schoolVEIncharge = academicYear.SchoolVEIncharges.FirstOrDefault(a => a.VEIId == veiId);
                //    Context.Entry<SchoolVEIncharge>(schoolVEIncharge).State = EntityState.Deleted;
                //}

                //IList<Guid> schoolVTPSectorIds = academicYear.SchoolVTPSectors.Select(a => a.SchoolVTPSectorId).ToList();
                //foreach (Guid schoolVTPSectorId in schoolVTPSectorIds)
                //{
                //    SchoolVTPSector schoolVTPSector = academicYear.SchoolVTPSectors.FirstOrDefault(a => a.SchoolVTPSectorId == schoolVTPSectorId);
                //    Context.Entry<SchoolVTPSector>(schoolVTPSector).State = EntityState.Deleted;
                //}

                //IList<Guid> sectorJobRoleIds = academicYear.SectorJobRoles.Select(a => a.SectorJobRoleId).ToList();
                //foreach (Guid sectorJobRoleId in sectorJobRoleIds)
                //{
                //    SectorJobRole sectorJobRole = academicYear.SectorJobRoles.FirstOrDefault(a => a.SectorJobRoleId == sectorJobRoleId);
                //    Context.Entry<SectorJobRole>(sectorJobRole).State = EntityState.Deleted;
                //}

                //IList<Guid> studentClassDetailIds = academicYear.StudentClassDetails.Select(a => a.StudentId).ToList();
                //foreach (Guid studentId in studentClassDetailIds)
                //{
                //    StudentClassDetail studentClassDetail = academicYear.StudentClassDetails.FirstOrDefault(a => a.StudentId == studentId);
                //    Context.Entry<StudentClassDetail>(studentClassDetail).State = EntityState.Deleted;
                //}

                //IList<Guid> studentClassIds = academicYear.StudentClasses.Select(a => a.StudentId).ToList();
                //foreach (Guid studentId in studentClassIds)
                //{
                //    StudentClass studentClass = academicYear.StudentClasses.FirstOrDefault(a => a.StudentId == studentId);
                //    Context.Entry<StudentClass>(studentClass).State = EntityState.Deleted;
                //}

                //IList<Guid> vcSchoolSectorIds = academicYear.VCSchoolSectors.Select(a => a.VCSchoolSectorId).ToList();
                //foreach (Guid vcSchoolSectorId in vcSchoolSectorIds)
                //{
                //    VCSchoolSector vcSchoolSector = academicYear.VCSchoolSectors.FirstOrDefault(a => a.VCSchoolSectorId == vcSchoolSectorId);
                //    Context.Entry<VCSchoolSector>(vcSchoolSector).State = EntityState.Deleted;
                //}

                //IList<Guid> vtClassIds = academicYear.VTClasses.Select(a => a.VTClassId).ToList();
                //foreach (Guid vtClassId in vtClassIds)
                //{
                //    VTClass vtClass = academicYear.VTClasses.FirstOrDefault(a => a.VTClassId == vtClassId);
                //    Context.Entry<VTClass>(vtClass).State = EntityState.Deleted;
                //}

                //IList<Guid> vtpSectorIds = academicYear.VTPSectors.Select(a => a.VTPSectorId).ToList();
                //foreach (Guid vtpSectorId in vtpSectorIds)
                //{
                //    VTPSector vtpSector = academicYear.VTPSectors.FirstOrDefault(a => a.VTPSectorId == vtpSectorId);
                //    Context.Entry<VTPSector>(vtpSector).State = EntityState.Deleted;
                //}

                //IList<Guid> vtSchoolSectorIds = academicYear.VTSchoolSectors.Select(a => a.VTSchoolSectorId).ToList();
                //foreach (Guid vtSchoolSectorId in vtSchoolSectorIds)
                //{
                //    VTSchoolSector vtSchoolSector = academicYear.VTSchoolSectors.FirstOrDefault(a => a.VTSchoolSectorId == vtSchoolSectorId);
                //    Context.Entry<VTSchoolSector>(vtSchoolSector).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate AcademicYear by Name
        /// </summary>
        /// <param name="academicYearModel"></param>
        /// <returns></returns>
        public bool CheckAcademicYearExistByName(AcademicYearModel academicYearModel)
        {
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(a => a.YearName == academicYearModel.YearName);

            return academicYear != null;
        }

        /// <summary>}
        /// List of AcademicYear with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<AcademicYearViewModel> GetAcademicYearsByCriteria(SearchAcademicYearModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.AcademicYearViewModels.FromSql<AcademicYearViewModel>("CALL GetAcademicYearsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
