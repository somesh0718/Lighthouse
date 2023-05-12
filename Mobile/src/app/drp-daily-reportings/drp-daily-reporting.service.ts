import { Injectable } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { AppConstants } from '../app.constants';
import { FormGroup } from '@angular/forms';
import { DRPDailyReportingModel } from './drp-daily-reporting.model';
import { DRPIndustryExposureVisitModel } from './drp-industry-exposure-visit.model';
import { DRPHolidayModel } from './drp-holiday.model';
import { DRPLeaveModel } from './drp-leave.model';
import { Storage } from '@ionic/storage';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DRPDailyReportingService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private helperService: HelperService,
        private localStorage: Storage,
        public httpCli: HttpClient,
    ) { }

    getDRPDailyReportings(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.DRPDailyReporting.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getDRPDailyReportingById(drpDailyReportingId: string) {
        return this.http
            .HttpGet(this.http.Services.DRPDailyReporting.GetById + '?drpDailyReportingId=' + drpDailyReportingId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateDRPDailyReporting(formData: any) {
        return this.http
            .HttpPost(this.http.Services.DRPDailyReporting.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    createOrUpdateDRPDailyReportingBulk(pageData) {
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
                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.DRPDailyReporting.CreateOrUpdate,
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


    deleteDRPDailyReportingById(drpDailyReportingId: string) {
        const drpDailyReportingParams = {
            DRPDailyReportingId: drpDailyReportingId
        };

        return this.http
            .HttpPost(this.http.Services.DRPDailyReporting.Delete, drpDailyReportingParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownForDRPDailyReporting(): Observable<any[]> {
        const reportTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'DRPReportType', SelectTitle: 'Report Type' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([reportTypeRequest]);
    }

    getDRPDailyReportingModelFromFormGroup(formGroup: FormGroup): DRPDailyReportingModel {
        const dailyReportingModel = new DRPDailyReportingModel();

        dailyReportingModel.ReportType = formGroup.get('ReportType').value;
        dailyReportingModel.ReportDate = formGroup.get('ReportDate').value;
        dailyReportingModel.WorkingDayTypeIds = formGroup.get('WorkingDayTypeIds').value;
        dailyReportingModel.WorkTypeDetails = formGroup.get('WorkTypeDetails').value;
        dailyReportingModel.SchoolId = formGroup.get('SchoolId').value;

        // Industry Exposure Visit
        if (formGroup.controls.industryExposureVisitGroup != null) {
            dailyReportingModel.IndustryExposureVisit = new DRPIndustryExposureVisitModel();
            dailyReportingModel.IndustryExposureVisit.TypeOfIndustryLinkage = formGroup.controls.industryExposureVisitGroup.get('TypeOfIndustryLinkage').value;
            dailyReportingModel.IndustryExposureVisit.ContactPersonName = formGroup.controls.industryExposureVisitGroup.get('ContactPersonName').value;
            dailyReportingModel.IndustryExposureVisit.ContactPersonMobile = formGroup.controls.industryExposureVisitGroup.get('ContactPersonMobile').value;
            dailyReportingModel.IndustryExposureVisit.ContactPersonEmail = formGroup.controls.industryExposureVisitGroup.get('ContactPersonEmail').value;
        }

        // Holiday Type
        if (formGroup.controls.holidayGroup != null) {
            dailyReportingModel.Holiday = new DRPHolidayModel();
            dailyReportingModel.Holiday.HolidayTypeId = formGroup.controls.holidayGroup.get('HolidayTypeId').value;
            dailyReportingModel.Holiday.HolidayDetails = formGroup.controls.holidayGroup.get('HolidayDetails').value;
        }

        // Leave
        if (formGroup.controls.leaveGroup != null) {
            dailyReportingModel.Leave = new DRPLeaveModel();
            dailyReportingModel.Leave.LeaveTypeId = formGroup.controls.leaveGroup.get('LeaveTypeId').value;
            dailyReportingModel.Leave.LeaveApprovalStatus = formGroup.controls.leaveGroup.get('LeaveApprovalStatus').value;
            dailyReportingModel.Leave.LeaveApprover = formGroup.controls.leaveGroup.get('LeaveApprover').value;
            dailyReportingModel.Leave.LeaveReason = formGroup.controls.leaveGroup.get('LeaveReason').value;
        }

        return dailyReportingModel;
    }


    getAndLoadDRPDailyReporting(obj) {
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
                    .post(this.http.Services.BaseUrl + this.http.Services.DRPDailyReporting.GetAllByCriteria, formData, options)
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
