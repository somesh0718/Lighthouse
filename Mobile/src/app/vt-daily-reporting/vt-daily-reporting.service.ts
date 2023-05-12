import { Injectable } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { AppConstants } from '../app.constants';
import { UserModel } from '../models/user.model';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { VTRAssessorInOtherSchoolForExamModel } from './vt-assessor-in-other-school-for-exam.model';
import { VTRAssignmentFromVocationalDepartmentModel } from './vt-assignment-from-vocational-department.model';
import { VTRCommunityHomeVisitModel } from './vt-community-home-visit.model';
import { VTDailyReportingModel } from './vt-daily-reporting.model';
import { VTRHolidayModel } from './vt-holiday.model';
import { VTRLeaveModel } from './vt-leave.model';
import { VTROnJobTrainingCoordinationModel } from './vt-on-job-training-coordination.model';
import { VTRParentTeachersMeetingModel } from './vt-parent-teachers-meeting.model';
import { VTRTeachingNonVocationalSubjectModel } from './vt-teaching-non-vocational-subject.model';
import { VTRTeachingVocationalEducationModel } from './vt-teaching-vocational-education.model';
import { VTRTrainingOfTeacherModel } from './vt-training-of-teacher.model';
import { VTRVisitToEducationalInstitutionModel } from './vt-visit-to-educational-institution.model';
import { VTRVisitToIndustryModel } from './vt-visit-to-industry.model';
import { Storage } from '@ionic/storage';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class VTDailyReportingService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private helperService: HelperService,
        private localStorage: Storage,
        public httpCli: HttpClient,
    ) { }

    getVTDailyReportings(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VTDailyReporting.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getVTDailyReportingById(vtDailyReportingId: string) {
        return this.http
            .HttpGet(this.http.Services.VTDailyReporting.GetById + '?vtDailyReportingId=' + vtDailyReportingId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateVTDailyReporting(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VTDailyReporting.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    createOrUpdateVTDailyReportingBulk(pageData) {
        return new Promise((resolve, reject) => {
            this.api.selectTableData(pageData.uploadTable).then((list: any) => {
                if (list.length > 0) {
                    const obsList = [];
                    const options = {
                        headers: {
                            Authorization: 'Bearer ' + AppConstants.AuthToken
                        }
                    };
                    for (const iterator of list) {
                        if (typeof iterator.WorkingDayTypeIds === 'string' && iterator.WorkingDayTypeIds !== null) { iterator.WorkingDayTypeIds = JSON.parse(iterator.WorkingDayTypeIds); }
                        if (typeof iterator.TeachingVocationalEducations === 'string' && iterator.TeachingVocationalEducations !== null) { iterator.TeachingVocationalEducations = JSON.parse(iterator.TeachingVocationalEducations); }
                        if (typeof iterator.TrainingOfTeacher === 'string' && iterator.TrainingOfTeacher !== null) { iterator.TrainingOfTeacher = JSON.parse(iterator.TrainingOfTeacher); }
                        if (typeof iterator.OnJobTrainingCoordination === 'string' && iterator.OnJobTrainingCoordination !== null) { iterator.OnJobTrainingCoordination = JSON.parse(iterator.OnJobTrainingCoordination); }
                        if (typeof iterator.AssessorInOtherSchoolForExam === 'string' && iterator.AssessorInOtherSchoolForExam !== null) { iterator.AssessorInOtherSchoolForExam = JSON.parse(iterator.AssessorInOtherSchoolForExam); }
                        if (typeof iterator.ParentTeachersMeeting === 'string' && iterator.ParentTeachersMeeting !== null) { iterator.ParentTeachersMeeting = JSON.parse(iterator.ParentTeachersMeeting); }
                        if (typeof iterator.CommunityHomeVisit === 'string' && iterator.CommunityHomeVisit !== null) { iterator.CommunityHomeVisit = JSON.parse(iterator.CommunityHomeVisit); }
                        if (typeof iterator.VisitToIndustries === 'string' && iterator.VisitToIndustries !== null) { iterator.VisitToIndustries = JSON.parse(iterator.VisitToIndustries); }
                        if (typeof iterator.VisitToEducationalInstitutions === 'string' && iterator.VisitToEducationalInstitutions !== null) { iterator.VisitToEducationalInstitutions = JSON.parse(iterator.VisitToEducationalInstitutions); }
                        if (typeof iterator.AssignmentFromVocationalDepartment === 'string' && iterator.AssignmentFromVocationalDepartment !== null) { iterator.AssignmentFromVocationalDepartment = JSON.parse(iterator.AssignmentFromVocationalDepartment); }
                        if (typeof iterator.TeachingNonVocationalSubject === 'string' && iterator.TeachingNonVocationalSubject !== null) { iterator.TeachingNonVocationalSubject = JSON.parse(iterator.TeachingNonVocationalSubject); }
                        if (typeof iterator.ObservationDay === 'string' && iterator.ObservationDay !== null) { iterator.ObservationDay = JSON.parse(iterator.ObservationDay); }

                        if (typeof iterator.Leave === 'string' && iterator.Leave !== null) { iterator.Leave = JSON.parse(iterator.Leave); }
                        if (typeof iterator.Holiday === 'string' && iterator.Holiday !== null) { iterator.Holiday = JSON.parse(iterator.Holiday); }

                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.VTDailyReporting.CreateOrUpdate,
                            iterator, options).pipe(
                                catchError(err => of({ isError: true, error: err }))
                            ));
                    }

                    forkJoin(obsList).subscribe((res: any) => {
                        const failedList: any = res.filter((x: any) => x.Success === false);
                        const erroredList: any = res.filter((x: any) => x.isError === true);
                        if (erroredList.length > 0 || failedList.length > 0) {
                            let errorMessage = '';
                            for (let index = 0; index < res.length; index++) {
                                if (res[index].hasOwnProperty('isError')) {
                                    errorMessage = errorMessage + pageData.title + ': ' + res[index].error.message + '<br><br>';
                                    if (res[index].error.status >= 400) {
                                        this.api.deleteSpecificData(pageData.uploadTable, list[index]);
                                    }
                                } else {
                                    if (res[index].Success === false) {
                                        if (res[index].Errors.length > 0)
                                            errorMessage = errorMessage + res[index].Errors.toString() + '<br><br>';

                                        if (res[index].Messages.length > 0)
                                            errorMessage = errorMessage + res[index].Messages.toString() + '<br><br>';
                                    }
                                    this.api.deleteSpecificData(pageData.uploadTable, list[index]);
                                }

                            }

                            if (errorMessage != '')
                                this.http.helperService.showAlert(errorMessage);

                            resolve(true);
                        } else {
                            this.api.deleteTableData(pageData.uploadTable);
                            resolve(true);
                        }

                    }, (err) => {
                        console.log(err);
                        reject(err);
                    });
                } else {
                    resolve(true);
                }
            }, (err) => {
                reject(err);
            });
        });
    }


    deleteVTDailyReportingById(vtDailyReportingId: string) {
        const vtDailyReportingParams = {
            VTDailyReportingId: vtDailyReportingId
        };

        return this.http
            .HttpPost(this.http.Services.VTDailyReporting.Delete, vtDailyReportingParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownForVTDailyReporting(): Observable<any[]> {
        const reportTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'VTReportType', SelectTitle: 'Report Type' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([reportTypeRequest]);
    }

    getDropdownForTeachingVocationalEducation(currentUser: UserModel): Observable<any[]> {
        const classRequest = this.http.GetClassesByVTId({ DataId: currentUser.LoginId, DataId1: currentUser.UserTypeId, SelectTitle: 'Class' });
        const moduleRequest = this.http.GetMasterDataByType({ DataType: 'CourseModules', SelectTitle: 'Modules Taught' });
        const otherWorkTypeRequest = this.http.GetMasterDataByType({ DataType: 'VTOtherWork', SelectTitle: 'Other Work Type' });
        const classTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'VTClassType', SelectTitle: 'Class Type' });
        const activityTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'VTActivityType', SelectTitle: 'Activity Type' }, false);
        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([classRequest, moduleRequest, otherWorkTypeRequest, classTypeRequest, activityTypeRequest]);
    }

    getVTDailyReportingModelFromFormGroup(formGroup: FormGroup): VTDailyReportingModel {
        const dailyReportingModel = new VTDailyReportingModel();

        dailyReportingModel.ReportType = formGroup.get('ReportType').value;
        dailyReportingModel.ReportingDate = this.http.getDateTimeFromControl(formGroup.get("ReportingDate").value);

        if (formGroup.get('WorkingDayTypeIds') != null) {
            dailyReportingModel.WorkingDayTypeIds = formGroup.get('WorkingDayTypeIds').value;
        }

        // Teaching Vocational Education
        if (formGroup.controls.teachingVocationalEducationGroup != null) {
            dailyReportingModel.TeachingVocationalEducation = new VTRTeachingVocationalEducationModel();
            dailyReportingModel.TeachingVocationalEducation.ClassTaughtId = formGroup.controls.teachingVocationalEducationGroup.get('ClassTaughtId').value;
            dailyReportingModel.TeachingVocationalEducation.DidYouTeachToday = formGroup.controls.teachingVocationalEducationGroup.get('DidYouTeachToday').value;
            dailyReportingModel.TeachingVocationalEducation.ClassSectionIds = formGroup.controls.teachingVocationalEducationGroup.get('ClassSectionIds').value;
            dailyReportingModel.TeachingVocationalEducation.ActivityTypeIds = formGroup.controls.teachingVocationalEducationGroup.get('ActivityTypeIds').value;
            dailyReportingModel.TeachingVocationalEducation.ClassTypeId = formGroup.controls.teachingVocationalEducationGroup.get('ClassTypeId').value;
            dailyReportingModel.TeachingVocationalEducation.ClassTime = formGroup.controls.teachingVocationalEducationGroup.get('ClassTime').value;
            //dailyReportingModel.TeachingVocationalEducation.ClassPicture = formGroup.controls.teachingVocationalEducationGroup.get('ClassPicture').value;
            //dailyReportingModel.TeachingVocationalEducation.LessonPlanPicture = formGroup.controls.teachingVocationalEducationGroup.get('LessonPlanPicture').value;
            dailyReportingModel.TeachingVocationalEducation.ReasonOfNotConductingTheClassIds = formGroup.controls.teachingVocationalEducationGroup.get('ReasonOfNotConductingTheClassIds').value;
            dailyReportingModel.TeachingVocationalEducation.ReasonDetails = formGroup.controls.teachingVocationalEducationGroup.get('ReasonDetails').value;

            dailyReportingModel.TeachingVocationalEducation.StudentAttendances = formGroup.controls.teachingVocationalEducationGroup.get('StudentAttendances').value;

            dailyReportingModel.TeachingVocationalEducations = [];
            dailyReportingModel.TeachingVocationalEducations.push(dailyReportingModel.TeachingVocationalEducation);
        }

        // Training Of Teacher
        if (formGroup.controls.trainingOfTeacherGroup != null) {
            dailyReportingModel.TrainingOfTeacher = new VTRTrainingOfTeacherModel();
            dailyReportingModel.TrainingOfTeacher.TrainingBy = formGroup.controls.trainingOfTeacherGroup.get('TrainingBy').value;
            dailyReportingModel.TrainingOfTeacher.TrainingTypeId = formGroup.controls.trainingOfTeacherGroup.get('TrainingTypeId').value;
            dailyReportingModel.TrainingOfTeacher.TrainingTopicIds = formGroup.controls.trainingOfTeacherGroup.get('TrainingTopicIds').value;
            dailyReportingModel.TrainingOfTeacher.TrainingDetails = formGroup.controls.trainingOfTeacherGroup.get('TrainingDetails').value;
        }

        // On Job Training Coordination
        if (formGroup.controls.onJobTrainingCoordinationGroup != null) {
            dailyReportingModel.OnJobTrainingCoordination = new VTROnJobTrainingCoordinationModel();
            dailyReportingModel.OnJobTrainingCoordination.OnJobTrainingActivityId = formGroup.controls.onJobTrainingCoordinationGroup.get('OnJobTrainingActivityId').value;
            dailyReportingModel.OnJobTrainingCoordination.IndustryName = formGroup.controls.onJobTrainingCoordinationGroup.get('IndustryName').value;
            dailyReportingModel.OnJobTrainingCoordination.IndustryContactPerson = formGroup.controls.onJobTrainingCoordinationGroup.get('IndustryContactPerson').value;
            dailyReportingModel.OnJobTrainingCoordination.IndustryContactNumber = formGroup.controls.onJobTrainingCoordinationGroup.get('IndustryContactNumber').value;
            dailyReportingModel.OnJobTrainingCoordination.OJTActivityDetails = formGroup.controls.onJobTrainingCoordinationGroup.get('OJTActivityDetails').value;
        }

        // Assessor In Other School
        if (formGroup.controls.assessorInOtherSchoolGroup != null) {
            dailyReportingModel.AssessorInOtherSchoolForExam = new VTRAssessorInOtherSchoolForExamModel();
            dailyReportingModel.AssessorInOtherSchoolForExam.SchoolName = formGroup.controls.assessorInOtherSchoolGroup.get('SchoolName').value;
            dailyReportingModel.AssessorInOtherSchoolForExam.Udise = formGroup.controls.assessorInOtherSchoolGroup.get('Udise').value;
            dailyReportingModel.AssessorInOtherSchoolForExam.ClassId = formGroup.controls.assessorInOtherSchoolGroup.get('ClassId').value;
            dailyReportingModel.AssessorInOtherSchoolForExam.BoysPresent = formGroup.controls.assessorInOtherSchoolGroup.get('BoysPresent').value;
            dailyReportingModel.AssessorInOtherSchoolForExam.GirlsPresent = formGroup.controls.assessorInOtherSchoolGroup.get('GirlsPresent').value;
            dailyReportingModel.AssessorInOtherSchoolForExam.ExamDetails = formGroup.controls.assessorInOtherSchoolGroup.get('ExamDetails').value;
        }

        // 5. School Event/ Celebration
        if (formGroup.controls.schoolEventCelebrationGroup != null) {
            dailyReportingModel.SchoolEventCelebration = formGroup.controls.schoolEventCelebrationGroup.get('SchoolEventCelebration').value;
        }

        // Parent Teachers Meeting
        if (formGroup.controls.parentTeacherMeetingGroup != null) {
            dailyReportingModel.ParentTeachersMeeting = new VTRParentTeachersMeetingModel();
            dailyReportingModel.ParentTeachersMeeting.VocationalParentsCount = formGroup.controls.parentTeacherMeetingGroup.get('VocationalParentsCount').value;
            dailyReportingModel.ParentTeachersMeeting.OtherParentsCount = formGroup.controls.parentTeacherMeetingGroup.get('OtherParentsCount').value;
            dailyReportingModel.ParentTeachersMeeting.PTADetails = formGroup.controls.parentTeacherMeetingGroup.get('PTADetails').value;
        }

        // Community Home Visit
        if (formGroup.controls.communityHomeVisitGroup != null) {
            dailyReportingModel.CommunityHomeVisit = new VTRCommunityHomeVisitModel();
            dailyReportingModel.CommunityHomeVisit.VocationalParentsCount = formGroup.controls.communityHomeVisitGroup.get('VocationalParentsCount').value;
            dailyReportingModel.CommunityHomeVisit.OtherParentsCount = formGroup.controls.communityHomeVisitGroup.get('OtherParentsCount').value;
            dailyReportingModel.CommunityHomeVisit.CommunityVisitDetails = formGroup.controls.communityHomeVisitGroup.get('CommunityVisitDetails').value;
        }

        // Industry Visit
        if (formGroup.controls.industryVisitGroup != null) {
            dailyReportingModel.VisitToIndustries = [];
            const industryVisitControls = formGroup.controls.industryVisitGroup.get('IndustryVisits') as FormArray;

            for (const industryVisitCtrl of industryVisitControls.controls) {
                dailyReportingModel.VisitToIndustry = new VTRVisitToIndustryModel();
                dailyReportingModel.VisitToIndustry.IndustryVisitCount = formGroup.controls.industryVisitGroup.get('IndustryVisitCount').value;
                dailyReportingModel.VisitToIndustry.IndustryName = industryVisitCtrl.get('IndustryName').value;
                dailyReportingModel.VisitToIndustry.IndustryContactPerson = industryVisitCtrl.get('IndustryContactPerson').value;
                dailyReportingModel.VisitToIndustry.IndustryContactNumber = industryVisitCtrl.get('IndustryContactNumber').value;
                dailyReportingModel.VisitToIndustry.IndustryVisitDetails = industryVisitCtrl.get('IndustryVisitDetails').value;

                dailyReportingModel.VisitToIndustries.push(dailyReportingModel.VisitToIndustry);
            }
        }

        // Visit to Educational Institute
        if (formGroup.controls.visitToEducationalInstituteGroup != null) {
            dailyReportingModel.VisitToEducationalInstitutions = [];
            const visitToEducationalInstitutionControls = formGroup.controls.visitToEducationalInstituteGroup.get('VisitToEducationalInstitutes') as FormArray;

            for (const visitToEducationalInstitutionCtrl of visitToEducationalInstitutionControls.controls) {
                dailyReportingModel.VisitToEducationalInstitution = new VTRVisitToEducationalInstitutionModel();
                dailyReportingModel.VisitToEducationalInstitution.EducationalInstituteVisitCount = formGroup.controls.visitToEducationalInstituteGroup.get('EducationalInstituteVisitCount').value;
                dailyReportingModel.VisitToEducationalInstitution.EducationalInstitute = visitToEducationalInstitutionCtrl.get('EducationalInstitute').value;
                dailyReportingModel.VisitToEducationalInstitution.InstituteContactPerson = visitToEducationalInstitutionCtrl.get('InstituteContactPerson').value;
                dailyReportingModel.VisitToEducationalInstitution.InstituteContactNumber = visitToEducationalInstitutionCtrl.get('InstituteContactNumber').value;
                dailyReportingModel.VisitToEducationalInstitution.InstituteVisitDetails = visitToEducationalInstitutionCtrl.get('InstituteVisitDetails').value;

                dailyReportingModel.VisitToEducationalInstitutions.push(dailyReportingModel.VisitToEducationalInstitution);
            }
        }

        // Assignment From Vocational Department
        if (formGroup.controls.assignmentFromVocationalDepartmentGroup != null) {
            dailyReportingModel.AssignmentFromVocationalDepartment = new VTRAssignmentFromVocationalDepartmentModel();
            dailyReportingModel.AssignmentFromVocationalDepartment.AssigmentNumber = formGroup.controls.assignmentFromVocationalDepartmentGroup.get('AssigmentNumber').value;
            dailyReportingModel.AssignmentFromVocationalDepartment.AssignmentDetails = formGroup.controls.assignmentFromVocationalDepartmentGroup.get('AssignmentDetails').value;
            //dailyReportingModel.AssignmentFromVocationalDepartment.AssignmentPhoto = formGroup.controls.assignmentFromVocationalDepartmentGroup.get('AssignmentPhoto').value;
        }

        // Teaching Non Vocational Subject
        if (formGroup.controls.teachingNonVocationalSubjectGroup != null) {
            dailyReportingModel.TeachingNonVocationalSubject = new VTRTeachingNonVocationalSubjectModel();
            dailyReportingModel.TeachingNonVocationalSubject.OtherClassTakenDetails = formGroup.controls.teachingNonVocationalSubjectGroup.get('OtherClassTakenDetails').value;
            dailyReportingModel.TeachingNonVocationalSubject.OtherClassTime = formGroup.controls.teachingNonVocationalSubjectGroup.get('OtherClassTime').value;
        }

        // Work Assigned By Head Master
        if (formGroup.controls.workAssignedByHeadMasterGroup != null) {
            dailyReportingModel.WorkAssignedByHeadMaster = formGroup.controls.workAssignedByHeadMasterGroup.get('WorkAssignedByHeadMaster').value;
        }

        // School Exam Duty
        if (formGroup.controls.schoolExamDutyGroup != null) {
            dailyReportingModel.SchoolExamDuty = formGroup.controls.schoolExamDutyGroup.get('SchoolExamDuty').value;
        }

        // Other Work
        if (formGroup.controls.otherWorkGroup != null) {
            dailyReportingModel.OtherWork = formGroup.controls.otherWorkGroup.get('OtherWork').value;
        }

        // Holiday Type
        if (formGroup.controls.holidayGroup != null) {
            dailyReportingModel.Holiday = new VTRHolidayModel();
            dailyReportingModel.Holiday.HolidayTypeId = formGroup.controls.holidayGroup.get('HolidayTypeId').value;
            dailyReportingModel.Holiday.HolidayDetails = formGroup.controls.holidayGroup.get('HolidayDetails').value;
        }

        // Observation Day
        if (formGroup.controls.observationDayGroup != null) {

            dailyReportingModel.ObservationDetails = formGroup.controls.observationDayGroup.get('ObservationDetails').value;
            dailyReportingModel.OBStudentCount = formGroup.controls.observationDayGroup.get('OBStudentCount').value;

            // dailyReportingModel.ObservationDay = new VTRObservationDayModel();
            // dailyReportingModel.ObservationDay.ClassId = formGroup.controls.observationDayGroup.get('ClassId').value;
            // dailyReportingModel.ObservationDay.StudentId = formGroup.controls.observationDayGroup.get('StudentId').value;
            // dailyReportingModel.ObservationDay.IsPresent = formGroup.controls.observationDayGroup.get('IsPresent').value;
        }

        // Leave
        if (formGroup.controls.leaveGroup != null) {
            dailyReportingModel.Leave = new VTRLeaveModel();
            dailyReportingModel.Leave.LeaveTypeId = formGroup.controls.leaveGroup.get('LeaveTypeId').value;
            dailyReportingModel.Leave.LeaveApprovalStatus = formGroup.controls.leaveGroup.get('LeaveApprovalStatus').value;
            dailyReportingModel.Leave.LeaveApprover = formGroup.controls.leaveGroup.get('LeaveApprover').value;
            dailyReportingModel.Leave.LeaveReason = formGroup.controls.leaveGroup.get('LeaveReason').value;
        }

        return dailyReportingModel;
    }

    getAndLoadVTDailyReporting(obj) {
        // don't have the data yet
        return new Promise((resolve, reject) => {
            // We're using Angular HTTP provider to request the data,
            // then on the response, it'll map the JSON data to a parsed JS object.
            // Next, we process the data and resolve the promise with the new data.
            this.localStorage.get('currentUser').then((cU) => {
                const user = JSON.parse(cU);
                const options = {
                    headers: {
                        Authorization: 'Bearer ' + user.AuthToken
                    }
                };
                const formData = {
                    UserId: user.UserId,
                    UserTypeId: user.UserTypeId,
                    Name: null,
                    CharBy: null,
                    Filter: {},
                    SortOrder: 'asc',
                    PageIndex: 1,
                    PageSize: 10000
                };
                this.httpCli
                    .post(this.http.Services.BaseUrl + this.http.Services.VTDailyReporting.GetAllByCriteria, formData, options)
                    .subscribe(
                        (data: any) => {
                            if (data.Success) {
                                this.api.dropTableByName(obj.getTable).then(
                                    () => {
                                        this.api.createTableByQuery(obj.getTableCreateQuery).then(
                                            () => {
                                                // tslint:disable-next-line: no-string-literal
                                                this.api.insertGetTable(obj.getTable, data['Results']).then(
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
}
