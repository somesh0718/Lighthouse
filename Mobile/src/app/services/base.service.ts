import { Injectable, Injector } from '@angular/core';
import { forkJoin, Observable, of, throwError } from 'rxjs';
import { AppConstants } from '../app.constants';
import { retry, catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse, HttpHandler } from '@angular/common/http';
import { ServiceConstants } from '../constants/service.constant';
import { Storage } from '@ionic/storage';
import { HelperService } from './helper.service';
import { ApiService } from './api.service';
import { LoadingController } from '@ionic/angular';
import { VTFieldIndustryVisitConductedService } from '../vt-field-industry-visit-conducted/vt-field-industry-visit-conducted.service';
import { VTIssueReportingService } from '../vt-issue-reporting/vt-issue-reporting.service';
import { VTDailyReportingService } from '../vt-daily-reporting/vt-daily-reporting.service';
import { VTGuestLectureConductedService } from '../vt-guest-lecture-conducted/vt-guest-lecture-conducted.service';
import { HMIssueReportingService } from '../hm-issue-reporting/hm-issue-reporting.service';
import { VCDailyReportingService } from '../vc-daily-reporting/vc-daily-reporting.service';
import { VCIssueReportingService } from '../vc-issue-reporting/vc-issue-reporting.service';
import { VCSchoolVisitService } from '../vc-school-visit/vc-school-visit.service';
import { BroadcastMessageService } from '../broadcast-message/broadcast-message.service';
import { DRPDailyReportingService } from '../drp-daily-reportings/drp-daily-reporting.service';
import { VcSchoolVisitReportingService } from '../vc-school-visit-reporting/vc-school-visit-reporting.service';
import { DatePipe } from '@angular/common';
import { ComplaintRegistrationService } from '../complaint-registration/complaint-registration.service';

@Injectable()

export class BaseService extends HttpClient {
    public Services: ServiceConstants;
    public MyConstants: AppConstants;
    private httpUrl = '';
    public options: any;
    public DateFormatPipe = new DatePipe('en-US');

    constructor(
        public http: HttpClient,
        public httpHandler: HttpHandler,
        public localStorage: Storage,
        public helperService: HelperService,
        public api: ApiService,
        public loadingController: LoadingController,
        private _injector: Injector,
    ) {
        super(httpHandler);
        this.Services = new ServiceConstants();
        this.MyConstants = new AppConstants();
    }

    private get vtFieldIndustryVisitConductedService() { return this._injector.get(VTFieldIndustryVisitConductedService); }
    private get vtIssueReportingService() { return this._injector.get(VTIssueReportingService); }
    private get vtDailyReportingService() { return this._injector.get(VTDailyReportingService); }
    private get vtGuestLectureConductedService() { return this._injector.get(VTGuestLectureConductedService); }
    private get hmIssueReportingService() { return this._injector.get(HMIssueReportingService); }
    private get vcDailyReportingService() { return this._injector.get(VCDailyReportingService); }
    private get vcIssueReportingService() { return this._injector.get(VCIssueReportingService); }
    private get vcSchoolVisitService() { return this._injector.get(VCSchoolVisitService); }
    private get broadcastMessageService() { return this._injector.get(BroadcastMessageService); }
    private get drpDailyReportingService() { return this._injector.get(DRPDailyReportingService); }
    private get vcSchoolVisitReportingService() { return this._injector.get(VcSchoolVisitReportingService); }
    private get complaintRegistrationService() { return this._injector.get(ComplaintRegistrationService); }

