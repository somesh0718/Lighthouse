import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray, NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { VTGuestLectureConductedService } from './vt-guest-lecture-conducted.service';
import { VTGuestLectureConductedModel } from './vt-guest-lecture-conducted.model';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { forkJoin } from 'rxjs';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';
import { StudentAttendanceModel } from '../models/student.attendance.model';
import { BaseService } from '../services/base.service';
import { Storage } from '@ionic/storage';
import { BasePage } from '../common/base.page';
import { FileUploadModel } from '../models/file.upload.model';

@Component({
  selector: 'app-vt-guest-lecture-conducted',
  templateUrl: './vt-guest-lecture-conducted.page.html',
  styleUrls: ['./vt-guest-lecture-conducted.page.scss'],
})
export class VtGuestLectureConductedPage extends BasePage<VTGuestLectureConductedModel> implements OnInit {

  maxDate: any;
  image: any;
  studentList = [];
  studentClassesList = [];
  // glTypeList = [{ Id: '180', Name: 'Paid' }, { Id: '181', Name: 'Unpaid' }];
  file: File;
  submittedRecords: any;

  // sectionsList = [{ Name: 'section1', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }, { Name: 'section2', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }, { Name: 'section3', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }];
  // unitsList = [{ Name: 'unit1', Id: 1 }, { Name: 'unit2', Id: 2 }, { Name: 'unit3', Id: 3 }];
  // sessionsList = [{ Name: 'session1', Id: 1 }, { Name: 'session2', Id: 2 }, { Name: 'session3', Id: 3 }];
  // glMethodologyList = [{ Name: 'method1', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }, { Name: 'method2', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }, { Name: 'method3', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }];
  // GLConductedByList = [{ Name: 'conducted1', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }, { Name: 'conducted2', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }, { Name: 'conducted3', Id: 'f23ad3ca-4529-4d00-83f1-b8c898f19f1f' }];
  glCompanyRequired = false;

  vtGuestLectureConductedForm: FormGroup;
  vtGuestLectureConductedModel: VTGuestLectureConductedModel;
  unitSessionsModels = [];
  studentAttendanceModel: StudentAttendanceModel[];
  guestLecturerPhotoFile: FileUploadModel;
  guestLecturerPhotoInClassFile: FileUploadModel;

  displayColumns: string[] = ['StudentName', 'PresentStatus'];
  dataSource: any;
  vtClassList: [];
  sectionList: [];
  glMethodlogyList: [];
  glConductedByList: [];
  glWorkStatusList: [];

  classTaughtList: [];
  unitList: [];
  sessionList: [];
  moduleTaughtList: [];
  glTypeList: [];

  CourseModuleUnitSessionsMaster = [] as any;
  ClassSectionsByVTIdMaster = [] as any;
  CommonMasterDataMaster = [] as any;
  StudentsByVTIdMaster = [] as any;
  minDate: any;

  constructor(
    public datepipe: DatePipe,
    private vtGuestLectureConductedService: VTGuestLectureConductedService,
    private helperService: HelperService,
    private router: Router,
    public loadingController: LoadingController,
    public api: ApiService,
    private modalCtrl: ModalController,
    private formBuilder: FormBuilder,
    private alertCtrl: AlertController,
    private localStorage: Storage
  ) {
    super();

    this.vtGuestLectureConductedModel = new VTGuestLectureConductedModel();
    this.guestLecturerPhotoFile = new FileUploadModel();
    this.guestLecturerPhotoInClassFile = new FileUploadModel();
    this.vtGuestLectureConductedForm = this.createVTGuestLectureConductedForm();
  }

  ngOnInit() {
  }

  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.loadSubmittedRecords();

