import { Injectable } from "@angular/core";
import { forkJoin, Observable } from "rxjs";
import { retry, catchError, tap } from "rxjs/operators";
import { BaseService } from 'app/services/base.service';
import { UserModel } from "app/models/user.model";
import { CommonService } from "app/services/common.service";

@Injectable()
export class ToolEquipmentService {
    constructor(private http: BaseService, private commonService: CommonService) { }

    getToolEquipments(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.ToolEquipment.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    GetAllByCriteria(filters: any): Observable<any> {
        return this.http
            .HttpPost(this.http.Services.ToolEquipment.GetAllByCriteria, filters)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getToolEquipmentById(toolEquipmentId: string) {
        let requestParams = {
            DataId: toolEquipmentId
        };

        return this.http
            .HttpPost(this.http.Services.ToolEquipment.GetById, requestParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(function (response: any) {
                    return response.Results;
                })
            );
    }

    createOrUpdateToolEquipment(formData: any) {
        return this.http
            .HttpPost(this.http.Services.ToolEquipment.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    deleteToolEquipmentById(toolEquipmentId: string) {
        var toolEquipmentParams = {
            DataId: toolEquipmentId
        };

        return this.http
            .HttpPost(this.http.Services.ToolEquipment.Delete, toolEquipmentParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getVTPByUser(userModel: UserModel): Observable<any[]> {
        let academicYearRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYears', UserId: userModel.UserTypeId, SelectTitle: 'Academic Year' }, false);
        let vtpRequest = this.commonService.GetVTPByAYId(userModel.RoleCode, userModel.UserTypeId, userModel.AcademicYearId);
        let schoolRequest = this.http.GetMasterDataByType({ DataType: 'SchoolsByVT', ParentId: userModel.UserTypeId, SelectTitle: 'School' }, false);
        let divisionRequest = this.http.GetMasterDataByType({ DataType: 'Divisions', RoleId: userModel.RoleCode, UserId: userModel.UserTypeId, ParentId: userModel.DefaultStateId, SelectTitle: 'Division' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([vtpRequest, academicYearRequest, schoolRequest, divisionRequest]);
    }

    getAcademicYearSectorByUser(vtId: any): Observable<any[]> {
        let academicYearRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYearsByVT', RoleId: 'VT', ParentId: vtId, SelectTitle: 'Academic Year' });
        let sectorRequest = this.http.GetMasterDataByType({ DataType: 'SectorsByVT', UserId: vtId, SelectTitle: 'Sector' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([academicYearRequest, sectorRequest]);
    }

    getJobRoleToolListRawMaterialBySector(sectorParams: any): Observable<any[]> {
        let jobRoleRequest = this.http.GetMasterDataByType({ DataType: 'TEJobRolesForVT', RoleId: sectorParams.AcademicYearId, UserId: sectorParams.VTId, ParentId: sectorParams.SectorId, SelectTitle: 'Job Role' });
        let toolListRequest = this.commonService.GetTEAndRMList();

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([jobRoleRequest, toolListRequest]);
    }


    initToolsAndEquipmentsData(userModel: UserModel): Observable<any[]> {
        let academicYearRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYears', UserId: userModel.UserTypeId, SelectTitle: 'Academic Year' }, false);
        let sectorRequest = this.http.GetMasterDataByType({ DataType: 'Sectors', SelectTitle: 'Sector' });
        let vtRequest = this.http.GetMasterDataByType({ DataType: 'VocationalTrainersByVC', RoleId: userModel.RoleCode, ParentId: userModel.UserTypeId, SelectTitle: 'Vocational Trainer' }, false);
        let vcRequest = this.http.GetMasterDataByType({ DataType: 'VocationalCoordinators', SelectTitle: 'Vocational Coordinator' }, false);
        let vtpRequest = this.commonService.GetVTPByAYId(userModel.RoleCode, userModel.UserTypeId, userModel.AcademicYearId);

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([academicYearRequest, vtpRequest, sectorRequest, vtRequest, vcRequest]);
    }
}
