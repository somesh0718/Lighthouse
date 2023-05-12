import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { VTFieldIndustryVisitConductedService } from './vt-field-industry-visit-conducted.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { VTFieldIndustryVisitConductedModel } from './vt-field-industry-visit-conducted.model';
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

@Component({
  selector: 'app-vt-field-industry-visit-conducted',
  templateUrl: './vt-field-industry-visit-conducted.page.html',
  styleUrls: ['./vt-field-industry-visit-conducted.page.scss'],
})
export class VtFieldIndustryVisitConductedPage extends BasePage<VTFieldIndustryVisitConductedModel> implements OnInit {
  maxDate: any;
  file: File;
  submittedRecords: any;
  vtFieldIndustryVisitConductedModel: VTFieldIndustryVisitConductedModel;
  vtFieldIndustryVisitConductedForm: FormGroup;
  unitSessionsModels = [];
  studentAttendanceModel: StudentAttendanceModel[];
  sectionList: [];
  dataSource: any;
  displayColumns: string[] = ['StudentName', 'PresentStatus'];
  classTaughtList: [];
  moduleTaughtList: [];
  unitList: [];
  sessionList: [];
  CourseModuleUnitSessionsMaster = [] as any;
  ClassSectionsByVTIdMaster = [] as any;
  StudentsByVTIdMaster = [] as any;
  minDate: any;
  fieldVisitPhotoFile: FileUploadModel;

  constructor(
    public datepipe: DatePipe,
    private vtFieldIndustryVisitConductedService: VTFieldIndustryVisitConductedService,
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

    this.vtFieldIndustryVisitConductedModel = new VTFieldIndustryVisitConductedModel();
    this.fieldVisitPhotoFile = new FileUploadModel();
    this.vtFieldIndustryVisitConductedForm = this.createVTFieldIndustryVisitConductedForm();
  }

