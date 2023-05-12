import { Component, OnInit, NgZone, ViewEncapsulation, ViewChild } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';
import { ToolListReportModel } from './tool-list-report.model';
import { ReportService } from 'app/reports/report.service';
import { DropdownModel } from 'app/models/dropdown.model';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { MatOption } from '@angular/material/core';


@Component({
  selector: 'app-tool-list-report',
  templateUrl: './tool-list-report.component.html',
  styleUrls: ['./tool-list-report.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ToolListReportComponent extends BaseListComponent<ToolListReportModel> implements OnInit {
  ToolListReportForm: FormGroup;

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
  isShowFilterContainer = false;
  @ViewChild('districtMatSelect') districtSelections: MatSelect;
  schoolId: any;
  vcId: any;
  filteredSchoolItems: any[];
  academicYearId: any;

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

      if (results[8].Success) {
        this.genderList = results[8].Results;
      }

      let currentYearItem = this.academicyearList.find(ay => ay.IsSelected == true)
      if (currentYearItem != null) {
        this.currentAcademicYearId = currentYearItem.Id;
        this.ToolListReportForm.get('AcademicYearId').setValue(this.currentAcademicYearId);

        if (this.UserModel.RoleCode === 'DivEO') {
          this.ToolListReportForm.controls["DivisionId"].setValue(this.UserModel.DivisionId);


          this.onChangeDivision(this.UserModel.DivisionId).then(response => {
            this.getToolListReports();
          });
        }
        else if (this.UserModel.RoleCode === 'DisEO' || this.UserModel.RoleCode === 'DisRP' || this.UserModel.RoleCode === 'BEO' || this.UserModel.RoleCode === 'BRP') {
          this.ToolListReportForm.controls["DivisionId"].setValue(this.UserModel.DivisionId);


          this.onChangeDivision(this.UserModel.DivisionId).then(response => {
            let districtIds = [];
            response.forEach(districtItem => {
              districtIds.push(districtItem.Id);
            });

            this.ToolListReportForm.controls["DistrictId"].setValue(districtIds);

            this.getToolListReports();
          });
        }
        else {
          this.getToolListReports();
        }
      }
    });

    this.ToolListReportForm = this.createToolListReportForm();

    this.displayedColumns = ['SrNo', 'AcademicYear', 'UDISE', 'SchoolName', 'DistrictName', 'SectorName', 'JobRoleName', 'Composite', 'VTPName', 'VCName', 'VCEmail', 'VTName', 'VTEmail', 'TEReceiveStatus', 'ReceiptDate', 'ToolName', 'Status', 'Action1', 'Action2'];
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

  resetReportFilters(): void {
    this.ToolListReportForm.reset();
    this.ToolListReportForm.get('AcademicYearId').setValue(this.currentAcademicYearId);
    this.districtList = <DropdownModel[]>[];
    this.jobRoleList = <DropdownModel[]>[];

    if (this.UserModel.RoleCode === 'DisEO' || this.UserModel.RoleCode === 'DisRP' || this.UserModel.RoleCode === 'BEO' || this.UserModel.RoleCode === 'BRP') {
      this.ToolListReportForm.controls["DivisionId"].setValue(this.UserModel.DivisionId);

      this.onChangeDivision(this.UserModel.DivisionId).then(response => {
        let districtIds = [];
        response.forEach(districtItem => {
          districtIds.push(districtItem.Id);
        });

        this.ToolListReportForm.controls["DistrictId"].setValue(districtIds);


        this.getToolListReports();
      });
    }
    else {
      this.getToolListReports();
    }
  }

  onChangeDivision(divisionId: string): Promise<any> {
    let promise = new Promise((resolve, reject) => {

      this.commonService.GetMasterDataByType({ DataType: 'Districts', RoleId: this.UserModel.RoleCode, UserId: this.UserModel.UserTypeId, ParentId: divisionId, SelectTitle: 'District' }, false)
        .subscribe((response: any) => {

          this.districtList = response.Results;
          this.ToolListReportForm.controls['DistrictId'].setValue(null);
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

  onChangeAcademicYear(academicYearId): Promise<any> {
    this.academicYearId = academicYearId
    this.IsLoading = true;

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetSchoolsByAcademicYearId(academicYearId).subscribe((response: any) => {
        if (response.Success) {
          this.schoolList = response.Results;
          this.filteredSchoolItems = this.schoolList.slice();
        }
        this.IsLoading = false;
        resolve(true);
      });
    });

    return promise;
  }

  //Create ToolListReport form and returns {FormGroup}
  createToolListReportForm(): FormGroup {
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
      // SchoolManagementId: new FormControl()
    });
  }

  getToolListReports(): void {
    if (!this.ToolListReportForm.valid) {
      return;
    }

    var reportParams: any = {
      UserId: this.UserModel.LoginId,
      AcademicYearId: this.ToolListReportForm.get('AcademicYearId').value,
      VTId: this.ToolListReportForm.get('VTId').value,
      SchoolId: this.ToolListReportForm.get('SchoolId').value,
      DivisionId: this.ToolListReportForm.get('DivisionId').value,
      DistrictId: this.ToolListReportForm.get('DistrictId').value,
      SectorId: this.ToolListReportForm.get('SectorId').value,
      JobRoleId: this.ToolListReportForm.get('JobRoleId').value,
      VTPId: this.ToolListReportForm.get('VTPId').value,
      ClassId: this.ToolListReportForm.get('ClassId').value,
      VCId: this.ToolListReportForm.get('VCId').value,

    };

    if (this.UserModel.RoleCode == 'HM') {
      reportParams.HMId = this.UserModel.UserTypeId;
    }

    reportParams.DistrictId = (reportParams.DistrictId != null && reportParams.DistrictId.length > 0) ? reportParams.DistrictId.toString() : null;

    this.reportService.GetToolListReport(reportParams).subscribe(response => {
      this.displayedColumns = ['SrNo', 'AcademicYear', 'UDISE', 'SchoolName', 'DistrictName', 'SectorName', 'JobRoleName', 'Composite', 'VTPName', 'VCName', 'VCEmail', 'VTName', 'VTEmail', 'TEReceiveStatus', 'ReceiptDate', 'ToolName', 'Status', 'Action1', 'Action2'];

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
