import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { BaseComponent } from 'app/common/base/base.component';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { RouteConstants } from 'app/constants/route.constant'
import { StudentClassService } from '../student-class.service';
import { StudentClassModel } from '../student-class.model';
import { DropdownModel } from 'app/models/dropdown.model';
import { TreeMapModule } from '@swimlane/ngx-charts';

@Component({
  selector: 'student-class',
  templateUrl: './create-student-class.component.html',
  styleUrls: ['./create-student-class.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class CreateStudentClassComponent extends BaseComponent<StudentClassModel> implements OnInit {
  studentClassForm: FormGroup;
  studentClassModel: StudentClassModel;
  currentAcademicYearId: string;
  academicYearAllList: [DropdownModel];
  academicYearList: [DropdownModel];
  classList: [DropdownModel];
  sectionList: [DropdownModel];
  genderList: [DropdownModel];

  vtpList: DropdownModel[];
  vcList: DropdownModel[];
  vtList: DropdownModel[];

  schoolList: DropdownModel[];
  filteredSchoolItems: any;
  userInfoForVT: any;
  noSectionId = "40b2d9eb-0dbf-11eb-ba32-0a761174c048";

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    private zone: NgZone,
    private route: ActivatedRoute,
    private studentClassService: StudentClassService,
    private dialogService: DialogService,
    private formBuilder: FormBuilder) {
    super(commonService, router, routeParams, snackBar);

    // Set the default studentClass Model
    this.studentClassModel = new StudentClassModel();
    this.studentClassForm = this.createStudentClassForm();
  }

  ngOnInit(): void {
    this.studentClassService.getDropdownforStudentClass(this.UserModel).subscribe((results) => {

      if (results[0].Success) {
        this.academicYearList = results[0].Results;
      }

      if (results[3].Success) {
        this.genderList = results[3].Results;
      }

      if (results[1].Success) {
        this.vtpList = results[1].Results;
      }

      if (results[4].Success) {
        this.academicYearAllList = results[4].Results;
      }

      this.currentAcademicYearId = this.UserModel.AcademicYearId;

      this.route.paramMap.subscribe(params => {
        if (params.keys.length > 0) {

          this.PageRights.ActionType = params.get('actionType');
          this.studentClassModel = new StudentClassModel();
          this.studentClassModel.AcademicYearId = this.UserModel.AcademicYearId;

          if (this.PageRights.ActionType == this.Constants.Actions.New) {

            if (this.UserModel.RoleCode == 'VC') {
              this.commonService.getVocationalTrainingProvidersByUserId(this.UserModel).then(vtpResp => {

                this.studentClassModel.VTPId = vtpResp[0].Id;
                this.studentClassModel.VCId = this.UserModel.UserTypeId;

                this.onChangeVTP(this.studentClassModel.VTPId).then(vcResp => {
                  this.studentClassForm = this.createStudentClassForm();

                  this.onChangeVC(this.studentClassModel.VCId);
                });
              });
            } else if (this.UserModel.RoleCode == 'VT') {
              this.studentClassModel.VTId = this.UserModel.UserTypeId;
              this.studentClassModel.AcademicYearId = this.UserModel.AcademicYearId;

              this.commonService.GetVTPVCAndSchoolIdByVTId(this.UserModel.UserTypeId).subscribe((response) => {
                if (response.Success) {
                  this.userInfoForVT = response.Result;

                  this.studentClassModel.VTPId = response.Result.VTPId;
                  this.studentClassModel.VCId = response.Result.VCId;
                  this.studentClassModel.SchoolId = response.Result.SchoolId;

                  this.onChangeVocationalTrainer(this.studentClassModel.VTId).then(sResp => {
                  });
                }
              });
            }

          } else {
            var studentId: string = params.get('studentId')

            this.studentClassService.getStudentClassById(studentId)
              .subscribe((response: any) => {

                this.studentClassModel = response.Result;

                if (this.PageRights.ActionType == this.Constants.Actions.Edit) {
                  this.studentClassModel.RequestType = this.Constants.PageType.Edit;
                  this.setDropoutReasonValidators();
                }
                else if (this.PageRights.ActionType == this.Constants.Actions.View) {
                  this.studentClassModel.RequestType = this.Constants.PageType.View;
                  this.PageRights.IsReadOnly = true;
                }

                this.onChangeAcademicYear(this.studentClassModel.AcademicYearId).then(vResp => {
                  this.onChangeVTP(this.studentClassModel.VTPId).then(vtpResp => {
                    this.onChangeVC(this.studentClassModel.VCId).then(vvResp => {
                      this.onChangeSchool(this.studentClassModel.SchoolId).then(sResp => {
                        this.onChangeVocationalTrainer(this.studentClassModel.VTId).then(sResp => {
                          this.onChangeClass(this.studentClassModel.ClassId).then(cResp => {
                            this.studentClassForm = this.createStudentClassForm();
                          });
                        });
                      });
                    });
                  });
                });
              });
          }
        }
      });
    });
  }

  onChangeAcademicYear(academicYearId): Promise<any> {
    this.IsLoading = true;
    this.studentClassModel.AcademicYearId = academicYearId;

    let promise = new Promise((resolve, reject) => {

      this.commonService.GetVTPByAYId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.UserModel.AcademicYearId).subscribe((response) => {
        if (response.Success) {
          this.vtpList = response.Results;

          this.studentClassForm.controls["VTPId"].setValue(null);
          this.studentClassForm.controls["VCId"].setValue(null);
          this.studentClassForm.controls["SchoolId"].setValue(null);
          this.studentClassForm.controls["VTId"].setValue(null);
          this.studentClassForm.controls["ClassId"].setValue(null);
          this.studentClassForm.controls["SectionId"].setValue(null);
        }
        resolve(true);
      });
    });
    return promise;
  }

  onChangeVTP(vtpId): Promise<any> {
    this.IsLoading = true;
    this.studentClassModel.VTPId = vtpId;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetVCByAYAndVTPId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.currentAcademicYearId, this.studentClassModel.VTPId).subscribe((response: any) => {
        if (response.Success) {
          this.vtList = [];
          this.vcList = [];
          this.filteredSchoolItems = [];

          this.vcList = response.Results;

          this.studentClassForm.controls["VCId"].setValue(null);
          this.studentClassForm.controls["SchoolId"].setValue(null);
          this.studentClassForm.controls["VTId"].setValue(null);
          this.studentClassForm.controls["ClassId"].setValue(null);
          this.studentClassForm.controls["SectionId"].setValue(null);
        }
        resolve(true);
      });
    });
    return promise;
  }

  onChangeVC(vcId): Promise<any> {
    this.IsLoading = true;
    this.studentClassModel.VCId = vcId;

    let promise = new Promise((resolve, reject) => {

      let schoolRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        schoolRequest = this.commonService.GetSchoolByHMId(this.currentAcademicYearId, this.UserModel.UserTypeId, this.studentClassModel.VCId);
      }
      else {
        schoolRequest = this.commonService.GetMasterDataByType({ DataType: 'SchoolsByVC', ParentId: this.studentClassModel.VCId, SelectTitle: 'School' }, false);
      }

      schoolRequest.subscribe((response: any) => {
        if (response.Success) {
          this.schoolList = response.Results;
          this.filteredSchoolItems = this.schoolList.slice();
          this.IsLoading = false;

          this.studentClassForm.controls["SchoolId"].setValue(null);
          this.studentClassForm.controls["VTId"].setValue(null);
          this.studentClassForm.controls["ClassId"].setValue(null);
          this.studentClassForm.controls["SectionId"].setValue(null);
        }
        resolve(true);
      });
    });
    return promise;
  }

  onChangeSchool(schoolId): Promise<any> {
    this.IsLoading = true;
    this.studentClassModel.SchoolId = schoolId;

    let promise = new Promise((resolve, reject) => {

      let vtParams = {
        DataId: this.UserModel.RoleCode,
        DataId1: this.UserModel.UserTypeId,
        DataId2: this.studentClassModel.AcademicYearId,
        DataId3: this.studentClassModel.VTPId,
        DataId4: this.studentClassModel.VCId,
        DataId5: this.studentClassModel.SchoolId,
      };

      this.commonService.GetVTByAYAndSchoolId(vtParams).subscribe((response: any) => {
        if (response.Success) {
          this.vtList = response.Results;

          this.studentClassForm.controls["VTId"].setValue(null);
          this.studentClassForm.controls["ClassId"].setValue(null);
          this.studentClassForm.controls["SectionId"].setValue(null);
        }
        this.IsLoading = false;
        resolve(true);
      });
    });
    return promise;
  }

  onChangeVocationalTrainer(vtId) {
    this.studentClassModel.VTId = vtId;

    this.IsLoading = true;
    let promise = new Promise((resolve, reject) => {

      this.commonService.GetMasterDataByType({ DataType: 'ClassesByVT', ParentId: this.currentAcademicYearId, UserId: this.studentClassModel.VTId, SelectTitle: 'Class' }).subscribe((response) => {
        if (response.Success) {
          this.classList = response.Results;

          this.studentClassForm.controls["ClassId"].setValue(null);
          this.studentClassForm.controls["SectionId"].setValue(null);
        }
        resolve(true);
      });
    });
    return promise;
  }

  onChangeClass(classId): Promise<any> {
    this.IsLoading = true;
    let promise = new Promise((resolve, reject) => {

      this.commonService.GetMasterDataByType({ DataType: 'SectionsByVT', ParentId: classId, UserId: this.studentClassModel.VTId, SelectTitle: 'Section' }).subscribe((response) => {
        if (response.Success) {
          this.sectionList = response.Results;

          this.studentClassForm.controls["SectionId"].setValue(null);
        }
        resolve(true);
      });
    });
    return promise;
  }

  saveOrUpdateStudentClassDetails() {
    if (!this.studentClassForm.valid) {
      this.validateAllFormFields(this.studentClassForm);
      return;
    }

    this.setValueFromFormGroup(this.studentClassForm, this.studentClassModel);

    if (this.UserModel.RoleCode == 'VT' && this.PageRights.ActionType == this.Constants.Actions.New) {
      this.studentClassModel.AcademicYearId = this.userInfoForVT.AcademicYearId;
      this.studentClassModel.VTPId = this.userInfoForVT.VTPId;
      this.studentClassModel.VCId = this.userInfoForVT.VCId;
      this.studentClassModel.SchoolId = this.userInfoForVT.SchoolId;
      this.studentClassModel.VTId = this.userInfoForVT.VTId;
    }

    const dateOfDropoutControl = this.studentClassForm.get('DateOfDropout');
    if (dateOfDropoutControl.value == null) {
      this.studentClassModel.DateOfDropout = null;
      this.studentClassModel.DropoutReason = null;
    }

    this.studentClassService.createOrUpdateStudentClass(this.studentClassModel)
      .subscribe((studentClassResp: any) => {

        if (studentClassResp.Success) {
          this.zone.run(() => {
            this.showActionMessage(
              this.Constants.Messages.RecordSavedMessage,
              this.Constants.Html.SuccessSnackbar
            );

            this.router.navigate([RouteConstants.StudentClass.List]);
          });
        }
        else {
          var errorMessages = this.getHtmlMessage(studentClassResp.Errors)
          this.dialogService.openShowDialog(errorMessages);
        }
      }, error => {
        console.log('StudentClass deletion errors =>', error);
      });
  }

  setDropoutReasonValidators() {
    const dropoutReasonControl = this.studentClassForm.get('DropoutReason');

    this.studentClassForm.get('DateOfDropout').valueChanges
      .subscribe(dateOfDropoutValue => {

        dropoutReasonControl.clearValidators();
        if (dateOfDropoutValue == null || dateOfDropoutValue == '') {
          dropoutReasonControl.setValidators([Validators.maxLength(350)]);
        }
        else {
          dropoutReasonControl.setValidators([Validators.required, Validators.maxLength(350)]);
        }
        dropoutReasonControl.updateValueAndValidity();

        const dateOfDropoutControl = this.studentClassForm.get('DateOfDropout');
        if (dateOfDropoutControl.value != null && dateOfDropoutControl.value != '') {
          dateOfDropoutControl.disable();
        }
        else {
          dateOfDropoutControl.enable();
        }
      });

    if (this.studentClassModel.DropoutReason == null || this.studentClassModel.DropoutReason == '') {
      this.studentClassForm.get('IsActive').enable();
    }
    else {
      this.studentClassForm.get('IsActive').disable();
    }
  }

  //Create studentClass form and returns {FormGroup}
  createStudentClassForm(): FormGroup {
    return this.formBuilder.group({
      StudentId: new FormControl(this.studentClassModel.StudentId),
      AcademicYearId: new FormControl({ value: this.studentClassModel.AcademicYearId, disabled: this.PageRights.IsReadOnly }),
      ClassId: new FormControl({ value: this.studentClassModel.ClassId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      SectionId: new FormControl({ value: this.studentClassModel.SectionId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      FirstName: new FormControl({ value: this.studentClassModel.FirstName, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(100), Validators.pattern(this.Constants.Regex.CharWithTitleCaseSpaceAndSpecialChars)]),
      MiddleName: new FormControl({ value: this.studentClassModel.MiddleName, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharWithTitleCaseSpaceAndSpecialChars)]),
      LastName: new FormControl({ value: this.studentClassModel.LastName, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharWithTitleCaseSpaceAndSpecialChars)]),
      FullName: new FormControl({ value: this.studentClassModel.FullName, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(150)]),
      Gender: new FormControl({ value: this.studentClassModel.Gender, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(10)]),
      Mobile: new FormControl({ value: this.studentClassModel.Mobile, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]),
      DateOfEnrollment: new FormControl({ value: new Date(this.studentClassModel.DateOfEnrollment), disabled: this.PageRights.IsReadOnly }, Validators.required),
      DateOfDropout: new FormControl({ value: this.getDateValue(this.studentClassModel.DateOfDropout), disabled: this.PageRights.IsReadOnly }),
      DropoutReason: new FormControl({ value: this.studentClassModel.DropoutReason, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(350)]),
      IsActive: new FormControl({ value: this.studentClassModel.IsActive, disabled: this.PageRights.IsReadOnly }),

      //For PMU-Admin
      VTPId: new FormControl({ value: this.studentClassModel.VTPId, disabled: this.PageRights.IsReadOnly }),
      VCId: new FormControl({ value: this.studentClassModel.VCId, disabled: this.PageRights.IsReadOnly }),
      SchoolId: new FormControl({ value: this.studentClassModel.SchoolId, disabled: this.PageRights.IsReadOnly }),
      VTId: new FormControl({ value: (this.UserModel.RoleCode == 'VT' ? this.UserModel.UserTypeId : this.studentClassModel.VTId), disabled: this.PageRights.IsReadOnly }),
    });
  }
}
