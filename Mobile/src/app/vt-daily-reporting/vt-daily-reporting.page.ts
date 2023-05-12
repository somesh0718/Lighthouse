import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm, Validators } from '@angular/forms';
import { DatePipe, formatDate } from '@angular/common';
import { VTDailyReportingService } from './vt-daily-reporting.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { VTDailyReportingModel } from './vt-daily-reporting.model';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { ApiService } from '../services/api.service';
import { forkJoin } from 'rxjs';
import { FormGroup, FormControl, FormArray, FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DropdownModel } from '../models/dropdown.model';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';
import { BaseService } from '../services/base.service';
import { VTRAssessorInOtherSchoolForExamModel } from './vt-assessor-in-other-school-for-exam.model';
import { VTRAssignmentFromVocationalDepartmentModel } from './vt-assignment-from-vocational-department.model';
import { VTRCommunityHomeVisitModel } from './vt-community-home-visit.model';
import { VTRHolidayModel } from './vt-holiday.model';
import { VTRLeaveModel } from './vt-leave.model';
import { VTRObservationDayModel } from './vt-observation-day.model';
import { VTROnJobTrainingCoordinationModel } from './vt-on-job-training-coordination.model';
import { VTRParentTeachersMeetingModel } from './vt-parent-teachers-meeting.model';
import { VTRTeachingNonVocationalSubjectModel } from './vt-teaching-non-vocational-subject.model';
import { VTRTeachingVocationalEducationModel } from './vt-teaching-vocational-education.model';
import { VTRTrainingOfTeacherModel } from './vt-training-of-teacher.model';
import { VTRVisitToEducationalInstitutionModel } from './vt-visit-to-educational-institution.model';
import { VTRVisitToIndustryModel } from './vt-visit-to-industry.model';
import { Storage } from '@ionic/storage';
import { AppConstants } from '../app.constants';
import { BasePage } from '../common/base.page';
import { FileUploadModel } from '../models/file.upload.model';

@Component({
  selector: 'app-vt-daily-reporting',
  templateUrl: './vt-daily-reporting.page.html',
  styleUrls: ['./vt-daily-reporting.page.scss'],
})

// tslint:disable: no-angle-bracket-type-assertion
// tslint:disable: prefer-const
// tslint:disable: whitespace

export class VtDailyReportingPage extends BasePage<VTDailyReportingModel> implements OnInit {
  maxDate: any;
  minDate: any;

  public Constants: AppConstants;

  submittedRecords: any;
  vtDailyReportingForm: FormGroup;
  vtDailyReportingModel: VTDailyReportingModel;

  vtSchoolSectorList: [];
  reportTypeList: [];
  classTaughtList: [];
  studentClassList: [];
  studentList: [];
  unitSessionsModels: ModuleUnitSessionModel[];
  moduleTaughtList: [];
  unitList: [];
  sessionList: [];
  classPictureFile: FileUploadModel;
  lessonPlanPictureFile: FileUploadModel;
  assignmentPhotoFile: FileUploadModel;

  displayColumns: string[] = ['StudentName', 'IsPresent'];
  dataSourceAttendance: any;
  workingDayTypeIdsList: [];
  vtClassList: [];
  sectionList: [];
  otherWorkList: [];
  classTypeList: [];
  activityTypeList: any;
  trainingTypeList: [];
  trainingTopicsList: [];
  holidayDayList: [];
  ojtActivityList: [];
  leaveApproverList: [];
  leaveTypeList: [];
  holidayTypeList: [];
  observationDayList: [];

  isAllowTeachingVocationalEducation = false;
  isAllowTrainingOfTeacher = false;
  isAllowOnJobTrainingCoordination = false;
  isAllowAssessorInOtherSchool = false;
  isAllowParentTeacherMeeting = false;
  isAllowCommunityHomeVisit = false;
  isAllowIndustryVisit = false;
  isAllowVisitToEducationalInstitute = false;
  isAllowAssignmentFromVocationalDepartment = false;
  isAllowTeachingNonVocationalSubject = false;
  isAllowWorkAssignedByHeadMaster = false;
  isAllowSchoolExamDuty = false;
  isAllowOtherWork = false;
  isAllowHoliday = false;
  isAllowObservationDay = false;
  isAllowLeave = false;
  isAllowSchoolEventCelebration = false;
  file: any;
  CommonMasterDataMaster = [] as any;
  CourseModuleUnitSessionsMaster = [] as any;
  ClassSectionsByVTIdMaster = [] as any;
  StudentsByVTIdMaster = [] as any;
  minReportingDate: any;
  /* tslint:disable:no-string-literal */

  constructor(
    public datepipe: DatePipe,
    private vtDailyReportingService: VTDailyReportingService,
    private helperService: HelperService,
    private router: Router,
    private alertCtrl: AlertController,
    public loadingController: LoadingController,
    public api: ApiService,
    private modalCtrl: ModalController,
    private baseService: BaseService,
    private localStorage: Storage,
    private formBuilder: FormBuilder,
  ) {
    super();

    this.vtDailyReportingModel = new VTDailyReportingModel();
    this.classPictureFile = new FileUploadModel();
    this.lessonPlanPictureFile = new FileUploadModel();
    this.assignmentPhotoFile = new FileUploadModel()
    this.vtDailyReportingForm = this.createVTDailyReportingForm();
    this.unitSessionsModels = [];
  }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.vtDailyReportingModel = new VTDailyReportingModel();
    this.classPictureFile = new FileUploadModel();
    this.lessonPlanPictureFile = new FileUploadModel();
    this.assignmentPhotoFile = new FileUploadModel();

