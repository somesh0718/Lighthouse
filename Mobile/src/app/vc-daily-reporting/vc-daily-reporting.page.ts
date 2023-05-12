import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { DatePipe, formatDate } from '@angular/common';
import { VCDailyReportingService } from './vc-daily-reporting.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { ApiService } from '../services/api.service';
import { AppConstants } from '../app.constants';
import { VCDailyReportingModel } from './vc-daily-reporting.model';
import { DropdownModel } from '../models/dropdown.model';
import { VCLeaveModel } from './vc-leave.model';
import { VCHolidayModel } from './vc-holiday.model';
import { VCIndustryExposureVisitModel } from './vc-industry-exposure-visit.model';
import { BaseService } from '../services/base.service';
import { Storage } from '@ionic/storage';
import { forkJoin } from 'rxjs';
import { BasePage } from '../common/base.page';
import { VCPraticalModel } from './vc-pratical.model';
import { CommonService } from '../services/common.service';

@Component({
  selector: 'app-vc-daily-reporting',
  templateUrl: './vc-daily-reporting.page.html',
  styleUrls: ['./vc-daily-reporting.page.scss'],
})

// tslint:disable: no-string-literal

export class VcDailyReportingPage extends BasePage<VCDailyReportingModel> implements OnInit {
  maxDate: any;
  submittedRecords: any;

  vcDailyReportingForm: FormGroup;
  vcDailyReportingModel: VCDailyReportingModel;
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
  isAllowPractical = false;
  isAllowWorkDetails = false;
  holidayTypeList: any;
  observationDayList: any;
  leaveTypeList: any;
  Constants: AppConstants;
  CommonMasterDataMaster = [] as any;
  SchoolsByVCIdMaster = [] as any;
  minDate: any;

  paSchoolList: [DropdownModel];
  filteredPASchoolItems: any;
  vtList: [DropdownModel];
  sectorList: [DropdownModel];
  jobRoleList: [DropdownModel];
  classList: any = [];

  constructor(
    public datepipe: DatePipe,
    private vcDailyReportingService: VCDailyReportingService,
    private helperService: HelperService,
    private router: Router,
    private alertCtrl: AlertController,
    public loadingController: LoadingController,
    private api: ApiService,
    private modalCtrl: ModalController,
    private baseService: BaseService,
    public commonService: CommonService,
    private localStorage: Storage,
    private formBuilder: FormBuilder,
  ) {
    super();

    this.vcDailyReportingModel = new VCDailyReportingModel();
    this.vcDailyReportingForm = this.createVCDailyReportingForm();
  }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.vcDailyReportingModel = new VCDailyReportingModel();
    this.vcDailyReportingForm = this.createVCDailyReportingForm();

