using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class TEAndRMListMapper
    {
        public static TEAndRMListModel ToModel(this TEAndRMList tEAndRMList)
        {
            if (tEAndRMList == null)
                return null;

            TEAndRMListModel tEAndRMListModel = new TEAndRMListModel
            {
                TEAndRMId = tEAndRMList.TEAndRMId,
                SectorId = tEAndRMList.SectorId,
                JobRoleId = tEAndRMList.JobRoleId,
                ClassId = tEAndRMList.ClassId,
                TEType = tEAndRMList.TEType,
                SrNo = tEAndRMList.SrNo,
                ToolEquipmentName = tEAndRMList.ToolEquipmentName,
                Specification = tEAndRMList.Specification,
                UnitType = tEAndRMList.UnitType,
                UnitName = tEAndRMList.UnitName,
                CreatedBy = tEAndRMList.CreatedBy,
                CreatedOn = tEAndRMList.CreatedOn,
                UpdatedBy = tEAndRMList.UpdatedBy,
                UpdatedOn = tEAndRMList.UpdatedOn,
                IsActive = tEAndRMList.IsActive
            };

            return tEAndRMListModel;
        }

        public static TEAndRMList FromModel(this TEAndRMListModel tEAndRMListModel, TEAndRMList tEAndRMList)
        {
            tEAndRMList.TEAndRMId = tEAndRMListModel.TEAndRMId;
            tEAndRMList.SectorId = tEAndRMListModel.SectorId;
            tEAndRMList.JobRoleId = tEAndRMListModel.JobRoleId;
            tEAndRMList.ClassId = tEAndRMListModel.ClassId;
            tEAndRMList.TEType = tEAndRMListModel.TEType;
            tEAndRMList.SrNo = tEAndRMListModel.SrNo;
            tEAndRMList.ToolEquipmentName = tEAndRMListModel.ToolEquipmentName;
            tEAndRMList.Specification = tEAndRMListModel.Specification;
            tEAndRMList.UnitType = tEAndRMListModel.UnitType;
            tEAndRMList.UnitName = tEAndRMListModel.UnitName;
            tEAndRMList.IsActive = tEAndRMListModel.IsActive;
         
            return tEAndRMList;
        }
    }
}