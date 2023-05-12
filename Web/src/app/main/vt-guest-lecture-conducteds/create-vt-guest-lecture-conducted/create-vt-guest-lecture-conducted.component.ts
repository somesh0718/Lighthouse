import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { BaseComponent } from 'app/common/base/base.component';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { RouteConstants } from 'app/constants/route.constant'
import { VTGuestLectureConductedService } from '../vt-guest-lecture-conducted.service';
import { VTGuestLectureConductedModel } from '../vt-guest-lecture-conducted.model';
import { DropdownModel } from 'app/models/dropdown.model';
import { ModuleUnitSessionModel } from 'app/models/module-unit-session-model';
import { StudentAttendanceModel } from 'app/models/student.attendance.model';
import { FileUploadModel } from 'app/models/file.upload.model';

@Component({
  selector: 'vt-guest-lecture-conducted',
  templateUrl: './create-vt-guest-lecture-conducted.component.html',
  styleUrls: ['./create-vt-guest-lecture-conducted.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class CreateVTGuestLectureConductedComponent extends BaseComponent<VTGuestLectureConductedModel> implements OnInit {

  vtGuestLectureConductedForm: FormGroup;
  vtGuestLectureConductedModel: VTGuestLectureConductedModel;
  unitSessionsModels: ModuleUnitSessionModel[];
  studentAttendanceModel: StudentAttendanceModel[];

  displayColumns: string[] = ['StudentName', 'PresentStatus'];
  dataSource: any;
  sectionList: DropdownModel[];
  glMethodlogyList: [DropdownModel];
  glConductedByList: [DropdownModel];
  glWorkStatusList: [DropdownModel];

  classList: [DropdownModel];
  unitList: DropdownModel[];
  sessionList: DropdownModel[];
  moduleTaughtList: [DropdownModel];
  glTypeList: [DropdownModel];
  glCompanyRequired: boolean = false;
  guestLecturerPhotoFile: FileUploadModel;
  guestLecturerPhotoInClassFile: FileUploadModel;
  minReportingDate: Date;
  otherMethodlogyId = "173"

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    private zone: NgZone,
    private route: ActivatedRoute,
    private vtGuestLectureConductedService: VTGuestLectureConductedService,
    private dialogService: DialogService,
    private formBuilder: FormBuilder) {
    super(commonService, router, routeParams, snackBar);

    // Set the default vtGuestLectureConducted Model
    this.vtGuestLectureConductedModel = new VTGuestLectureConductedModel();
    this.unitSessionsModels = <ModuleUnitSessionModel[]>[];
    this.guestLecturerPhotoFile = new FileUploadModel();
    this.guestLecturerPhotoInClassFile = new FileUploadModel();

    let dateOfAllocation = new Date(this.UserModel.DateOfAllocation);
    let maxDate = new Date(Date.now());

    let Time = maxDate.getTime() - dateOfAllocation.getTime();
    let Days = Math.floor(Time / (1000 * 3600 * 24));
    if (Days < this.Constants.BackDatedReportingDays) {
      this.minReportingDate = new Date(this.UserModel.DateOfAllocation);
    }
    else {
      let past7days = maxDate;
      past7days.setDate(past7days.getDate() - this.Constants.BackDatedReportingDays)
      this.minReportingDate = past7days;
    }
  }

  ngOnInit(): void {

    this.vtGuestLectureConductedService.getDropdownForVTGuestLectureConducted(this.UserModel).subscribe(results => {
      if (results[0].Success) {
        this.glMethodlogyList = results[0].Results;
      }

      if (results[1].Success) {
        this.glConductedByList = results[1].Results;
      }

      if (results[2].Success) {
        this.glWorkStatusList = results[2].Results;
      }

      if (results[3].Success) {
        this.glTypeList = results[3].Results;
      }

      if (results[4].Success) {
        this.classList = results[4].Results;
      }

      if (results[5].Success) {
        this.moduleTaughtList = results[5].Results;
      }

      this.route.paramMap.subscribe(params => {
        if (params.keys.length > 0) {
          this.PageRights.ActionType = params.get('actionType');

          if (this.PageRights.ActionType == this.Constants.Actions.New) {
            this.vtGuestLectureConductedModel = new VTGuestLectureConductedModel();

          } else {
            var vtGuestLectureId: string = params.get('vtGuestLectureId')

            this.vtGuestLectureConductedService.getVTGuestLectureConductedById(vtGuestLectureId)
              .subscribe((response: any) => {
                this.vtGuestLectureConductedModel = response.Result;

                if (this.PageRights.ActionType == this.Constants.Actions.Edit)
                  this.vtGuestLectureConductedModel.RequestType = this.Constants.PageType.Edit;
                else if (this.PageRights.ActionType == this.Constants.Actions.View) {
                  this.vtGuestLectureConductedModel.RequestType = this.Constants.PageType.View;
                  this.PageRights.IsReadOnly = true;
                }

                this.unitSessionsModels = this.vtGuestLectureConductedModel.UnitSessionsModels;

                this.commonService.GetSectionsByVTClassId({ DataId: this.UserModel.UserTypeId, DataId1: this.vtGuestLectureConductedModel.ClassTaughtId }).subscribe(response => {
                  if (response.Success) {
                    this.sectionList = response.Results;

                    this.vtGuestLectureConductedForm = this.createVTGuestLectureConductedForm();
                  }
                });
              });
          }
        }
      });
    });

    this.vtGuestLectureConductedForm = this.createVTGuestLectureConductedForm();
    this.onChangeGuestLectureType();
    this.onChangeGLWorkStatus();
  }

  onChangeReportingDate(): boolean {
    let reportingDate = this.vtGuestLectureConductedForm.get('ReportingDate').value;

    if (reportingDate != null && new Date(reportingDate).getDay() == 0) {
      this.dialogService.openShowDialog("User cannot submit the VT Guest Lecture Conducted on Sunday");
      return false
    }
    return true
  }

  onChangeClassForTaught(classId): void {
    if (classId != null) {
      this.commonService.GetSectionsByVTClassId({ DataId: this.UserModel.UserTypeId, DataId1: classId }).subscribe(response => {
        if (response.Success) {
          this.sectionList = response.Results;
        }
      });

      let moduleItem = this.vtGuestLectureConductedForm.get('ModuleId').value;
      if (moduleItem != null && moduleItem.Id != null) {
        this.onChangeCourseModule(moduleItem);
      }
    }

    this.sectionList = <DropdownModel[]>[];
    this.unitList = <DropdownModel[]>[];
    this.sessionList = <DropdownModel[]>[];
    this.unitSessionsModels = <ModuleUnitSessionModel[]>[];

    this.vtGuestLectureConductedForm.get("SectionIds").setValue(null);

    let studentAttendancesControls = <FormArray>this.vtGuestLectureConductedForm.get('StudentAttendances');
    studentAttendancesControls.clear();
  }

  onChangeSectionForTaught(sectionId) {
    if (sectionId != null) {
      let classId = this.vtGuestLectureConductedForm.get("ClassTaughtId").value;

      this.commonService.getStudentsByClassId({ DataId: this.UserModel.UserTypeId, DataId1: classId, DataId2: sectionId }).subscribe(response => {
        if (response.Success) {
          let studentAttendancesControls = <FormArray>this.vtGuestLectureConductedForm.get('StudentAttendances');
          studentAttendancesControls.clear();

          response.Results.forEach(studentItem => {
            studentAttendancesControls.push(this.formBuilder.group(studentItem));
          });

          let initialFormValues = this.vtGuestLectureConductedForm.value;
          this.vtGuestLectureConductedForm.reset(initialFormValues);
        }
      });
    }
    else {
      let studentAttendancesControls = <FormArray>this.vtGuestLectureConductedForm.get('StudentAttendances');
      studentAttendancesControls.clear();
    }
  }

  onChangeCourseModule(moduleItem): void {
    let classId = this.vtGuestLectureConductedForm.get('ClassTaughtId').value;

    if (classId != '' && moduleItem.Id != null) {
      this.commonService.GetUnitsByClassAndModuleId({ DataId: classId, DataId1: moduleItem.Id, DataId2: this.UserModel.UserTypeId, SelectTitle: 'Unit Taught' }).subscribe((response: any) => {
        if (response.Success) {
          this.unitList = response.Results;
        }
      });
    }
    else {
      this.unitList = <DropdownModel[]>[];
      this.sessionList = <DropdownModel[]>[];
    }
  }

  onChangeUnitsTaught(unitItem): void {
    let classId = this.vtGuestLectureConductedForm.get('ClassTaughtId').value;

    if (classId != '' && unitItem.Id != null) {
      this.commonService.GetSessionsByUnitId({ DataId: unitItem.Id, SelectTitle: 'Sessions Taught' }).subscribe((response: any) => {
        if (response.Success) {
          this.sessionList = response.Results;
        }
      });
    }
    else {
      this.sessionList = <DropdownModel[]>[];
    }
  }

  addUnitSession() {
    let moduleCtrl = this.vtGuestLectureConductedForm.get('ModuleId');
    let unitCtrl = this.vtGuestLectureConductedForm.get('UnitId');
    let sessionIdsCtrl = this.vtGuestLectureConductedForm.get('SessionIds');

    if (moduleCtrl.value !== '' && unitCtrl.value !== '' && sessionIdsCtrl.value !== '') {
      this.unitSessionsModels.push(
        new ModuleUnitSessionModel({
          ModuleId: moduleCtrl.value.Id, ModuleName: moduleCtrl.value.Name,
          UnitId: unitCtrl.value.Id, UnitName: unitCtrl.value.Name,
          SessionIds: sessionIdsCtrl.value.map(x => x.Id), SessionNames: sessionIdsCtrl.value.map(x => x.Name).join(", ")
        }));

      moduleCtrl.setValue('');
      unitCtrl.setValue('');
      sessionIdsCtrl.setValue('');

      this.unitList = <DropdownModel[]>[];
      this.sessionList = <DropdownModel[]>[];
    }
  }

  removeUnitSession(sessionItem) {
    const sessionIndex = this.unitSessionsModels.indexOf(sessionItem);
    this.unitSessionsModels.splice(sessionIndex, 1);
  }

  uploadedGuestLecturerPhotoFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtGuestLectureConductedForm.get('GLPhotoFile').setValue(null);
        this.dialogService.openShowDialog("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.GuestLecture).then((response: FileUploadModel) => {
        this.guestLecturerPhotoFile = response;

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
        this.dialogService.openShowDialog("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.GuestLecture).then((response: FileUploadModel) => {
        this.guestLecturerPhotoInClassFile = response;

        this.vtGuestLectureConductedForm.get('IsGLLecturerPhotoFile').setValue(false);
        this.setMandatoryFieldControl(this.vtGuestLectureConductedForm, 'GLLecturerPhotoFile', this.Constants.DefaultImageState);
      });
    }
  }

  onGLMethodologyChange(selectedGLMethodologyIds) {
    if (selectedGLMethodologyIds.length == 0) {
      this.glMethodlogyList.forEach(methodologyItem => {
        methodologyItem.IsDisabled = false;
      });
    }
    else {
      if (selectedGLMethodologyIds[0] == this.otherMethodlogyId) {
        this.glMethodlogyList.forEach(methodologyItem => {
          if (methodologyItem.Id != selectedGLMethodologyIds[0]) {
            methodologyItem.IsDisabled = true;
          }
        });
      }
      else {
        let methodologyItem = this.glMethodlogyList.find(s => s.Id == this.otherMethodlogyId);
        methodologyItem.IsDisabled = true;
      }
    }
  }

  saveOrUpdateVTGuestLectureConductedDetails() {
    if (!this.vtGuestLectureConductedForm.valid) {
      this.validateAllFormFields(this.vtGuestLectureConductedForm);
      console.log("Guest Lecture Saving Errors : ", this.Errors);
      return;
    }

    if (this.unitSessionsModels.length == 0) {
      this.dialogService.openShowDialog("Please add course module taught first!");
      return;
    }

    this.vtGuestLectureConductedModel = this.vtGuestLectureConductedService.getGuestLectureModelFromFormGroup(this.vtGuestLectureConductedForm);
    this.vtGuestLectureConductedModel.UnitSessionsModels = this.unitSessionsModels;
    this.vtGuestLectureConductedModel.VTId = this.UserModel.UserTypeId;
    this.vtGuestLectureConductedModel.GLPhotoFile = (this.guestLecturerPhotoFile.Base64Data != '' ? this.setUploadedFile(this.guestLecturerPhotoFile) : null);
    this.vtGuestLectureConductedModel.GLLecturerPhotoFile = (this.guestLecturerPhotoInClassFile.Base64Data != '' ? this.setUploadedFile(this.guestLecturerPhotoInClassFile) : null);

    this.vtGuestLectureConductedService.createOrUpdateVTGuestLectureConducted(this.vtGuestLectureConductedModel)
      .subscribe((vtGuestLectureConductedResp: any) => {

        if (vtGuestLectureConductedResp.Success) {
          this.zone.run(() => {
            this.showActionMessage(
              this.Constants.Messages.RecordSavedMessage,
              this.Constants.Html.SuccessSnackbar
            );

            this.router.navigate([RouteConstants.VTGuestLectureConducted.List]);
          });
        }
        else {
          var errorMessages = this.getHtmlMessage(vtGuestLectureConductedResp.Errors)
          this.dialogService.openShowDialog(errorMessages);
        }
      }, error => {
        console.log('VTGuestLectureConducted deletion errors =>', error);
      });
  }

  //Create vtGuestLectureConducted form and returns {FormGroup}
  createVTGuestLectureConductedForm(): FormGroup {
    return this.formBuilder.group({
      VTGuestLectureId: new FormControl(this.vtGuestLectureConductedModel.VTGuestLectureId),
      ReportingDate: new FormControl({ value: new Date(this.vtGuestLectureConductedModel.ReportingDate), disabled: this.PageRights.IsReadOnly }, Validators.required),
      ClassTaughtId: new FormControl({ value: this.vtGuestLectureConductedModel.ClassTaughtId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      SectionIds: new FormControl({ value: this.vtGuestLectureConductedModel.SectionIds, disabled: this.PageRights.IsReadOnly }),
      GLType: new FormControl({ value: this.vtGuestLectureConductedModel.GLType, disabled: this.PageRights.IsReadOnly }, [Validators.required]),
      GLTopic: new FormControl({ value: this.vtGuestLectureConductedModel.GLTopic, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(150)),
      ModuleId: new FormControl({ value: this.vtGuestLectureConductedModel.ModuleId, disabled: this.PageRights.IsReadOnly }),
      UnitId: new FormControl({ value: this.vtGuestLectureConductedModel.UnitId, disabled: this.PageRights.IsReadOnly }),
      SessionIds: new FormControl({ value: this.vtGuestLectureConductedModel.SessionIds, disabled: this.PageRights.IsReadOnly }),
      ClassTime: new FormControl({ value: this.vtGuestLectureConductedModel.ClassTime, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(50)),
      MethodologyIds: new FormControl({ value: this.vtGuestLectureConductedModel.MethodologyIds, disabled: this.PageRights.IsReadOnly }),
      GLMethodologyDetails: new FormControl({ value: this.vtGuestLectureConductedModel.GLMethodologyDetails, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(350)),
      GLLecturerPhotoFile: new FormControl({ value: this.vtGuestLectureConductedModel.GLLecturerPhotoFile, disabled: this.PageRights.IsReadOnly }, Validators.required),
      GLConductedBy: new FormControl({ value: this.vtGuestLectureConductedModel.GLConductedBy, disabled: this.PageRights.IsReadOnly }),
      GLPersonDetails: new FormControl({ value: this.vtGuestLectureConductedModel.GLPersonDetails, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(350)),
      GLName: new FormControl({ value: this.vtGuestLectureConductedModel.GLName, disabled: this.PageRights.IsReadOnly }),
      GLMobile: new FormControl({ value: this.vtGuestLectureConductedModel.GLMobile, disabled: this.PageRights.IsReadOnly }, [Validators.pattern(this.Constants.Regex.MobileNumber), Validators.maxLength(10), Validators.minLength(10)]),
      GLEmail: new FormControl({ value: this.vtGuestLectureConductedModel.GLEmail, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(100), Validators.pattern(this.Constants.Regex.Email)]),
      GLQualification: new FormControl({ value: this.vtGuestLectureConductedModel.GLQualification, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(100)),
      GLAddress: new FormControl({ value: this.vtGuestLectureConductedModel.GLAddress, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(350)),
      GLWorkStatus: new FormControl({ value: this.vtGuestLectureConductedModel.GLWorkStatus, disabled: this.PageRights.IsReadOnly }),
      GLCompany: new FormControl({ value: this.vtGuestLectureConductedModel.GLCompany, disabled: this.PageRights.IsReadOnly },),
      GLDesignation: new FormControl({ value: this.vtGuestLectureConductedModel.GLDesignation, disabled: this.PageRights.IsReadOnly }),
      GLWorkExperience: new FormControl({ value: this.vtGuestLectureConductedModel.GLWorkExperience, disabled: this.PageRights.IsReadOnly }),
      GLPhotoFile: new FormControl({ value: this.vtGuestLectureConductedModel.GLPhotoFile, disabled: this.PageRights.IsReadOnly }, Validators.required),
      IsGLLecturerPhotoFile: new FormControl({ value: false, disabled: this.PageRights.IsReadOnly }),
      IsGLPhotoFile: new FormControl({ value: false, disabled: this.PageRights.IsReadOnly }),
      StudentAttendances: this.formBuilder.array(this.vtGuestLectureConductedModel.StudentAttendances.map(studentModel => this.setStudentAttendanceFormGroup(studentModel)))
    });

  }

  private onChangeGuestLectureType() {
    this.vtGuestLectureConductedForm.get("GLType").valueChanges
      .subscribe(data => {

        if (data == "181") {
          this.vtGuestLectureConductedForm.controls["GLConductedBy"].setValidators([Validators.required, Validators.maxLength(100)]);
          this.vtGuestLectureConductedForm.controls["GLPersonDetails"].setValidators([Validators.required, Validators.maxLength(100)]);
          this.vtGuestLectureConductedForm.controls["GLName"].clearValidators();
          this.vtGuestLectureConductedForm.controls["GLQualification"].clearValidators();
          this.vtGuestLectureConductedForm.controls["GLMobile"].clearValidators();
          this.vtGuestLectureConductedForm.controls["GLWorkExperience"].clearValidators();
          this.vtGuestLectureConductedForm.controls["GLWorkStatus"].clearValidators();
          this.vtGuestLectureConductedForm.controls["GLPhotoFile"].clearValidators();

        } else if (data == "180") {
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
      });
  }

  private onChangeGLWorkStatus() {
    this.vtGuestLectureConductedForm.get("GLWorkStatus").valueChanges
      .subscribe(workStatusId => {

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
      });
  }

  private setStudentAttendanceFormGroup(studentAttendanceItem) {
    return this.formBuilder.group({
      StudentId: this.formBuilder.control({ value: studentAttendanceItem.StudentId, disabled: true }),
      ClassId: this.formBuilder.control({ value: studentAttendanceItem.ClassId, disabled: true }),
      StudentName: this.formBuilder.control({ value: studentAttendanceItem.StudentName, disabled: true }),
      IsPresent: this.formBuilder.control({ value: studentAttendanceItem.IsPresent, disabled: true })
    });
  }
}
