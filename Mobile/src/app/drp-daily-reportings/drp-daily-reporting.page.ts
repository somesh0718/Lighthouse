import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { DatePipe, formatDate } from '@angular/common';
import { DRPDailyReportingService } from './drp-daily-reporting.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { ApiService } from '../services/api.service';
import { AppConstants } from '../app.constants';
import { DRPDailyReportingModel } from './drp-daily-reporting.model';
import { DropdownModel } from '../models/dropdown.model';
import { DRPLeaveModel } from './drp-leave.model';
import { DRPHolidayModel } from './drp-holiday.model';
import { DRPIndustryExposureVisitModel } from './drp-industry-exposure-visit.model';
import { BaseService } from '../services/base.service';
import { Storage } from '@ionic/storage';
import { forkJoin } from 'rxjs';
import { BasePage } from '../common/base.page';

@Component({
  selector: 'app-drp-daily-reporting',
  templateUrl: './drp-daily-reporting.page.html',
  styleUrls: ['./drp-daily-reporting.page.scss'],
})

// tslint:disable: no-string-literal

export class DRPDailyReportingPage extends BasePage<DRPDailyReportingModel> implements OnInit {
  maxDate: any;
  submittedRecords: any;

  drpDailyReportingForm: FormGroup;
  drpDailyReportingModel: DRPDailyReportingModel;
  vocationalCoordinatorList: [];
  reportTypeList = [] as any;
  workTypeList: [];
  industryLinkageList: [];
  leaveApproverList: [];
  schoolList: [];

  isAllowHoliday = false;
  isAllowLeave = false;
  isAllowSchool = false;
  isAllowIndustryExposureVisit = false;
  isAllowWorkDetails = false;
  holidayTypeList: any;
  observationDayList: any;
  leaveTypeList: any;
  Constants: AppConstants;
  CommonMasterDataMaster = [] as any;
  SchoolsByDRPIdMaster = [] as any;
  minDate: any;

  constructor(
    public datepipe: DatePipe,
    private drpDailyReportingService: DRPDailyReportingService,
    private helperService: HelperService,
    private router: Router,
    private alertCtrl: AlertController,
    public loadingController: LoadingController,
    private api: ApiService,
    private modalCtrl: ModalController,
    private baseService: BaseService,
    private localStorage: Storage,
    private formBuilder: FormBuilder,
  ) {
    super();

    this.drpDailyReportingModel = new DRPDailyReportingModel();
    this.drpDailyReportingForm = this.createDRPDailyReportingForm();
  }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.drpDailyReportingModel = new DRPDailyReportingModel();
    this.drpDailyReportingForm = this.createDRPDailyReportingForm();

