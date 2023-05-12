using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class BlockMapper
    {
        public static BlockModel ToModel(this Block block)
        {
            if (block == null)
                return null;

            BlockModel blockModel = new BlockModel
            {
                BlockId = block.BlockId,
                DivisionId = block.DivisionId,
                DistrictId = block.DistrictId,
                BlockName = block.BlockName,
                Description = block.Description,
                CreatedBy = block.CreatedBy,
                CreatedOn = block.CreatedOn,
                UpdatedBy = block.UpdatedBy,
                UpdatedOn = block.UpdatedOn,
                IsActive = block.IsActive
            };

            return blockModel;
        }

        public static Block FromModel(this BlockModel blockModel, Block block)
        {
            block.DistrictId = blockModel.DistrictId;
            block.BlockName = blockModel.BlockName;
            block.Description = blockModel.Description;
            block.IsActive = blockModel.IsActive;
            block.RequestType = blockModel.RequestType;
            block.SetAuditValues(blockModel.RequestType);

            return block;
        }
    }
}