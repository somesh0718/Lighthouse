import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, forkJoin, observable, Observable, throwError } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { UserModel } from '../models/user.model';
import { Storage } from '@ionic/storage';
import { ServiceConstants } from '../constants/service.constant';
import { environment } from '../../environments/environment';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { LoadingController, Platform } from '@ionic/angular';
import { HelperService } from './helper.service';
import { HMIssueReportingService } from '../hm-issue-reporting/hm-issue-reporting.service';
import { VCDailyReportingService } from '../vc-daily-reporting/vc-daily-reporting.service';
import { VCIssueReportingService } from '../vc-issue-reporting/vc-issue-reporting.service';
import { VCSchoolVisitService } from '../vc-school-visit/vc-school-visit.service';
import { VTDailyReportingService } from '../vt-daily-reporting/vt-daily-reporting.service';
import { VTFieldIndustryVisitConductedService } from '../vt-field-industry-visit-conducted/vt-field-industry-visit-conducted.service';
import { VTGuestLectureConductedService } from '../vt-guest-lecture-conducted/vt-guest-lecture-conducted.service';
import { VTIssueReportingService } from '../vt-issue-reporting/vt-issue-reporting.service';
import { AppConstants } from '../app.constants';


@Injectable({
  providedIn: 'root'
})


// tslint:disable: prefer-const
// tslint:disable: variable-name
// tslint:disable: no-string-literal

export class ApiService {
  private userSubject: BehaviorSubject<UserModel>;
  public Services: ServiceConstants;


  DatabaseName = 'LightHouse.db';
  DB: SQLiteObject = null;


  constructor(
    private http: HttpClient,
    private localStorage: Storage,
    public sqlite: SQLite,
    public platform: Platform,
    private helperService: HelperService,
    public loadingController: LoadingController,
    private _injector: Injector

  ) {
    this.localStorage.get('currentUser').then((currentUserJson) => {
      this.userSubject = new BehaviorSubject<UserModel>(JSON.parse(currentUserJson));
    });
    this.createOrOpenDB();
    this.Services = new ServiceConstants();
  }
  // private get vtFieldIndustryVisitConductedService() { return this._injector.get(VTFieldIndustryVisitConductedService); }
  createOrOpenDB() {
    this.platform.ready().then(() => {
      try {
        this.sqlite
          .create({
            name: this.DatabaseName,
            location: 'default'
          })
          .then((db: SQLiteObject) => {
            this.DB = db;
          }).catch(dbEx => {
            console.log(dbEx);
          });
      } catch (dbCr) {
        console.log(dbCr);
        return;
      }
    });
  }

