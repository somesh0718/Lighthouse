import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { BaseComponent } from 'app/common/base/base.component';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { RouteConstants } from 'app/constants/route.constant'
import { StudentClassDetailService } from '../student-class-detail.service';
import { StudentClassDetailModel } from '../student-class-detail.model';
import { DropdownModel } from 'app/models/dropdown.model';

@Component({
  selector: 'student-class-detail',
  templateUrl: './create-student-class-detail.component.html',
  styleUrls: ['./create-student-class-detail.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class CreateStudentClassDetailComponent extends BaseComponent<StudentClassDetailModel> implements OnInit {
  studentClassDetailForm: FormGroup;
  studentClassDetailModel: StudentClassDetailModel;
  studentList: DropdownModel[];
  socialCategoryList: DropdownModel[];
  religionList: DropdownModel[];
  academicYearList: DropdownModel[];
  currentAcademicYearId: string;
  classList: DropdownModel[];
  sectionList: DropdownModel[];
  jobRoleList: DropdownModel[];
  sectorList: DropdownModel[];
  vtpList: DropdownModel[];
  vcList: DropdownModel[];
  vtList: DropdownModel[];
  streamList: DropdownModel[];
  schoolList: DropdownModel[];
  filteredSchoolItems: any;
  filteredStudentItems: any;

  noSectionId = "40b2d9eb-0dbf-11eb-ba32-0a761174c048";
  classId: any;
  sectorId: string;
  jobRoleId: string;
  userInfoForVT: any;
  isDisableStudentTrade: boolean = false;

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    private zone: NgZone,
    private route: ActivatedRoute,
    private studentClassDetailService: StudentClassDetailService,
    private dialogService: DialogService,
    private formBuilder: FormBuilder) {
    super(commonService, router, routeParams, snackBar);

    // Set the default studentClassDetail Model
    this.studentClassDetailModel = new StudentClassDetailModel();
    this.studentClassDetailForm = this.createStudentClassDetailForm();
  }

  ngOnInit(): void {
    this.studentClassDetailService.getDropdownforStudentClassDetails(this.UserModel).subscribe((results) => {
      if (results[1].Success) {
        this.socialCategoryList = results[1].Results;
      }

      if (results[2].Success) {
        this.religionList = results[2].Results;
      }

      if (results[3].Success) {
        this.academicYearList = results[3].Results;
      }

      this.currentAcademicYearId = this.UserModel.AcademicYearId;

      if (results[7].Success) {
        this.vtpList = results[7].Results;
      }

      if (this.UserModel.RoleCode == 'VT') {
        if (results[8].Success) {

          this.schoolList = results[8].Results;
          if (this.schoolList.length > 0) {
            this.studentClassDetailModel.SchoolId = this.schoolList[0].Id;
          }

          this.filteredSchoolItems = this.schoolList.slice();
        }
      }

      if (results[9].Success) {
        this.streamList = results[9].Results;
      }

      this.route.paramMap.subscribe(params => {
        if (params.keys.length > 0) {
          this.PageRights.ActionType = params.get('actionType');
          this.studentClassDetailModel = new StudentClassDetailModel();
          this.studentClassDetailModel.AcademicYearId = this.UserModel.AcademicYearId;

          if (this.PageRights.ActionType == this.Constants.Actions.New) {

            if (this.UserModel.RoleCode == 'VC') {
              this.commonService.getVocationalTrainingProvidersByUserId(this.UserModel).then(vtpResp => {
                this.studentClassDetailModel.VTPId = vtpResp[0].Id;
                this.studentClassDetailModel.VCId = this.UserModel.UserTypeId;

                this.onChangeVTP(this.studentClassDetailModel.VTPId).then(vcResp => {
                  this.studentClassDetailForm = this.createStudentClassDetailForm();

                  this.onChangeVC(this.studentClassDetailModel.VCId);
                });
              });

            } else if (this.UserModel.RoleCode == 'VT') {
              this.studentClassDetailModel.VTId = this.UserModel.UserTypeId;
              this.studentClassDetailModel.AcademicYearId = this.UserModel.AcademicYearId;

              this.commonService.GetVTPVCAndSchoolIdByVTId(this.UserModel.UserTypeId).subscribe((response) => {
                if (response.Success) {
                  this.userInfoForVT = response.Result;

                  this.studentClassDetailModel.VTPId = response.Result.VTPId;
                  this.studentClassDetailModel.VCId = response.Result.VCId;
                  this.studentClassDetailModel.SchoolId = response.Result.SchoolId;
                }
              });
            }
          } else {
            var studentId: string = params.get('studentId')

            this.studentClassDetailService.getStudentClassDetailById(studentId)
              .subscribe((response: any) => {
                this.studentClassDetailModel = response.Result;

                if (this.PageRights.ActionType == this.Constants.Actions.Edit)
                  this.studentClassDetailModel.RequestType = this.Constants.PageType.Edit;
                else if (this.PageRights.ActionType == this.Constants.Actions.View) {
                  this.studentClassDetailModel.RequestType = this.Constants.PageType.View;
                  this.PageRights.IsReadOnly = true;
                }

                this.studentClassDetailModel.VTId = this.studentClassDetailModel.VTId;
                this.studentClassDetailModel.AcademicYearId = this.studentClassDetailModel.AcademicYearId;
                this.studentClassDetailModel.SchoolId = this.studentClassDetailModel.SchoolId;
                this.classId = this.studentClassDetailModel.ClassId;

                this.onChangeAcademicYear(this.studentClassDetailModel.AcademicYearId).then(vResp => {
                  this.onChangeVTP(this.studentClassDetailModel.VTPId).then(vtpResp => {
                    this.onChangeVC(this.studentClassDetailModel.VCId).then(vvResp => {
                      this.onChangeSchool(this.studentClassDetailModel.SchoolId).then(sResp => {
                        this.onChangeVocationalTrainer(this.studentClassDetailModel.VTId).then(sResp => {
                          this.onChangeClass(this.studentClassDetailModel.ClassId).then(cResp => {
                            this.onChangeSection(this.studentClassDetailModel.SectionId).then(cResp => {
                              this.studentClassDetailForm = this.createStudentClassDetailForm();
                              this.onChangeAssessmentConducted(this.studentClassDetailModel.AssessmentConducted);

                              this.studentClassDetailForm.get('SectorId').setValue(this.sectorId);
                              this.studentClassDetailForm.get('JobRoleId').setValue(this.jobRoleId);
                            });
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
    this.studentClassDetailModel.AcademicYearId = academicYearId;

    this.IsLoading = true;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetVTPByAYId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.studentClassDetailModel.AcademicYearId).subscribe((response) => {
        if (response.Success) {
          this.vtpList = response.Results;
        }

        this.resetFormControls('AY');
        this.IsLoading = false;
        resolve(true);
      });
    });

    return promise;
  }

  onChangeAssessmentConducted(AssessmentConductedValue) {
    if (AssessmentConductedValue === 'Yes') {
      this.studentClassDetailForm.controls["Mobile1"].setValidators([Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]);
    }
    else {
      this.studentClassDetailForm.controls["Mobile1"].setValidators([Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]);
    }

    this.studentClassDetailForm.controls["Mobile1"].updateValueAndValidity();
  }

  onChangeVTP(vtpId): Promise<any> {
    this.IsLoading = true;
    this.studentClassDetailModel.VTPId = vtpId;

    let promise = new Promise((resolve, reject) => {

      this.commonService.GetVCByAYAndVTPId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.currentAcademicYearId, this.studentClassDetailModel.VTPId).subscribe((response: any) => {
        if (response.Success) {
          this.vcList = response.Results;
        }

        this.resetFormControls('VTP');
        this.IsLoading = false;
        resolve(true);
      });
    });
    return promise;
  }

  onChangeVC(vcId): Promise<any> {
    this.IsLoading = true;
    this.studentClassDetailModel.VCId = vcId;

    let promise = new Promise((resolve, reject) => {

      let schoolRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        schoolRequest = this.commonService.GetSchoolByHMId(this.currentAcademicYearId, this.UserModel.UserTypeId, vcId);
      }
      else {
        schoolRequest = this.commonService.GetMasterDataByType({ DataType: 'SchoolsByVC', ParentId: vcId, SelectTitle: 'School' }, false);
      }

      schoolRequest.subscribe((response: any) => {
        if (response.Success) {
          this.schoolList = response.Results;
          this.filteredSchoolItems = this.schoolList.slice();
        }

        this.resetFormControls('VC');
        this.IsLoading = false;
        resolve(true);
      });
    });
    return promise;
  }

  onChangeSchool(schoolId): Promise<any> {
    this.IsLoading = true;
    this.studentClassDetailModel.SchoolId = schoolId

    let promise = new Promise((resolve, reject) => {

      let vtParams = {
        DataId: this.UserModel.RoleCode,
        DataId1: this.UserModel.UserTypeId,
        DataId2: this.studentClassDetailModel.AcademicYearId,
        DataId3: this.studentClassDetailModel.VTPId,
        DataId4: this.studentClassDetailModel.VCId,
        DataId5: this.studentClassDetailModel.SchoolId,
      };

      this.commonService.GetVTByAYAndSchoolId(vtParams).subscribe((response: any) => {
        if (response.Success) {
          this.vtList = response.Results;
          if (this.UserModel.RoleCode == 'VT') {
            this.onChangeVocationalTrainer(this.UserModel.UserTypeId);
            this.studentClassDetailForm.controls["VTId"].setValue(this.UserModel.UserTypeId);
          }
          else {
            this.studentClassDetailForm.controls["VTId"].reset();
          }
        }

        this.resetFormControls('School');
        this.IsLoading = true;
        resolve(true);
      });
    });

    return promise;
  }

  onChangeSector(sectorId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.commonService.GetMasterDataByType({ DataType: 'JobRoles', ParentId: sectorId, SelectTitle: "Job Role" }).subscribe((response) => {

        if (response.Success) {
          this.jobRoleList = response.Results;
          this.studentClassDetailForm.controls['JobRoleId'].setValue(this.jobRoleId);
        }

        resolve(true);
      });
    });
  }

  onChangeVocationalTrainer(vtId) {
    this.IsLoading = true;
    this.studentClassDetailModel.VTId = vtId;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetMasterDataByType({ DataType: 'ClassesByVT', ParentId: this.studentClassDetailModel.AcademicYearId, UserId: this.studentClassDetailModel.VTId, SelectTitle: 'Class' }).subscribe((response) => {
        if (response.Success) {
          this.classList = response.Results;
        }

        this.resetFormControls('VT');
        resolve(true);
      });
    });
    return promise;
  }

  onChangeClass(classId): Promise<any> {
    this.classId = classId;
    this.IsLoading = true;

    if (classId == "69257ab5-e836-46f1-b888-dfae5da5489c" || classId == "e0302e36-a8a7-49a0-b621-21d48986c43e") {
      this.studentClassDetailForm.controls["StreamId"].setValidators([Validators.required]);
    }
    else {
      this.studentClassDetailForm.controls["StreamId"].clearValidators();
      this.studentClassDetailForm.controls["StreamId"].setValue(null);
      this.studentClassDetailForm.controls["IsStudentVE9And10"].setValue(null);
      this.studentClassDetailForm.controls["IsSameStudentTrade"].setValue(null);
    }
    this.onChangeStudentVE9And10();

    let promise = new Promise((resolve, reject) => {

      this.commonService.GetMasterDataByType({ DataType: 'SectionsByVT', ParentId: classId, UserId: this.studentClassDetailModel.VTId, SelectTitle: 'Section' }).subscribe((response) => {
        if (response.Success) {
          this.sectionList = response.Results;
        }

        this.resetFormControls('Class');

        this.getSectorByVTIdAyIdSchoolId();

        resolve(true);
      });
    });
    return promise;
  }

  onChangeSection(sectionId: string): Promise<any> {
    this.IsLoading = true;
    let promise = new Promise((resolve, reject) => {

      let studentParams = {
        AcademicYearId: this.studentClassDetailModel.AcademicYearId,
        SchoolId: this.studentClassDetailModel.SchoolId,
        VTId: this.studentClassDetailModel.VTId,
        ClassId: this.classId,
        SectionId: sectionId,
      };

      this.commonService.GetStudentsByClassIdSectionId(studentParams).subscribe((response) => {
        if (response.Success) {
          this.studentList = response.Results;
          this.filteredStudentItems = this.studentList.slice();
        }

        this.resetFormControls('Section');
        resolve(true);
      });
    });
    return promise;
  }

  getSectorByVTIdAyIdSchoolId(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.commonService.GetSectorByVTIdAyIdSchoolId(this.studentClassDetailModel.VTId, this.studentClassDetailModel.AcademicYearId, this.studentClassDetailModel.SchoolId).subscribe((resSector) => {
        if (resSector.Success) {
          this.sectorList = resSector.Results;

          if (this.sectorList.length > 0) {
            this.sectorId = this.sectorList[0].Id;
            this.jobRoleId = this.sectorList[0].Description;

            this.studentClassDetailForm.get('SectorId').setValue(this.sectorId);

            this.onChangeSector(this.sectorList[0].Id).then(jrResp => {
              this.IsLoading = false;
              resolve(true);
            });
          }
        }
      });
    });
  }

  onChangeStudentVE9And10() {
    let classId = this.studentClassDetailForm.controls['ClassId'].value;
    let isStudentVE9And10 = this.studentClassDetailForm.controls['IsStudentVE9And10'].value;

    this.isDisableStudentTrade = (classId == 'ef71e220-ed0a-4cec-a5b0-e06325d3dbf4' || classId == '3d99b3d3-f955-4e8f-9f2e-ec697a774bbc' || isStudentVE9And10 == 'No' || isStudentVE9And10 == null);
    if (this.isDisableStudentTrade == true) {
      this.studentClassDetailForm.controls['IsSameStudentTrade'].setValue(null);
    }
  }

  resetFormControls(resetLevel: string): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      if (resetLevel == 'AY') {
        this.studentClassDetailForm.controls["VTPId"].reset();
        this.studentClassDetailForm.controls["VCId"].reset();
        this.studentClassDetailForm.controls["SchoolId"].reset();
        this.studentClassDetailForm.controls["VTId"].reset();
        this.studentClassDetailForm.controls["ClassId"].reset();
        this.studentClassDetailForm.controls["SectionId"].reset();
        this.studentClassDetailForm.controls["StudentId"].reset();

        if (this.UserModel.RoleCode != 'VT') {
          this.schoolList = [];
          this.filteredSchoolItems = [];
        }

        this.vcList = [];
        this.vtList = [];
        this.classList = [];
        this.sectionList = [];
        this.studentList = [];
        this.filteredStudentItems = [];

      } else if (resetLevel == 'VTP') {
        this.studentClassDetailForm.controls["VCId"].reset();
        this.studentClassDetailForm.controls["SchoolId"].reset();
        this.studentClassDetailForm.controls["VTId"].reset();
        this.studentClassDetailForm.controls["ClassId"].reset();
        this.studentClassDetailForm.controls["SectionId"].reset();
        this.studentClassDetailForm.controls["StudentId"].reset();

        if (this.UserModel.RoleCode != 'VT') {
          this.schoolList = [];
          this.filteredSchoolItems = [];
        }

        this.vtList = [];
        this.classList = [];
        this.sectionList = [];
        this.studentList = [];
        this.filteredStudentItems = [];

      } else if (resetLevel == 'VC') {
        this.studentClassDetailForm.controls["SchoolId"].reset();
        this.studentClassDetailForm.controls["VTId"].reset();
        this.studentClassDetailForm.controls["ClassId"].reset();
        this.studentClassDetailForm.controls["SectionId"].reset();
        this.studentClassDetailForm.controls["StudentId"].reset();

        this.vtList = [];
        this.classList = [];
        this.sectionList = [];
        this.studentList = [];
        this.filteredStudentItems = [];

      } else if (resetLevel == 'School') {
        this.studentClassDetailForm.controls["VTId"].reset();
        this.studentClassDetailForm.controls["ClassId"].reset();
        this.studentClassDetailForm.controls["SectionId"].reset();
        this.studentClassDetailForm.controls["StudentId"].reset();

        this.classList = [];
        this.sectionList = [];
        this.studentList = [];
        this.filteredStudentItems = [];

      } else if (resetLevel == 'VT') {
        this.studentClassDetailForm.controls["ClassId"].reset();
        this.studentClassDetailForm.controls["SectionId"].reset();
        this.studentClassDetailForm.controls["StudentId"].reset();

        this.sectionList = [];
        this.studentList = [];
        this.filteredStudentItems = [];

      } else if (resetLevel == 'Class') {
        this.studentClassDetailForm.controls["SectionId"].reset();
        this.studentClassDetailForm.controls["StudentId"].reset();

        this.studentList = [];
        this.filteredStudentItems = [];

      } else if (resetLevel == 'Section') {
        this.studentClassDetailForm.controls["StudentId"].reset();
      }

      resolve(true);
    });

    return promise;
  }

  saveOrUpdateStudentClassDetailDetails() {
    if (!this.studentClassDetailForm.valid) {
      this.validateAllFormFields(this.studentClassDetailForm);
      console.log("Student Class Details Saving Errors : ", this.Errors);
      return;
    }

    this.setValueFromFormGroup(this.studentClassDetailForm, this.studentClassDetailModel);

    if (this.UserModel.RoleCode == 'VT' && this.PageRights.ActionType == this.Constants.Actions.New) {
      this.studentClassDetailModel.AcademicYearId = this.userInfoForVT.AcademicYearId;
      this.studentClassDetailModel.VTPId = this.userInfoForVT.VTPId;
      this.studentClassDetailModel.VCId = this.userInfoForVT.VCId;
      this.studentClassDetailModel.SchoolId = this.userInfoForVT.SchoolId;
      this.studentClassDetailModel.VTId = this.userInfoForVT.VTId;
    }

    this.studentClassDetailService.createOrUpdateStudentClassDetail(this.studentClassDetailModel)
      .subscribe((studentClassDetailResp: any) => {

        if (studentClassDetailResp.Success) {
          this.zone.run(() => {
            this.showActionMessage(
              this.Constants.Messages.RecordSavedMessage,
              this.Constants.Html.SuccessSnackbar
            );

            this.router.navigate([RouteConstants.StudentClassDetail.List]);
          });
        }
        else {
          var errorMessages = this.getHtmlMessage(studentClassDetailResp.Errors)
          this.dialogService.openShowDialog(errorMessages);
        }
      }, error => {
        console.log('StudentClassDetail deletion errors =>', error);
      });
  }

  //Create studentClassDetail form and returns {FormGroup}
  createStudentClassDetailForm(): FormGroup {
    return this.formBuilder.group({
      StudentId: new FormControl({ value: this.studentClassDetailModel.StudentId, disabled: this.PageRights.IsReadOnly }),
      AcademicYearId: new FormControl({ value: this.studentClassDetailModel.AcademicYearId, disabled: this.PageRights.IsReadOnly }),
      VTPId: new FormControl({ value: this.studentClassDetailModel.VTPId, disabled: this.PageRights.IsReadOnly }),
      VCId: new FormControl({ value: this.studentClassDetailModel.VCId, disabled: this.PageRights.IsReadOnly }),
      SchoolId: new FormControl({ value: this.studentClassDetailModel.SchoolId, disabled: this.PageRights.IsReadOnly }),
      VTId: new FormControl({ value: (this.UserModel.RoleCode == 'VT' ? this.UserModel.UserTypeId : this.studentClassDetailModel.VTId), disabled: this.PageRights.IsReadOnly }),
      ClassId: new FormControl({ value: this.studentClassDetailModel.ClassId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      SectionId: new FormControl({ value: this.studentClassDetailModel.SectionId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      FatherName: new FormControl({ value: this.studentClassDetailModel.FatherName, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      MotherName: new FormControl({ value: this.studentClassDetailModel.MotherName, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      GuardianName: new FormControl({ value: this.studentClassDetailModel.GuardianName, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(50), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      DateOfBirth: new FormControl({ value: new Date(this.studentClassDetailModel.DateOfBirth), disabled: this.PageRights.IsReadOnly }, Validators.required),
      AadhaarNumber: new FormControl({ value: this.maskingStudentAadhaarNo(this.studentClassDetailModel.AadhaarNumber), disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(12), Validators.minLength(12), Validators.pattern(this.Constants.Regex.Number)]),
      StudentRollNumber: new FormControl({ value: this.studentClassDetailModel.StudentRollNumber, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(30)]),
      SocialCategory: new FormControl({ value: this.studentClassDetailModel.SocialCategory, disabled: this.PageRights.IsReadOnly }, [Validators.required]),
      Religion: new FormControl({ value: this.studentClassDetailModel.Religion, disabled: this.PageRights.IsReadOnly }),
      CWSNStatus: new FormControl({ value: this.studentClassDetailModel.CWSNStatus, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(150)]),
      Mobile: new FormControl({ value: this.studentClassDetailModel.Mobile, disabled: this.PageRights.IsReadOnly }, [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]),
      Mobile1: new FormControl({ value: this.studentClassDetailModel.Mobile1, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]),
      IsActive: new FormControl({ value: true, disabled: this.PageRights.IsReadOnly }),

      StreamId: new FormControl({ value: this.studentClassDetailModel.StreamId, disabled: this.PageRights.IsReadOnly }),
      IsStudentVE9And10: new FormControl({ value: this.studentClassDetailModel.IsStudentVE9And10, disabled: this.PageRights.IsReadOnly }),
      IsSameStudentTrade: new FormControl({ value: this.studentClassDetailModel.IsSameStudentTrade, disabled: this.PageRights.IsReadOnly }),
      AssessmentConducted: new FormControl({ value: this.studentClassDetailModel.AssessmentConducted, disabled: this.PageRights.IsReadOnly }, Validators.required),
      WhatsAppNo: new FormControl({ value: this.studentClassDetailModel.WhatsAppNo, disabled: this.PageRights.IsReadOnly }, [Validators.maxLength(10), Validators.minLength(10), Validators.pattern(this.Constants.Regex.MobileNumber)]),
      SectorId: new FormControl({ value: this.studentClassDetailModel.SectorId, disabled: true }, Validators.required),
      JobRoleId: new FormControl({ value: this.studentClassDetailModel.JobRoleId, disabled: true }, Validators.required),
    });
  }
}
