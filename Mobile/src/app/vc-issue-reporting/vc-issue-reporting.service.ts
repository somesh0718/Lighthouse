import { Injectable } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { AppConstants } from '../app.constants';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { Storage } from '@ionic/storage';
import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { VCIssueReportingModel } from './vc-issue-reporting.model';

@Injectable()
export class VCIssueReportingService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private helperService: HelperService,
        private localStorage: Storage,
        public httpCli: HttpClient,
    ) { }

    getVCIssueReportings(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VCIssueReporting.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    getVCIssueReportingById(vcIssueReportingId: string) {
        return this.http
            .HttpGet(this.http.Services.VCIssueReporting.GetById + '?vcIssueReportingId=' + vcIssueReportingId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateVCIssueReporting(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VCIssueReporting.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    createOrUpdateVCIssueReportingBulk(pageData) {
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
                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.VCIssueReporting.CreateOrUpdate,
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

    deleteVCIssueReportingById(vcIssueReportingId: string) {
        const vcIssueReportingParams = {
            VCIssueReportingId: vcIssueReportingId
        };

        return this.http
            .HttpPost(this.http.Services.VCIssueReporting.Delete, vcIssueReportingParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getAndLoadVCIssueReporting(obj) {
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
                    .post(this.http.Services.BaseUrl + this.http.Services.VCIssueReporting.GetAllByCriteria, formData, options)
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

    getVCIssueReportingFromFormGroup(formGroup: FormGroup): VCIssueReportingModel {
        const issueReportingModel = new VCIssueReportingModel();

        issueReportingModel.IssueReportDate = formGroup.get('IssueReportDate').value;
        issueReportingModel.MainIssue = formGroup.get('MainIssue').value;
        issueReportingModel.SubIssue = formGroup.get('SubIssue').value;
        issueReportingModel.StudentClass = formGroup.get('StudentClass').value.join(',');
        issueReportingModel.Month = formGroup.get('Month').value.join(',');
        issueReportingModel.StudentType = formGroup.get('StudentType').value;
        issueReportingModel.NoOfStudents = formGroup.get('NoOfStudents').value;
        issueReportingModel.IssueDetails = formGroup.get('IssueDetails').value;
        issueReportingModel.IssueStatus = formGroup.get('IssueStatus').value;

        return issueReportingModel;
    }
}
