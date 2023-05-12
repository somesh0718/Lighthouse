using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class ToolEquipmentMapper
    {
        public static ToolEquipmentModel ToModel(this ToolEquipment toolEquipment)
        {
            if (toolEquipment == null)
                return null;

            ToolEquipmentModel toolEquipmentModel = new ToolEquipmentModel
            {
                ToolEquipmentId = toolEquipment.ToolEquipmentId,
                DivisionId = toolEquipment.DivisionId,
                DistrictCode = toolEquipment.DistrictCode,
                SchoolId = toolEquipment.SchoolId,
                VTPId = toolEquipment.VTPId,
                VCId = toolEquipment.VCId,
                VTId = toolEquipment.VTId,
                AcademicYearId = toolEquipment.AcademicYearId,
                SectorId = toolEquipment.SectorId,
                JobRoleId = toolEquipment.JobRoleId,
                ReceiptDate = toolEquipment.ReceiptDate,
                TEReceiveStatus = toolEquipment.TEReceiveStatus,
                TEStatus = toolEquipment.TEStatus,
                RMStatus = toolEquipment.RMStatus,
                RMFundStatus = toolEquipment.RMFundStatus,
                Details = toolEquipment.Details,
                OATEStatus = toolEquipment.OATEStatus,
                OFTEStatus = toolEquipment.OFTEStatus,
                Reason = toolEquipment.Reason,
                ToolListStatus = toolEquipment.ToolListStatus,
                IsSelected = toolEquipment.IsSelected,
                IsSpecify = toolEquipment.IsSpecify,
                RFNReceiveStatus = toolEquipment.RFNReceiveStatus,
                IsCommunicated = toolEquipment.IsCommunicated,
                IsSetUpWorkShop = toolEquipment.IsSetUpWorkShop,
                RoomType = toolEquipment.RoomType,
                RoomSize =toolEquipment.RoomSize,
                AccommodateTools = toolEquipment.AccommodateTools,
                IsDoorLock = toolEquipment.IsDoorLock,
                Flooring = toolEquipment.Flooring,
                RoomWindows = toolEquipment.RoomWindows,
                TotalWindowCount = toolEquipment.TotalWindowCount,
                IsWindowGrills = toolEquipment.IsWindowGrills,
                IsWindowLocked = toolEquipment.IsWindowLocked,
                IsRoomActive = toolEquipment.IsRoomActive,
                REFInstalled = toolEquipment.REFInstalled,
                WorkingSwitchBoard = toolEquipment.WorkingSwitchBoard,
                PSSCount = toolEquipment.PSSCount,
                WLCount = toolEquipment.WLCount,
                WFCount = toolEquipment.WFCount,
                RawMaterialRequired = toolEquipment.RawMaterialRequired,
                ToolListId = toolEquipment.ToolListId,
                TLActionNeeded1 = toolEquipment.TLActionNeeded1,
                TLActionNeeded2 = toolEquipment.TLActionNeeded2,
                RawMaterialId= toolEquipment.RawMaterialId,
                RawMaterialStatus = toolEquipment.RawMaterialStatus,
                RMLastReceivedDate = toolEquipment.RMLastReceivedDate,
                RawMaterialAction = toolEquipment.RawMaterialAction,
                QuantityCount = toolEquipment.QuantityCount,
                TLFilePath = toolEquipment.TLFilePath,
                LabFilePath = toolEquipment.LabFilePath,
                Remarks = toolEquipment.Remarks,
                CreatedBy = toolEquipment.CreatedBy,
                CreatedOn = toolEquipment.CreatedOn,
                UpdatedBy = toolEquipment.UpdatedBy,
                UpdatedOn = toolEquipment.UpdatedOn,
                IsActive = toolEquipment.IsActive
            };

            return toolEquipmentModel;
        }

        public static ToolEquipment FromModel(this ToolEquipmentModel toolEquipmentModel, ToolEquipment toolEquipment)
        {
            toolEquipment.VTId = toolEquipmentModel.VTId;
            toolEquipment.AcademicYearId = toolEquipmentModel.AcademicYearId;
            toolEquipment.DivisionId = toolEquipmentModel.DivisionId;
            toolEquipment.DistrictCode = toolEquipmentModel.DistrictCode;
            toolEquipment.SchoolId = toolEquipmentModel.SchoolId;
            toolEquipment.VTPId = toolEquipmentModel.VTPId;
            toolEquipment.VCId = toolEquipmentModel.VCId;
            toolEquipment.AcademicYearId = toolEquipmentModel.AcademicYearId;
            toolEquipment.SectorId = toolEquipmentModel.SectorId;
            toolEquipment.JobRoleId = toolEquipmentModel.JobRoleId;
            toolEquipment.ReceiptDate = toolEquipmentModel.ReceiptDate;
            toolEquipment.TEReceiveStatus = toolEquipmentModel.TEReceiveStatus;
            toolEquipment.TEStatus = toolEquipmentModel.TEStatus;
            toolEquipment.RMStatus = toolEquipmentModel.RMStatus;
            toolEquipment.RMFundStatus = toolEquipmentModel.RMFundStatus;
            toolEquipment.Details = toolEquipmentModel.Details;
            toolEquipment.OATEStatus = toolEquipmentModel.OATEStatus;
            toolEquipment.OFTEStatus = toolEquipmentModel.OFTEStatus;
            toolEquipment.Reason = toolEquipmentModel.Reason;
            toolEquipment.IsSelected = toolEquipmentModel.IsSelected;
            toolEquipment.IsSpecify = toolEquipmentModel.IsSpecify;
            toolEquipment.ToolListStatus = toolEquipmentModel.ToolListStatus;
            toolEquipment.RFNReceiveStatus = toolEquipmentModel.RFNReceiveStatus;
            toolEquipment.IsCommunicated = toolEquipmentModel.IsCommunicated;
            toolEquipment.IsSetUpWorkShop = toolEquipmentModel.IsSetUpWorkShop;
            toolEquipment.RoomType = toolEquipmentModel.RoomType;
            toolEquipment.RoomSize = toolEquipmentModel.RoomSize;
            toolEquipment.AccommodateTools = toolEquipmentModel.AccommodateTools;
            toolEquipment.IsDoorLock = toolEquipmentModel.IsDoorLock;
            toolEquipment.Flooring = toolEquipmentModel.Flooring;
            toolEquipment.REFInstalled = toolEquipmentModel.REFInstalled;
            toolEquipment.RoomWindows = toolEquipmentModel.RoomWindows;
            toolEquipment.TotalWindowCount = toolEquipmentModel.TotalWindowCount;
            toolEquipment.IsWindowGrills = toolEquipmentModel.IsWindowGrills;
            toolEquipment.IsWindowLocked = toolEquipmentModel.IsWindowLocked;
            toolEquipment.IsRoomActive = toolEquipmentModel.IsRoomActive;
            toolEquipment.WorkingSwitchBoard = toolEquipmentModel.WorkingSwitchBoard;
            toolEquipment.PSSCount = toolEquipmentModel.PSSCount;
            toolEquipment.WLCount = toolEquipmentModel.WLCount;
            toolEquipment.WFCount = toolEquipmentModel.WFCount;
            toolEquipment.RawMaterialId = toolEquipmentModel.RawMaterialId;
            toolEquipment.RawMaterialRequired = toolEquipmentModel.RawMaterialRequired;
            toolEquipment.ToolListId = toolEquipmentModel.ToolListId;
            toolEquipment.TLActionNeeded1 = toolEquipmentModel.TLActionNeeded1;
            toolEquipment.TLActionNeeded2 = toolEquipmentModel.TLActionNeeded2;
            toolEquipment.RawMaterialStatus = toolEquipmentModel.RawMaterialStatus;
            toolEquipment.RMLastReceivedDate = toolEquipmentModel.RMLastReceivedDate;
            toolEquipment.RawMaterialAction = toolEquipmentModel.RawMaterialAction;
            toolEquipment.QuantityCount = toolEquipmentModel.QuantityCount;
            toolEquipment.TLFilePath = toolEquipmentModel.TLFilePath;
            toolEquipment.LabFilePath = toolEquipmentModel.LabFilePath;
            toolEquipment.Remarks = toolEquipmentModel.Remarks;
            toolEquipment.IsActive = toolEquipmentModel.IsActive;
            toolEquipment.RequestType = toolEquipmentModel.RequestType;
            toolEquipment.SetAuditValues(toolEquipmentModel.RequestType);

            return toolEquipment;
        }
    }
}