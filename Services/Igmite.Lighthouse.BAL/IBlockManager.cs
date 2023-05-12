using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Block entity
    /// </summary>
    public interface IBlockManager : IGenericManager<BlockModel>
    {
        /// <summary>
        /// Get list of Blocks
        /// </summary>
        /// <returns></returns>
        IQueryable<BlockModel> GetBlocks();

        /// <summary>
        /// Get list of Blocks by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<BlockModel> GetBlocksByName(string blockName);

        /// <summary>
        /// Get Block by BlockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        BlockModel GetBlockById(Guid blockId);

        /// <summary>
        /// Get Block by BlockId using async
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        Task<BlockModel> GetBlockByIdAsync(Guid blockId);

        /// <summary>
        /// Insert/Update Block entity
        /// </summary>
        /// <param name="blockModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateBlockDetails(BlockModel blockModel);

        /// <summary>
        /// Delete a record by BlockId
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