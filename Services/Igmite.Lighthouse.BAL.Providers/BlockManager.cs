using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the Block entity
    /// </summary>
    public class BlockManager : GenericManager<BlockModel>, IBlockManager
    {
        private readonly IBlockRepository blockRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the block manager.
        /// </summary>
        /// <param name="blockRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public BlockManager(IBlockRepository _blockRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.blockRepository = _blockRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Blocks
        /// </summary>
        /// <returns></returns>
        public IQueryable<BlockModel> GetBlocks()
        {
            var blocks = this.blockRepository.GetBlocks();

            IList<BlockModel> blockModels = new List<BlockModel>();
            blocks.ForEach((user) => blockModels.Add(user.ToModel()));

            return blockModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Blocks by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<BlockModel> GetBlocksByName(string blockName)
        {
            var blocks = this.blockRepository.GetBlocksByName(blockName);

            IList<BlockModel> blockModels = new List<BlockModel>();
            blocks.ForEach((user) => blockModels.Add(user.ToModel()));

            return blockModels.AsQueryable();
        }

        /// <summary>
        /// Get Block by BlockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public BlockModel GetBlockById(Guid blockId)
        {
            Block block = this.blockRepository.GetBlockById(blockId);

            return (block != null) ? block.ToModel() : null;
        }

        /// <summary>
        /// Get Block by BlockId using async
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public async Task<BlockModel> GetBlockByIdAsync(Guid blockId)
        {
            var block = await this.blockRepository.GetBlockByIdAsync(blockId);

            return (block != null) ? block.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Block entity
        /// </summary>
        /// <param name="blockModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateBlockDetails(BlockModel blockModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Block block = null;

            //Validate model data
            blockModel = blockModel.GetModelValidationErrors<BlockModel>();

            if (blockModel.ErrorMessages.Count > 0)
            {
                response.Errors = blockModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (blockModel.RequestType == RequestType.Edit)
            {
                block = this.blockRepository.GetBlockById(blockModel.BlockId);
            }
            else
            {
                block = new Block();
                block.BlockId = Guid.NewGuid();
            }

            if (blockModel.ErrorMessages.Count == 0 && (blockModel.BlockName.StringVal().ToLower() != block.BlockName.StringVal().ToLower()))
            {
                bool isBlockExists = this.blockRepository.CheckBlockExistByName(blockModel);

                if (isBlockExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                block.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                block = blockModel.FromModel(block);

                //Save Or Update block details
                bool isSaved = this.blockRepository.SaveOrUpdateBlockDetails(block);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by BlockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid blockId)
        {
            return this.blockRepository.DeleteById(blockId);
        }

        /// <summary>
        /// Check duplicate Block by Name
        /// </summary>
        /// <param name="blockModel"></param>
        /// <returns></returns>
        public bool CheckBlockExistByName(BlockModel blockModel)
        {
            return this.blockRepository.CheckBlockExistByName(blockModel);
        }

        /// <summary>}
        /// List of Block with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<BlockViewModel> GetBlocksByCriteria(SearchBlockModel searchModel)
        {
            return this.blockRepository.GetBlocksByCriteria(searchModel);
        }
    }
}