  ngOnInit() {
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
    });

    this.vtFieldIndustryVisitConductedForm.reset();
  }

  ionViewWillEnter() {
    this.vtFieldIndustryVisitConductedModel = new VTFieldIndustryVisitConductedModel();
    this.fieldVisitPhotoFile = new FileUploadModel();
    this.vtFieldIndustryVisitConductedForm = this.createVTFieldIndustryVisitConductedForm();

    this.loadMasters();
  }

  ionViewWillLeave() {
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
        const obsList = [this.api.loadCourseModuleUnitSessions(), this.api.loadClassSectionsByVTId(), this.api.loadStudentsByVTId()];

        forkJoin(obsList).subscribe((res: any) => {
          this.CourseModuleUnitSessionsMaster = res[0];
          const distinctModules = this.CourseModuleUnitSessionsMaster.filter((thing, i, arr) => arr.findIndex(t => t.ModuleTypeId === thing.ModuleTypeId) === i);
          this.moduleTaughtList = distinctModules;

          this.ClassSectionsByVTIdMaster = res[1];
          this.StudentsByVTIdMaster = res[2];

          const distinctClasses = this.ClassSectionsByVTIdMaster.filter((thing, i, arr) => arr.findIndex(t => t.ClassId === thing.ClassId) === i);
          this.classTaughtList = distinctClasses;

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

        this.api.selectTableData('GetVTFieldIndustryVisitConducted').then((res) => {
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
        title: 'VT Field Industry Visit Conducted',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

    });
  }

  uploadedFieldVisitPhotoFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtFieldIndustryVisitConductedForm.get('FVPictureFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.FieldVisit).then((response: FileUploadModel) => {
        this.fieldVisitPhotoFile = response;
        this.vtFieldIndustryVisitConductedForm.get('FVPictureFile').setValue(response.Base64Data);

        this.vtFieldIndustryVisitConductedForm.get('IsFVPictureFile').setValue(false);
        this.setMandatoryFieldControl(this.vtFieldIndustryVisitConductedForm, 'FVPictureFile', this.Constants.DefaultImageState);
      });
    }
  }

  onChangeClassForTaught(classId): void {
    this.sectionList = this.ClassSectionsByVTIdMaster.filter((x) => x.ClassId === classId);
  }

  onChangeSection(sectionId) {
    const classId = this.vtFieldIndustryVisitConductedForm.get('ClassTaughtId').value;

    const studentsList = this.StudentsByVTIdMaster.filter((x) => x.ClassId === classId && x.SectionId === sectionId);
    const studentAttendancesControls = this.vtFieldIndustryVisitConductedForm.get('StudentAttendances') as FormArray;
    studentAttendancesControls.clear();

    studentsList.forEach(studentItem => {
      studentAttendancesControls.push(this.formBuilder.group(studentItem));
    });

    const initialFormValues = this.vtFieldIndustryVisitConductedForm.value;
    this.vtFieldIndustryVisitConductedForm.reset(initialFormValues);
  }

  onChangeCourseModule(moduleItem): void {
    const classId = this.vtFieldIndustryVisitConductedForm.get('ClassTaughtId').value;

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
    const classId = this.vtFieldIndustryVisitConductedForm.get('ClassTaughtId').value;

    this.sessionList = this.CourseModuleUnitSessionsMaster.filter((x) => x.UnitId === unitItem.UnitId);
  }


  addUnitSession() {
    const moduleCtrl = this.vtFieldIndustryVisitConductedForm.get('ModuleId');
    const unitCtrl = this.vtFieldIndustryVisitConductedForm.get('UnitId');
    const sessionIdsCtrl = this.vtFieldIndustryVisitConductedForm.get('SessionIds');

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

  saveOrUpdateVTFieldIndustryVisitConductedDetails() {
    if (!this.vtFieldIndustryVisitConductedForm.valid) {
      this.validateDynamicFormArrayFields(this.vtFieldIndustryVisitConductedForm);
      return;
    }

    if (this.unitSessionsModels.length == 0) {
      this.showUserMessage("Please add course module, unit and sessions taught first using Plus icon!");
      return;
    }

    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        this.vtFieldIndustryVisitConductedModel = this.vtFieldIndustryVisitConductedService.getFieldIndustryVisitModelFromFormGroup(this.vtFieldIndustryVisitConductedForm);
        this.vtFieldIndustryVisitConductedModel.FVPictureFile = (this.fieldVisitPhotoFile.Base64Data != '' ? this.setUploadedFile(this.fieldVisitPhotoFile) : null);
        this.vtFieldIndustryVisitConductedModel.UnitSessionsModels = this.unitSessionsModels;

        this.helperService.getCurrentCoordinates().then((res: any) => {
          this.vtFieldIndustryVisitConductedModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
          this.vtFieldIndustryVisitConductedModel.Latitude = res.coords.latitude;
          this.vtFieldIndustryVisitConductedModel.Longitude = res.coords.longitude;
          this.vtFieldIndustryVisitConductedModel.VTId = this.UserModel.UserTypeId;

          if (this.helperService.checkInternetConnection()) {
            this.vtFieldIndustryVisitConductedService.createOrUpdateVTFieldIndustryVisitConducted(this.vtFieldIndustryVisitConductedModel)
              .subscribe((vtFieldIndustryVisitConductedResp: any) => {

                if (vtFieldIndustryVisitConductedResp.Success) {
                  const successMessages = this.helperService.getHtmlMessage(vtFieldIndustryVisitConductedResp.Messages);
                  loadEl.dismiss();
                  this.helperService.presentToast(successMessages);
                  this.unitSessionsModels = [];
                  this.file = null;
                  this.router.navigateByUrl('/folder/Home', { replaceUrl: true });
                }
                else {
                  const errorMessages = this.helperService.getHtmlMessage(vtFieldIndustryVisitConductedResp.Errors);
                  loadEl.dismiss();
                  this.helperService.showAlert(errorMessages);
                }
              }, error => {
                this.savingOfflineVTFieldIndustryVisitConducted();
                loadEl.dismiss();
                console.log('VTFieldIndustryVisitConducted errors =>', error);
              });
          } else {
            this.savingOfflineVTFieldIndustryVisitConducted();
            loadEl.dismiss();
          }
        }, (err) => {
          loadEl.dismiss();
          return;
        });
      });
  }

  savingOfflineVTFieldIndustryVisitConducted() {
    this.helperService.presentToast('Saving Offline...');

    const storeForm = JSON.parse(JSON.stringify(this.vtFieldIndustryVisitConductedModel));
    storeForm.StudentAttendances = JSON.stringify(this.vtFieldIndustryVisitConductedModel.StudentAttendances);
    storeForm.UnitSessionsModels = JSON.stringify(this.vtFieldIndustryVisitConductedModel.UnitSessionsModels);

    if (this.fieldVisitPhotoFile.Base64Data != '') {
      storeForm.FVPictureFile = JSON.stringify(this.vtFieldIndustryVisitConductedModel.FVPictureFile);
    }

    this.api.insertUploadTable('UploadVTFieldIndustryVisitConducted', storeForm).then(() => {
      this.unitSessionsModels = [];
      this.file = null;
      this.router.navigateByUrl('/folder/Home', { replaceUrl: true });
      this.helperService.presentToast('VT Field Industry Visit Conducted Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('VT Field Industry Visit Conducted errors =>', err);
    });
  }

  // Create vtFieldIndustryVisitConducted form and returns {FormGroup}
  createVTFieldIndustryVisitConductedForm(): FormGroup {
    return this.formBuilder.group({
      VTFieldIndustryVisitConductedId: new FormControl(this.vtFieldIndustryVisitConductedModel.VTFieldIndustryVisitConductedId),
      // tslint:disable-next-line: max-line-length
      ClassTaughtId: new FormControl('', Validators.required),
      ReportingDate: new FormControl('', Validators.required),
      SectionIds: new FormControl('', Validators.required),
      FieldVisitTheme: new FormControl('', Validators.maxLength(50)),
      FieldVisitActivities: new FormControl('', Validators.maxLength(50)),
      ModuleId: new FormControl('', [Validators.maxLength(50)]),
      UnitId: new FormControl('', [Validators.maxLength(50)]),
      SessionIds: new FormControl('', [Validators.maxLength(50)]),
      FVOrganisation: new FormControl('', Validators.maxLength(50)),
      FVOrganisationAddress: new FormControl('', Validators.maxLength(50)),
      FVDistance: new FormControl('', Validators.maxLength(50)),
      FVPictureFile: new FormControl(''),
      FVContactPersonName: new FormControl('', [Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      FVContactPersonMobile: new FormControl('', [Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]),
      FVContactPersonEmail: new FormControl('', Validators.pattern(this.Constants.Regex.Email)),
      FVContactPersonDesignation: new FormControl('', [Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      FVOrganisationInterestStatus: new FormControl('', Validators.maxLength(50)),
      FVOrignisationOJTStatus: new FormControl('', Validators.maxLength(50)),
      FeedbackFromOrgnisation: new FormControl('', Validators.maxLength(350)),
      Remarks: new FormControl('', Validators.maxLength(350)),
      FVStudentSafety: new FormControl('', Validators.maxLength(50)),
      IsFVPictureFile: new FormControl(false),
      StudentAttendances: this.formBuilder.array([])
    });
  }
}
