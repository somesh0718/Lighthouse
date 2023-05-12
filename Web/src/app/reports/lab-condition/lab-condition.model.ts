import { FuseUtils } from '@fuse/utils';

export class LabConditionModel {
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
    RoomDamaged: string;
    RawMaterial: string;
    IsSelected: string;
    DivisionId: string;
    DistrictId: string;
    Composite: string;
    ImgToolList: any;
    ImgLab: any;
    constructor() { }
}
