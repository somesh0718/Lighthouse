import { FuseUtils } from '@fuse/utils';

export class MaterialListReportModel {
    SrNo: number;
    VTPName: string;
    VCName: string;
    VCEmail: string;
    VTEmail: string;
    SchoolName: string;
    VTName: string;
    AcademicYear: string;
    SectorName: string;
    JobRoleName: string;
    ReceiptDate: Date;
    RawMaterialRequired: string;
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
    RFNreceiveStatus: string;
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
    RoomDamged: string;
    RawMaterial: string;
    IsSelected: string;
    DivisionId: string;
    DistrictId: string;
    ToolName: string;
    Status: string;
    ActionNeeded: string;
    MaterialName: string;
    RawMaterialStatus: string;
    QuantityCount: string;
    Composite: String;
    constructor() { }
}
