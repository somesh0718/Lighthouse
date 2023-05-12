import { Component, OnInit, ViewChild } from '@angular/core';
// import { NgForm } from '@angular/forms';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { VcSchoolVisitReportingService } from './vc-school-visit-reporting.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { VCSchoolVisitReportingModel } from './vc-school-visit-reporting.model';
import { ApiService } from '../services/api.service';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { forkJoin, Observable, of } from 'rxjs';
import { DropdownModel } from '../models/dropdown.model';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';
import { StudentAttendanceModel } from '../models/student.attendance.model';
import { BaseService } from '../services/base.service';
import { Storage } from '@ionic/storage';
import { BasePage } from '../common/base.page';
import { FileUploadModel } from '../models/file.upload.model';
import { promise } from 'protractor';

@Component({
  selector: 'app-vc-school-visit-reporting',
  templateUrl: './vc-school-visit-reporting.page.html',
  styleUrls: ['./vc-school-visit-reporting.page.scss'],
})
export class VcSchoolVisitReportingPage extends BasePage<VCSchoolVisitReportingModel> implements OnInit {
  maxDate: any;
  file: File;
  submittedRecords: any;
  vcSchoolVisitReportingModel: VCSchoolVisitReportingModel;
  vcSchoolVisitReportingForm: FormGroup;
  dataSource: any;
  minDate: any;
  schoolList: [];
  CommonMasterDataMaster = [] as any;
  SchoolsByVCIdMaster = [] as any;
  DistrictByVCIdMAster = [] as any;
  districtList: [];
  sectorsByUser: any = [];
  jobRolesByUser: any = [];

  svPhotoWithPrincipalFile: FileUploadModel;
  svPhotoWithStudentFile: FileUploadModel;
  sectorList: any;
  userId: string;
  jobRoleList: any;
  vtList: any;
  userTypeId: string;
  defaultStateId: string;
  monthList: any;
  vcName: string;


  constructor(
    public datepipe: DatePipe,
    private vcSchoolVisitReportingService: VcSchoolVisitReportingService,
    private helperService: HelperService,
    private router: Router,
    public loadingController: LoadingController,
    public api: ApiService,
    private alertCtrl: AlertController,
    private modalCtrl: ModalController,
    private formBuilder: FormBuilder,
    private localStorage: Storage
  ) {
    super();

    this.vcSchoolVisitReportingModel = new VCSchoolVisitReportingModel();
    this.svPhotoWithPrincipalFile = new FileUploadModel();
    this.svPhotoWithStudentFile = new FileUploadModel();
    this.vcSchoolVisitReportingForm = this.createVCSchoolVisitReportingForm();
  }

