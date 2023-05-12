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
    /// Repository of the State entity
    /// </summary>
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        /// <summary>
        /// Get list of State
        /// </summary>
        /// <returns></returns>
        public IQueryable<State> GetStates()
        {
            return this.Context.States.AsQueryable();
        }

        /// <summary>
        /// Get list of State by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<State> GetStatesByName(string name)
        {
            var states = (from s in this.Context.States
                         where s.StateName.Contains(name)
                         select s).AsQueryable();

            return states;
        }

        /// <summary>
        /// Get State by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public State GetStateById(string stateCode)
        {
            return this.Context.States.FirstOrDefault(s => s.StateCode == stateCode);
        }

        /// <summary>
        /// Get State by StateCode using async
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public async Task<State> GetStateByIdAsync(string stateCode)
        {
            var state = await (from s in this.Context.States
                              where s.StateCode == stateCode
                              select s).FirstOrDefaultAsync();

            return state;
        }

        /// <summary>
        /// Insert/Update State entity
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SaveOrUpdateStateDetails(State state)
        {
            if (RequestType.New == state.RequestType)
                Context.States.Add(state);
            else
            {
                Context.Entry<State>(state).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by StateCode
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public bool DeleteById(string stateCode)
        {
            State state = this.Context.States.FirstOrDefault(s => s.StateCode == stateCode);

            if (state != null)
            {
                Context.Entry<State>(state).State = EntityState.Deleted;

                //IList<string> districtIds = state.Districts.Select(s => s.DistrictCode).ToList();
                //foreach (string districtCode in districtIds)
                //{
                //    District district = state.Districts.FirstOrDefault(s => s.DistrictCode == districtCode);
                //    Context.Entry<District>(district).State = EntityState.Deleted;
                //}

                //IList<Guid> schoolIds = state.Schools.Select(s => s.SchoolId).ToList();
                //foreach (Guid schoolId in schoolIds)
                //{
                //    School school = state.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
                //    Context.Entry<School>(school).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate State by Name
        /// </summary>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        public bool CheckStateExistByName(StateModel stateModel)
        {
            State state = this.Context.States.FirstOrDefault(s => s.StateName == stateModel.StateName);

            return state != null;
        }

        /// <summary>}
        /// List of State with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<StateViewModel> GetStatesByCriteria(SearchStateModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.StateViewModels.FromSql<StateViewModel>("CALL GetStatesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
