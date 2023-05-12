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
    /// Repository of the Sector entity
    /// </summary>
    public class SectorRepository : GenericRepository<Sector>, ISectorRepository
    {
        /// <summary>
        /// Get list of Sector
        /// </summary>
        /// <returns></returns>
        public IQueryable<Sector> GetSectors()
        {
            return this.Context.Sectors.AsQueryable();
        }

        /// <summary>
        /// Get list of Sector by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Sector> GetSectorsByName(string name)
        {
            var sectors = (from s in this.Context.Sectors
                         where s.SectorName.Contains(name)
                         select s).AsQueryable();

            return sectors;
        }

        /// <summary>
        /// Get Sector by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public Sector GetSectorById(Guid sectorId)
        {
            return this.Context.Sectors.FirstOrDefault(s => s.SectorId == sectorId);
        }

        /// <summary>
        /// Get Sector by SectorId using async
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public async Task<Sector> GetSectorByIdAsync(Guid sectorId)
        {
            var sector = await (from s in this.Context.Sectors
                              where s.SectorId == sectorId
                              select s).FirstOrDefaultAsync();

            return sector;
        }

        /// <summary>
        /// Insert/Update Sector entity
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSectorDetails(Sector sector)
        {
            if (RequestType.New == sector.RequestType)
                Context.Sectors.Add(sector);
            else
            {
                Context.Entry<Sector>(sector).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid sectorId)
        {
            Sector sector = this.Context.Sectors.FirstOrDefault(s => s.SectorId == sectorId);

            if (sector != null)
            {
                Context.Entry<Sector>(sector).State = EntityState.Deleted;

                //IList<Guid> academicYearSchoolVTPJobRoleIds = sector.AcademicYearSchoolVTPJobRoles.Select(s => s.AcademicYearSchoolVTPSectorJobRoleId).ToList();
                //foreach (Guid academicYearSchoolVTPJobRoleId in academicYearSchoolVTPJobRoleIds)
                //{
                //    AcademicYearSchoolVTPSectorJobRole academicYearSchoolVTPJobRole = sector.AcademicYearSchoolVTPJobRoles.FirstOrDefault(s => s.AcademicYearSchoolVTPSectorJobRoleId == academicYearSchoolVTPJobRoleId);
                //    Context.Entry<AcademicYearSchoolVTPSectorJobRole>(academicYearSchoolVTPJobRole).State = EntityState.Deleted;
                //}

                //IList<Guid> jobRoleIds = sector.JobRoles.Select(s => s.SectorJobRoleId).ToList();
                //foreach (Guid jobRoleId in jobRoleIds)
                //{
                //    SectorJobRole jobRole = sector.JobRoles.FirstOrDefault(s => s.SectorJobRoleId == jobRoleId);
                //    Context.Entry<SectorJobRole>(jobRole).State = EntityState.Deleted;
                //}

                //IList<Guid> vtpJobRoleIds = sector.VTPJobRoles.Select(s => s.VTPSectorJobRoleId).ToList();
                //foreach (Guid vtpJobRoleId in vtpJobRoleIds)
                //{
                //    VTPSectorJobRole vtpJobRole = sector.VTPJobRoles.FirstOrDefault(s => s.VTPSectorJobRoleId == vtpJobRoleId);
                //    Context.Entry<VTPSectorJobRole>(vtpJobRole).State = EntityState.Deleted;
                //}

                //IList<Guid> vtSchoolSectorIds = sector.VTSchoolSectors.Select(s => s.VTSchoolSectorId).ToList();
                //foreach (Guid vtSchoolId in vtSchoolSectorIds)
                //{
                //    VTSchoolSector vtSchoolSector = sector.VTSchoolSectors.FirstOrDefault(s => s.VTSchoolSectorId == vtSchoolId);
                //    Context.Entry<VTSchoolSector>(vtSchoolSector).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Sector by Name
        /// </summary>
        /// <param name="sectorModel"></param>
        /// <returns></returns>
        public bool CheckSectorExistByName(SectorModel sectorModel)
        {
            Sector sector = this.Context.Sectors.FirstOrDefault(s => s.SectorName == sectorModel.SectorName);

            return sector != null;
        }

        /// <summary>}
        /// List of Sector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SectorViewModel> GetSectorsByCriteria(SearchSectorModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SectorViewModels.FromSql<SectorViewModel>("CALL GetSectorsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