    this.vtDailyReportingForm = this.createVTDailyReportingForm();

    this.localStorage.get('currentUser').then((res) => {
      this.UserModel = JSON.parse(res);

      let dateOfAllocation = new Date(this.UserModel.DateOfAllocation);
      let maxDate = new Date(Date.now());

      let Time = maxDate.getTime() - dateOfAllocation.getTime();
      let Days = Math.floor(Time / (1000 * 3600 * 24));

      if (Days < this.Constants.BackDatedReportingDays) {
        this.minReportingDate = this.DateFormatPipe.transform(this.UserModel.DateOfAllocation, this.Constants.DateValFormat);
      }
      else {
        let past7days = maxDate;
        past7days.setDate(past7days.getDate() - this.Constants.BackDatedReportingDays)
        this.minReportingDate = this.DateFormatPipe.transform(past7days, this.Constants.DateValFormat);
      }
    });

    this.loadMasters();
  }

  ionViewWillLeave() {
  }

  loadMasters() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        const obsList = [this.api.loadCommonMasterData(), this.api.loadCourseModuleUnitSessions(), this.api.loadClassSectionsByVTId(), this.api.loadStudentsByVTId()];

        forkJoin(obsList).subscribe((res: any) => {
          this.CommonMasterDataMaster = res[0];

          this.reportTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTReportType');

          this.activityTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTActivityType');

          this.CourseModuleUnitSessionsMaster = res[1];

          this.ClassSectionsByVTIdMaster = res[2];

          this.StudentsByVTIdMaster = res[3];

          loadEl.dismiss();
        }, (err) => {
          this.showAlert('Please do Master Sync first.');
          loadEl.dismiss();
        });

      });
  }

  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.loadSubmittedRecords();
    this.localStorage.get('currentUser').then((r) => {
      this.UserModel = JSON.parse(r);
      //this.minDate = this.UserModel.DateOfAllocation.split(' ')[0].replace(/\//gi, '-');
      this.minDate = this.DateFormatPipe.transform(this.UserModel.DateOfAllocation, this.Constants.DateValFormat);
    });

    this.vtDailyReportingForm.reset();
  }

  loadSubmittedRecords() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        this.api.selectTableData('GetVTDailyReporting').then((res) => {
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
    const controls = this.vtDailyReportingForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }

    this.loadSubmittedRecords();

    for (const iterator of this.submittedRecords) {
      iterator.calendarDate = iterator.ReportingDate;
    }
    const modal = await this.modalCtrl.create({
      component: CalModalPage,
      componentProps: {
        Records: this.submittedRecords,
        title: 'VT Daily Reporting',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

    });
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

  uploadedClassPhotoFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ClassPictureFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.VTReporting).then((response: FileUploadModel) => {
        this.classPictureFile = response;

        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('IsClassPictureFile').setValue(false);
        this.setMandatoryFieldControl(this.vtDailyReportingForm.controls.teachingVocationalEducationGroup as FormGroup, 'ClassPictureFile', this.Constants.DefaultImageState);

        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ClassPictureFile').setValue(response.Base64Data);
        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get("ClassPictureFile").updateValueAndValidity();
      });
    }
  }

  uploadedLessonPlanPhotoFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('LessonPlanPictureFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.VTReporting).then((response: FileUploadModel) => {
        this.lessonPlanPictureFile = response;

        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('IsLessonPlanPictureFile').setValue(false);
        this.setMandatoryFieldControl(this.vtDailyReportingForm.controls.teachingVocationalEducationGroup as FormGroup, 'LessonPlanPictureFile', this.Constants.DefaultImageState);

        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('LessonPlanPictureFile').setValue(response.Base64Data);
        this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get("LessonPlanPictureFile").updateValueAndValidity();
      });
    }
  }

  uploadedAssignmentPhotoFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.vtDailyReportingForm.controls.assignmentFromVocationalDepartmentGroup.get('AssignmentPhotoFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.VTReporting).then((response: FileUploadModel) => {
        this.assignmentPhotoFile = response;
        this.vtDailyReportingForm.controls.assignmentFromVocationalDepartmentGroup.get('AssignmentPhotoFile').setValue(response.Base64Data);

        this.vtDailyReportingForm.controls.assignmentFromVocationalDepartmentGroup.get('IsAssignmentPhotoFile').setValue(false);
        this.setMandatoryFieldControl(this.vtDailyReportingForm.controls.assignmentFromVocationalDepartmentGroup as FormGroup, 'AssignmentPhotoFile', this.Constants.DefaultImageState);
      });
    }
  }

  onChangeReportType(reportTypeId): void {
    this.resetReportTypeFormGroups();

    if (reportTypeId === '38') {
      this.isAllowLeave = true;
      this.vtDailyReportingModel.Leave = new VTRLeaveModel();

      this.vtDailyReportingForm = this.formBuilder.group({
        ...this.vtDailyReportingForm.controls,

        leaveGroup: this.formBuilder.group({
          LeaveTypeId: new FormControl('', []),
          LeaveApprovalStatus: new FormControl(''),
          LeaveApprover: new FormControl(''),
          LeaveReason: new FormControl('', [Validators.maxLength(350)]),
        })
      });

      this.leaveTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'WorkingDayType');
      this.leaveApproverList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'LeaveApprover');
    }

    // Holiday/ School Off
    else if (reportTypeId === '40') {
      this.isAllowHoliday = true;
      this.vtDailyReportingModel.Holiday = new VTRHolidayModel();

      this.vtDailyReportingForm = this.formBuilder.group({
        ...this.vtDailyReportingForm.controls,

        holidayGroup: this.formBuilder.group({
          HolidayTypeId: new FormControl(''),
          HolidayDetails: new FormControl('', [Validators.maxLength(350)]),
        })
      });

      // this.holidayTypeList = response.Results;
      this.holidayTypeList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'WorkingDayType');
    }
    // Observation Day
    else if (reportTypeId === '123') {
      this.isAllowObservationDay = true;
      this.vtDailyReportingModel.ObservationDay = new VTRObservationDayModel();

      this.vtDailyReportingForm = this.formBuilder.group({
        ...this.vtDailyReportingForm.controls,

        observationDayGroup: this.formBuilder.group({
          ObservationDetails: new FormControl('', Validators.maxLength(350)),
          OBStudentCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
          StudentAttendances: this.formBuilder.array([])
        })
      });

      // this.observationDayList = response.Results;
      this.observationDayList = this.CommonMasterDataMaster.filter((x) => x.ParentId === reportTypeId && x.DataTypeId === 'WorkingDayType');
    }
    else {
      this.vtDailyReportingForm = this.formBuilder.group({
        ...this.vtDailyReportingForm.controls,

        WorkingDayTypeIds: new FormControl(''),
      });

      this.workingDayTypeIdsList = this.CommonMasterDataMaster.filter((x) => x.ParentId !== '38' && x.ParentId !== '40' && x.ParentId !== '123' && x.DataTypeId === 'WorkingDayType');
    }
  }

  onChangeWorkingType(workingTypes): void {
    this.resetWorkTypesFormGroups();

    workingTypes.forEach(workTypeId => {

      // 1. Teaching Vocational Education
      if (workTypeId === '101') {
        this.isAllowTeachingVocationalEducation = true;
        this.vtDailyReportingModel.TeachingVocationalEducation = new VTRTeachingVocationalEducationModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          teachingVocationalEducationGroup: this.formBuilder.group({
            ClassTaughtId: new FormControl(''),
            DidYouTeachToday: new FormControl(true),
            ClassSectionIds: new FormControl(''),
            ActivityTypeIds: new FormControl(''),
            ModuleId: new FormControl(''),
            UnitId: new FormControl(''),
            SessionsTaught: new FormControl(''),
            ClassTypeId: new FormControl(''),
            ClassTime: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            ClassPictureFile: new FormControl('', Validators.required),
            LessonPlanPictureFile: new FormControl('', Validators.required),
            ReasonOfNotConductingTheClassIds: new FormControl(''),
            ReasonDetails: new FormControl('', Validators.maxLength(350)),
            IsClassPictureFile: new FormControl(false),
            IsLessonPlanPictureFile: new FormControl(false),
            StudentAttendances: this.formBuilder.array([])
          })
        });

        const distinctClasses = this.ClassSectionsByVTIdMaster.filter((thing, i, arr) => arr.findIndex(t => t.ClassId === thing.ClassId) === i);
        this.classTaughtList = distinctClasses;

        const distinctModules = this.CourseModuleUnitSessionsMaster.filter((thing, i, arr) => arr.findIndex(t => t.ModuleTypeId === thing.ModuleTypeId) === i);
        this.moduleTaughtList = distinctModules;

        this.otherWorkList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTOtherWork');
        this.classTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTClassType');
        this.activityTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTActivityType');
      }

      // 2. Training Of Teacher
      if (workTypeId === '102') {
        this.isAllowTrainingOfTeacher = true;
        this.vtDailyReportingModel.TrainingOfTeacher = new VTRTrainingOfTeacherModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          trainingOfTeacherGroup: this.formBuilder.group({
            TrainingTypeId: new FormControl(''),
            TrainingBy: new FormControl('', Validators.maxLength(100)),
            TrainingTopicIds: new FormControl(''),
            TrainingDetails: new FormControl('', Validators.maxLength(350)),
          })
        });

        this.trainingTypeList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTTrainingType');
        this.trainingTopicsList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'VTTrainingTopics');
        //this.onChangeTrainingOfTeacherType();
      }

      // 3. On Job Training Coordination
      if (workTypeId === '103') {
        this.isAllowOnJobTrainingCoordination = true;
        this.vtDailyReportingModel.OnJobTrainingCoordination = new VTROnJobTrainingCoordinationModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          onJobTrainingCoordinationGroup: this.formBuilder.group({
            OnJobTrainingActivityId: new FormControl(''),
            IndustryName: new FormControl('', Validators.maxLength(150)),
            IndustryContactPerson: new FormControl('', [Validators.maxLength(100), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
            IndustryContactNumber: new FormControl('', [Validators.pattern(this.Constants.Regex.MobileNumber)]),
            OJTActivityDetails: new FormControl('', [Validators.maxLength(350)]),
          })
        });

        this.ojtActivityList = this.CommonMasterDataMaster.filter((x) => x.DataTypeId === 'OnJobTrainingActivity');
        //this.onChangeOnjobTrainingCoordinationType();
      }

      // 4. Assessor In Other School
      if (workTypeId === '104') {
        this.isAllowAssessorInOtherSchool = true;
        this.vtDailyReportingModel.AssessorInOtherSchoolForExam = new VTRAssessorInOtherSchoolForExamModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          assessorInOtherSchoolGroup: this.formBuilder.group({
            SchoolName: new FormControl('', [Validators.maxLength(200)]),
            Udise: new FormControl('', [Validators.maxLength(11), Validators.minLength(11), Validators.pattern(this.Constants.Regex.Number)]),
            ClassId: new FormControl(''),
            BoysPresent: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            GirlsPresent: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            ExamDetails: new FormControl('', [Validators.maxLength(350)]),
          })
        });

        const distinctClasses = this.ClassSectionsByVTIdMaster.filter((thing, i, arr) => arr.findIndex(t => t.ClassId === thing.ClassId) === i);
        this.studentClassList = distinctClasses;
      }

      // 5. School Event/ Celebration
      if (workTypeId === '105') {
        this.isAllowSchoolEventCelebration = true;
        this.vtDailyReportingModel.SchoolEventCelebration = '';

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          schoolEventCelebrationGroup: this.formBuilder.group({
            SchoolEventCelebration: new FormControl('', Validators.maxLength(350)),
          })
        });
      }

      // 5. Parent Teacher Meeting
      if (workTypeId === '106') {
        this.isAllowParentTeacherMeeting = true;
        this.vtDailyReportingModel.ParentTeachersMeeting = new VTRParentTeachersMeetingModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          parentTeacherMeetingGroup: this.formBuilder.group({
            VocationalParentsCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            OtherParentsCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            PTADetails: new FormControl('', Validators.maxLength(350)),
          })
        });
      }

      // 6. Community Home Visit
      if (workTypeId === '107') {
        this.isAllowCommunityHomeVisit = true;
        this.vtDailyReportingModel.CommunityHomeVisit = new VTRCommunityHomeVisitModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          communityHomeVisitGroup: this.formBuilder.group({
            VocationalParentsCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            OtherParentsCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            CommunityVisitDetails: new FormControl('', Validators.maxLength(350)),
          })
        });
      }

      // 7. Industry Visit
      if (workTypeId === '108') {
        this.isAllowIndustryVisit = true;
        this.vtDailyReportingModel.VisitToIndustry = new VTRVisitToIndustryModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          industryVisitGroup: this.formBuilder.group({
            IndustryVisitCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            IndustryVisits: this.formBuilder.array([this.createIndustryVisit()])
          })
        });
      }

      // 8. Visit to Educational Institute
      if (workTypeId === '109') {
        this.isAllowVisitToEducationalInstitute = true;
        this.vtDailyReportingModel.VisitToEducationalInstitution = new VTRVisitToEducationalInstitutionModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          visitToEducationalInstituteGroup: this.formBuilder.group({
            EducationalInstituteVisitCount: new FormControl('', Validators.pattern(this.Constants.Regex.Number)),
            VisitToEducationalInstitutes: this.formBuilder.array([this.createVisitToEducationalInstitute()])
          })
        });
      }

      // 9. Assignment From Vocational Department
      if (workTypeId === '110') {
        this.isAllowAssignmentFromVocationalDepartment = true;
        this.vtDailyReportingModel.AssignmentFromVocationalDepartment = new VTRAssignmentFromVocationalDepartmentModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          assignmentFromVocationalDepartmentGroup: this.formBuilder.group({
            AssigmentNumber: new FormControl('', [Validators.maxLength(30), Validators.pattern(this.Constants.Regex.Number)]),
            AssignmentDetails: new FormControl('', Validators.maxLength(350)),
            AssignmentPhotoFile: new FormControl('', Validators.required),
            IsAssignmentPhotoFile: new FormControl(false),
          })
        });
      }

      // 10. Teaching Non Vocational Subject
      if (workTypeId === '111') {
        this.isAllowTeachingNonVocationalSubject = true;
        this.vtDailyReportingModel.TeachingNonVocationalSubject = new VTRTeachingNonVocationalSubjectModel();

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          teachingNonVocationalSubjectGroup: this.formBuilder.group({
            OtherClassTakenDetails: new FormControl('', Validators.maxLength(150)),
            OtherClassTime: new FormControl(''),
          })
        });
      }

      // 11. Work Assigned By Head Master
      if (workTypeId === '112') {
        this.isAllowWorkAssignedByHeadMaster = true;
        this.vtDailyReportingModel.WorkAssignedByHeadMaster = '';

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          workAssignedByHeadMasterGroup: this.formBuilder.group({
            WorkAssignedByHeadMaster: new FormControl('', Validators.maxLength(150)),
          })
        });
      }

      // 11. School Exam Duty
      if (workTypeId === '113') {
        this.isAllowSchoolExamDuty = true;
        this.vtDailyReportingModel.SchoolExamDuty = '';

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          schoolExamDutyGroup: this.formBuilder.group({
            SchoolExamDuty: new FormControl('', Validators.maxLength(150)),
          })
        });
      }

      // 11. Other Work
      if (workTypeId === '114') {
        this.isAllowOtherWork = true;
        this.vtDailyReportingModel.OtherWork = '';

        this.vtDailyReportingForm = this.formBuilder.group({
          ...this.vtDailyReportingForm.controls,

          otherWorkGroup: this.formBuilder.group({
            OtherWork: new FormControl('', Validators.maxLength(150)),
          })
        });
      }
    });

    const initialFormValues = this.vtDailyReportingForm.value;
    this.vtDailyReportingForm.reset(initialFormValues);
  }

  onChangeIndustryVisitCount(industryVisitCount: number): void {
    const industryVisitControls = this.vtDailyReportingForm.controls.industryVisitGroup.get('IndustryVisits') as FormArray;
    industryVisitControls.clear();

    for (let industryVisitIndex = 1; industryVisitIndex <= industryVisitCount; industryVisitIndex++) {
      industryVisitControls.push(this.createIndustryVisit());
    }
  }

  createIndustryVisit(): FormGroup {
    return this.formBuilder.group({
      IndustryName: new FormControl(''),
      IndustryContactPerson: new FormControl('', [Validators.maxLength(100), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      IndustryContactNumber: new FormControl('', Validators.pattern(this.Constants.Regex.MobileNumber)),
      IndustryVisitDetails: new FormControl('', Validators.maxLength(150)),
    });
  }

  onChangeEducationalInstituteVisitCount(educationalInstituteVisitCount: number): void {
    const educationalInstituteVisitControls = this.vtDailyReportingForm.controls.visitToEducationalInstituteGroup.get('VisitToEducationalInstitutes') as FormArray;
    educationalInstituteVisitControls.clear();

    for (let visitToEducationalInstituteIndex = 1; visitToEducationalInstituteIndex <= educationalInstituteVisitCount; visitToEducationalInstituteIndex++) {
      educationalInstituteVisitControls.push(this.createVisitToEducationalInstitute());
    }
  }

  createVisitToEducationalInstitute(): FormGroup {
    return this.formBuilder.group({
      EducationalInstitute: new FormControl(''),
      InstituteContactPerson: new FormControl('', [Validators.maxLength(100), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]),
      InstituteContactNumber: new FormControl('', Validators.pattern(this.Constants.Regex.MobileNumber)),
      InstituteVisitDetails: new FormControl(''),
    });
  }

  onChangeClassForTaught(classId): void {
    if (classId != null) {
      this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('DidYouTeachToday').setValue(true);

      this.sectionList = this.ClassSectionsByVTIdMaster.filter((x) => x.ClassId === classId);

      let moduleItem = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ModuleId').value;
      if (moduleItem != null && moduleItem.Id != null) {
        this.onChangeCourseModule(moduleItem);
      }
    }

    this.unitList = [] as any;
    this.sessionList = [] as any;
    this.unitSessionsModels = <ModuleUnitSessionModel[]>[];

    this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ClassSectionIds').setValue(null);

    let studentAttendancesControls = <FormArray>this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('StudentAttendances');
    studentAttendancesControls.clear();
  }

  onChangeSectionForTaught(sectionId) {
    if (sectionId != null) {
      let classId = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ClassTaughtId').value;

      let studentAttendancesControls = <FormArray>this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('StudentAttendances');
      studentAttendancesControls.clear();

      const studentsList = this.StudentsByVTIdMaster.filter((x) => x.ClassId === classId && x.SectionId === sectionId);
      studentsList.forEach(studentItem => {
        studentAttendancesControls.push(this.formBuilder.group(studentItem));
      });

    }
    else {
      let studentAttendancesControls = <FormArray>this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('StudentAttendances');
      studentAttendancesControls.clear();
    }
  }

  onChangeCourseModule(moduleItem): void {
    let classId = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ClassTaughtId').value;

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
    const classId = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ClassTaughtId').value;

    if (classId !== '' && unitItem.Id != null) {
      this.sessionList = this.CourseModuleUnitSessionsMaster.filter((x) => x.UnitId === unitItem.UnitId);
    } else {
      this.sessionList = [] as any;
    }
  }

  addUnitSession() {
    const moduleCtrl = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('ModuleId');
    const unitCtrl = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('UnitId');
    const sessionIdsCtrl = this.vtDailyReportingForm.controls.teachingVocationalEducationGroup.get('SessionsTaught');

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

      this.unitList = [] as any;
      this.sessionList = [] as any;
    }
  }

  removeUnitSession(sessionItem) {
    const sessionIndex = this.unitSessionsModels.indexOf(sessionItem);
    this.unitSessionsModels.splice(sessionIndex, 1);
  }

  resetReportTypeFormGroups(): void {
    this.isAllowTeachingVocationalEducation = false;
    this.isAllowTrainingOfTeacher = false;
    this.isAllowOnJobTrainingCoordination = false;
    this.isAllowAssessorInOtherSchool = false;
    this.isAllowParentTeacherMeeting = false;
    this.isAllowSchoolEventCelebration = false;
    this.isAllowCommunityHomeVisit = false;
    this.isAllowIndustryVisit = false;
    this.isAllowVisitToEducationalInstitute = false;
    this.isAllowAssignmentFromVocationalDepartment = false;
    this.isAllowTeachingNonVocationalSubject = false;
    this.isAllowWorkAssignedByHeadMaster = false;
    this.isAllowSchoolExamDuty = false;
    this.isAllowOtherWork = false;
    this.isAllowHoliday = false;
    this.isAllowObservationDay = false;
    this.isAllowLeave = false;

    delete this.vtDailyReportingForm.controls['WorkingDayTypeIds'];
    delete this.vtDailyReportingForm.value['WorkingDayTypeIds'];
    delete this.vtDailyReportingForm.controls['teachingVocationalEducationGroup'];
    delete this.vtDailyReportingForm.value['teachingVocationalEducationGroup'];
    delete this.vtDailyReportingForm.controls['trainingOfTeacherGroup'];
    delete this.vtDailyReportingForm.value['trainingOfTeacherGroup'];
    delete this.vtDailyReportingForm.controls['onJobTrainingCoordinationGroup'];
    delete this.vtDailyReportingForm.value['onJobTrainingCoordinationGroup'];
    delete this.vtDailyReportingForm.controls['assessorInOtherSchoolGroup'];
    delete this.vtDailyReportingForm.value['assessorInOtherSchoolGroup'];
    delete this.vtDailyReportingForm.controls['schoolEventCelebrationGroup'];
    delete this.vtDailyReportingForm.value['schoolEventCelebrationGroup'];
    delete this.vtDailyReportingForm.controls['parentTeacherMeetingGroup'];
    delete this.vtDailyReportingForm.value['parentTeacherMeetingGroup'];
    delete this.vtDailyReportingForm.controls['communityHomeVisitGroup'];
    delete this.vtDailyReportingForm.value['communityHomeVisitGroup'];
    delete this.vtDailyReportingForm.controls['industryVisitGroup'];
    delete this.vtDailyReportingForm.value['industryVisitGroup'];
    delete this.vtDailyReportingForm.controls['visitToEducationalInstituteGroup'];
    delete this.vtDailyReportingForm.value['visitToEducationalInstituteGroup'];
    delete this.vtDailyReportingForm.controls['assignmentFromVocationalDepartmentGroup'];
    delete this.vtDailyReportingForm.value['assignmentFromVocationalDepartmentGroup'];
    delete this.vtDailyReportingForm.controls['teachingNonVocationalSubjectGroup'];
    delete this.vtDailyReportingForm.value['teachingNonVocationalSubjectGroup'];
    delete this.vtDailyReportingForm.controls['workAssignedByHeadMasterGroup'];
    delete this.vtDailyReportingForm.value['workAssignedByHeadMasterGroup'];
    delete this.vtDailyReportingForm.controls['schoolExamDutyGroup'];
    delete this.vtDailyReportingForm.value['schoolExamDutyGroup'];
    delete this.vtDailyReportingForm.controls['otherWorkGroup'];
    delete this.vtDailyReportingForm.value['otherWorkGroup'];
    delete this.vtDailyReportingForm.controls['leaveGroup'];
    delete this.vtDailyReportingForm.value['leaveGroup'];
    delete this.vtDailyReportingForm.controls['holidayGroup'];
    delete this.vtDailyReportingForm.value['holidayGroup'];
    delete this.vtDailyReportingForm.controls['observationDayGroup'];
    delete this.vtDailyReportingForm.value['observationDayGroup'];

    let initialFormValues = this.vtDailyReportingForm.value;
    this.vtDailyReportingForm.reset(initialFormValues);
  }

  resetWorkTypesFormGroups(): void {
    this.isAllowTeachingVocationalEducation = false;
    this.isAllowTrainingOfTeacher = false;
    this.isAllowOnJobTrainingCoordination = false;
    this.isAllowAssessorInOtherSchool = false;
    this.isAllowParentTeacherMeeting = false;
    this.isAllowSchoolEventCelebration = false;
    this.isAllowCommunityHomeVisit = false;
    this.isAllowIndustryVisit = false;
    this.isAllowVisitToEducationalInstitute = false;
    this.isAllowAssignmentFromVocationalDepartment = false;
    this.isAllowTeachingNonVocationalSubject = false;
    this.isAllowWorkAssignedByHeadMaster = false;
    this.isAllowSchoolExamDuty = false;
    this.isAllowOtherWork = false;

    delete this.vtDailyReportingForm.controls['teachingVocationalEducationGroup'];
    delete this.vtDailyReportingForm.value['teachingVocationalEducationGroup'];
    delete this.vtDailyReportingForm.controls['trainingOfTeacherGroup'];
    delete this.vtDailyReportingForm.value['trainingOfTeacherGroup'];
    delete this.vtDailyReportingForm.controls['onJobTrainingCoordinationGroup'];
    delete this.vtDailyReportingForm.value['onJobTrainingCoordinationGroup'];
    delete this.vtDailyReportingForm.controls['assessorInOtherSchoolGroup'];
    delete this.vtDailyReportingForm.value['assessorInOtherSchoolGroup'];
    delete this.vtDailyReportingForm.controls['schoolEventCelebrationGroup'];
    delete this.vtDailyReportingForm.value['schoolEventCelebrationGroup'];
    delete this.vtDailyReportingForm.controls['parentTeacherMeetingGroup'];
    delete this.vtDailyReportingForm.value['parentTeacherMeetingGroup'];
    delete this.vtDailyReportingForm.controls['communityHomeVisitGroup'];
    delete this.vtDailyReportingForm.value['communityHomeVisitGroup'];
    delete this.vtDailyReportingForm.controls['industryVisitGroup'];
    delete this.vtDailyReportingForm.value['industryVisitGroup'];
    delete this.vtDailyReportingForm.controls['visitToEducationalInstituteGroup'];
    delete this.vtDailyReportingForm.value['visitToEducationalInstituteGroup'];
    delete this.vtDailyReportingForm.controls['assignmentFromVocationalDepartmentGroup'];
    delete this.vtDailyReportingForm.value['assignmentFromVocationalDepartmentGroup'];
    delete this.vtDailyReportingForm.controls['teachingNonVocationalSubjectGroup'];
    delete this.vtDailyReportingForm.value['teachingNonVocationalSubjectGroup'];
    delete this.vtDailyReportingForm.controls['workAssignedByHeadMasterGroup'];
    delete this.vtDailyReportingForm.value['workAssignedByHeadMasterGroup'];
    delete this.vtDailyReportingForm.controls['schoolExamDutyGroup'];
    delete this.vtDailyReportingForm.value['schoolExamDutyGroup'];
    delete this.vtDailyReportingForm.controls['otherWorkGroup'];
    delete this.vtDailyReportingForm.value['otherWorkGroup'];

    let initialFormValues = this.vtDailyReportingForm.value;
    this.vtDailyReportingForm.reset(initialFormValues);
  }

  saveOrUpdateVTDailyReportingDetails() {
    if (!this.vtDailyReportingForm.valid) {
      this.validateDynamicFormArrayFields(this.vtDailyReportingForm);
      return;
    }

    let workingDayTypeControl = this.vtDailyReportingForm.get('WorkingDayTypeIds');

    if (workingDayTypeControl != null && workingDayTypeControl.value != null) {
      let workingDayType = workingDayTypeControl.value.find(w => w == '101');

      if (workingDayType != undefined && this.unitSessionsModels.length == 0) {
        this.showUserMessage("Please add course module, unit and sessions taught first using Plus icon!");
        return;
      }
    }

    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        this.vtDailyReportingModel = this.vtDailyReportingService.getVTDailyReportingModelFromFormGroup(this.vtDailyReportingForm);

        this.vtDailyReportingModel.VTId = this.UserModel.UserTypeId;
        this.helperService.getCurrentCoordinates().then((res: any) => {
          this.vtDailyReportingModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
          this.vtDailyReportingModel.Latitude = res.coords.latitude;
          this.vtDailyReportingModel.Longitude = res.coords.longitude;


          if (this.vtDailyReportingModel.TeachingVocationalEducations !== undefined) {
            if (this.vtDailyReportingModel.TeachingVocationalEducations.length > 0) {
              this.vtDailyReportingModel.TeachingVocationalEducations[0].UnitSessionsModels = this.unitSessionsModels;
              this.vtDailyReportingModel.TeachingVocationalEducations[0].ClassPictureFile = (this.classPictureFile.Base64Data != '' ? this.setUploadedFile(this.classPictureFile) : null);
              this.vtDailyReportingModel.TeachingVocationalEducations[0].LessonPlanPictureFile = (this.lessonPlanPictureFile.Base64Data != '' ? this.setUploadedFile(this.lessonPlanPictureFile) : null);
            }
            else {
              delete this.vtDailyReportingModel['TeachingVocationalEducations'];
            }
          }

          if (this.vtDailyReportingModel.AssignmentFromVocationalDepartment !== undefined) {
            this.vtDailyReportingModel.AssignmentFromVocationalDepartment.AssignmentPhotoFile = (this.assignmentPhotoFile.Base64Data != '' ? this.setUploadedFile(this.assignmentPhotoFile) : null);
          }

          delete this.vtDailyReportingModel['TeachingVocationalEducation'];
          delete this.vtDailyReportingModel['UnitSessionsModels'];
          delete this.vtDailyReportingModel['VisitToIndustry'];

          // this.vtDailyReportingForm.reset();
          if (this.helperService.checkInternetConnection()) {
            this.vtDailyReportingService.createOrUpdateVTDailyReporting(this.vtDailyReportingModel)
              .subscribe((vtDailyReportingResp: any) => {

                if (vtDailyReportingResp.Success) {
                  const successMessages = this.helperService.getHtmlMessage(vtDailyReportingResp.Messages);
                  loadEl.dismiss();
                  this.helperService.presentToast(successMessages);
                  this.router.navigateByUrl('/folder/Home');
                }
                else {
                  const errorMessages = this.helperService.getHtmlMessage(vtDailyReportingResp.Errors);
                  loadEl.dismiss();
                  this.helperService.showAlert(errorMessages);
                }
              }, error => {
                this.savingOfflineVTDailyReporting();
                loadEl.dismiss();
                console.log('VTDailyReporting errors =>', error);
              });
          } else {
            this.savingOfflineVTDailyReporting();
            loadEl.dismiss();
          }
        }, (err) => {
          loadEl.dismiss();
          return;
        });
      });
  }

  savingOfflineVTDailyReporting() {
    this.helperService.presentToast('Saving Offline...');

    this.api.selectTableData('UploadVTDailyReporting').then((vtDailyReportedlist: any) => {
      if (vtDailyReportedlist.length > 0) {
        for (const iterator of vtDailyReportedlist) {
          if (iterator.ReportingDate.split('T')[0] === this.vtDailyReportingModel.ReportingDate.split('T')[0]) {
            if (iterator.ReportType === this.vtDailyReportingModel.ReportType) {
              if (this.vtDailyReportingModel.ReportType === '37') {
                for (const iterator2 of JSON.parse(iterator.WorkingDayTypeIds)) {
                  let ind = this.vtDailyReportingModel.WorkingDayTypeIds.findIndex((x) => x === iterator2);
                  if (ind !== -1) {
                    alert('VT Daily Form with same Date and Report Type and Working Day Type already exists.');
                    return;
                  }
                }
              } else {
                alert('VT Daily Form with same Date and Report Type already exists.');
                return;
              }
            } else {
              alert('VT Daily Form with same Date and different Report Type already exists.');
              return;
            }
          }
        }
      }

      const storeForm = JSON.parse(JSON.stringify(this.vtDailyReportingModel));

      storeForm.WorkingDayTypeIds = JSON.stringify(this.vtDailyReportingModel.WorkingDayTypeIds);
      storeForm.TeachingVocationalEducations = JSON.stringify(this.vtDailyReportingModel.TeachingVocationalEducations);
      storeForm.TrainingOfTeacher = JSON.stringify(this.vtDailyReportingModel.TrainingOfTeacher);
      storeForm.OnJobTrainingCoordination = JSON.stringify(this.vtDailyReportingModel.OnJobTrainingCoordination);
      storeForm.AssessorInOtherSchoolForExam = JSON.stringify(this.vtDailyReportingModel.AssessorInOtherSchoolForExam);
      storeForm.ParentTeachersMeeting = JSON.stringify(this.vtDailyReportingModel.ParentTeachersMeeting);
      storeForm.CommunityHomeVisit = JSON.stringify(this.vtDailyReportingModel.CommunityHomeVisit);
      storeForm.VisitToIndustries = JSON.stringify(this.vtDailyReportingModel.VisitToIndustries);
      storeForm.VisitToEducationalInstitutions = JSON.stringify(this.vtDailyReportingModel.VisitToEducationalInstitutions);
      storeForm.AssignmentFromVocationalDepartment = JSON.stringify(this.vtDailyReportingModel.AssignmentFromVocationalDepartment);
      storeForm.TeachingNonVocationalSubject = JSON.stringify(this.vtDailyReportingModel.TeachingNonVocationalSubject);
      storeForm.Leave = JSON.stringify(this.vtDailyReportingModel.Leave);
      storeForm.Holiday = JSON.stringify(this.vtDailyReportingModel.Holiday);
      storeForm.ObservationDay = JSON.stringify(this.vtDailyReportingModel.ObservationDay);

      this.api.insertUploadTable('UploadVTDailyReporting', storeForm).then(() => {
        this.router.navigateByUrl('/folder/Home');
        this.helperService.presentToast('VT Daily Reporting Form Saved Offline Successfully.');
      }, (err) => {
        alert(err);
        console.log('VT Daily Reporting errors =>', err);
      });
    });
  }

  createVTDailyReportingForm(): FormGroup {
    return this.formBuilder.group({
      VTDailyReportingId: new FormControl(''),
      ReportingDate: new FormControl('', Validators.required),
      ReportType: new FormControl('', [Validators.maxLength(50)]),
      WorkingDayTypeIds: new FormControl('', Validators.required)
    });
  }

  private onChangeTrainingOfTeacherType() {
    this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get('TrainingTypeId').valueChanges
      .subscribe(data => {

        if (data == "126") {
          this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get("TrainingBy").setValidators([Validators.required, Validators.maxLength(100)]);
          this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get("TrainingTopicIds").clearValidators();

        }
        else if (data == "124" || data == "125") {
          this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get("TrainingTopicIds").setValidators(Validators.required);
          this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get("TrainingBy").clearValidators();
        }

        this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get("TrainingTopicIds").updateValueAndValidity();
        this.vtDailyReportingForm.controls.trainingOfTeacherGroup.get("TrainingBy").updateValueAndValidity();
      });
  }

  private onChangeOnjobTrainingCoordinationType() {
    this.vtDailyReportingForm.get("OnJobTrainingActivityId").valueChanges
      .subscribe(data => {

        if (data == "139") {
          this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryName").setValidators(Validators.required);
          this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryContactPerson").setValidators([Validators.required, Validators.maxLength(100), Validators.pattern(this.Constants.Regex.CharsWithTitleCase)]);
          this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryContactNumber").setValidators([Validators.required, Validators.pattern(this.Constants.Regex.MobileNumber)]);
        }
        else if (data == "140") {
          this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryName").setValidators(Validators.required);

          this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryContactPerson").clearValidators();
          this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryContactNumber").clearValidators();
        }
        this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryName").updateValueAndValidity();
        this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryContactPerson").updateValueAndValidity();
        this.vtDailyReportingForm.controls.onJobTrainingCoordinationGroup.get("IndustryContactNumber").updateValueAndValidity();
      });
  }
}
