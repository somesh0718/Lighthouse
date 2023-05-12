export class VCTransferModel {
    UserId: string;
    VCSchoolModels: any = [];

    constructor(transferItem?: any) {
        transferItem = transferItem || {};

        this.UserId = transferItem.UserId || '';
        this.VCSchoolModels = transferItem.VCSchoolModels || [];
    }
}
