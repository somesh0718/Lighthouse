import { Injectable } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { AppConstants } from '../app.constants';
import { FormGroup } from '@angular/forms';
import { VCDailyReportingModel } from './vc-daily-reporting.model';
import { VCIndustryExposureVisitModel } from './vc-industry-exposure-visit.model';
import { VCHolidayModel } from './vc-holiday.model';
import { VCLeaveModel } from './vc-leave.model';
import { Storage } from '@ionic/storage';
import { HttpClient } from '@angular/common/http';
import { VCPraticalModel } from './vc-pratical.model';
import { UserModel } from '../models/user.model';

@Injectable()
export class VCDailyReportingService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private helperService: HelperService,
        private localStorage: Storage,
        public httpCli: HttpClient,
    ) { }

    getVCDailyReportings(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VCDailyReporting.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getVCDailyReportingById(vcDailyReportingId: string) {
        return this.http
            .HttpGet(this.http.Services.VCDailyReporting.GetById + '?vcDailyReportingId=' + vcDailyReportingId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateVCDailyReporting(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VCDailyReporting.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    createOrUpdateVCDailyReportingBulk(pageData) {
        return new Promise((resolve, reject) => {
            this.api.selectTableData(pageData.uploadTable).then((list: any) => {
                if (list.length > 0) {
                    const obsList = [];
                    const options = {
                        headers: {
                            Authorization: 'Bearer ' + AppConstants.AuthToken
                        }
                    };
                    for (const iterator of list) {
                        if (typeof iterator.WorkingDayTypeIds === 'string' && iterator.WorkingDayTypeIds !== null) { iterator.WorkingDayTypeIds = JSON.parse(iterator.WorkingDayTypeIds); }
                        if (typeof iterator.Leave === 'string' && iterator.Leave !== null) { iterator.Leave = JSON.parse(iterator.Leave); }
                        if (typeof iterator.Holiday === 'string' && iterator.Holiday !== null) { iterator.Holiday = JSON.parse(iterator.Holiday); }
                        if (typeof iterator.IndustryExposureVisit === 'string' && iterator.IndustryExposureVisit !== null) { iterator.IndustryExposureVisit = JSON.parse(iterator.IndustryExposureVisit); }
                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.VCDailyReporting.CreateOrUpdate,
                            iterator, options).pipe(
                                catchError(err => of({ isError: true, error: err }))
                            ));
                    }

                    forkJoin(obsList).subscribe((res: any) => {
                        const failedList: any = res.filter((x: any) => x.Success === false);
                        const erroredList: any = res.filter((x: any) => x.isError === true);
                        if (erroredList.length > 0 || failedList.length > 0) {
                            let errorMessage = '';
                            for (let index = 0; index < res.length; index++) {
                                if (res[index].hasOwnProperty('isError')) {
                                    errorMessage = errorMessage + pageData.title + ': ' + res[index].error.message + '<br><br>';
                                    if (res[index].error.status >= 400) {
                                        this.api.deleteSpecificData(pageData.uploadTable, list[index]);
                                    }
                                } else {
                                    if (res[index].Success === false) {
                                        if (res[index].Errors.length > 0)
                                            errorMessage = errorMessage + res[index].Errors.toString() + '<br><br>';

                                        if (res[index].Messages.length > 0)
                                            errorMessage = errorMessage + res[index].Messages.toString() + '<br><br>';
                                    }
                                    this.api.deleteSpecificData(pageData.uploadTable, list[index]);
                                }

                            }

                            if (errorMessage != '')
                                this.http.helperService.showAlert(errorMessage);

                            resolve(true);
                        } else {
                            this.api.deleteTableData(pageData.uploadTable);
                            resolve(true);
                        }

                    }, (err) => {
                        console.log(err);
                        reject(err);
                    });
                } else {
                    resolve(true);
                }
            }, (err) => {
                reject(err);
            });
        });
    }


    deleteVCDailyReportingById(vcDailyReportingId: string) {
        const vcDailyReportingParams = {
            VCDailyReportingId: vcDailyReportingId
        };

        return this.http
            .HttpPost(this.http.Services.VCDailyReporting.Delete, vcDailyReportingParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownForVCDailyReporting(): Observable<any[]> {
        const reportTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'VCReportType', SelectTitle: 'Report Type' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([reportTypeRequest]);
    }

    getDropdownForVCPracticalAssessment(academicYearId: string, schoolId: string, vtId: string): Observable<any[]> {
        let classRequest = this.http.GetMasterDataByType({ DataType: 'ClassesForPA', RoleId: academicYearId, UserId: schoolId, ParentId: vtId, SelectTitle: 'Class' });
        let sectorRequest = this.http.GetMasterDataByType({ DataType: 'SectorsByVT', UserId: vtId, SelectTitle: 'Sector' });
        let jobRoleRequest = this.http.GetMasterDataByType({ DataType: 'JobRolesByVT', ParentId: vtId, SelectTitle: 'Job Role' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([classRequest, sectorRequest, jobRoleRequest]);
    }

    getVCDailyReportingModelFromFormGroup(formGroup: FormGroup, userModel: UserModel): VCDailyReportingModel {
        const dailyReportingModel = new VCDailyReportingModel();

        dailyReportingModel.ReportType = formGroup.get('ReportType').value;
        dailyReportingModel.ReportDate = formGroup.get('ReportDate').value;
        dailyReportingModel.WorkingDayTypeIds = formGroup.get('WorkingDayTypeIds').value;
        dailyReportingModel.WorkTypeDetails = formGroup.get('WorkTypeDetails').value;
        dailyReportingModel.SchoolId = formGroup.get('SchoolId').value;

        // Industry Exposure Visit
        if (formGroup.controls.industryExposureVisitGroup != null) {
            dailyReportingModel.IndustryExposureVisit = new VCIndustryExposureVisitModel();
            dailyReportingModel.IndustryExposureVisit.TypeOfIndustryLinkage = formGroup.controls.industryExposureVisitGroup.get('TypeOfIndustryLinkage').value;
            dailyReportingModel.IndustryExposureVisit.ContactPersonName = formGroup.controls.industryExposureVisitGroup.get('ContactPersonName').value;
            dailyReportingModel.IndustryExposureVisit.ContactPersonMobile = formGroup.controls.industryExposureVisitGroup.get('ContactPersonMobile').value;
            dailyReportingModel.IndustryExposureVisit.ContactPersonEmail = formGroup.controls.industryExposureVisitGroup.get('ContactPersonEmail').value;
        }

        // Pratical
        if (formGroup.controls.praticalGroup != null) {
            dailyReportingModel.Pratical = new VCPraticalModel();

            dailyReportingModel.Pratical.AcademicYearId = userModel.AcademicYearId;
            dailyReportingModel.Pratical.VCId = userModel.UserTypeId;
            dailyReportingModel.Pratical.IsPratical = formGroup.controls.praticalGroup.get('IsPratical').value;
            dailyReportingModel.Pratical.SchoolId = formGroup.controls.praticalGroup.get('SchoolId').value;
            dailyReportingModel.Pratical.VTId = formGroup.controls.praticalGroup.get('VTId').value;
            dailyReportingModel.Pratical.SectorId = formGroup.controls.praticalGroup.get('SectorId').value;
            dailyReportingModel.Pratical.JobRoleId = formGroup.controls.praticalGroup.get('JobRoleId').value;
            dailyReportingModel.Pratical.ClassId = formGroup.controls.praticalGroup.get('ClassId').value;
            dailyReportingModel.Pratical.StudentCount = formGroup.controls.praticalGroup.get('StudentCount').value;
            dailyReportingModel.Pratical.VTPresent = formGroup.controls.praticalGroup.get('VTPresent').value;
            dailyReportingModel.Pratical.PresentStudentCount = formGroup.controls.praticalGroup.get('PresentStudentCount').value;
            dailyReportingModel.Pratical.AssesorName = formGroup.controls.praticalGroup.get('AssesorName').value;
            dailyReportingModel.Pratical.AssesorMobileNo = formGroup.controls.praticalGroup.get('AssesorMobileNo').value;
            dailyReportingModel.Pratical.Remarks = formGroup.controls.praticalGroup.get('Remarks').value;
        }

        // Holiday Type
        if (formGroup.controls.holidayGroup != null) {
            dailyReportingModel.Holiday = new VCHolidayModel();
            dailyReportingModel.Holiday.HolidayTypeId = formGroup.controls.holidayGroup.get('HolidayTypeId').value;
            dailyReportingModel.Holiday.HolidayDetails = formGroup.controls.holidayGroup.get('HolidayDetails').value;
        }

        // Leave
        if (formGroup.controls.leaveGroup != null) {
            dailyReportingModel.Leave = new VCLeaveModel();
            dailyReportingModel.Leave.LeaveTypeId = formGroup.controls.leaveGroup.get('LeaveTypeId').value;
            dailyReportingModel.Leave.LeaveApprovalStatus = formGroup.controls.leaveGroup.get('LeaveApprovalStatus').value;
            dailyReportingModel.Leave.LeaveApprover = formGroup.controls.leaveGroup.get('LeaveApprover').value;
            dailyReportingModel.Leave.LeaveReason = formGroup.controls.leaveGroup.get('LeaveReason').value;
        }

        return dailyReportingModel;
    }


    getAndLoadVCDailyReporting(obj) {
        // don't have the data yet
        return new Promise((resolve, reject) => {
            // We're using Angular HTTP provider to request the data,
            // then on the response, it'll map the JSON data to a parsed JS object.
            // Next, we process the data and resolve the promise with the new data.
            this.localStorage.get('currentUser').then((cU) => {
                const user = JSON.parse(cU);
                const options = {
                    headers: {
                        Authorization: 'Bearer ' + user.AuthToken
                    }
                };
                const formData = {
                    UserId: user.UserId,
                    UserTypeId: user.UserTypeId,
                    Name: null,
                    CharBy: null,
                    Filter: {},
                    SortOrder: 'asc',
                    PageIndex: 1,
                    PageSize: 10000
                };
                this.httpCli
                    .post(this.http.Services.BaseUrl + this.http.Services.VCDailyReporting.GetAllByCriteria, formData, options)
                    .subscribe(
                        (data: any) => {
                            if (data.Success) {
                                this.api.dropTableByName(obj.getTable).then(
                                    () => {
                                        this.api.createTableByQuery(obj.getTableCreateQuery).then(
                                            () => {
                                                // tslint:disable-next-line: no-string-literal
                                                this.api.insertGetTable(obj.getTable, data['Results']).then(
                                                    () => {
                                                        resolve(true);
                                                    },
                                                    (err) => {
                                                        reject(err);
                                                    }
                                                );
                                            },
                                            (err) => {
                                                reject(err);
                                            }
                                        );
                                    },
                                    (err) => {
                                        reject(err);
                                    }
                                );
                            } else {
                                reject(data.Errors[0]);
                            }
                        },
                        (err) => {
                            console.log(err);
                            reject(err);
                        }
                    );
            });
        });
    }
}