    this.loadMasters();
  }
  ionViewWillLeave() {
    // this.drpDailyReportingForm.reset();
  }


  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.minDate = new Date(Date.now());

    this.minDate.setDate(this.minDate.getDate());
    this.minDate.setFullYear(this.minDate.getFullYear() - 1);

    this.minDate = this.DateFormatPipe.transform(this.minDate, this.Constants.DateValFormat);
    this.loadSubmittedRecords();

    this.localStorage.get('currentUser').then((r) => {
      this.UserModel = JSON.parse(r);

      // this.minDate = this.DateFormatPipe.transform(this.UserModel.DateOfAllocation, this.Constants.DateValFormat);


    });

    this.drpDailyReportingForm.reset();
  }

  async showAlert(msg) {
    const alert = await this.alertCtrl.create({
      message: msg,
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            this.router.navigateByUrl('/folder/Home');
          }
        }
      ],
      backdropDismiss: false
    });

    await alert.present();
  }

  loadMasters() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        const obsList = [this.api.loadCommonMasterData(), this.api.loadSchoolsByDRPId()];

        forkJoin(obsList).subscribe((res: any) => {
          this.CommonMasterDataMaster = res[0];

          this.reportTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'DRPReportType');

          this.SchoolsByDRPIdMaster = res[1];

          loadEl.dismiss();

        }, (err) => {
          this.showAlert('Please do Master Sync first.');
          loadEl.dismiss();
        });

      });
  }


  loadSubmittedRecords() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        this.api.selectTableData('GetDRPDailyReporting').then((res) => {
          this.submittedRecords = res;
          loadEl.dismiss();
        }, (err) => {
          this.showAlert('Please do Get My Sync first.');
          loadEl.dismiss();
        });
      });
  }

  async openCalModal() {
    const invalid = [];
    const controls = this.drpDailyReportingForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }

    //this.loadSubmittedRecords();

    for (const iterator of this.submittedRecords) {
      iterator.calendarDate = iterator.ReportDate;
    }
    const modal = await this.modalCtrl.create({
      component: CalModalPage,
      componentProps: {
        Records: this.submittedRecords,
        title: 'DRP Daily Reporting',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

    });
  }

  onChangeReportType(reportTypeId): void {
    // this.baseService.GetMasterDataByType({ DataType: 'DRPWorkType', ParentId: reportTypeId, SelectTitle: 'Working Day' }).subscribe((response: any) => {
    //   if (response.Success) {
    this.resetReportingFormGroups();

    // On Leave
    if (reportTypeId === '305') {
      this.isAllowLeave = true;
      this.drpDailyReportingModel.Leave = new DRPLeaveModel();

      this.drpDailyReportingForm = this.formBuilder.group({
        ...this.drpDailyReportingForm.controls,

        leaveGroup: this.formBuilder.group({
          LeaveTypeId: new FormControl(''),
          LeaveApprovalStatus: new FormControl(''),
          LeaveApprover: new FormControl(''),
          LeaveReason: new FormControl('', [Validators.maxLength(350)]),
        })
      });

      // this.leaveTypeList = response.Results;
      this.leaveTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'DRPWorkType');

      // this.baseService.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'DRPLeaveApprover', SelectTitle: 'Leave Approver' }).subscribe((resp) => {
      //   if (resp.Success) {
      //     this.leaveApproverList = resp.Results;
      this.leaveApproverList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'DRPLeaveApprover');
      // }
      // });
    }

    // Holiday/ School Off
    else if (reportTypeId === '306') {
      this.isAllowHoliday = true;
      this.drpDailyReportingModel.Holiday = new DRPHolidayModel();

      this.drpDailyReportingForm = this.formBuilder.group({
        ...this.drpDailyReportingForm.controls,

        holidayGroup: this.formBuilder.group({
          HolidayTypeId: new FormControl('', Validators.maxLength(50)),
          HolidayDetails: new FormControl('', Validators.maxLength(250)),
        })
      });

      // this.holidayTypeList = response.Results;
      this.holidayTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'DRPWorkType');
    }

    else {
      // this.workTypeList = response.Results;
      this.workTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId == '307' && x.DataTypeId === 'DRPWorkType');
    }
    // }

    // });

  }

  onChangeWorkingType(workingTypes): void {
    this.resetReportingFormGroups();

    workingTypes.forEach(workTypeId => {

      // 1. Industry Exposure Visit
      if (workTypeId === '160') {
        this.isAllowIndustryExposureVisit = true;
        this.drpDailyReportingModel.IndustryExposureVisit = new DRPIndustryExposureVisitModel();

        this.drpDailyReportingForm = this.formBuilder.group({
          ...this.drpDailyReportingForm.controls,

          industryExposureVisitGroup: this.formBuilder.group({
            TypeOfIndustryLinkage: new FormControl(''),
            ContactPersonName: new FormControl('', [Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharsWithSpace)]),
            ContactPersonMobile: new FormControl('', [Validators.pattern(this.Constants.Regex.MobileNumber), Validators.minLength(10), Validators.maxLength(10)]),
            ContactPersonEmail: new FormControl('', [Validators.maxLength(100), Validators.pattern(this.Constants.Regex.Email)]),
          })
        });

        // this.baseService.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'IndustryLinkageType', SelectTitle: 'Industry Linkage' }).subscribe((response) => {
        //   if (response.Success) {
        //     this.industryLinkageList = response.Results;
        this.industryLinkageList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'IndustryLinkageType');
        // }
        // });
      }
      // School
      else if (workTypeId === '313') {
        this.isAllowSchool = true;

        // this.baseService.GetSchoolsByDRPId({ DataId: this.UserModel.LoginId, DataId1: this.UserModel.UserTypeId, SelectTitle: 'School' }).subscribe((response) => {
        //   if (response.Success) {
        //     this.schoolList = response.Results;
        this.schoolList = this.SchoolsByDRPIdMaster;
        //   }
        // });
      }

      // Work details
      if (workTypeId !== '160') {
        this.isAllowWorkDetails = true;
      }
    });

    const initialFormValues = this.drpDailyReportingForm.value;
    this.drpDailyReportingForm.reset(initialFormValues);
  }

  onChangeLeaveApprovalStatus(val) {
    // if (val === 'no') {
    //   this.drpDailyReportingForm.controls.leaveGroup['LeaveApprover'].disable();
    //   this.drpDailyReportingForm.controls.leaveGroup['LeaveApprover'].setValue('');
    // } else {
    //   this.drpDailyReportingForm.controls.leaveGroup['LeaveApprover'].enable();
    // }
  }

  resetReportingFormGroups(): void {
    this.isAllowHoliday = false;
    this.isAllowLeave = false;
    this.isAllowWorkDetails = false;
    this.isAllowSchool = false;
    this.isAllowIndustryExposureVisit = false;

    delete this.drpDailyReportingForm.controls.leaveGroup;
    delete this.drpDailyReportingForm.value.leaveGroup;
    delete this.drpDailyReportingForm.controls.holidayGroup;
    delete this.drpDailyReportingForm.value.holidayGroup;
    delete this.drpDailyReportingForm.controls.industryExposureVisitGroup;
    delete this.drpDailyReportingForm.value.industryExposureVisitGroup;
    const initialFormValues = this.drpDailyReportingForm.value;
    this.drpDailyReportingForm.reset(initialFormValues);
  }

  saveOrUpdateDRPDailyReportingDetails() {

    if (!this.drpDailyReportingForm.valid) {
      this.validateDynamicFormArrayFields(this.drpDailyReportingForm);
      return;
    }

    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        this.drpDailyReportingModel = this.drpDailyReportingService.getDRPDailyReportingModelFromFormGroup(this.drpDailyReportingForm);
        this.drpDailyReportingModel.ReportDate = this.DateFormatPipe.transform(this.drpDailyReportingForm.get('ReportDate').value, this.Constants.ServerDateFormat);
        this.drpDailyReportingModel.DRPId = this.UserModel.UserTypeId;

        this.helperService.getCurrentCoordinates().then((res: any) => {
          this.drpDailyReportingModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
          this.drpDailyReportingModel.Latitude = res.coords.latitude;
          this.drpDailyReportingModel.Longitude = res.coords.longitude;
          if (this.helperService.checkInternetConnection()) {
            this.drpDailyReportingService.createOrUpdateDRPDailyReporting(this.drpDailyReportingModel)
              .subscribe((drpDailyReportingResp: any) => {

                if (drpDailyReportingResp.Success) {
                  const successMessages = this.helperService.getHtmlMessage(drpDailyReportingResp.Messages);
                  loadEl.dismiss();
                  this.helperService.presentToast(successMessages);

                  this.router.navigateByUrl('/folder/Home');
                }
                else {
                  const errorMessages = this.helperService.getHtmlMessage(drpDailyReportingResp.Errors);
                  loadEl.dismiss();
                  this.helperService.showAlert(errorMessages);
                }
              }, error => {
                this.savingOfflineDRPDailyReporting();
                loadEl.dismiss();
                console.log('DRPDailyReporting errors =>', error);
              });
          } else {
            this.savingOfflineDRPDailyReporting();
            loadEl.dismiss();
          }
        }, (err) => {
          loadEl.dismiss();
          return;
        });
      });
  }

  savingOfflineDRPDailyReporting() {
    this.helperService.presentToast('Saving Offline...');

    const storeForm = JSON.parse(JSON.stringify(this.drpDailyReportingModel));
    storeForm.WorkingDayTypeIds = JSON.stringify(this.drpDailyReportingModel.WorkingDayTypeIds);
    storeForm.Leave = JSON.stringify(this.drpDailyReportingModel.Leave);
    storeForm.Holiday = JSON.stringify(this.drpDailyReportingModel.Holiday);
    storeForm.IndustryExposureVisit = JSON.stringify(this.drpDailyReportingModel.IndustryExposureVisit);

    this.api.insertUploadTable('UploadDRPDailyReporting', storeForm).then(() => {
      this.router.navigateByUrl('/folder/Home');
      this.helperService.presentToast('DRP Daily Reporting Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('DRP Daily Reporting errors =>', err);
    });
  }

  // Create drpDailyReporting form and returns {FormGroup}
  createDRPDailyReportingForm(): FormGroup {
    return this.formBuilder.group({
      DRPDailyReportingId: new FormControl(this.drpDailyReportingModel.DRPDailyReportingId),
      ReportDate: new FormControl('', Validators.required),
      ReportType: new FormControl('', Validators.required),
      WorkingDayTypeIds: new FormControl(''),
      SchoolId: new FormControl(''),
      WorkTypeDetails: new FormControl('', Validators.maxLength(350)),
    });
  }

}
