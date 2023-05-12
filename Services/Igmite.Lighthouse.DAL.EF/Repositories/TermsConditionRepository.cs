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
    /// Repository of the TermsCondition entity
    /// </summary>
    public class TermsConditionRepository : GenericRepository<TermsCondition>, ITermsConditionRepository
    {
        /// <summary>
        /// Get list of TermsCondition
        /// </summary>
        /// <returns></returns>
        public IQueryable<TermsCondition> GetTermsConditions()
        {
            return this.Context.TermsConditions.AsQueryable();
        }

        /// <summary>
        /// Get list of TermsCondition by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<TermsCondition> GetTermsConditionsByName(string name)
        {
            var termsConditions = (from t in this.Context.TermsConditions
                         where t.Name.Contains(name)
                         select t).AsQueryable();

            return termsConditions;
        }

        /// <summary>
        /// Get TermsCondition by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        public TermsCondition GetTermsConditionById(Guid termsConditionId)
        {
            return this.Context.TermsConditions.FirstOrDefault(t => t.TermsConditionId == termsConditionId);
        }

        /// <summary>
        /// Get TermsCondition by TermsConditionId using async
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        public async Task<TermsCondition> GetTermsConditionByIdAsync(Guid termsConditionId)
        {
            var termsCondition = await (from t in this.Context.TermsConditions
                              where t.TermsConditionId == termsConditionId
                              select t).FirstOrDefaultAsync();

            return termsCondition;
        }

        /// <summary>
        /// Insert/Update TermsCondition entity
        /// </summary>
        /// <param name="termsCondition"></param>
        /// <returns></returns>
        public bool SaveOrUpdateTermsConditionDetails(TermsCondition termsCondition)
        {
            if (RequestType.New == termsCondition.RequestType)
                Context.TermsConditions.Add(termsCondition);
            else
            {
                Context.Entry<TermsCondition>(termsCondition).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid termsConditionId)
        {
            TermsCondition termsCondition = this.Context.TermsConditions.FirstOrDefault(t => t.TermsConditionId == termsConditionId);

            if (termsCondition != null)
            {
                Context.Entry<TermsCondition>(termsCondition).State = EntityState.Deleted;

                //IList<Guid> accountUserTermIds = termsCondition.AccountUserTerms.Select(t => t.AccountTermsId).ToList();
                //foreach (Guid accountId in accountUserTermIds)
                //{
                //    AccountUserTerm accountUserTerm = termsCondition.AccountUserTerms.FirstOrDefault(t => t.AccountTermsId == accountId);
                //    Context.Entry<AccountUserTerm>(accountUserTerm).State = EntityState.Deleted;
                //}

                //IList<Guid> userAcceptanceIds = termsCondition.UserAcceptances.Select(t => t.UserAcceptanceId).ToList();
                //foreach (Guid userAcceptanceId in userAcceptanceIds)
                //{
                //    UserAcceptance userAcceptance = termsCondition.UserAcceptances.FirstOrDefault(t => t.UserAcceptanceId == userAcceptanceId);
                //    Context.Entry<UserAcceptance>(userAcceptance).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate TermsCondition by Name
        /// </summary>
        /// <param name="termsConditionModel"></param>
        /// <returns></returns>
        public bool CheckTermsConditionExistByName(TermsConditionModel termsConditionModel)
        {
            TermsCondition termsCondition = this.Context.TermsConditions.FirstOrDefault(t => t.Name == termsConditionModel.Name);

            return termsCondition != null;
        }

        /// <summary>}
        /// List of TermsCondition with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<TermsConditionViewModel> GetTermsConditionsByCriteria(SearchTermsConditionModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.TermsConditionViewModels.FromSql<TermsConditionViewModel>("CALL GetTermsConditionsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
