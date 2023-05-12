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
    /// Repository of the VTPSectorJobRole entity
    /// </summary>
    public class VTPSectorJobRoleRepository : GenericRepository<VTPSectorJobRole>, IVTPSectorJobRoleRepository
    {
        /// <summary>
        /// Get list of VTPSectorJobRole
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPSectorJobRole> GetVTPSectorJobRoles()
        {
            return this.Context.VTPSectorJobRoles.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPSectorJobRole by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPSectorJobRole> GetVTPSectorJobRolesByName(string name)
        {
            var vtpSectorJobRoles = (from v in this.Context.VTPSectorJobRoles
                         where v.VTPSectorJobRoleName.Contains(name)
                         select v).AsQueryable();

            return vtpSectorJobRoles;
        }

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        public VTPSectorJobRole GetVTPSectorJobRoleById(Guid vtpSectorJobRoleId)
        {
            return this.Context.VTPSectorJobRoles.FirstOrDefault(v => v.VTPSectorJobRoleId == vtpSectorJobRoleId);
        }

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId using async
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        public async Task<VTPSectorJobRole> GetVTPSectorJobRoleByIdAsync(Guid vtpSectorJobRoleId)
        {
            var vtpSectorJobRole = await (from v in this.Context.VTPSectorJobRoles
                              where v.VTPSectorJobRoleId == vtpSectorJobRoleId
                              select v).FirstOrDefaultAsync();

            return vtpSectorJobRole;
        }

        /// <summary>
        /// Insert/Update VTPSectorJobRole entity
        /// </summary>
        /// <param name="vtpSectorJobRole"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTPSectorJobRoleDetails(VTPSectorJobRole vtpSectorJobRole)
        {
            if (RequestType.New == vtpSectorJobRole.RequestType)
                Context.VTPSectorJobRoles.Add(vtpSectorJobRole);
            else
            {
                Context.Entry<VTPSectorJobRole>(vtpSectorJobRole).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpSectorJobRoleId)
        {
            VTPSectorJobRole vtpSectorJobRole = this.Context.VTPSectorJobRoles.FirstOrDefault(v => v.VTPSectorJobRoleId == vtpSectorJobRoleId);

            if (vtpSectorJobRole != null)
            {
                Context.Entry<VTPSectorJobRole>(vtpSectorJobRole).State = EntityState.Deleted;

                //IList<Guid> schoolVTPSectorIds = vtpSectorJobRole.SchoolVTPSectors.Select(v => v.SchoolVTPSectorId).ToList();
                //foreach (Guid schoolId in schoolVTPSectorIds)
                //{
                //    SchoolVTPSector schoolVTPSector = vtpSectorJobRole.SchoolVTPSectors.FirstOrDefault(v => v.SchoolVTPSectorId == schoolId);
                //    Context.Entry<SchoolVTPSector>(schoolVTPSector).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTPSectorJobRole by Name
        /// </summary>
        /// <param name="vtpSectorJobRoleModel"></param>
        /// <returns></returns>
        public bool CheckVTPSectorJobRoleExistByName(VTPSectorJobRoleModel vtpSectorJobRoleModel)
        {
            VTPSectorJobRole vtpSectorJobRole = this.Context.VTPSectorJobRoles.FirstOrDefault(v => v.VTPSectorJobRoleName == vtpSectorJobRoleModel.VTPSectorJobRoleName);

            return vtpSectorJobRole != null;
        }

        /// <summary>}
        /// List of VTPSectorJobRole with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPSectorJobRoleViewModel> GetVTPSectorJobRolesByCriteria(SearchVTPSectorJobRoleModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTPSectorJobRoleViewModels.FromSql<VTPSectorJobRoleViewModel>("CALL GetVTPSectorJobRolesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
