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
    /// Repository of the Role entity
    /// </summary>
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        /// <summary>
        /// Get list of Role
        /// </summary>
        /// <returns></returns>
        public IQueryable<Role> GetRoles()
        {
            return this.Context.Roles.Where(r => r.Code != "SUR").AsQueryable();
        }

        /// <summary>
        /// Get list of Role by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Role> GetRolesByName(string name)
        {
            var roles = (from r in this.Context.Roles
                         where r.Code != "SUR" && r.Name.Contains(name)
                         select r).AsQueryable();

            return roles;
        }

        /// <summary>
        /// Get Role by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetRoleById(Guid roleId)
        {
            return this.Context.Roles.FirstOrDefault(r => r.RoleId == roleId);
        }

        /// <summary>
        /// Get Role by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Role GetRoleByCode(string code)
        {
            return this.Context.Roles.FirstOrDefault(r => r.Code == code);
        }

        /// <summary>
        /// Get Role by RoleId using async
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            var role = await (from r in this.Context.Roles
                              where r.RoleId == roleId
                              select r).FirstOrDefaultAsync();

            return role;
        }

        /// <summary>
        /// Insert/Update Role entity
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool SaveOrUpdateRoleDetails(Role role)
        {
            if (RequestType.New == role.RequestType)
                Context.Roles.Add(role);
            else
            {
                Context.Entry<Role>(role).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid roleId)
        {
            Role role = this.Context.Roles.FirstOrDefault(r => r.RoleId == roleId);

            if (role != null)
            {
                Context.Entry<Role>(role).State = EntityState.Deleted;

                //IList<Guid> accountRoleIds = role.AccountRoles.Select(r => r.AccountRoleId).ToList();
                //foreach (Guid accountId in accountRoleIds)
                //{
                //    AccountRole accountRole = role.AccountRoles.FirstOrDefault(r => r.AccountRoleId == accountId);
                //    Context.Entry<AccountRole>(accountRole).State = EntityState.Deleted;
                //}

                //IList<Guid> transactionIds = role.Transactions.Select(r => r.RoleTransactionId).ToList();
                //foreach (Guid transactionId in transactionIds)
                //{
                //    RoleTransaction transaction = role.Transactions.FirstOrDefault(r => r.RoleTransactionId == transactionId);
                //    Context.Entry<RoleTransaction>(transaction).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Role by Name
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        public bool CheckRoleExistByName(RoleModel roleModel)
        {
            Role role = this.Context.Roles.FirstOrDefault(r => (r.Code == roleModel.Code));

            return role != null;
        }

        /// <summary>}
        /// List of Role with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<RoleViewModel> GetRolesByCriteria(SearchRoleModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.RoleViewModels.FromSql<RoleViewModel>("CALL GetRolesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}