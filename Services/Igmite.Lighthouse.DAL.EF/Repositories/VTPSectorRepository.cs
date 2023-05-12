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
    /// Repository of the VTPSector entity
    /// </summary>
    public class VTPSectorRepository : GenericRepository<VTPSector>, IVTPSectorRepository
    {
        /// <summary>
        /// Get list of VTPSector
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPSector> GetVTPSectors()
        {
            return this.Context.VTPSectors.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPSector by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPSector> GetVTPSectorsByName(string name)
        {
            var vtpSectors = (from v in this.Context.VTPSectors
                              select v).AsQueryable();

            return vtpSectors;
        }

        /// <summary>
        /// Get VTPSector by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        public VTPSector GetVTPSectorById(Guid vtpSectorId)
        {
            return this.Context.VTPSectors.FirstOrDefault(v => v.VTPSectorId == vtpSectorId);
        }

        /// <summary>
        /// Get VTPSector by VTPSectorId using async
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        public async Task<VTPSector> GetVTPSectorByIdAsync(Guid vtpSectorId)
        {
            var vtpSector = await (from v in this.Context.VTPSectors
                                   where v.VTPSectorId == vtpSectorId
                                   select v).FirstOrDefaultAsync();

            return vtpSector;
        }

        /// <summary>
        /// Insert/Update VTPSector entity
        /// </summary>
        /// <param name="vtpSector"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTPSectorDetails(VTPSector vtpSector)
        {
            if (RequestType.New == vtpSector.RequestType)
                Context.VTPSectors.Add(vtpSector);
            else
            {
                Context.Entry<VTPSector>(vtpSector).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpSectorId)
        {
            VTPSector vtpSector = this.Context.VTPSectors.FirstOrDefault(v => v.VTPSectorId == vtpSectorId);

            if (vtpSector != null)
            {
                Context.Entry<VTPSector>(vtpSector).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTPSector by Name
        /// </summary>
        /// <param name="vtpSectorModel"></param>
        /// <returns></returns>
        public bool CheckVTPSectorExistByName(VTPSectorModel vtpSectorModel)
        {
            VTPSector vtpSector = this.Context.VTPSectors.FirstOrDefault(v => v.AcademicYearId == vtpSectorModel.AcademicYearId && v.VTPId == vtpSectorModel.VTPId && v.SectorId == vtpSectorModel.SectorId);

            return vtpSector != null;
        }

        /// <summary>
        /// Check VTP Sector can be inactive
        /// </summary>
        /// <param name="vtpSector"></param>
        /// <returns></returns>
        public bool CheckUserCanInactiveVTPSectorById(VTPSector vtpSector)
        {
            SchoolVTPSector schoolVTPSector = this.Context.SchoolVTPSectors.FirstOrDefault(svs => svs.AcademicYearId == vtpSector.AcademicYearId && svs.VTPId == vtpSector.VTPId && svs.SectorId == vtpSector.SectorId);

            return (schoolVTPSector == null);
        }

        /// <summary>
        /// Get list of VTPSector
        /// </summary>
        /// <returns></returns>
        public IList<VTPSector> GetVTPSectorList()
        {
            var vtpSectorList = (from vs in this.Context.VTPSectors
                                 join vtpm in this.Context.VTPAcademicYearMap on new { a = vs.AcademicYearId, b = vs.VTPId } equals new { a = vtpm.AcademicYearId, b = vtpm.VTPId }
                                 join ay in this.Context.AcademicYears on vtpm.AcademicYearId equals ay.AcademicYearId
                                 where vs.IsActive == true && vtpm.IsActive == true && ay.IsCurrentAcademicYear == true && vtpm.DateOfResignation == null
                                 select vs).ToList();

            return vtpSectorList;
        }

        /// <summary>}
        /// List of VTPSector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPSectorViewModel> GetVTPSectorsByCriteria(SearchVTPSectorModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[9];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[3] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[4] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[5] = new MySqlParameter { ParameterName = "isRollover", MySqlDbType = MySqlDbType.Bool, Value = searchModel.IsRollover };
            sqlParams[6] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[7] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[8] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VTPSectorViewModels.FromSql<VTPSectorViewModel>("CALL GetVTPSectorsByCriteria (@academicYearId, @vtpId, @sectorId, @status, @name, @isRollover, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}