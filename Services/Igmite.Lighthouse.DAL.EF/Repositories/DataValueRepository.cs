using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the DataValue entity
    /// </summary>
    public class DataValueRepository : GenericRepository<DataValue>, IDataValueRepository
    {
        /// <summary>
        /// Get list of DataValue
        /// </summary>
        /// <returns></returns>
        public IQueryable<DataValue> GetDataValues()
        {
            return this.Context.DataValues.AsQueryable();
        }

        /// <summary>
        /// Get last DataValue value
        /// </summary>
        /// <returns></returns>
        public DataValue GetLastDataValue()
        {
            return this.Context.DataValues.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
        }

        /// <summary>
        /// Get list of DataValue by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DataValue> GetDataValuesByName(string name)
        {
            var dataValues = (from d in this.Context.DataValues
                              where d.Name.Contains(name)
                              select d).AsQueryable();

            return dataValues;
        }

        /// <summary>
        /// Get DataValue by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        public DataValue GetDataValueById(string dataValueId)
        {
            return this.Context.DataValues.FirstOrDefault(d => d.DataValueId == dataValueId);
        }

        /// <summary>
        /// Get DataValue by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataValue GetDataValueByCode(string code)
        {
            return this.Context.DataValues.FirstOrDefault(d => d.Code == code);
        }

        /// <summary>
        /// Get DataValue by DataValueId using async
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        public async Task<DataValue> GetDataValueByIdAsync(string dataValueId)
        {
            var dataValue = await (from d in this.Context.DataValues
                                   where d.DataValueId == dataValueId
                                   select d).FirstOrDefaultAsync();

            return dataValue;
        }

        /// <summary>
        /// Insert/Update DataValue entity
        /// </summary>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public bool SaveOrUpdateDataValueDetails(DataValue dataValue)
        {
            if (RequestType.New == dataValue.RequestType)
                Context.DataValues.Add(dataValue);
            else
            {
                Context.Entry<DataValue>(dataValue).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by DataValueId
        /// </summary>
        /// <param name="dataValueId"></param>
        /// <returns></returns>
        public bool DeleteById(string dataValueId)
        {
            DataValue dataValue = this.Context.DataValues.FirstOrDefault(d => d.DataValueId == dataValueId);

            if (dataValue != null)
            {
                Context.Entry<DataValue>(dataValue).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate DataValue by Name
        /// </summary>
        /// <param name="dataValueModel"></param>
        /// <returns></returns>
        public bool CheckDataValueExistByName(DataValueModel dataValueModel)
        {
            DataValue dataValue = this.Context.DataValues.FirstOrDefault(d => d.DataTypeId == dataValueModel.DataTypeId && d.Code == dataValueModel.Code && d.Name == dataValueModel.Name);

            return dataValue != null;
        }

        /// <summary>}
        /// List of DataValue with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DataValueViewModel> GetDataValuesByCriteria(SearchDataValueModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.DataValueViewModels.FromSql<DataValueViewModel>("CALL GetDataValuesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of DataValue by DataType
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public IQueryable<DataValue> GetDataValuesByType(string dataTypeId)
        {
            return this.Context.DataValues.Where(d => d.DataTypeId == dataTypeId).AsQueryable();
        }
    }
}