    GetMasterDataByType(formData: any, isSelectOption?: boolean) {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetAll;
        let selectTitle = (formData.SelectTitle == undefined ? 'Select' : 'Select ' + formData.SelectTitle);
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(function (response: any) {
                return response;
            })
        );
    }

    GetSchoolsByVCId(formData: any) {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetSchoolsByVCId;
        const selectTitle = (formData.SelectTitle === undefined ? 'Select' : 'Select ' + formData.SelectTitle);
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap((response: any) => {
                // response.Results.unshift({ Id: null, Name: selectTitle, Description: "", SequenceNo: 1 });
                return response;
            })
        );
    }

    GetClassesByVTId(formData: any) {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetClassesByVTId;
        const selectTitle = (formData.SelectTitle == undefined ? 'Select' : 'Select ' + formData.SelectTitle);
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(function (response: any) {
                // response.Results.unshift({ Id: null, Name: selectTitle, Description: "", SequenceNo: 1 });
                return response;
            })
        );
    }

    GetSectionsByVTClassId(formData: any) {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetSectionsByVTClassId;
        const selectTitle = (formData.SelectTitle == undefined ? 'Select' : 'Select ' + formData.SelectTitle);
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(function (response: any) {
                // response.Results.unshift({ Id: null, Name: selectTitle, Description: "", SequenceNo: 1 });
                return response;
            })
        );
    }

    GetUnitsByClassAndModuleId(formData: any) {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetUnitsByClassAndModuleId;
        const selectTitle = (formData.SelectTitle == undefined ? 'Select' : 'Select ' + formData.SelectTitle);
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(function (response: any) {
                // response.Results.unshift({ Id: null, Name: selectTitle, Description: "", SequenceNo: 1 });
                return response;
            })
        );
    }

    GetSessionsByUnitId(formData: any) {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetSessionsByUnitId;
        const selectTitle = (formData.SelectTitle == undefined ? 'Select' : 'Select ' + formData.SelectTitle);
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(function (response: any) {
                // response.Results.unshift({ Id: null, Name: selectTitle, Description: "", SequenceNo: 1 });
                return response;
            })
        );
    }
    getStudentsByClassId(formData: any): Observable<any> {
        this.httpUrl = this.Services.BaseUrl + this.Services.MasterData.GetStudentsByClassIdForVT;
        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };
        return this.http
            .post(this.httpUrl, formData, options)
            .pipe(
                retry(0),
                catchError(this.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getDataSync() {
        this.loadingController
            .create({
                message: 'Please wait...'
            })
            .then((loadEl) => {
                loadEl.present();

                this.localStorage.get('allowedPages').then((r) => {
                    const allPages = JSON.parse(r);
                    this.api.createGetTables().then(() => {
                        if (allPages[0].access === true && allPages[0].getSync === false) {
                            // tslint:disable-next-line: max-line-length
                            return this.vtDailyReportingService.getAndLoadVTDailyReporting(allPages[0]);

                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[0].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[1].access === true && allPages[1].getSync === false) {
                            return this.vtFieldIndustryVisitConductedService.getAndLoadVTFieldIndustryVisitConducteds(allPages[1]);

                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[1].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[2].access === true && allPages[2].getSync === false) {
                            return this.vtGuestLectureConductedService.getAndLoadVTGuestLectureConducted(allPages[2]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[2].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[3].access === true && allPages[3].getSync === false) {
                            return this.vtIssueReportingService.getAndLoadVTIssueReporting(allPages[3]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[3].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[4].access === true && allPages[4].getSync === false) {
                            return this.vcDailyReportingService.getAndLoadVCDailyReporting(allPages[4]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[4].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[5].access === true && allPages[5].getSync === false) {
                            return this.vcSchoolVisitService.getAndLoadVCSchoolVisit(allPages[5]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[5].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[6].access === true && allPages[6].getSync === false) {
                            return this.vcIssueReportingService.getAndLoadVCIssueReporting(allPages[6]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        allPages[6].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[7].access === true && allPages[7].getSync === false) {
                            return this.hmIssueReportingService.getAndLoadHMIssueReporting(allPages[7]);
                        } else {
                            return Promise.resolve(true);
                        }

                    }).then(() => {
                        allPages[7].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[8].access === true && allPages[8].getSync === false) {
                            return this.vcSchoolVisitReportingService.getAndLoadVcSchoolVisitReporting(allPages[8]);
                        } else {
                            return Promise.resolve(true);
                        }

                    }).then(() => {
                        allPages[8].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[9].access === true && allPages[9].getSync === false) {
                            return this.drpDailyReportingService.getAndLoadDRPDailyReporting(allPages[9]);
                        } else {

                            return Promise.resolve(true);
                        }

                    }).then(() => {
                        allPages[9].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        if (allPages[10].access === true && allPages[10].getSync === false) {
                            return this.complaintRegistrationService.getAndLoadComplaintRegistration(allPages[10]);
                        } else {

                            return Promise.resolve(true);
                        }

                    }).then(() => {
                        allPages[10].getSync = true;
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        return this.broadcastMessageService.getAndLoadBroadcastMessages(allPages[11]);

                    }).then(() => {
                        for (const iterator of allPages) {
                            iterator.getSync = false;
                        }
                        this.localStorage.set('allowedPages', JSON.stringify(allPages));
                        loadEl.dismiss();
                        this.helperService.presentToast('My Sync Successful.');
                    }
                        , (err) => {
                            loadEl.dismiss();
                            alert(err);
                            this.helperService.showAlert('My Sync Failed. Try Again.');
                            console.log(err);
                        });
                });
            });
    }

    masterDataSync() {
        this.loadingController
            .create({
                message: 'Please wait...'
            })
            .then((loadEl) => {
                loadEl.present();

                this.localStorage.get('currentUser').then((user) => {
                    var currentUser = JSON.parse(user);

                    this.localStorage.get('masterList').then((mL) => {
                        const masterList = JSON.parse(mL);
                        Promise.resolve(true).then(() => {
                        }).then(() => {
                            if (masterList[0].sync === false) {
                                return this.api.GetCourseModuleUnitSessions(masterList[0]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[0].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[1].sync === false) {
                                return this.api.GetClassSectionsByVTId(masterList[1]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[1].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[2].sync === false) {
                                return this.api.GetCommonMasterData(masterList[2]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[2].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[3].sync === false) {
                                return this.api.GetStudentsByVTId(masterList[3]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[3].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[4].sync === false) {
                                return this.api.GetSchoolsByVCId(masterList[4]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[4].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[5].sync === false) {
                                return this.api.GetSchoolsByDRPId(masterList[5]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[5].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[6].sync === false) {
                                return this.api.GetVTByVCId(masterList[6]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[6].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[7].sync === false) {
                                return this.api.GetDistrictsByStateId(masterList[7]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[7].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[8].sync === false) {
                                return this.api.GetMainIssueByUser(masterList[8]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[8].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[9].sync === false) {
                                return this.api.GetSubIssueByUser(masterList[9]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[9].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[10].sync === false && currentUser.RoleCode != 'HM') {
                                return this.api.GetSectorsByUser(masterList[10]);
                            } else {
                                return true;
                            }
                        }).then(() => {
                            masterList[10].sync = true;
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            if (masterList[11].sync === false && currentUser.RoleCode != 'HM') {
                                return this.api.GetJobRolesByUser(masterList[11]);
                            } else {
                                return true;
                            }
                        }).then((data) => {
                            for (const iterator of masterList) {
                                iterator.sync = false;
                            }
                            this.localStorage.set('masterList', JSON.stringify(masterList));
                            loadEl.dismiss();
                            this.helperService.presentToast('Master Data Sync Successful.');
                        }, (err) => {
                            loadEl.dismiss();
                            console.log(err);
                            alert(err);
                            this.helperService.showAlert('Master Data Sync Failed. Try Again.');
                        });
                    });
                });
            });
    }

    uploadDataSync() {
        this.loadingController
            .create({
                message: 'Please wait...'
            })
            .then((loadEl) => {
                loadEl.present();
                this.localStorage.get('allowedPages').then((r) => {
                    const allPages = JSON.parse(r);
                    this.api.createUploadTables().then(() => {
                        if (allPages[0].access === true) {
                            // tslint:disable-next-line: max-line-length
                            return this.vtDailyReportingService.createOrUpdateVTDailyReportingBulk(allPages[0]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[1].access === true) {
                            return this.vtFieldIndustryVisitConductedService.createOrUpdateVTFieldIndustryVisitConductedBulk(allPages[1]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[2].access === true) {
                            return this.vtGuestLectureConductedService.createOrUpdateVTGuestLectureConductedBulk(allPages[2]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[3].access === true) {
                            return this.vtIssueReportingService.createOrUpdateVTIssueReportingBulk(allPages[3]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[4].access === true) {
                            return this.vcDailyReportingService.createOrUpdateVCDailyReportingBulk(allPages[4]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[5].access === true) {
                            return this.vcSchoolVisitService.createOrUpdateVCSchoolVisitBulk(allPages[5]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[6].access === true) {
                            return this.vcIssueReportingService.createOrUpdateVCIssueReportingBulk(allPages[6]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[7].access === true) {
                            return this.hmIssueReportingService.createOrUpdateHMIssueReportingBulk(allPages[7]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[9].access === true) {
                            return this.drpDailyReportingService.createOrUpdateDRPDailyReportingBulk(allPages[9]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        if (allPages[8].access === true) {
                            return this.vcSchoolVisitReportingService.createOrUpdateVcSchoolVisitReportingBulk(allPages[8]);
                        } else {
                            return Promise.resolve(true);
                        }
                    }).then(() => {
                        this.api.dropUploadTables().then(() => {
                            this.api.createUploadTables().then(() => {
                                loadEl.dismiss();
                                this.helperService.presentToast('Upload Data Sync Successful.');
                            }, (err) => {
                                alert('Error Creating Upload Tables. Please use Upload Sync first to try again.');
                            });
                        });

                    }
                        , (err) => {
                            loadEl.dismiss();
                            alert(err);
                            this.helperService.showAlert('Upload Data Sync Failed. Try Again.');
                            console.log(err);
                        });
                });
            });
    }

    HttpGet<T>(srvUrl) {
        // let queryParams = srvUrl.indexOf("?") == -1 ? "?" : "&";
        // queryParams += "UserId=" + AppConstants.UserId;

        this.httpUrl = this.Services.BaseUrl + srvUrl;

        const options = {
            headers: {
                Authorization: 'Bearer ' + AppConstants.AuthToken
            }
        };

        return this.http.get(this.httpUrl, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap((response: any) => {
                return response;
            })
        );
    }

    HttpPost(srvUrl, formData: any, options?: any) {
        // formData.UserId = AppConstants.UserId;
        // formData.AuthToken = AppConstants.AuthToken;

        this.httpUrl = this.Services.BaseUrl + srvUrl;
        options = {
            headers: {
                'Authorization': 'Bearer ' + AppConstants.AuthToken
                //'Access-Control-Allow-Origin': 'Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers'
            }
        };
        return this.http.post(this.httpUrl, formData, options).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(response => {
                return response;
            })
        );

    }

    HttpDelete(srvUrl, postBody: any) {
        this.httpUrl = this.Services.BaseUrl + srvUrl;

        return this.http.delete(this.httpUrl).pipe(
            retry(0),
            catchError(this.HandleError),
            tap(response => { })
        );
    }

    HttpUpload(srvUrl: string, file: File) {
        const formData: FormData = new FormData();
        if (file) {
            formData.append('files', file, file.name);
        }

        this.httpUrl = this.Services.BaseUrl + srvUrl;

        return this.post(this.httpUrl, formData);
    }

    FormUrlParam(url, data) {
        let queryString = '';
        for (const key in data) {
            if (data.hasOwnProperty(key)) {
                if (!queryString) {
                    queryString = `?${key}=${data[key]}`;
                } else {
                    queryString += `&${key}=${data[key]}`;
                }
            }
        }
        return url + queryString;
    }

    HandleError(error: HttpErrorResponse) {
        let errorMessage = 'Unknown error!';
        if (error.error instanceof ErrorEvent) {
            // Client-side errors
            errorMessage = `Error: ${error.error.message}`;
        } else if (error.status != undefined && error.message != undefined) {
            // Server-side errors
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        else if (error.error instanceof String) {
            errorMessage = error.toString();
        }

        if (error.status != undefined) {
            alert(errorMessage);
        }
        return throwError(errorMessage);
    }

    RefreshToken(response: Response) {
        const accessToken = response.headers.get(AppConstants.AccessTokenServer);
        if (accessToken) {
            this.localStorage.set(AppConstants.AccessTokenLocalStorage, `${accessToken}`);
        }
    }

    getDateTimeFromControl(controlValue): string {
        let dateTimeValue = this.DateFormatPipe.transform(controlValue, this.MyConstants.ServerDateFormat);

        return dateTimeValue;
    }
}