    this.localStorage.get('currentUser').then(
      (r) => {
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
      }
    );
    this.vtGuestLectureConductedForm.reset();
  }

  ionViewWillLeave() {
    // this.vtGuestLectureConductedForm.reset();
  }

  ionViewWillEnter() {
    this.vtGuestLectureConductedModel = new VTGuestLectureConductedModel();
    this.guestLecturerPhotoFile = new FileUploadModel();
    this.guestLecturerPhotoInClassFile = new FileUploadModel();
    this.vtGuestLectureConductedForm = this.createVTGuestLectureConductedForm();
    //this.onChangeGuestLectureType()
    this.loadMasters();
  }

  async showUserMessage(messageText) {
    const alert = await this.alertCtrl.create({
      message: messageText,
      buttons: [
        {
          text: 'Okay',
          handler: () => {
          }
        }
      ],
      backdropDismiss: false
    });

    await alert.present();
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
        const obsList = [this.api.loadCourseModuleUnitSessions(), this.api.loadClassSectionsByVTId(), this.api.loadCommonMasterData(), this.api.loadStudentsByVTId()];

        forkJoin(obsList).subscribe((res: any) => {
          console.log(res);
          this.CourseModuleUnitSessionsMaster = res[0];
          const distinctModules = this.CourseModuleUnitSessionsMaster.filter((thing, i, arr) => arr.findIndex(t => t.ModuleTypeId === thing.ModuleTypeId) === i);
          this.moduleTaughtList = distinctModules;

          this.ClassSectionsByVTIdMaster = res[1];
          const distinctClasses = this.ClassSectionsByVTIdMaster.filter((thing, i, arr) => arr.findIndex(t => t.ClassId === thing.ClassId) === i);
          this.classTaughtList = distinctClasses;

          this.CommonMasterDataMaster = res[2];
          this.glMethodlogyList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'GLMethodology');

          this.glConductedByList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'GLConductedBy');

          this.glWorkStatusList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'GLWorkStatus');

          this.glTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'GLType');

          this.StudentsByVTIdMaster = res[3];

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

        this.api.selectTableData('GetVTGuestLectureConducted').then((res) => {
          this.submittedRecords = res;
          loadEl.dismiss();
        }, (err) => {
          this.showAlert('Please do Get My Sync first.');
          loadEl.dismiss();
        });
      });
  }

  async openCalModal() {
    this.loadSubmittedRecords();

    for (const iterator of this.submittedRecords) {
      iterator.calendarDate = iterator.ReportingDate;
    }
    const modal = await this.modalCtrl.create({
      component: CalModalPage,
      componentProps: {
        Records: this.submittedRecords,
        title: 'VT Guest Lecture Conducted',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

    });
  }

  onChangeGuestLectureType(guestLectureTypeId) {
    //this.vtGuestLectureConductedForm.get("GLType").valueChanges.subscribe(data => {

    if (guestLectureTypeId == "181") {
      this.vtGuestLectureConductedForm.controls["GLConductedBy"].setValidators([Validators.required, Validators.maxLength(100)]);
      this.vtGuestLectureConductedForm.controls["GLPersonDetails"].setValidators([Validators.required, Validators.maxLength(100)]);
      this.vtGuestLectureConductedForm.controls["GLName"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLQualification"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLMobile"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLWorkExperience"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLWorkStatus"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLPhotoFile"].clearValidators();

    } else if (guestLectureTypeId == "180") {
      this.vtGuestLectureConductedForm.controls["GLConductedBy"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLPersonDetails"].clearValidators();

      this.vtGuestLectureConductedForm.controls["GLName"].setValidators([Validators.required, Validators.maxLength(150), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]);
      this.vtGuestLectureConductedForm.controls["GLQualification"].setValidators([Validators.required, Validators.maxLength(100)]);
      this.vtGuestLectureConductedForm.controls["GLMobile"].setValidators([Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]);
      this.vtGuestLectureConductedForm.controls["GLWorkExperience"].setValidators([Validators.required, Validators.pattern(this.Constants.Regex.Number)]);
      this.vtGuestLectureConductedForm.controls["GLWorkStatus"].setValidators(Validators.required);
      this.vtGuestLectureConductedForm.controls["GLPhotoFile"].setValidators(Validators.required);
    }

    this.vtGuestLectureConductedForm.controls["GLConductedBy"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLPersonDetails"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLName"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLQualification"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLMobile"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLWorkExperience"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLWorkStatus"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLPhotoFile"].updateValueAndValidity();

    this.onChangeGLWorkStatus('00000');
    //});
  }

  onChangeGLWorkStatus(workStatusId) {
    //this.vtGuestLectureConductedForm.get("GLWorkStatus").valueChanges.subscribe(workStatusId => {

    if (workStatusId == '178') {
      this.vtGuestLectureConductedForm.controls["GLCompany"].setValidators([Validators.required, Validators.maxLength(150)]);
      this.vtGuestLectureConductedForm.controls["GLDesignation"].setValidators([Validators.required, Validators.maxLength(150)]);
    }
    else {
      this.vtGuestLectureConductedForm.controls["GLCompany"].clearValidators();
      this.vtGuestLectureConductedForm.controls["GLDesignation"].clearValidators();
    }

    this.vtGuestLectureConductedForm.controls["GLCompany"].updateValueAndValidity();
    this.vtGuestLectureConductedForm.controls["GLDesignation"].updateValueAndValidity();
    //});
  }

  uploadedGuestLecturerPhotoFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtGuestLectureConductedForm.get('GLPhotoFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.GuestLecture).then((response: FileUploadModel) => {
        this.guestLecturerPhotoFile = response;
        this.vtGuestLectureConductedForm.get('GLPhotoFile').setValue(response.Base64Data);

        this.vtGuestLectureConductedForm.get('IsGLPhotoFile').setValue(false);
        this.setMandatoryFieldControl(this.vtGuestLectureConductedForm, 'GLPhotoFile', this.Constants.DefaultImageState);
      });
    }
  }

  uploadedGuestLecturerPhotoInClassFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtGuestLectureConductedForm.get('GLLecturerPhotoFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.GuestLecture).then((response: FileUploadModel) => {
        this.guestLecturerPhotoInClassFile = response;
        this.vtGuestLectureConductedForm.get('GLLecturerPhotoFile').setValue(response.Base64Data);

        this.vtGuestLectureConductedForm.get('IsGLLecturerPhotoFile').setValue(false);
        this.setMandatoryFieldControl(this.vtGuestLectureConductedForm, 'GLLecturerPhotoFile', this.Constants.DefaultImageState);
      });
    }
  }

  onChangeClassForTaught(classId): void {
    this.sectionList = this.ClassSectionsByVTIdMaster.filter((x) => x.ClassId === classId);
  }

  onChangeSection(sectionId) {
    const classId = this.vtGuestLectureConductedForm.get('ClassTaughtId').value;
    const studentsList = this.StudentsByVTIdMaster.filter((x) => x.ClassId === classId && x.SectionId === sectionId);
    const studentAttendancesControls = this.vtGuestLectureConductedForm.get('StudentAttendances') as FormArray;
    studentAttendancesControls.clear();

    studentsList.forEach(studentItem => {
      studentAttendancesControls.push(this.formBuilder.group(studentItem));
    });

    const initialFormValues = this.vtGuestLectureConductedForm.value;
    this.vtGuestLectureConductedForm.reset(initialFormValues);
  }

  onChangeCourseModule(moduleItem): void {
    const classId = this.vtGuestLectureConductedForm.get('ClassTaughtId').value;

    if (classId !== '' && moduleItem.Id != null) {
      var distinctUnits: any = [];

      if (moduleItem.ModuleTypeId == 121) {
        distinctUnits = this.CourseModuleUnitSessionsMaster.filter((x) => x.ClassId === classId && x.ModuleTypeId === moduleItem.ModuleTypeId);
      }
      else {
        distinctUnits = this.CourseModuleUnitSessionsMaster.filter((x) => x.ClassId === classId && x.ModuleTypeId === moduleItem.ModuleTypeId && x.SectorId === this.UserModel.SectorId);
      }

      this.unitList = distinctUnits.filter((thing, i, arr) => arr.findIndex(t => t.UnitId === thing.UnitId) === i);
    } else {
      this.unitList = [] as any;
      this.sessionList = [] as any;
    }
  }

  onChangeUnitsTaught(unitItem): void {
    const classId = this.vtGuestLectureConductedForm.get('ClassTaughtId').value;
    this.sessionList = this.CourseModuleUnitSessionsMaster.filter((x) => x.UnitId === unitItem.UnitId);
  }

  resetReportingFormGroups(): void {
    this.vtGuestLectureConductedForm.controls.GLConductedBy.disable();
    this.vtGuestLectureConductedForm.controls.GLPersonDetails.disable();

    this.vtGuestLectureConductedForm.controls.GLConductedBy.setValue('');
    this.vtGuestLectureConductedForm.controls.GLPersonDetails.setValue('');

    this.vtGuestLectureConductedForm.controls.GLName.disable();
    this.vtGuestLectureConductedForm.controls.GLMobile.disable();
    this.vtGuestLectureConductedForm.controls.GLEmail.disable();
    this.vtGuestLectureConductedForm.controls.GLQualification.disable();
    this.vtGuestLectureConductedForm.controls.GLWorkExperience.disable();
    this.vtGuestLectureConductedForm.controls.GLAddress.disable();
    this.vtGuestLectureConductedForm.controls.GLWorkStatus.disable();
    this.vtGuestLectureConductedForm.controls.GLCompany.disable();
    this.vtGuestLectureConductedForm.controls.GLDesignation.disable();
    this.vtGuestLectureConductedForm.controls.GLPhoto.disable();

    this.vtGuestLectureConductedForm.controls.GLName.setValue('');
    this.vtGuestLectureConductedForm.controls.GLMobile.setValue('');
    this.vtGuestLectureConductedForm.controls.GLEmail.setValue('');
    this.vtGuestLectureConductedForm.controls.GLQualification.setValue('');
    this.vtGuestLectureConductedForm.controls.GLWorkExperience.setValue('');
    this.vtGuestLectureConductedForm.controls.GLAddress.setValue('');
    this.vtGuestLectureConductedForm.controls.GLWorkStatus.setValue('');
    this.vtGuestLectureConductedForm.controls.GLCompany.setValue('');
    this.vtGuestLectureConductedForm.controls.GLDesignation.setValue('');
    this.vtGuestLectureConductedForm.controls.GLPhoto.setValue('');
  }

  addUnitSession() {
    const moduleCtrl = this.vtGuestLectureConductedForm.get('ModuleId');
    const unitCtrl = this.vtGuestLectureConductedForm.get('UnitId');
    const sessionIdsCtrl = this.vtGuestLectureConductedForm.get('SessionIds');

    if (moduleCtrl.value !== '' && unitCtrl.value !== '' && sessionIdsCtrl.value !== '') {
      this.unitSessionsModels.push(
        new ModuleUnitSessionModel({
          ModuleId: moduleCtrl.value.ModuleTypeId, ModuleName: moduleCtrl.value.ModuleName,
          UnitId: unitCtrl.value.UnitId, UnitName: unitCtrl.value.UnitName,
          SessionIds: sessionIdsCtrl.value.map(x => x.SessionId), SessionNames: sessionIdsCtrl.value.map(x => x.SessionName).join(', ')
        }));

      moduleCtrl.setValue('');
      unitCtrl.setValue('');
      sessionIdsCtrl.setValue('');

      this.unitList = [];
      this.sessionList = [];
    }
  }

  removeUnitSession(sessionItem) {
    const sessionIndex = this.unitSessionsModels.indexOf(sessionItem);
    this.unitSessionsModels.splice(sessionIndex, 1);
  }

  saveOrUpdateVTGuestLectureConductedDetails() {
    if (!this.vtGuestLectureConductedForm.valid) {
      this.validateDynamicFormArrayFields(this.vtGuestLectureConductedForm);
      return;
    }

    if (this.unitSessionsModels.length == 0) {
      this.showUserMessage("Please add course module, unit and sessions taught first using Plus icon!");
      return;
    }

    if (this.vtGuestLectureConductedForm.valid) {
      this.loadingController
        .create({
          message: 'Please wait...'
        })
        .then((loadEl) => {
          loadEl.present();
          this.vtGuestLectureConductedModel = this.vtGuestLectureConductedService.getGuestLectureModelFromFormGroup(this.vtGuestLectureConductedForm);
          this.vtGuestLectureConductedModel.GLPhotoFile = (this.guestLecturerPhotoFile.Base64Data != '' ? this.setUploadedFile(this.guestLecturerPhotoFile) : null);
          this.vtGuestLectureConductedModel.GLLecturerPhotoFile = (this.guestLecturerPhotoInClassFile.Base64Data != '' ? this.setUploadedFile(this.guestLecturerPhotoInClassFile) : null);
          this.vtGuestLectureConductedModel.UnitSessionsModels = this.unitSessionsModels;

          this.helperService.getCurrentCoordinates().then((res: any) => {
            this.vtGuestLectureConductedModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
            this.vtGuestLectureConductedModel.Latitude = res.coords.latitude;
            this.vtGuestLectureConductedModel.Longitude = res.coords.longitude;
            this.vtGuestLectureConductedModel.VTId = this.UserModel.UserTypeId;

            if (this.helperService.checkInternetConnection()) {
              this.vtGuestLectureConductedService.createOrUpdateVTGuestLectureConducted(this.vtGuestLectureConductedModel)
                .subscribe((vtGuestLectureConductedResp: any) => {

                  if (vtGuestLectureConductedResp.Success) {
                    const successMessages = this.helperService.getHtmlMessage(vtGuestLectureConductedResp.Messages);
                    loadEl.dismiss();
                    this.helperService.presentToast(successMessages);

                    this.unitSessionsModels = [];
                    this.file = null;
                    this.router.navigateByUrl('/folder/Home');
                  }
                  else {
                    const errorMessages = this.helperService.getHtmlMessage(vtGuestLectureConductedResp.Errors);
                    loadEl.dismiss();
                    this.helperService.showAlert(errorMessages);
                  }
                }, error => {
                  this.savingOfflineVTGuestLectureConducted();
                  loadEl.dismiss();
                  console.log('VTGuestLectureConducted errors =>', error);
                });

            } else {
              this.savingOfflineVTGuestLectureConducted();
              loadEl.dismiss();
            }
          }, (err) => {
            loadEl.dismiss();
            return;
          });
        });
    }
    else {
      const invalid = [];
      const controls = this.vtGuestLectureConductedForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          invalid.push(name);
        }
      }
      console.log(invalid);
      alert('Invalid Inputs. ' + JSON.stringify(invalid));
    }
  }

  savingOfflineVTGuestLectureConducted() {
    this.helperService.presentToast('Saving Offline...');

    const storeForm = JSON.parse(JSON.stringify(this.vtGuestLectureConductedModel));
    storeForm.MethodologyIds = JSON.stringify(this.vtGuestLectureConductedModel.MethodologyIds);
    storeForm.StudentAttendances = JSON.stringify(this.vtGuestLectureConductedModel.StudentAttendances);
    storeForm.UnitSessionsModels = JSON.stringify(this.vtGuestLectureConductedModel.UnitSessionsModels);

    if (this.guestLecturerPhotoFile.Base64Data != '') {
      storeForm.GLPhotoFile = JSON.stringify(this.vtGuestLectureConductedModel.GLPhotoFile);
    }

    if (this.guestLecturerPhotoInClassFile.Base64Data != '') {
      storeForm.GLLecturerPhotoFile = JSON.stringify(this.vtGuestLectureConductedModel.GLLecturerPhotoFile);
    }

    this.api.insertUploadTable('UploadVTGuestLectureConducted', storeForm).then(() => {
      this.unitSessionsModels = [];
      this.file = null;
      this.router.navigateByUrl('/folder/Home');
      this.helperService.presentToast('VT Guest Lecture Conducted Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('VT Guest Lecture Conducted errors =>', err);
    });
  }

  createVTGuestLectureConductedForm(): FormGroup {
    return this.formBuilder.group({
      VTGuestLectureId: new FormControl(this.vtGuestLectureConductedModel.VTGuestLectureId),
      ReportingDate: new FormControl('', Validators.required),
      ClassTaughtId: new FormControl('', Validators.required),
      SectionIds: new FormControl(),
      GLType: new FormControl('', Validators.required),
      GLTopic: new FormControl(''),
      ModuleId: new FormControl(''),
      UnitId: new FormControl(''),
      SessionIds: new FormControl(''),
      ClassTime: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
      MethodologyIds: new FormControl(''),
      GLMethodologyDetails: new FormControl('', Validators.maxLength(350)),
      GLLecturerPhotoFile: new FormControl('', Validators.required),
      GLConductedBy: new FormControl(''),
      GLPersonDetails: new FormControl('', Validators.maxLength(350)),
      GLName: new FormControl(''),
      GLMobile: new FormControl('', [Validators.pattern(this.Constants.Regex.MobileNumber), Validators.maxLength(10), Validators.minLength(10)]),
      GLEmail: new FormControl('', Validators.pattern(this.Constants.Regex.Email)),
      GLQualification: new FormControl('', Validators.maxLength(100)),
      GLAddress: new FormControl('', Validators.maxLength(350)),
      GLWorkStatus: new FormControl(''),
      GLCompany: new FormControl(''),
      GLDesignation: new FormControl(''),
      GLWorkExperience: new FormControl(''),
      GLPhotoFile: new FormControl('', Validators.required),
      IsGLLecturerPhotoFile: new FormControl(false),
      IsGLPhotoFile: new FormControl(false),
      StudentAttendances: this.formBuilder.array([])
    });
  }
}