  ngOnInit() {
  }

  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.loadSubmittedRecords();
    this.localStorage.get('currentUser').then((r) => {
      this.UserModel = JSON.parse(r);
      this.userId = this.UserModel.LoginId;
      this.userTypeId = this.UserModel.UserTypeId;
      this.defaultStateId = this.UserModel.DefaultStateId;
      this.vcName = this.UserModel.UserName;

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
    });
    this.vcSchoolVisitReportingForm.reset();

  }

  ionViewWillEnter() {
    this.vcSchoolVisitReportingModel = new VCSchoolVisitReportingModel();
    this.svPhotoWithPrincipalFile = new FileUploadModel();
    this.svPhotoWithStudentFile = new FileUploadModel();
    this.vcSchoolVisitReportingForm = this.createVCSchoolVisitReportingForm();
    this.loadMasters();
  }

  ionViewWillLeave() {
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
        const obsList = [this.api.loadCommonMasterData(), this.api.loadSchoolsByVCId(), this.api.loadVTByVCId(), this.api.loadDistrictsByStateId(), this.api.loadDropdownMasterData('SectorsByUser'), this.api.loadDropdownMasterData('JobRolesByUser')];

        forkJoin(obsList).subscribe((res: any) => {
          this.CommonMasterDataMaster = res[0];

          // = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VocationalTrainersByVC' && x.ParentId == this.userTypeId);
          this.monthList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'Months');
          //this.districtList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'DistrictForBlock' && x.UserId == this.defaultStateId);
          // this.SchoolsByVCIdMaster = res[1];
          //this.DistrictByVCIdMAster = res[2];
          this.schoolList = res[1];
          this.vtList = res[2];
          this.districtList = res[3];

          this.sectorsByUser = res[4];
          this.jobRolesByUser = res[5];

          this.vcSchoolVisitReportingForm.get('VCName').setValue(this.UserModel.UserName);
          loadEl.dismiss();

        }, (err) => {
          this.showAlert('Please do Master Sync first.');
          loadEl.dismiss();
        });

      });

  }

  onChangeSchool(schoolId: any): void {
    this.sectorList = this.sectorsByUser.filter((x) => x.Id == schoolId);
  }

  onChangeSector(sectorId: any): void {
    this.jobRoleList = this.jobRolesByUser.filter((x) => x.Description == sectorId);
  }

  loadSubmittedRecords(): Promise<any> {
    let promise = new Promise((resolve, reject) => {

      this.loadingController
        .create({
          message: 'Please wait...'
        })
        .then((loadEl) => {
          loadEl.present();

          this.api.selectTableData('GetVCSchoolVisitReporting').then((res) => {
            this.submittedRecords = res;
            resolve(res);
            loadEl.dismiss();

          }, (err) => {
            this.showAlert('Please do Get My Sync first.');
            reject(null);
            loadEl.dismiss();
          });

        });

    });
    return promise;
  }

  openCalModal() {
    this.loadSubmittedRecords().then(resp => {
      this.openCalenderPopupModel();
    });
  }

  async openCalenderPopupModel() {
    for (const iterator of this.submittedRecords) {
      iterator.calendarDate = iterator.VisitDate;
    }

    const modal = await this.modalCtrl.create({
      component: CalModalPage,
      componentProps: {
        Records: this.submittedRecords,
        title: 'VC School Visit Reporting',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {
    });
  }

  uploadedSVPhotoWithPrincipalFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vcSchoolVisitReportingForm.get('SVPhotoWithPrincipalFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.VCSchoolVisitReport).then((response: FileUploadModel) => {
        this.svPhotoWithPrincipalFile = response;
        this.vcSchoolVisitReportingForm.get('SVPhotoWithPrincipalFile').setValue(response.Base64Data);

        this.vcSchoolVisitReportingForm.get('IsSVPhotoWithPrincipalFile').setValue(false);
        this.setMandatoryFieldControl(this.vcSchoolVisitReportingForm, 'SVPhotoWithPrincipalFile', this.Constants.DefaultImageState);
      });
    }
  }

  uploadedSVPhotoWithStudentFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vcSchoolVisitReportingForm.get('SVPhotoWithStudentFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.VCSchoolVisitReport).then((response: FileUploadModel) => {
        this.svPhotoWithStudentFile = response;
        this.vcSchoolVisitReportingForm.get('SVPhotoWithStudentFile').setValue(response.Base64Data);

        this.vcSchoolVisitReportingForm.get('IsSVPhotoWithStudentFile').setValue(false);
        this.setMandatoryFieldControl(this.vcSchoolVisitReportingForm, 'SVPhotoWithStudentFile', this.Constants.DefaultImageState);
      });
    }
  }

  saveOrUpdateVCSchoolVisitReportingDetails() {
    if (!this.vcSchoolVisitReportingForm.valid) {
      this.validateDynamicFormArrayFields(this.vcSchoolVisitReportingForm);
      return;
    }
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        this.vcSchoolVisitReportingModel = this.vcSchoolVisitReportingService.getVCSchoolVisitReportingModelFromFormGroup(this.vcSchoolVisitReportingForm);
        this.vcSchoolVisitReportingModel.SVPhotoWithPrincipalFile = (this.svPhotoWithPrincipalFile.Base64Data != '' ? this.setUploadedFile(this.svPhotoWithPrincipalFile) : null);
        this.vcSchoolVisitReportingModel.SVPhotoWithStudentFile = (this.svPhotoWithStudentFile.Base64Data != '' ? this.setUploadedFile(this.svPhotoWithStudentFile) : null);

        this.helperService.getCurrentCoordinates().then((res: any) => {
          this.vcSchoolVisitReportingModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
          this.vcSchoolVisitReportingModel.Latitude = res.coords.latitude;
          this.vcSchoolVisitReportingModel.Longitude = res.coords.longitude;
          this.vcSchoolVisitReportingModel.VCId = this.UserModel.UserTypeId;

          if (this.helperService.checkInternetConnection()) {
            this.vcSchoolVisitReportingService.createOrUpdateVcSchoolVisitReporting(this.vcSchoolVisitReportingModel)
              .subscribe((vcSchoolVisitReportingResp: any) => {

                if (vcSchoolVisitReportingResp.Success) {
                  const successMessages = this.helperService.getHtmlMessage(vcSchoolVisitReportingResp.Messages);
                  loadEl.dismiss();
                  this.helperService.presentToast(successMessages);
                  // this.vcSchoolVisitReportingForm.reset();
                  this.file = null;
                  this.router.navigateByUrl('/folder/Home', { replaceUrl: true });
                }
                else {
                  const errorMessages = this.helperService.getHtmlMessage(vcSchoolVisitReportingResp.Errors);
                  loadEl.dismiss();
                  this.helperService.showAlert(errorMessages);
                }
              }, error => {
                this.savingOfflineVCSchoolVisitReporting();
                loadEl.dismiss();
                console.log('VCSchoolVisitReporting errors =>', error);
              });
          } else {
            this.savingOfflineVCSchoolVisitReporting();
            loadEl.dismiss();
          }
        }, (err) => {
          loadEl.dismiss();
          return;
        });
      });
  }

  savingOfflineVCSchoolVisitReporting() {
    this.helperService.presentToast('Saving Offline...');

    const storeForm = JSON.parse(JSON.stringify(this.vcSchoolVisitReportingModel));
    this.api.insertUploadTable('UploadVCSchoolVisitReporting', storeForm).then(() => {
      this.file = null;
      this.router.navigateByUrl('/folder/Home', { replaceUrl: true });
      this.helperService.presentToast('VC School Visit Reporting Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('VC School Visit Reporting errors =>', err);
    });
  }

  // Create vcSchoolVisitReporting form and returns {FormGroup}
  createVCSchoolVisitReportingForm(): FormGroup {
    return this.formBuilder.group({
      VCSchoolVisitReportingId: new FormControl(this.vcSchoolVisitReportingModel.VCSchoolVisitReportingId),
      // tslint:disable-next-line: max-line-length
      //VCId: new FormControl('', [Validators.required]),
      VCName: new FormControl(''),
      CompanyName: new FormControl('', [Validators.maxLength(100)]),
      //Month: new FormControl('', [Validators.required]),
      VisitDate: new FormControl('', Validators.required),
      SchoolId: new FormControl('', [Validators.required]),
      DistrictCode: new FormControl('', [Validators.required]),
      SchoolEmailId: new FormControl('', [Validators.required, Validators.maxLength(150), Validators.pattern(this.Constants.Regex.Email)]),
      PrincipalName: new FormControl('', [Validators.required, Validators.maxLength(150)]),
      PrincipalPhoneNo: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.Number)]),
      SectorId: new FormControl('', [Validators.required]),
      JobRoleId: new FormControl('', [Validators.required]),
      VTId: new FormControl('', [Validators.required]),
      VTPhoneNo: new FormControl('', [Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.Number)]),
      Labs: new FormControl('', [Validators.maxLength(100)]),
      Books: new FormControl('', [Validators.maxLength(100)]),
      NoOfGLConducted: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      NoOfIndustrialVisits: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      SVPhotoWithPrincipalFile: new FormControl(''),
      SVPhotoWithStudentFile: new FormControl(''),
      Class9Boys: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class9Girls: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class10Boys: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class10Girls: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class11Boys: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class11Girls: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class12Boys: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      Class12Girls: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      TotalBoys: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      TotalGirls: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      IsSVPhotoWithPrincipalFile: new FormControl(false),
      IsSVPhotoWithStudentFile: new FormControl(false),
    });
  }
}
