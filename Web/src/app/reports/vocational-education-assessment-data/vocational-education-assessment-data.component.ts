import { Component, OnInit, NgZone, ViewEncapsulation, ViewChild } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';

import { ReportService } from 'app/reports/report.service';
import { DropdownModel } from 'app/models/dropdown.model';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { MatOption } from '@angular/material/core';
import { VocationalEducationAssessmentDataModel } from './vocational-education-assessment-data.model';

@Component({
  selector: 'data-list-view',
  templateUrl: './vocational-education-assessment-data.component.html',
  styleUrls: ['./vocational-education-assessment-data.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class VocationalEducationAssessmentDataComponent extends BaseListComponent<VocationalEducationAssessmentDataModel> implements OnInit {
  VocationalEducationAssessmentDataForm: FormGroup;

  academicyearList: [DropdownModel];
  divisionList: [DropdownModel];
  districtList: DropdownModel[];
  sectorList: [DropdownModel];
  jobRoleList: DropdownModel[];
  vtpList: [DropdownModel];
  vtList: [DropdownModel];
  schoolList: [DropdownModel];
  classList: [DropdownModel];
  monthList: [DropdownModel];
  schoolManagementList: [DropdownModel];
  genderList: [DropdownModel];

  currentAcademicYearId: string;
  isShowFilterContainer = false;
  @ViewChild('districtMatSelect') districtSelections: MatSelect;
  vEAHeader: any;
  filteredSchoolItems: DropdownModel[];
  academicYearId: any;

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    public formBuilder: FormBuilder,
    private reportService: ReportService) {
    super(commonService, router, routeParams, snackBar, zone);

    this.displayedColumns = ['SrNo', 'StudentName', 'Class', 'Gender', 'DOB', 'AadhaarNumber', 'StudentRollNumber', 'PrimaryContact', 'AlternativeContact', 'FatherName', 'Category', 'Sector', 'JobRole', 'StreamName', 'Assesment'];
    this.VocationalEducationAssessmentDataForm = this.createVocationalEducationAssessmentDataForm();
  }

  ngOnInit(): void {
    this.reportService.GetDropdownForReports(this.UserModel).subscribe(results => {
      if (results[0].Success) {
        this.academicyearList = results[0].Results;
      }

      this.currentAcademicYearId = this.UserModel.AcademicYearId;
      this.VocationalEducationAssessmentDataForm.get('AcademicYearId').setValue(this.currentAcademicYearId);

      this.commonService.GetSchoolsByAYIdAndRoleId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.currentAcademicYearId).subscribe((response: any) => {
        if (response.Success) {
          this.schoolList = response.Results;
          this.filteredSchoolItems = this.schoolList.slice();
        }
      });
    });
  }

  resetReportFilters(): void {
    this.VocationalEducationAssessmentDataForm.reset();
    this.tableDataSource.data = [];
    this.VocationalEducationAssessmentDataForm.get('AcademicYearId').setValue(this.currentAcademicYearId);
  }

  onChangeAcademicYear(academicYearId): Promise<any> {
    this.academicYearId = academicYearId
    this.IsLoading = true;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetSchoolsByAYIdAndRoleId(this.UserModel.RoleCode, this.UserModel.UserTypeId, academicYearId).subscribe((response: any) => {
        if (response.Success) {
          if (this.UserModel.RoleCode == 'VT') {
            this.VocationalEducationAssessmentDataForm.get('SchoolId').setValue(this.schoolList[0].Id);
          }
          else {
            this.schoolList = response.Results;
            this.filteredSchoolItems = this.schoolList.slice();
          }

        }
        this.IsLoading = false;
        resolve(true);
      });
    });

    return promise;
  }

  onChangeSchool(schoolId): Promise<any> {
    this.IsLoading = true;
    let promise = new Promise((resolve, reject) => {

      let vtRequest = null;
      vtRequest = this.commonService.GetVocationalTrainersByAcademicYearIdAndSchoolId(this.currentAcademicYearId, schoolId).subscribe((response: any) => {
        if (response.Success) {

          if (this.UserModel.RoleCode == 'VT') {
            this.VocationalEducationAssessmentDataForm.get('VTId').setValue(this.UserModel.UserTypeId);

          }
          else {
            this.vtList = response.Results;
          }
        }
        this.IsLoading = false;
        resolve(true);
      });
      vtRequest
    });
    return promise;
  }

  //Create StudentEnrollment form and returns {FormGroup}
  createVocationalEducationAssessmentDataForm(): FormGroup {
    return this.formBuilder.group({
      AcademicYearId: new FormControl(),
      VTId: new FormControl(),
      SchoolId: new FormControl()
    });
  }

  getVocationalEducationAssessmentDataPrint(): void {
    if (!this.VocationalEducationAssessmentDataForm.valid) {
      return;
    }

    var reportParams: any = {
      AcademicYearId: this.VocationalEducationAssessmentDataForm.get('AcademicYearId').value,
      VTId: this.VocationalEducationAssessmentDataForm.get('VTId').value,
      SchoolId: this.VocationalEducationAssessmentDataForm.get('SchoolId').value
    };

    if (this.UserModel.RoleCode == 'VT') {
      reportParams.VTId = this.UserModel.UserTypeId;
    }

    this.reportService.GetVocationalEducationAssessmentBySchoolAndVTId(reportParams).subscribe(response => {
      this.vEAHeader = response.Result.VEAHeaderModels;

      this.tableDataSource.data = response.Result.VEADetailsModels;
      this.tableDataSource.sort = this.ListSort;
      this.tableDataSource.paginator = this.ListPaginator;
      this.tableDataSource.filteredData = this.tableDataSource.data;

      this.zone.run(() => {
        if (this.tableDataSource.data.length == 0) {
          this.showNoDataFoundSnackBar();
        }
      });
      this.IsLoading = false;
    }, error => {
      console.log(error);
    });
  }

  getVocationalEducationAssessmentDataReport(): void {
    if (!this.VocationalEducationAssessmentDataForm.valid) {
      return;
    }

    var reportParams: any = {
      AcademicYearId: this.VocationalEducationAssessmentDataForm.get('AcademicYearId').value,
      VTId: this.VocationalEducationAssessmentDataForm.get('VTId').value,
      SchoolId: this.VocationalEducationAssessmentDataForm.get('SchoolId').value
    };

    this.reportService.GetVocationalEducationAssessmentReport(reportParams).subscribe(response => {
      if (response.Result != null && response.Result != '') {
        let pdfReportUrl = this.Constants.Services.BaseUrl + 'Lighthouse/DownloadReportFile?fileId=' + response.Result + '&folderName=VEAReports';
        window.open(pdfReportUrl, '_blank', '');
      }
    });
  }
}
