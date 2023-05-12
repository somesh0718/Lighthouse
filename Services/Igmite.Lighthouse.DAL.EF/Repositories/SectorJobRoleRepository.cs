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
    /// Repository of the SectorJobRole entity
    /// </summary>
    public class SectorJobRoleRepository : GenericRepository<SectorJobRole>, ISectorJobRoleRepository
    {
        /// <summary>
        /// Get list of SectorJobRole
        /// </summary>
        /// <returns></returns>
        public IQueryable<SectorJobRole> GetSectorJobRoles()
        {
            return this.Context.SectorJobRoles.AsQueryable();
        }

        /// <summary>
        /// Get list of SectorJobRole by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SectorJobRole> GetSectorJobRolesByName(string name)
        {
            var sectorJobRoles = (from s in this.Context.SectorJobRoles
                         select s).AsQueryable();

            return sectorJobRoles;
        }

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        public SectorJobRole GetSectorJobRoleById(Guid sectorJobRoleId)
        {
            return this.Context.SectorJobRoles.FirstOrDefault(s => s.SectorJobRoleId == sectorJobRoleId);
        }

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId using async
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        public async Task<SectorJobRole> GetSectorJobRoleByIdAsync(Guid sectorJobRoleId)
        {
            var sectorJobRole = await (from s in this.Context.SectorJobRoles
                              where s.SectorJobRoleId == sectorJobRoleId
                              select s).FirstOrDefaultAsync();

            return sectorJobRole;
        }

        /// <summary>
        /// Insert/Update SectorJobRole entity
        /// </summary>
        /// <param name="sectorJobRole"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSectorJobRoleDetails(SectorJobRole sectorJobRole)
        {
            if (RequestType.New == sectorJobRole.RequestType)
                Context.SectorJobRoles.Add(sectorJobRole);
            else
            {
                Context.Entry<SectorJobRole>(sectorJobRole).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid sectorJobRoleId)
        {
            SectorJobRole sectorJobRole = this.Context.SectorJobRoles.FirstOrDefault(s => s.SectorJobRoleId == sectorJobRoleId);

            if (sectorJobRole != null)
            {
                Context.Entry<SectorJobRole>(sectorJobRole).State = EntityState.Deleted;

                //IList<Guid> vtpSectorIds = sectorJobRole.VTPSectors.Select(s => s.VTPSectorId).ToList();
                //foreach (Guid vtpId in vtpSectorIds)
                //{
                //    VTPSector vtpSector = sectorJobRole.VTPSectors.FirstOrDefault(s => s.VTPSectorId == vtpId);
                //    Context.Entry<VTPSector>(vtpSector).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SectorJobRole by Name
        /// </summary>
        /// <param name="sectorJobRoleModel"></param>
        /// <returns></returns>
        public bool CheckSectorJobRoleExistByName(SectorJobRoleModel sectorJobRoleModel)
        {
            SectorJobRole sectorJobRole = this.Context.SectorJobRoles.FirstOrDefault(s => s.SectorId == sectorJobRoleModel.SectorId && s.JobRoleId == sectorJobRoleModel.JobRoleId && s.QPCode == sectorJobRoleModel.QPCode);

            return sectorJobRole != null;
        }

        /// <summary>}
        /// List of SectorJobRole with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SectorJobRoleViewModel> GetSectorJobRolesByCriteria(SearchSectorJobRoleModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SectorJobRoleViewModels.FromSql<SectorJobRoleViewModel>("CALL GetSectorJobRolesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
