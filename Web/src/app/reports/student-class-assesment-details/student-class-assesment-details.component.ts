import { Component, OnInit, NgZone, ViewEncapsulation, ViewChild } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';
import { StudentClassAssessmentDetailModel } from './student-class-assesment-details.model';
import { ReportService } from 'app/reports/report.service';
import { DropdownModel } from 'app/models/dropdown.model';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { MatOption } from '@angular/material/core';
import { debounceTime, distinctUntilChanged, filter } from 'rxjs/operators';

@Component({
  selector: 'data-list-view',
  templateUrl: './student-class-assesment-details.component.html',
  styleUrls: ['./student-class-assesment-details.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class StudentClassAssesmentDetailsComponent extends BaseListComponent<StudentClassAssessmentDetailModel> implements OnInit {
  studentClassAssesmentDetailsForm: FormGroup;
  studentClassAssesmentSearchForm: FormGroup;

  academicyearList: [DropdownModel];
  divisionList: [DropdownModel];
  districtList: DropdownModel[];
  sectorList: [DropdownModel];
  jobRoleList: DropdownModel[];
  vtpList: [DropdownModel];
  vtList: [DropdownModel];
  vcList: [DropdownModel];
  schoolList: [DropdownModel];
  classList: [DropdownModel];
  monthList: [DropdownModel];
  schoolManagementList: [DropdownModel];
  genderList: [DropdownModel];

  currentAcademicYearId: string;
  @ViewChild('districtMatSelect') districtSelections: MatSelect;
  schoolId: any;
  vcId: any;
  filteredSchoolItems: any[];
  academicYearId: any;
  vtpId: string;

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    public formBuilder: FormBuilder,
    private reportService: ReportService) {
    super(commonService, router, routeParams, snackBar, zone);

    this.IsShowFilter = true;
    this.SearchBy.PageIndex = 0; // delete after script changed
    this.SearchBy.PageSize = 250; // delete after script changed

    this.displayedColumns = ['SrNo', 'YearName', 'State', 'District', 'School', 'UDISE', 'HMName', 'HMMobile', 'HMEmail', 'VTPName', 'VCName', 'VTName', 'VTMobile', 'FirstName', 'MiddleName', 'LastName', 'Class', 'Gender', 'DOB', 'AadhaarNumber', 'RollNo', 'FatherName', 'MotherName', 'PrimaryContact', 'AlternativeContact', 'Category', 'Sector', 'JobRole', 'StreamName'];
    this.studentClassAssesmentSearchForm = this.formBuilder.group({ SearchText: new FormControl() });
    this.studentClassAssesmentDetailsForm = this.createstudentClassAssesmentDetailsForm();
  }

  ngOnInit(): void {
    this.reportService.GetDropdownForReports(this.UserModel).subscribe(results => {
      if (results[0].Success) {
        this.academicyearList = results[0].Results;
      }

      if (results[1].Success) {
        this.divisionList = results[1].Results;
      }

      if (results[2].Success) {
        this.sectorList = results[2].Results;
      }

      if (results[3].Success) {
        this.vtpList = results[3].Results;
      }

      if (results[4].Success) {
        this.classList = results[4].Results;
      }

      if (results[5].Success) {
        this.monthList = results[5].Results;
      }

      if (results[6].Success) {
        this.schoolManagementList = results[6].Results;
      }

      if (results[8].Success) {
        this.genderList = results[8].Results;
      }

      this.currentAcademicYearId = this.UserModel.AcademicYearId;

      if (this.UserModel.RoleCode == 'VC') {
        this.commonService.getVocationalTrainingProvidersByUserId(this.UserModel).then(vtpResp => {
          this.vtpId = this.vtpList[0].Id
          this.studentClassAssesmentDetailsForm.controls['VTPId'].setValue(this.vtpList[0].Id);
          this.studentClassAssesmentDetailsForm.controls['VCId'].setValue(this.UserModel.UserTypeId);
          this.onChangeVC(this.UserModel.UserTypeId);
        });
      }

      if (this.UserModel.RoleCode == 'VT') {
        this.commonService.GetMasterDataByType({ DataType: 'SchoolsByVT', ParentId: this.UserModel.UserTypeId, SelectTitle: 'School' }, false)
          .subscribe((response: any) => {

            this.schoolList = response.Results;
            this.filteredSchoolItems = this.schoolList.slice();
          });
      }

      this.studentClassAssesmentDetailsForm.get('AcademicYearId').setValue(this.currentAcademicYearId);

      if (this.UserModel.RoleCode === 'DivEO') {
        this.studentClassAssesmentDetailsForm.controls["DivisionId"].setValue(this.UserModel.DivisionId);

        this.onChangeDivision(this.UserModel.DivisionId).then(response => {
        });
      }
      else if (this.UserModel.RoleCode === 'DisEO' || this.UserModel.RoleCode === 'DisRP' || this.UserModel.RoleCode === 'BEO' || this.UserModel.RoleCode === 'BRP') {
        this.studentClassAssesmentDetailsForm.controls["DivisionId"].setValue(this.UserModel.DivisionId);

        this.onChangeDivision(this.UserModel.DivisionId).then(response => {
          let districtIds = [];
          response.forEach(districtItem => {
            districtIds.push(districtItem.Id);
          });

          this.studentClassAssesmentDetailsForm.controls["DistrictId"].setValue(districtIds);
        });
      }

      this.studentClassAssesmentSearchForm.get('SearchText').valueChanges.pipe(
        // if character length greater then 2
        filter(res => {
          this.SearchBy.PageIndex = 0;

          if (res.length == 0) {
            this.SearchBy.Name = '';
            this.onLoadStudentClassAssesmentDetailsReportsByFilters();
            return false;
          }

          return res.length > 2
        }),

        // Time in milliseconds between key events
        debounceTime(650),

        // If previous query is diffent from current   
        distinctUntilChanged()

        // subscription for response
      ).subscribe((searchText: string) => {
        this.SearchBy.Name = searchText;
        this.onLoadStudentClassAssesmentDetailsReportsByFilters();
      });
    });
  }

  onChangeVTP(vtpId): Promise<any> {
    this.IsLoading = true;
    let promise = new Promise((resolve, reject) => {

      let vcRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        vcRequest = this.commonService.GetVCByHMId(this.currentAcademicYearId, this.UserModel.UserTypeId, vtpId);
      }
      else {
        vcRequest = this.commonService.GetMasterDataByType({ DataType: 'VocationalCoordinators', ParentId: vtpId, SelectTitle: 'Vocational Coordinator' }, false);
      }

      vcRequest.subscribe((response: any) => {
        if (response.Success) {
          this.filteredSchoolItems = [];
          this.vcList = response.Results;
        }
        resolve(true);
      });
    });
    return promise;
  }

  onChangeVC(vcId): Promise<any> {
    this.IsLoading = true;
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
          this.vcId = vcId;
          this.schoolList = response.Results;
          this.filteredSchoolItems = this.schoolList.slice();
          this.IsLoading = false;
        }
        resolve(true);
      });
    });
    return promise;
  }

  onChangeSchool(schoolId): Promise<any> {
    this.IsLoading = true;
    this.schoolId = schoolId
    let promise = new Promise((resolve, reject) => {

      let vtRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        vtRequest = this.commonService.GetVTBySchoolIdHMId(this.currentAcademicYearId, this.UserModel.UserTypeId, this.vcId, schoolId);
      }
      else {
        vtRequest = this.commonService.GetMasterDataByType({ DataType: 'TrainersBySchool', ParentId: schoolId, SelectTitle: 'Vocational Trainer' }, false);
      }

      vtRequest.subscribe((response: any) => {
        if (response.Success) {
          this.vtList = response.Results;
        }
        this.IsLoading = false;
        resolve(true);
      });
    });
    return promise;
  }

  onChangeDivision(divisionId: string): Promise<any> {
    let promise = new Promise((resolve, reject) => {

      this.commonService.GetMasterDataByType({ DataType: 'Districts', RoleId: this.UserModel.RoleCode, UserId: this.UserModel.UserTypeId, ParentId: divisionId, SelectTitle: 'District' }, false)
        .subscribe((response: any) => {

          this.districtList = response.Results;
          this.studentClassAssesmentDetailsForm.controls['DistrictId'].setValue(null);
          resolve(response.Results);
        }, err => {

          reject(err);
        });
    });

    return promise;
  }

  onChangeSector(sectorId: string): void {
    this.commonService.GetMasterDataByType({ DataType: 'JobRoles', ParentId: sectorId, SelectTitle: 'Job Role' }).subscribe((response: any) => {
      this.jobRoleList = response.Results;
    });
  }

  toggleDistrictSelections(evt) {
    //To control select-unselect all
    let isAllSelected = (evt.currentTarget.classList.toString().indexOf('mat-active') > 0)

    if (isAllSelected) {
      this.districtSelections.options.forEach((item: MatOption) => item.select());
      this.districtSelections.options.first.deselect();
    } else {
      this.districtSelections.options.forEach((item: MatOption) => { item.deselect() });
    }
    setTimeout(() => { this.districtSelections.close(); }, 200);
  }

  onLoadStudentClassAssesmentDetailsReportsByFilters(): any {
    this.SearchBy.PageIndex = 0;
    this.getStudentClassAssesmentDetailsReports();
  }

  getStudentClassAssesmentDetailsReports(): void {
    this.getStudentClassAssesmentReportsData().then(response => {

      this.tableDataSource.data = response.Results;
      this.tableDataSource.sort = this.ListSort;
      this.tableDataSource.paginator = this.ListPaginator;
      this.tableDataSource.filteredData = this.tableDataSource.data;

      this.SearchBy.TotalResults = response.TotalResults;

      setTimeout(() => {
        this.ListPaginator.pageIndex = this.SearchBy.PageIndex;
        this.ListPaginator.length = this.SearchBy.TotalResults;
      });

      this.zone.run(() => {
        if (this.tableDataSource.data.length == 0) {
          this.showNoDataFoundSnackBar();
        }
      });

      this.IsLoading = false;
    }, error => {
      console.log(error);
      this.IsLoading = false;
    });
  }

  getStudentClassAssesmentReportsData(): Promise<any> {
    if (!this.studentClassAssesmentDetailsForm.valid) {
      return;
    }

    var reportParams: any = {
      UserId: this.UserModel.UserTypeId,
      AcademicYearId: this.studentClassAssesmentDetailsForm.get('AcademicYearId').value,
      VTId: this.studentClassAssesmentDetailsForm.get('VTId').value,
      SchoolId: this.studentClassAssesmentDetailsForm.get('SchoolId').value,
      DivisionId: this.studentClassAssesmentDetailsForm.get('DivisionId').value,
      DistrictId: this.studentClassAssesmentDetailsForm.get('DistrictId').value,
      SectorId: this.studentClassAssesmentDetailsForm.get('SectorId').value,
      JobRoleId: this.studentClassAssesmentDetailsForm.get('JobRoleId').value,
      VTPId: this.studentClassAssesmentDetailsForm.get('VTPId').value,
      ClassId: this.studentClassAssesmentDetailsForm.get('ClassId').value,
      VCId: this.studentClassAssesmentDetailsForm.get('VCId').value,
      Name: this.studentClassAssesmentSearchForm.get('SearchText').value,
      PageIndex: this.SearchBy.PageIndex,
      PageSize: this.SearchBy.PageSize
    };

    if (this.UserModel.RoleCode == 'VC') {
      reportParams.VTPId = this.vtpId;
      reportParams.VCId = this.UserModel.UserTypeId;
    }

    if (this.UserModel.RoleCode == 'VT') {
      reportParams.VTId = this.UserModel.UserTypeId;
    }

    if (this.UserModel.RoleCode == 'HM') {
      reportParams.HMId = this.UserModel.UserTypeId;
    }

    reportParams.DistrictId = (reportParams.DistrictId != null && reportParams.DistrictId.length > 0) ? reportParams.DistrictId.toString() : null;

    let promise = new Promise((resolve, reject) => {
      this.reportService.GetStudentClassAssessmentReportsByCriteria(reportParams).subscribe(response => {
        resolve(response);
      }, error => {
        console.log(error);
        resolve(error);
      });
    });

    return promise;
  }

  resetReportFilters(): void {
    this.studentClassAssesmentDetailsForm.reset();
    this.studentClassAssesmentDetailsForm.get('AcademicYearId').setValue(this.currentAcademicYearId);
    this.districtList = <DropdownModel[]>[];
    this.jobRoleList = <DropdownModel[]>[];

    this.SearchBy.PageIndex = 0;
    this.SearchBy.PageSize = 250;

    if (this.UserModel.RoleCode === 'DisEO' || this.UserModel.RoleCode === 'DisRP' || this.UserModel.RoleCode === 'BEO' || this.UserModel.RoleCode === 'BRP') {
      this.studentClassAssesmentDetailsForm.controls["DivisionId"].setValue(this.UserModel.DivisionId);

      this.onChangeDivision(this.UserModel.DivisionId).then(response => {
        let districtIds = [];
        response.forEach(districtItem => {
          districtIds.push(districtItem.Id);
        });

        this.studentClassAssesmentDetailsForm.controls["DistrictId"].setValue(districtIds);
      });
    }

    this.tableDataSource.data = [];
    this.tableDataSource.filteredData = [];
  }

  onPageIndexChanged(evt) {
    this.SearchBy.PageIndex = evt.pageIndex;
    this.SearchBy.PageSize = evt.pageSize;

    this.getStudentClassAssesmentDetailsReports();
  }

  exportFilterData(): void {
    this.SearchBy.PageIndex = 0;
    this.SearchBy.PageSize = 25000000;

    this.getStudentClassAssesmentReportsData().then(response => {
      response.Results.forEach(
        function (obj) {
          delete obj.TotalRows;
        });

      this.exportExcelFromTable(response.Results, "StudentClassAssesmentReports");
      this.IsLoading = false;
      this.SearchBy.PageSize = 250;
    }, error => {
      console.log(error);
      this.IsLoading = false;
    });
  }

  exportBulkData(): void {
    if (!this.studentClassAssesmentDetailsForm.valid) {
      this.validateAllFormFields(this.studentClassAssesmentDetailsForm);
      return;
    }

    this.IsLoading = true;

    var reportParams: any = {
      UserId: this.UserModel.UserTypeId,
      AcademicYearId: this.studentClassAssesmentDetailsForm.get('AcademicYearId').value,
      PageIndex: 0,
      PageSize: 25000000
    };

    if (this.UserModel.RoleCode == 'VC') {
      reportParams.VTPId = this.vtpId;
      reportParams.VCId = this.UserModel.UserTypeId;
    }

    if (this.UserModel.RoleCode == 'VT') {
      reportParams.VTId = this.UserModel.UserTypeId;
    }

    if (this.UserModel.RoleCode == 'HM') {
      reportParams.HMId = this.UserModel.UserTypeId;
    }

    if (this.UserModel.RoleCode === 'DisEO' || this.UserModel.RoleCode === 'DisRP' || this.UserModel.RoleCode === 'BEO' || this.UserModel.RoleCode === 'BRP') {
      reportParams.DivisionId = this.UserModel.DivisionId;
      reportParams.DistrictId = this.districtList.map(d => d.Id).toString();
    }

    this.reportService.GetStudentClassAssessmentReportsByCriteria(reportParams).subscribe(response => {
      response.Results.forEach(
        function (obj) {
          delete obj.TotalRows;
        });

      this.exportExcelFromTable(response.Results, "StudentDetailsReport");
      this.SearchBy.PageSize = 250;
      this.IsLoading = false;
    }, error => {
      console.log(error);
      this.IsLoading = false;
    });
  }

  //Create StudentEnrollment form and returns {FormGroup}
  createstudentClassAssesmentDetailsForm(): FormGroup {
    return this.formBuilder.group({
      AcademicYearId: new FormControl(),
      DivisionId: new FormControl(),
      DistrictId: new FormControl(),
      SectorId: new FormControl(),
      JobRoleId: new FormControl(),
      ClassId: new FormControl(),
      VTPId: new FormControl(),
      VCId: new FormControl(),
      SchoolId: new FormControl(),
      VTId: new FormControl(),
      Gender: new FormControl(),
    });
  }
}
