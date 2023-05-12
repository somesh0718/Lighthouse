using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Block entity
    /// </summary>
    public interface IBlockRepository : IGenericRepository<Block>
    {
        /// <summary>
        /// Get list of Block
        /// </summary>
        /// <returns></returns>
        IQueryable<Block> GetBlocks();

        /// <summary>
        /// Get list of Block by blockName
        /// </summary>
        /// <param name="blockName"></param>
        /// <returns></returns>
        IQueryable<Block> GetBlocksByName(string blockName);

        /// <summary>
        /// Get Block by blockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        Block GetBlockById(Guid blockId);

        /// <summary>
        /// Get Block by blockId using async
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        Task<Block> GetBlockByIdAsync(Guid blockId);

        /// <summary>
        /// Insert/Update Block entity
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        bool SaveOrUpdateBlockDetails(Block block);

        /// <summary>
        /// Delete a record by blockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        bool DeleteById(Guid blockId);

        /// <summary>
        /// Check duplicate Block by Name
        /// </summary>
        /// <param name="blockModel"></param>
        /// <returns></returns>
        bool CheckBlockExistByName(BlockModel blockModel);

        /// <summary>
        /// List of Block with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<BlockViewModel> GetBlocksByCriteria(SearchBlockModel searchModel);
    }
}