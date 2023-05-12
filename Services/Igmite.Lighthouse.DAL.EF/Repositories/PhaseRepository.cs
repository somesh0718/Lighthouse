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
    /// Repository of the Phase entity
    /// </summary>
    public class PhaseRepository : GenericRepository<Phase>, IPhaseRepository
    {
        /// <summary>
        /// Get list of Phase
        /// </summary>
        /// <returns></returns>
        public IQueryable<Phase> GetPhases()
        {
            return this.Context.Phases.AsQueryable();
        }

        /// <summary>
        /// Get list of Phase by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Phase> GetPhasesByName(string name)
        {
            var phases = (from p in this.Context.Phases
                         where p.PhaseName.Contains(name)
                         select p).AsQueryable();

            return phases;
        }

        /// <summary>
        /// Get Phase by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        public Phase GetPhaseById(Guid phaseId)
        {
            return this.Context.Phases.FirstOrDefault(p => p.PhaseId == phaseId);
        }

        /// <summary>
        /// Get Phase by PhaseId using async
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        public async Task<Phase> GetPhaseByIdAsync(Guid phaseId)
        {
            var phase = await (from p in this.Context.Phases
                              where p.PhaseId == phaseId
                              select p).FirstOrDefaultAsync();

            return phase;
        }

        /// <summary>
        /// Insert/Update Phase entity
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public bool SaveOrUpdatePhaseDetails(Phase phase)
        {
            if (RequestType.New == phase.RequestType)
                Context.Phases.Add(phase);
            else
            {
                Context.Entry<Phase>(phase).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid phaseId)
        {
            Phase phase = this.Context.Phases.FirstOrDefault(p => p.PhaseId == phaseId);

            if (phase != null)
            {
                Context.Entry<Phase>(phase).State = EntityState.Deleted;

                //IList<Guid> schoolIds = phase.Schools.Select(p => p.SchoolId).ToList();
                //foreach (Guid schoolId in schoolIds)
                //{
                //    School school = phase.Schools.FirstOrDefault(p => p.SchoolId == schoolId);
                //    Context.Entry<School>(school).State = EntityState.Deleted;
                //}

                //IList<Guid> sectorJobRoleIds = phase.SectorJobRoles.Select(p => p.SectorJobRoleId).ToList();
                //foreach (Guid sectorJobRoleId in sectorJobRoleIds)
                //{
                //    SectorJobRole sectorJobRole = phase.SectorJobRoles.FirstOrDefault(p => p.SectorJobRoleId == sectorJobRoleId);
                //    Context.Entry<SectorJobRole>(sectorJobRole).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Phase by Name
        /// </summary>
        /// <param name="phaseModel"></param>
        /// <returns></returns>
        public bool CheckPhaseExistByName(PhaseModel phaseModel)
        {
            Phase phase = this.Context.Phases.FirstOrDefault(p => p.PhaseName == phaseModel.PhaseName);

            return phase != null;
        }

        /// <summary>}
        /// List of Phase with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<PhaseViewModel> GetPhasesByCriteria(SearchPhaseModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.PhaseViewModels.FromSql<PhaseViewModel>("CALL GetPhasesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
