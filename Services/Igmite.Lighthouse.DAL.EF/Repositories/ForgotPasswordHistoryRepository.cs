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
    /// Repository of the ForgotPasswordHistory entity
    /// </summary>
    public class ForgotPasswordHistoryRepository : GenericRepository<ForgotPasswordHistory>, IForgotPasswordHistoryRepository
    {
        /// <summary>
        /// Get list of ForgotPasswordHistory
        /// </summary>
        /// <returns></returns>
        public IQueryable<ForgotPasswordHistory> GetForgotPasswordHistories()
        {
            return this.Context.ForgotPasswordHistories.AsQueryable();
        }

        /// <summary>
        /// Get list of ForgotPasswordHistory by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ForgotPasswordHistory> GetForgotPasswordHistoriesByName(string name)
        {
            var forgotPasswordHistories = (from f in this.Context.ForgotPasswordHistories
                         select f).AsQueryable();

            return forgotPasswordHistories;
        }

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        public ForgotPasswordHistory GetForgotPasswordHistoryById(Guid forgotPasswordId)
        {
            return this.Context.ForgotPasswordHistories.FirstOrDefault(f => f.ForgotPasswordId == forgotPasswordId);
        }

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId using async
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordHistory> GetForgotPasswordHistoryByIdAsync(Guid forgotPasswordId)
        {
            var forgotPasswordHistory = await (from f in this.Context.ForgotPasswordHistories
                              where f.ForgotPasswordId == forgotPasswordId
                              select f).FirstOrDefaultAsync();

            return forgotPasswordHistory;
        }

        /// <summary>
        /// Insert/Update ForgotPasswordHistory entity
        /// </summary>
        /// <param name="forgotPasswordHistory"></param>
        /// <returns></returns>
        public bool SaveOrUpdateForgotPasswordHistoryDetails(ForgotPasswordHistory forgotPasswordHistory)
        {
            if (RequestType.New == forgotPasswordHistory.RequestType)
                Context.ForgotPasswordHistories.Add(forgotPasswordHistory);
            else
            {
                Context.Entry<ForgotPasswordHistory>(forgotPasswordHistory).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid forgotPasswordId)
        {
            ForgotPasswordHistory forgotPasswordHistory = this.Context.ForgotPasswordHistories.FirstOrDefault(f => f.ForgotPasswordId == forgotPasswordId);

            if (forgotPasswordHistory != null)
            {
                Context.Entry<ForgotPasswordHistory>(forgotPasswordHistory).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate ForgotPasswordHistory by Name
        /// </summary>
        /// <param name="forgotPasswordHistoryModel"></param>
        /// <returns></returns>
        public bool CheckForgotPasswordHistoryExistByName(ForgotPasswordHistoryModel forgotPasswordHistoryModel)
        {
            ForgotPasswordHistory forgotPasswordHistory = this.Context.ForgotPasswordHistories.FirstOrDefault(f => f.RequestDate == forgotPasswordHistoryModel.RequestDate);

            return forgotPasswordHistory != null;
        }

        /// <summary>}
        /// List of ForgotPasswordHistory with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ForgotPasswordHistoryViewModel> GetForgotPasswordHistoriesByCriteria(SearchForgotPasswordHistoryModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.ForgotPasswordHistoryViewModels.FromSql<ForgotPasswordHistoryViewModel>("CALL GetForgotPasswordHistoriesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
