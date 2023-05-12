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
    /// Repository of the VTStatusOfInductionInserviceTraining entity
    /// </summary>
    public class VTStatusOfInductionInserviceTrainingRepository : GenericRepository<VTStatusOfInductionInserviceTraining>, IVTStatusOfInductionInserviceTrainingRepository
    {
        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTraining
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStatusOfInductionInserviceTraining> GetVTStatusOfInductionInserviceTrainings()
        {
            return this.Context.VTStatusOfInductionInserviceTrainings.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTraining by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStatusOfInductionInserviceTraining> GetVTStatusOfInductionInserviceTrainingsByName(string name)
        {
            var vtStatusOfInductionInserviceTrainings = (from v in this.Context.VTStatusOfInductionInserviceTrainings
                         select v).AsQueryable();

            return vtStatusOfInductionInserviceTrainings;
        }

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        public VTStatusOfInductionInserviceTraining GetVTStatusOfInductionInserviceTrainingById(Guid vtStatusOfInductionInserviceTrainingId)
        {
            return this.Context.VTStatusOfInductionInserviceTrainings.FirstOrDefault(v => v.VTStatusOfInductionInserviceTrainingId == vtStatusOfInductionInserviceTrainingId);
        }

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId using async
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        public async Task<VTStatusOfInductionInserviceTraining> GetVTStatusOfInductionInserviceTrainingByIdAsync(Guid vtStatusOfInductionInserviceTrainingId)
        {
            var vtStatusOfInductionInserviceTraining = await (from v in this.Context.VTStatusOfInductionInserviceTrainings
                              where v.VTStatusOfInductionInserviceTrainingId == vtStatusOfInductionInserviceTrainingId
                              select v).FirstOrDefaultAsync();

            return vtStatusOfInductionInserviceTraining;
        }

        /// <summary>
        /// Insert/Update VTStatusOfInductionInserviceTraining entity
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTraining"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining)
        {
            if (RequestType.New == vtStatusOfInductionInserviceTraining.RequestType)
                Context.VTStatusOfInductionInserviceTrainings.Add(vtStatusOfInductionInserviceTraining);
            else
            {
                Context.Entry<VTStatusOfInductionInserviceTraining>(vtStatusOfInductionInserviceTraining).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStatusOfInductionInserviceTrainingId)
        {
            VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining = this.Context.VTStatusOfInductionInserviceTrainings.FirstOrDefault(v => v.VTStatusOfInductionInserviceTrainingId == vtStatusOfInductionInserviceTrainingId);

            if (vtStatusOfInductionInserviceTraining != null)
            {
                Context.Entry<VTStatusOfInductionInserviceTraining>(vtStatusOfInductionInserviceTraining).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTStatusOfInductionInserviceTraining by Name
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingModel"></param>
        /// <returns></returns>
        public bool CheckVTStatusOfInductionInserviceTrainingExistByName(VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel)
        {
            VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining = this.Context.VTStatusOfInductionInserviceTrainings.FirstOrDefault(v => v.IndustryTrainingStatus == vtStatusOfInductionInserviceTrainingModel.IndustryTrainingStatus);

            return vtStatusOfInductionInserviceTraining != null;
        }

        /// <summary>}
        /// List of VTStatusOfInductionInserviceTraining with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStatusOfInductionInserviceTrainingViewModel> GetVTStatusOfInductionInserviceTrainingsByCriteria(SearchVTStatusOfInductionInserviceTrainingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTStatusOfInductionInserviceTrainingViewModels.FromSql<VTStatusOfInductionInserviceTrainingViewModel>("CALL GetVTStatusOfInductionInserviceTrainingsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
