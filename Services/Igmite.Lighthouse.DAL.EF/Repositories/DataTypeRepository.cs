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
    /// Repository of the DataType entity
    /// </summary>
    public class DataTypeRepository : GenericRepository<DataType>, IDataTypeRepository
    {
        /// <summary>
        /// Get list of DataType
        /// </summary>
        /// <returns></returns>
        public IQueryable<DataType> GetDataTypes()
        {
            return this.Context.DataTypes.AsQueryable();
        }

        /// <summary>
        /// Get last DataType value
        /// </summary>
        /// <returns></returns>
        public DataType GetLastDataType()
        {
            return this.Context.DataTypes.LastOrDefault();
        }

        /// <summary>
        /// Get list of DataType by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DataType> GetDataTypesByName(string name)
        {
            var dataTypes = (from d in this.Context.DataTypes
                             where d.Name.Contains(name)
                             select d).AsQueryable();

            return dataTypes;
        }

        /// <summary>
        /// Get DataType by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public DataType GetDataTypeById(int dataTypeId)
        {
            return this.Context.DataTypes.FirstOrDefault(d => d.DataTypeId == dataTypeId);
        }

        /// <summary>
        /// Get DataType by DataTypeId using async
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public async Task<DataType> GetDataTypeByIdAsync(int dataTypeId)
        {
            var dataType = await (from d in this.Context.DataTypes
                                  where d.DataTypeId == dataTypeId
                                  select d).FirstOrDefaultAsync();

            return dataType;
        }

        /// <summary>
        /// Insert/Update DataType entity
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public bool SaveOrUpdateDataTypeDetails(DataType dataType)
        {
            if (RequestType.New == dataType.RequestType)
                Context.DataTypes.Add(dataType);
            else
            {
                Context.Entry<DataType>(dataType).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by DataTypeId
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public bool DeleteById(int dataTypeId)
        {
            DataType dataType = this.Context.DataTypes.FirstOrDefault(d => d.DataTypeId == dataTypeId);

            if (dataType != null)
            {
                Context.Entry<DataType>(dataType).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate DataType by Name
        /// </summary>
        /// <param name="dataTypeModel"></param>
        /// <returns></returns>
        public bool CheckDataTypeExistByName(DataTypeModel dataTypeModel)
        {
            DataType dataType = this.Context.DataTypes.FirstOrDefault(d => d.Name == dataTypeModel.Name);

            return dataType != null;
        }

        /// <summary>}
        /// List of DataType with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DataTypeViewModel> GetDataTypesByCriteria(SearchDataTypeModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.DataTypeViewModels.FromSql<DataTypeViewModel>("CALL GetDataTypesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}