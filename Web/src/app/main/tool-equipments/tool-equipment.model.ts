import { FuseUtils } from '@fuse/utils';

export class ToolEquipmentModel {
    ToolEquipmentId: string;
    VTPId: string;
    VCId: string;
    SchoolId: string;
    VTId: string;
    AcademicYearId: string;
    SectorId: string;
    JobRoleId: string;
    ReceiptDate: Date;
    TEReceiveStatus: string;
    TEStatus: string;
    RMStatus: string;
    RMFundStatus: string;
    Details: string;
    IsActive: boolean;
    RequestType: any;
    
    //New Questions
    OATEStatus: string;
    OFTEStatus: string;
    Reason: string;
    IsSpecify: string;
    RFNReceiveStatus: string;
    IsCommunicated: string;
    IsSetUpWorkShop: string;
    RoomType: string;
    AccommodateTools: string;
    RoomSize: number;
    IsDoorLock: string;
    Flooring: string;
    RoomWindows: string;
    TotalWindowCount: number;
    IsWindowGrills: string;
    IsWindowLocked: string;
    IsRoomActive: string;
    REFInstalled: string;
    WorkingSwitchBoard: string;
    PSSCount: number;
    WLCount: number;
    WFCount: number;
    RoomDamaged: string;
    IsSelected: string;
    DivisionId: string;
    DistrictCode: string;
    Remarks: string;

    TLPhotoFile: any;
    LabPhotoFile: any;
    RawMaterialRequired: string;
    TEToolList: any;
    TEMaterialList: any;

    constructor(toolEquipmentItem?: any) {
        toolEquipmentItem = toolEquipmentItem || {};

        this.ToolEquipmentId = toolEquipmentItem.ToolEquipmentId || FuseUtils.NewGuid();
        this.VTId = toolEquipmentItem.VTId || FuseUtils.NewGuid();
        this.AcademicYearId = toolEquipmentItem.AcademicYearId || null;
        this.SectorId = toolEquipmentItem.SectorId || null;
        this.JobRoleId = toolEquipmentItem.JobRoleId || null;
        this.ReceiptDate = toolEquipmentItem.ReceiptDate || null;
        this.TEReceiveStatus = toolEquipmentItem.TEReceiveStatus || null;
        this.TEStatus = toolEquipmentItem.TEStatus || null;
        this.RMStatus = toolEquipmentItem.RMStatus || null;
        this.RMFundStatus = toolEquipmentItem.RMFundStatus || null;
        this.Details = toolEquipmentItem.Details || null;
        this.IsActive = toolEquipmentItem.IsActive || true;
        this.RequestType = 0; // New

        //New qustions
        this.DivisionId = toolEquipmentItem.DivisionId || null;
        this.DistrictCode = toolEquipmentItem.DistrictId || null;
        this.OATEStatus = toolEquipmentItem.OATEStatus || null;
        this.OFTEStatus = toolEquipmentItem.OFTEStatus || null;
        this.Reason = toolEquipmentItem.Reason || null;
        this.IsSelected = toolEquipmentItem.IsSelected || null;
        this.IsSpecify = toolEquipmentItem.IsSpecify || null;
        this.RFNReceiveStatus = toolEquipmentItem.RFNReceiveStatus || null;
        this.IsCommunicated = toolEquipmentItem.IsCommunicated || null;
        this.IsSetUpWorkShop = toolEquipmentItem.IsSetUpWorkShop || null;
        this.RoomType = toolEquipmentItem.RoomType || null;
        this.AccommodateTools = toolEquipmentItem.AccommodateTools || null;
        this.RoomSize = toolEquipmentItem.RoomSize || null;
        this.IsDoorLock = toolEquipmentItem.IsDoorLock || null;
        this.Flooring = toolEquipmentItem.Flooring || null;
        this.RoomWindows = toolEquipmentItem.RoomWindows || null;
        this.TotalWindowCount = toolEquipmentItem.TotalWindowCount || null;
        this.IsWindowGrills = toolEquipmentItem.IsWindowGrills || null;
        this.IsWindowLocked = toolEquipmentItem.IsWindowLocked || null;
        this.IsRoomActive = toolEquipmentItem.IsRoomActive || null;
        this.REFInstalled = toolEquipmentItem.REFInstalled || null;
        this.WorkingSwitchBoard = toolEquipmentItem.WorkingSwitchBoard || null;
        this.PSSCount = toolEquipmentItem.PSSCount || null;
        this.WLCount = toolEquipmentItem.WLCount || null;
        this.WFCount = toolEquipmentItem.WFCount || null;
        this.RoomDamaged = toolEquipmentItem.RoomDamaged || null;
        this.Remarks = toolEquipmentItem.Remarks || null;
        this.TLPhotoFile = toolEquipmentItem.TLPhotoFile || null;
        this.LabPhotoFile = toolEquipmentItem.LabPhotoFile || null;
        this.RawMaterialRequired = toolEquipmentItem.RawMaterialRequired || null;
        this.TEToolList = toolEquipmentItem.TEToolList || [];
        this.TEMaterialList = toolEquipmentItem.TEMaterialList || [];
    }
}

export class ToolAndEquimentListModel {
    TEToolListId: string;
    ToolEquipmentId: string;
    ToolListId: string;
    ToolListName: string;
    ToolListStatus: string;
    TLActionNeeded1: string;
    RequestType: any;

    constructor(toolEquipmentItem?: any) {
        toolEquipmentItem = toolEquipmentItem || {};

        this.TEToolListId = toolEquipmentItem.TEToolListId || FuseUtils.NewGuid();
        this.ToolEquipmentId = toolEquipmentItem.ToolEquipmentId || FuseUtils.NewGuid();
        this.ToolListId = toolEquipmentItem.StateCode || null;
        this.ToolListName = toolEquipmentItem.ToolListName || null;
        this.ToolListStatus = toolEquipmentItem.ToolListStatus || null;
        this.TLActionNeeded1 = toolEquipmentItem.TLActionNeeded1 || null;
        this.RequestType = 0; // New
    }
}

export class RMListModel {
    TEMaterialListId: string;
    ToolEquipmentId: string;
    RawMaterialId: string;
    RawMaterialName: string;
    RawMaterialStatus: string;
    RMLastReceivedDate: Date;
    RawMaterialAction: string;
    QuantityCount: string;
    RequestType: any;

    constructor(toolEquipmentItem?: any) {
        toolEquipmentItem = toolEquipmentItem || {};

        this.TEMaterialListId = toolEquipmentItem.TEMaterialListId || FuseUtils.NewGuid();
        this.ToolEquipmentId = toolEquipmentItem.ToolEquipmentId || FuseUtils.NewGuid();
        this.RawMaterialId = toolEquipmentItem.RawMaterialId || null;
        this.RawMaterialName = toolEquipmentItem.RawMaterialName || null;
        this.RawMaterialStatus = toolEquipmentItem.RawMaterialStatus || null;
        this.RMLastReceivedDate = toolEquipmentItem.RMLastReceivedDate || null;
        this.RawMaterialAction = toolEquipmentItem.RawMaterialAction || null;
        this.QuantityCount = toolEquipmentItem.QuantityCount || null;
        this.RequestType = 0; // New
    }
}
