using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all block WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class BlockController : BaseController
    {
        private readonly IBlockManager blockManager;

        /// <summary>
        /// Initializes the Block controller class.
        /// </summary>
        /// <param name="_blockManager"></param>
        public BlockController(IBlockManager _blockManager)
        {
            this.blockManager = _blockManager;
        }

        /// <summary>
        /// Get list of block data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetBlocks")]
        public async Task<ListResponse<BlockModel>> GetBlocks()
        {
            ListResponse<BlockModel> response = new ListResponse<BlockModel>();

            try
            {
                IQueryable<BlockModel> blockModels = await Task.Run(() =>
                {
                    return this.blockManager.GetBlocks();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = blockModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Block with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetBlocksByCriteria")]
        public async Task<ListResponse<BlockViewModel>> GetBlocksByCriteria([FromBody] SearchBlockModel searchModel)
        {
            ListResponse<BlockViewModel> response = new ListResponse<BlockViewModel>();

            try
            {
                var blockModels = await Task.Run(() =>
                {
                    return this.blockManager.GetBlocksByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = blockModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of block data by name
        /// </summary>
        /// <param name="blockName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetBlocksByName")]
        public async Task<ListResponse<BlockModel>> GetBlocksByName([FromQuery] string blockName)
        {
            ListResponse<BlockModel> response = new ListResponse<BlockModel>();

            try
            {
                var blockModels = await Task.Run(() =>
                {
                    return this.blockManager.GetBlocksByName(blockName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = blockModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get block data by Id
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetBlockById")]
        public async Task<SingularResponse<BlockModel>> GetBlockById([FromBody] DataRequest blockRequest)
        {
            SingularResponse<BlockModel> response = new SingularResponse<BlockModel>();

            try
            {
                var blockModel = await Task.Run(() =>
                {
                    return this.blockManager.GetBlockById(Guid.Parse(blockRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = blockModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new block
        /// </summary>
        /// <param name="blockRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateBlock"), Route("CreateOrUpdateBlockDetails")]
        public async Task<SingularResponse<string>> CreateBlock([FromBody] BlockRequest blockRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //blockRequest.RequestType = RequestType.New;
                    return this.blockManager.SaveOrUpdateBlockDetails(blockRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update block by Id
        /// </summary>
        /// <param name="blockRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateBlock")]
        public async Task<SingularResponse<string>> UpdateBlock([FromBody] BlockRequest blockRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    blockRequest.RequestType = RequestType.Edit;
                    return this.blockManager.SaveOrUpdateBlockDetails(blockRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete block by Id
        /// </summary>
        /// <param name="blockRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteBlockById")]
        public async Task<SingularResponse<bool>> DeleteBlockById([FromBody] DeleteRequest<string> blockRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.blockManager.DeleteById(Guid.Parse(blockRequest.DataId));
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}