  getDbData() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    this.DB.executeSql('select * from UploadVTDailyReporting', []).then((data) => {
      console.log('UploadVTDailyReporting: ', data);
    });
  }


  GetSchoolsByVCId(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataId: user.LoginId, DataId1: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetSchoolsByVCId, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableSchoolsByVCId(obj.masterTable, data['Results']).then(
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

  GetStudentsByVTId(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataId: user.LoginId, DataId1: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetStudentsByVTId, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableStudentsByVTId(obj.masterTable, data['Results']).then(
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


  GetCommonMasterData(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataId: user.LoginId, DataId1: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetCommonMasterData, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableCommonMasterData(obj.masterTable, data['Results']).then(
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

  GetSchoolsByDRPId(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataId: user.LoginId, DataId1: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetSchoolsByDRPId, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableSchoolsByVCId(obj.masterTable, data['Results']).then(
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


  GetClassSectionsByVTId(obj) {
    // don't have the data yet
    return new Promise((resolve, reject) => {
      // We're using Angular HTTP provider to request the data,
      // then on the response, it'll map the JSON data to a parsed JS object.
      // Next, we process the data and resolve the promise with the new data.
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataId: user.LoginId, DataId1: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetClassSectionsByVTId, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableClassSectionsByVTId(obj.masterTable, data['Results']).then(
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



  GetCourseModuleUnitSessions(obj) {
    // don't have the data yet
    return new Promise((resolve, reject) => {
      // We're using Angular HTTP provider to request the data,
      // then on the response, it'll map the JSON data to a parsed JS object.
      // Next, we process the data and resolve the promise with the new data.
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataId: user.LoginId, DataId1: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetCourseModuleUnitSessions, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableCourseModuleUnitSessions(obj.masterTable, data['Results']).then(
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

  GetVTByVCId(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataType: 'VocationalTrainersByVC', ParentId: user.UserTypeId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetAll, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableVTByVCId(obj.masterTable, data['Results']).then(
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

  GetDistrictsByStateId(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataType: 'DistrictForBlock', UserId: user.DefaultStateId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetAll, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableDistrictsByStateId(obj.masterTable, data['Results']).then(
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

  GetMainIssueByUser(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataType: 'MainIssue', UserId: user.RoleCode };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetAll, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableDistrictsByStateId(obj.masterTable, data['Results']).then(
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

  GetSubIssueByUser(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataType: 'SubIssueForMobile', UserId: user.RoleCode };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetAll, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableDistrictsByStateId(obj.masterTable, data['Results']).then(
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

  GetSectorsByUser(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataType: 'SectorsForVCReporting', RoleId: user.RoleCode, UserId: user.UserTypeId, ParentId: user.AcademicYearId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetAll, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableDistrictsByStateId(obj.masterTable, data['Results']).then(
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

  GetJobRolesByUser(obj) {
    return new Promise((resolve, reject) => {
      this.localStorage.get('currentUser').then((cU) => {
        let user = JSON.parse(cU);
        const options = {
          headers: {
            Authorization: 'Bearer ' + user.AuthToken
          }
        };
        let formData = { DataType: 'JobRolesByUserId', UserId: user.LoginId };
        this.http
          .post(this.Services.BaseUrl + this.Services.MasterData.GetAll, formData, options)
          .subscribe(
            (data: any) => {
              if (data.Success) {
                this.dropTableByName(obj.masterTable).then(
                  () => {
                    this.createTableByQuery(obj.masterTableCreateQuery).then(
                      () => {
                        this.insertMasterTableDistrictsByStateId(obj.masterTable, data['Results']).then(
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

  loadSchoolsByVCId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from SchoolsByVCId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadDistrictByVCId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from DistrictByVCId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadSchoolsByDRPId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from SchoolsByDRPId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadStudentsByVTId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from StudentsByVTId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }


  loadCommonMasterData() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('SELECT * FROM CommonMasterData ORDER BY DisplayOrder', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }


  loadCourseModuleUnitSessions() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from CourseModuleUnitSessions', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadClassSectionsByVTId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from ClassSectionsByVTId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadVTByVCId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from VTByVCId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadDistrictsByStateId() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from DistrictByStateId', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });

  }

  loadMainIssueByUser() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from MainIssueByUser', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });
  }

  loadSubIssueByUser() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('select * from SubIssueByUser', []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });
  }

  loadDropdownMasterData(tableName) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      const list = [];
      if (this.DB != null) {
        this.DB.executeSql('SELECT * FROM ' + tableName, []).then(
          (data) => {
            for (let i = 0; i < data.rows.length; i++) {
              list[i] = data.rows.item(i);
            }
            resolve(list);
          },
          (error) => {
            reject(error);
          }
        );
      } else {
        reject();
      }
    });
  }

  selectTableData(tableName) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      let selectQuery = 'select * from ' + tableName;
      let list = [];
      this.DB.executeSql(selectQuery, []).then((data) => {
        for (let i = 0; i < data.rows.length; i++) {
          list[i] = data.rows.item(i);
        }
        resolve(list);
      }, (err) => {
        reject(err);
      });
    });
  }

  deleteTableData(tableName) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      let deleteQuery = 'delete from ' + tableName;
      this.DB.executeSql(deleteQuery, []).then((data) => {
        resolve(data);
      }, (err) => {
        reject(err);
      });
    });
  }

  deleteSpecificData(tableName, record) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      const deleteQuery = 'delete from ' + tableName + ' where Id = ?';
      this.DB.executeSql(deleteQuery, [record['Id']]).then((data) => {
        resolve(data);
      }, (err) => {
        reject(err);
      });
    });
  }



  dropGetTables() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      this.localStorage.get('allowedPages').then((r) => {
        let temp = JSON.parse(r);
        let uploadList = temp.filter((x) => x.access === true);
        const promiseList = [];
        for (const iterator of uploadList) {
          const dropQuery = 'drop table if exists ' + iterator.getTable;
          promiseList.push(this.DB.executeSql(dropQuery, []));
        }
        Promise.all(promiseList).then(
          () => {
            resolve(true);
          },
          (err) => {
            console.log(err);
            reject('Error deleting table. ' + err.message);
          }
        );
      });
    });
  }


  dropUploadTables() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      this.localStorage.get('allowedPages').then((r) => {
        let temp = JSON.parse(r);
        let uploadList = temp.filter((x) => x.access === true);
        const promiseList = [];
        for (const iterator of uploadList) {
          const dropQuery = 'drop table if exists ' + iterator.uploadTable;
          promiseList.push(this.DB.executeSql(dropQuery, []));
        }
        Promise.all(promiseList).then(
          () => {
            resolve(true);
          },
          (err) => {
            console.log(err);
            reject('Error deleting table. ' + err.message);
          }
        );
      });
    });
  }



  createGetTables() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      this.localStorage.get('allowedPages').then((r) => {
        let temp = JSON.parse(r);
        let getList = temp.filter((x) => x.access === true);
        const promiseList = [];
        for (const iterator of getList) {
          promiseList.push(this.DB.executeSql(iterator.getTableCreateQuery, []));
        }
        Promise.all(promiseList).then(
          () => {
            resolve(true);
          },
          (err) => {
            console.log(err);
            reject('Error creating table. ' + err.message);
          }
        );
      });
    });
  }

  createUploadTables() {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      this.localStorage.get('allowedPages').then((r) => {
        let temp = JSON.parse(r);
        let uploadList = temp.filter((x) => x.access === true);
        const promiseList = [];
        for (const iterator of uploadList) {
          promiseList.push(this.DB.executeSql(iterator.uploadTableCreateQuery, []));
        }
        Promise.all(promiseList).then(
          () => {
            resolve(true);
          },
          (err) => {
            console.log(err);
            reject('Error creating table. ' + err.message);
          }
        );
      });
    });
  }






  createTableByQuery(createQuery) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      this.DB.executeSql(createQuery, []).then(
        () => {
          resolve(true);
        },
        (err) => {
          console.log(err);
          reject('Error creating ' + createQuery + ' table.');
        }
      );
    });
  }



  insertGetTable(tableName, dataResults) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let insertList = [];
      for (const data of dataResults) {

        let dataList = [];
        let query1 = 'INSERT INTO ' + tableName + ' (';
        let query2 = ' VALUES (';
        for (const key in data) {
          if (data.hasOwnProperty(key)) {
            query1 = query1 + key + ',';
            query2 = query2 + '?,';
            dataList.push(data[key]);
          }
        }
        query1 = query1.replace(/.$/, ')');
        query2 = query2.replace(/.$/, ')');

        let insertQuery = query1 + query2;
        insertList.push([insertQuery, dataList]);
      }

      this.DB.sqlBatch(insertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
      // this.DB.executeSql(insertQuery, dataList).then(() => {
      //   resolve(true);
      // }, (err) => {
      //   reject(err);
      // });
    });
  }


  insertUploadTable(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let dataList = [];
      let query1 = 'INSERT INTO ' + tableName + ' (';
      let query2 = ' VALUES (';
      for (const key in data) {
        if (data.hasOwnProperty(key)) {
          query1 = query1 + key + ',';
          query2 = query2 + '?,';
          dataList.push(data[key]);
        }
      }
      query1 = query1.replace(/.$/, ')');
      query2 = query2.replace(/.$/, ')');

      let insertQuery = query1 + query2;
      this.DB.executeSql(insertQuery, dataList).then(() => {
        resolve(true);
      }, (err) => {
        reject(err);
      });
    });
  }




  insertMasterTableSchoolsByVCId(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.Id + '",' +
          '"' + element.Name + '",' +
          '"' + element.Description + '",' +
          '"' + element.IsSelected + '",' +
          '"' + element.IsDisabled + '",' +
          '' + element.SequenceNo + ')'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }



  insertMasterTableStudentsByVTId(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.VTId + '",' +
          '"' + element.ClassId + '",' +
          '"' + element.SectionId + '",' +
          '"' + element.StudentId + '",' +
          '"' + element.StudentName + '",' +
          '' + element.IsPresent + ')'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }


  insertMasterTableCommonMasterData(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.DataValueId + '",' +
          '"' + element.DataTypeId + '",' +
          '"' + element.ParentId + '",' +
          '"' + element.Code + '",' +
          '"' + element.Name + '",' +
          '"' + element.Description + '",' +
          '' + element.DisplayOrder + ')'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }


  insertMasterTableClassSectionsByVTId(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.VTId + '",' +
          '"' + element.ClassId + '",' +
          '"' + element.ClassName + '",' +
          '"' + element.SectionId + '",' +
          '"' + element.SectionName + '")'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }




  insertMasterTableCourseModuleUnitSessions(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.UnitId + '",' +
          '"' + element.ClassId + '",' +
          '"' + element.ClassName + '",' +
          '"' + element.ModuleTypeId + '",' +
          '"' + element.ModuleName + '",' +
          '"' + element.SectorId + '",' +
          '"' + element.SectorName + '",' +
          '"' + element.JobRoleId + '",' +
          '"' + element.JobRoleName + '",' +
          '"' + element.UnitName + '",' +
          '"' + element.SessionId + '",' +
          '"' + element.SessionName + '")'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }

  insertMasterTableVTByVCId(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.Id + '",' +
          '"' + element.Name + '",' +
          '"' + element.Description + '",' +
          '"' + element.IsSelected + '",' +
          '"' + element.IsDisabled + '",' +
          '' + element.SequenceNo + ')'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }

  insertMasterTableDistrictsByStateId(tableName, data) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      let masterInsertList = [];
      data.forEach((element) => {
        masterInsertList.push([
          'INSERT INTO ' + tableName + ' VALUES (Null,' +
          '"' + element.Id + '",' +
          '"' + element.Name + '",' +
          '"' + element.Description + '",' +
          '"' + element.IsSelected + '",' +
          '"' + element.IsDisabled + '",' +
          '' + element.SequenceNo + ')'
          , []
        ]);
      });
      this.DB.sqlBatch(masterInsertList).then(
        () => {
          resolve(true);
        },
        (err) => {
          reject(err);
        }
      );
    });
  }


  dropTableByName(tableName) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }
    return new Promise((resolve, reject) => {
      this.DB.executeSql('drop table if exists ' + tableName, []).then(
        (d) => {
          resolve(true);
        },
        (e) => {
          reject('Error dropping ' + tableName + ' table.');
        }
      );
    });
  }


  storeMasterData(response, masterList) {
    return new Promise((resolve, reject) => {
      this.dropMasterTables(masterList).then(() => {
        this.createMasterTables(masterList).then(() => {
          this.insertMasterTables(response, masterList).then(() => {
            this.helperService.presentToast('Master Data Sync Successful.');
            resolve(true);
          }, (err) => {
            this.helperService.showAlert(err);
            reject(err);
          });
        }, (err) => {
          reject(err);
          this.helperService.showAlert(err);
        });
      }, (err) => {
        reject(err);
        this.helperService.showAlert(err);
      });
    });
  }


  dropMasterTables(masterList) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      const promiseList = [];
      for (const iterator of masterList) {
        const dropQuery = 'drop table if exists ' + iterator.name;
        promiseList.push(this.DB.executeSql(dropQuery, []));
      }
      Promise.all(promiseList).then(
        () => {
          resolve(true);
        },
        (err) => {
          console.log(err);
          reject('Error dropping table. ' + err.message);
        }
      );
    });
  }

  createMasterTables(masterList) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      const promiseList = [];
      for (const iterator of masterList) {
        const createQuery = 'create TABLE IF NOT EXISTS ' + iterator.name + ' (Id TEXT, Name TEXT, Description TEXT, SequenceNo INTEGER)';
        promiseList.push(this.DB.executeSql(createQuery, []));
      }
      Promise.all(promiseList).then(
        () => {
          resolve(true);
        },
        (err) => {
          console.log(err);
          reject('Error creating table. ' + err.message);
        }
      );
    });
  }

  insertMasterTables(response, masterList) {
    if (this.DB == null) {
      this.createOrOpenDB();
    }

    return new Promise((resolve, reject) => {
      const insertMasterList = [];
      let index = 0;
      for (const iterator of masterList) {
        for (const iterator2 of response[index].Results) {
          insertMasterList.push([
            'insert into ' + iterator.name + ' (Id, Name, Description, SequenceNo) values (?,?,?,?)',
            [
              iterator2.Id,
              iterator2.Name,
              iterator2.Description,
              // iterator2.IsSelected,
              // iterator2.IsDisabled,
              iterator2.SequenceNo,
            ]]
          );
        }
        index++;
      }
      this.DB.sqlBatch(insertMasterList).then(
        () => {
          resolve(true);
        },
        (err) => {
          console.log(err);
          reject('Error inserting table. ' + err.message);
        }
      );
    });
  }

}
