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
    /// Repository of the Block entity
    /// </summary>
    public class BlockRepository : GenericRepository<Block>, IBlockRepository
    {
        /// <summary>
        /// Get list of Block
        /// </summary>
        /// <returns></returns>
        public IQueryable<Block> GetBlocks()
        {
            return this.Context.Blocks.AsQueryable();
        }

        /// <summary>
        /// Get list of Block by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Block> GetBlocksByName(string name)
        {
            var blocks = (from d in this.Context.Blocks
                          where d.BlockName.Contains(name)
                          select d).AsQueryable();

            return blocks;
        }

        /// <summary>
        /// Get Block by blockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public Block GetBlockById(Guid blockId)
        {
            Block block = (from b in this.Context.Blocks
                           join d in this.Context.Districts on b.DistrictId equals d.DistrictCode
                           where b.BlockId == blockId
                           select new Block
                           {
                               BlockId = b.BlockId,
                               DistrictId = b.DistrictId,
                               DivisionId = d.DivisionId,
                               BlockName = b.BlockName,
                               Description = b.Description,
                               CreatedBy = b.CreatedBy,
                               CreatedOn = b.CreatedOn,
                               UpdatedBy = b.UpdatedBy,
                               UpdatedOn = b.UpdatedOn,
                               IsActive = b.IsActive
                           }).FirstOrDefault();

            return block;
        }

        /// <summary>
        /// Get Block by blockId using async
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public async Task<Block> GetBlockByIdAsync(Guid blockId)
        {
            var block = await (from d in this.Context.Blocks
                               where d.BlockId == blockId
                               select d).FirstOrDefaultAsync();

            return block;
        }

        /// <summary>
        /// Insert/Update Block entity
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public bool SaveOrUpdateBlockDetails(Block block)
        {
            if (RequestType.New == block.RequestType)
                Context.Blocks.Add(block);
            else
            {
                Context.Entry<Block>(block).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by BlockCode
        /// </summary>
        /// <param name="blockCode"></param>
        /// <returns></returns>
        public bool DeleteById(Guid blockId)
        {
            Block block = this.Context.Blocks.FirstOrDefault(d => d.BlockId == blockId);

            if (block != null)
            {
                Context.Entry<Block>(block).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Block by Name
        /// </summary>
        /// <param name="blockModel"></param>
        /// <returns></returns>
        public bool CheckBlockExistByName(BlockModel blockModel)
        {
            Block block = this.Context.Blocks.FirstOrDefault(d => d.BlockName == blockModel.BlockName);

            return block != null;
        }

        /// <summary>}
        /// List of Block with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<BlockViewModel> GetBlocksByCriteria(SearchBlockModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.BlockViewModels.FromSql<BlockViewModel>("CALL GetBlocksByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}