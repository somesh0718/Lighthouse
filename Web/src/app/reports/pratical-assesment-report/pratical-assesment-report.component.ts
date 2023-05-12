import { Component, NgZone, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { DropdownModel } from 'app/models/dropdown.model';
import { CommonService } from 'app/services/common.service';
import { ReportService } from '../report.service';
import { PraticalAssesmentReportModel } from './pratical-assesment-report.model';

@Component({
  selector: 'data-list-view',
  templateUrl: './pratical-assesment-report.component.html',
  styleUrls: ['./pratical-assesment-report.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class PraticalAssesmentReportComponent extends BaseListComponent<PraticalAssesmentReportModel> implements OnInit {
  practicalAssesmentReortForm: FormGroup;

  academicyearList: [DropdownModel];
  divisionList: [DropdownModel];
  districtList: DropdownModel[];
  sectorList: [DropdownModel];
  jobRoleList: DropdownModel[];
  vtpList: [DropdownModel];
  classList: [DropdownModel];
  monthList: [DropdownModel];
  schoolManagementList: [DropdownModel];

  currentAcademicYearId: string;
  isShowFilterContainer = false;
  @ViewChild('districtMatSelect') districtSelections: MatSelect;

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    public formBuilder: FormBuilder,
    private reportService: ReportService) {
    super(commonService, router, routeParams, snackBar, zone);
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

      let currentYearItem = this.academicyearList.find(ay => ay.IsSelected == true)
      if (currentYearItem != null) {
        this.currentAcademicYearId = currentYearItem.Id;
        this.practicalAssesmentReortForm.get('AcademicYearId').setValue(this.currentAcademicYearId);

        this.GetPracticalAssesmentReports();
      }
    });

    this.practicalAssesmentReortForm = this.createPracticalAssesmentReortForm();
  }

  resetReportFilters(): void {
    this.practicalAssesmentReortForm.reset();
    this.practicalAssesmentReortForm.get('AcademicYearId').setValue(this.currentAcademicYearId);
    this.districtList = <DropdownModel[]>[];
    this.jobRoleList = <DropdownModel[]>[];
    this.GetPracticalAssesmentReports();

  }

  onChangeDivision(divisionId: string): Promise<any> {
    let promise = new Promise((resolve, reject) => {

      this.commonService.GetMasterDataByType({ DataType: 'Districts', RoleId: this.UserModel.RoleCode, UserId: this.UserModel.UserTypeId, ParentId: divisionId, SelectTitle: 'District' }, false)
        .subscribe((response: any) => {

          this.districtList = response.Results;
          this.practicalAssesmentReortForm.controls['DistrictId'].setValue(null);
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

  //Create PracticalAssesmentReport form and returns {FormGroup}
  createPracticalAssesmentReortForm(): FormGroup {
    return this.formBuilder.group({
      AcademicYearId: new FormControl(),
      MonthId: new FormControl(),
      DivisionId: new FormControl(),
      DistrictId: new FormControl(),
      SectorId: new FormControl(),
      JobRoleId: new FormControl(),
      ClassId: new FormControl(),
      VTPId: new FormControl(),
      SchoolManagementId: new FormControl()
    });
  }

  GetPracticalAssesmentReports(): void {
    if (!this.practicalAssesmentReortForm.valid) {
      return;
    }
    var reportParams = {
      UserId: this.UserModel.LoginId,
      AcademicYearId: this.practicalAssesmentReortForm.get('AcademicYearId').value,
      DivisionId: this.practicalAssesmentReortForm.get('DivisionId').value,
      DistrictId: this.practicalAssesmentReortForm.get('DistrictId').value,
      SectorId: this.practicalAssesmentReortForm.get('SectorId').value,
      JobRoleId: this.practicalAssesmentReortForm.get('JobRoleId').value,
      VTPId: this.practicalAssesmentReortForm.get('VTPId').value,
      ClassId: this.practicalAssesmentReortForm.get('ClassId').value,
      MonthId: this.practicalAssesmentReortForm.get('MonthId').value,
      SchoolManagementId: this.practicalAssesmentReortForm.get('SchoolManagementId').value
    };

    reportParams.DistrictId = (reportParams.DistrictId != null && reportParams.DistrictId.length > 0) ? reportParams.DistrictId.toString() : null;

    this.reportService.GetPracticalAssesmentReportsByCriteria(reportParams).subscribe(response => {
      this.displayedColumns = ['VTPName', 'VCName', 'Mobile', 'EmailId', 'ReportDate', 'IsPratical', 'VTName', 'UDISE', 'SchoolName', 'SectorName', 'JobRoleName', 'ClassName', 'EnrolledStudents', 'VTPresent', 'PresentStudents', 'AssesorName', 'AssessorMobile', 'Remarks'];

      this.tableDataSource.data = response.Results;
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

}
