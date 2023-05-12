
export class ModuleUnitSessionModel {
    ModuleId: string;
    ModuleName: string;
    UnitId: string;
    UnitName: string;
    SessionIds: [string];
    SessionNames: [string];

    constructor(sessionItem?: any) {
        sessionItem = sessionItem || {};

        this.ModuleId = sessionItem.ModuleId || '';
        this.ModuleName = sessionItem.ModuleName || '';
        this.UnitId = sessionItem.UnitId || '';
        this.UnitName = sessionItem.UnitName || '';
        this.SessionIds = sessionItem.SessionIds || <string[]>[];
        this.SessionNames = sessionItem.SessionNames || '';
    }
}