export class VTPTransferModel {
    UserId: string;
    VTPSchoolModels: any = [];

    constructor(transferItem?: any) {
        transferItem = transferItem || {};

        this.UserId = transferItem.UserId || '';
        this.VTPSchoolModels = transferItem.VTPSchoolModels || [];
    }
}