    this.loadMasters();
  }
  ionViewWillLeave() {
    // this.vcDailyReportingForm.reset();
  }


  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.loadSubmittedRecords();

    this.localStorage.get('currentUser').then((r) => {
      this.UserModel = JSON.parse(r);

      let dateOfAllocation = new Date(this.UserModel.DateOfAllocation);
      let maxDate = new Date(Date.now());

      let Time = maxDate.getTime() - dateOfAllocation.getTime();
      let Days = Math.floor(Time / (1000 * 3600 * 24));

      if (Days < this.Constants.BackDatedReportingDays) {
        this.minDate = this.DateFormatPipe.transform(this.UserModel.DateOfAllocation, this.Constants.DateValFormat);
      }
      else {
        let past7days = maxDate;
        past7days.setDate(past7days.getDate() - this.Constants.BackDatedReportingDays)
        this.minDate = this.DateFormatPipe.transform(past7days, this.Constants.DateValFormat);
      }

      if (this.Constants.Services.Environment.target == 'od') {
        this.minDate = this.DateFormatPipe.transform('2023/02/01', this.Constants.DateValFormat);
      }
    });

    this.vcDailyReportingForm.reset();
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
        const obsList = [this.api.loadCommonMasterData(), this.api.loadSchoolsByVCId()];

        forkJoin(obsList).subscribe((res: any) => {
          this.CommonMasterDataMaster = res[0];

          this.reportTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VCReportType');

          this.SchoolsByVCIdMaster = res[1];

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

        this.api.selectTableData('GetVCDailyReporting').then((res) => {
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
    const controls = this.vcDailyReportingForm.controls;
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
        title: 'VC Daily Reporting',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

    });
  }

  onChangeReportType(reportTypeId): void {
    // this.baseService.GetMasterDataByType({ DataType: 'VCWorkType', ParentId: reportTypeId, SelectTitle: 'Working Day' }).subscribe((response: any) => {
    //   if (response.Success) {
    this.resetReportingFormGroups();

    // On Leave
    if (reportTypeId === '47') {
      this.isAllowLeave = true;
      this.vcDailyReportingModel.Leave = new VCLeaveModel();

      this.vcDailyReportingForm = this.formBuilder.group({
        ...this.vcDailyReportingForm.controls,

        leaveGroup: this.formBuilder.group({
          LeaveTypeId: new FormControl(''),
          LeaveApprovalStatus: new FormControl(''),
          LeaveApprover: new FormControl(''),
          LeaveReason: new FormControl('', [Validators.maxLength(350)]),
        })
      });

      // this.leaveTypeList = response.Results;
      this.leaveTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'VCWorkType');

      // this.baseService.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'VCLeaveApprover', SelectTitle: 'Leave Approver' }).subscribe((resp) => {
      //   if (resp.Success) {
      //     this.leaveApproverList = resp.Results;
      this.leaveApproverList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VCLeaveApprover');
      // }
      // });
    }

    // Holiday/ School Off
    else if (reportTypeId === '48') {
      this.isAllowHoliday = true;
      this.vcDailyReportingModel.Holiday = new VCHolidayModel();

      this.vcDailyReportingForm = this.formBuilder.group({
        ...this.vcDailyReportingForm.controls,

        holidayGroup: this.formBuilder.group({
          HolidayTypeId: new FormControl('', Validators.maxLength(50)),
          HolidayDetails: new FormControl('', Validators.maxLength(250)),
        })
      });

      // this.holidayTypeList = response.Results;
      this.holidayTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'VCWorkType');
    }

    else {
      // this.workTypeList = response.Results;
      this.workTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId == '49' && x.DataTypeId === 'VCWorkType');
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
        this.vcDailyReportingModel.IndustryExposureVisit = new VCIndustryExposureVisitModel();

        this.vcDailyReportingForm = this.formBuilder.group({
          ...this.vcDailyReportingForm.controls,

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
      else if (workTypeId === '153') {
        this.isAllowSchool = true;

        // this.baseService.GetSchoolsByVCId({ DataId: this.UserModel.LoginId, DataId1: this.UserModel.UserTypeId, SelectTitle: 'School' }).subscribe((response) => {
        //   if (response.Success) {
        //     this.schoolList = response.Results;
        this.schoolList = this.SchoolsByVCIdMaster;
        //   }
        // });
      }

      //pratical
      else if (workTypeId == '10') {
        this.isAllowPractical = true;
        this.vcDailyReportingModel.Pratical = new VCPraticalModel(this.vcDailyReportingModel.Pratical);

        this.vcDailyReportingForm = this.formBuilder.group({
          ...this.vcDailyReportingForm.controls,

          praticalGroup: this.formBuilder.group({
            IsPratical: new FormControl('', [Validators.required]),
            SchoolId: new FormControl('', [Validators.required]),
            VTId: new FormControl('', [Validators.required]),
            SectorId: new FormControl('', [Validators.required]),
            JobRoleId: new FormControl('', [Validators.required]),
            ClassId: new FormControl('', [Validators.required]),
            StudentCount: new FormControl('', [Validators.required, , Validators.pattern(this.Constants.Regex.Number)]),
            VTPresent: new FormControl('', [Validators.required]),
            PresentStudentCount: new FormControl('', [Validators.required, , Validators.pattern(this.Constants.Regex.Number)]),
            AssesorName: new FormControl('', [Validators.required, Validators.maxLength(100), Validators.pattern(this.Constants.Regex.CharWithTitleCaseSpaceAndSpecialChars)]),
            AssesorMobileNo: new FormControl('', [Validators.maxLength(10)]),
            Remarks: new FormControl('', [Validators.maxLength(350)]),
          })
        });
      }

      // Work details
      if (workTypeId !== '160') {
        this.isAllowWorkDetails = true;
      }
    });

    const initialFormValues = this.vcDailyReportingForm.value;
    this.vcDailyReportingForm.reset(initialFormValues);
  }

  onPratical(pratical): void {
    this.IsLoading = true;

    if (pratical == 'Yes') {
      this.commonService.GetSchoolsByVCId({ DataId: this.UserModel.LoginId, DataId1: this.UserModel.UserTypeId, SelectTitle: 'School' }).subscribe((response) => {
        if (response.Success) {
          this.paSchoolList = response.Results;
        }
      });
    } else {
      this.commonService.GetSchoolsByAcademicYearId(this.UserModel.AcademicYearId).subscribe((response) => {
        if (response.Success) {
          this.paSchoolList = response.Results;
        }
      });
    }
  }

  onChangeSchool(schoolId): Promise<any> {
    this.IsLoading = true;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetMasterDataByType({ DataType: 'TrainersBySchool', ParentId: schoolId, SelectTitle: 'Select Vocational Trainer' }).subscribe((response) => {
        if (response.Success) {
          this.vtList = response.Results;
        }
        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });

    });
    return promise;
  }

  onChangeVT(vtId): Promise<any> {
    this.IsLoading = true;
    let schoolId = this.vcDailyReportingForm.controls.praticalGroup.get('SchoolId').value;

    let promise = new Promise((resolve, reject) => {
      this.vcDailyReportingService.getDropdownForVCPracticalAssessment(this.UserModel.AcademicYearId, schoolId, vtId).subscribe(results => {
        if (results[0].Success) {
          this.classList = results[0].Results;
        }

        if (results[1].Success) {
          this.sectorList = results[1].Results;
        }

        if (results[2].Success) {
          this.jobRoleList = results[2].Results;
        }

        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });

    return promise;
  }

  onChangeVTClass(classId): Promise<any> {
    this.IsLoading = true;

    let schoolId = this.vcDailyReportingForm.controls.praticalGroup.get('SchoolId').value;
    let vtId = this.vcDailyReportingForm.controls.praticalGroup.get('VTId').value;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetMasterDataByType({ DataType: 'StudentEnrolledCount', RoleId: schoolId, UserId: vtId, ParentId: classId, SelectTitle: 'StudentCount' }, false).subscribe((response) => {
        if (response.Success) {
          if (response.Results.length == 1) {
            this.vcDailyReportingForm.controls.praticalGroup.get('StudentCount').setValue(response.Results[0].Description);
            this.vcDailyReportingForm.controls.praticalGroup.get('StudentCount').disable();
          }
          else {
            this.vcDailyReportingForm.controls.praticalGroup.get('StudentCount').enable();
          }
        }
        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onChangeLeaveApprovalStatus(val) {
    // if (val === 'no') {
    //   this.vcDailyReportingForm.controls.leaveGroup['LeaveApprover'].disable();
    //   this.vcDailyReportingForm.controls.leaveGroup['LeaveApprover'].setValue('');
    // } else {
    //   this.vcDailyReportingForm.controls.leaveGroup['LeaveApprover'].enable();
    // }
  }

  resetReportingFormGroups(): void {
    this.isAllowHoliday = false;
    this.isAllowLeave = false;
    this.isAllowWorkDetails = false;
    this.isAllowSchool = false;
    this.isAllowIndustryExposureVisit = false;
    this.isAllowPractical = false;

    delete this.vcDailyReportingForm.controls.leaveGroup;
    delete this.vcDailyReportingForm.value.leaveGroup;
    delete this.vcDailyReportingForm.controls.holidayGroup;
    delete this.vcDailyReportingForm.value.holidayGroup;
    delete this.vcDailyReportingForm.controls.industryExposureVisitGroup;
    delete this.vcDailyReportingForm.value.industryExposureVisitGroup;
    delete this.vcDailyReportingForm.controls.praticalGroup;
    delete this.vcDailyReportingForm.value.praticalGroup;

    const initialFormValues = this.vcDailyReportingForm.value;
    this.vcDailyReportingForm.reset(initialFormValues);
  }

  saveOrUpdateVCDailyReportingDetails() {

    if (!this.vcDailyReportingForm.valid) {
      this.validateDynamicFormArrayFields(this.vcDailyReportingForm);
      return;
    }

    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        this.vcDailyReportingModel = this.vcDailyReportingService.getVCDailyReportingModelFromFormGroup(this.vcDailyReportingForm, this.UserModel);
        this.vcDailyReportingModel.ReportDate = this.DateFormatPipe.transform(this.vcDailyReportingForm.get('ReportDate').value, this.Constants.ServerDateFormat);
        this.vcDailyReportingModel.VCId = this.UserModel.UserTypeId;

        this.helperService.getCurrentCoordinates().then((res: any) => {
          this.vcDailyReportingModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
          this.vcDailyReportingModel.Latitude = res.coords.latitude;
          this.vcDailyReportingModel.Longitude = res.coords.longitude;
          if (this.helperService.checkInternetConnection()) {
            this.vcDailyReportingService.createOrUpdateVCDailyReporting(this.vcDailyReportingModel)
              .subscribe((vcDailyReportingResp: any) => {

                if (vcDailyReportingResp.Success) {
                  const successMessages = this.helperService.getHtmlMessage(vcDailyReportingResp.Messages);
                  loadEl.dismiss();
                  this.helperService.presentToast(successMessages);
                  this.router.navigateByUrl('/folder/Home');
                }
                else {
                  const errorMessages = this.helperService.getHtmlMessage(vcDailyReportingResp.Errors);
                  loadEl.dismiss();
                  this.helperService.showAlert(errorMessages);
                }
              }, error => {
                this.savingOfflineVCDailyReporting();
                loadEl.dismiss();
                console.log('VCDailyReporting errors =>', error);
              });
          } else {
            this.savingOfflineVCDailyReporting();
            loadEl.dismiss();
          }
        }, (err) => {
          loadEl.dismiss();
          return;
        });
      });
  }

  savingOfflineVCDailyReporting() {
    this.helperService.presentToast('Saving Offline...');

    const storeForm = JSON.parse(JSON.stringify(this.vcDailyReportingModel));
    storeForm.WorkingDayTypeIds = JSON.stringify(this.vcDailyReportingModel.WorkingDayTypeIds);
    storeForm.Leave = JSON.stringify(this.vcDailyReportingModel.Leave);
    storeForm.Holiday = JSON.stringify(this.vcDailyReportingModel.Holiday);
    storeForm.IndustryExposureVisit = JSON.stringify(this.vcDailyReportingModel.IndustryExposureVisit);

    this.api.insertUploadTable('UploadVCDailyReporting', storeForm).then(() => {
      this.router.navigateByUrl('/folder/Home');
      this.helperService.presentToast('VC Daily Reporting Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('VC Daily Reporting errors =>', err);
    });
  }

  // Create vcDailyReporting form and returns {FormGroup}
  createVCDailyReportingForm(): FormGroup {
    return this.formBuilder.group({
      VCDailyReportingId: new FormControl(this.vcDailyReportingModel.VCDailyReportingId),
      ReportDate: new FormControl('', Validators.required),
      ReportType: new FormControl('', Validators.required),
      WorkingDayTypeIds: new FormControl(''),
      SchoolId: new FormControl(''),
      WorkTypeDetails: new FormControl('', Validators.maxLength(350)),
    });
  }

}
