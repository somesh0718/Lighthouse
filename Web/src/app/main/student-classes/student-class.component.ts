import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { StudentClassModel } from './student-class.model';
import { StudentClassService } from './student-class.service';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter } from 'rxjs/operators';
import { DropdownModel } from 'app/models/dropdown.model';

@Component({
  selector: 'data-list-view',
  templateUrl: './student-class.component.html',
  styleUrls: ['./student-class.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})

export class StudentClassComponent extends BaseListComponent<StudentClassModel> implements OnInit {
  studentSearchForm: FormGroup;
  studentFilterForm: FormGroup;
  currentAcademicYearId: string;
  vtpId: string;
  vcId: string;

  academicYearList: [DropdownModel];
  vtpList: DropdownModel[];
  filteredVTPItems: any;
  vcList: DropdownModel[];
  filteredVCItems: any;
  vtList: DropdownModel[];
  filteredVTItems: any;
  schoolList: DropdownModel[];
  filteredSchoolItems: any;
  sectorList: [DropdownModel];
  jobRoleList: DropdownModel[];
  classList: [DropdownModel];

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    private formBuilder: FormBuilder,
    private dialogService: DialogService,
    private studentClassService: StudentClassService) {

    super(commonService, router, routeParams, snackBar, zone);
    this.studentSearchForm = this.formBuilder.group({ SearchText: new FormControl() });
    this.studentFilterForm = this.createStudentFilterForm()
  }

  ngOnInit(): void {
    this.studentClassService.getDropdownforStudentClass(this.UserModel).subscribe(results => {
      if (results[4].Success) {
        this.academicYearList = results[4].Results;
      }

      if (results[1].Success) {
        this.vtpList = results[1].Results;
        this.filteredVTPItems = this.vtpList.slice();
      }

      if (results[2].Success) {
        this.sectorList = results[2].Results;
      }

      if (results[5].Success) {
        this.classList = results[5].Results;
      }

      this.AcademicYearId = this.UserModel.AcademicYearId;
      this.studentFilterForm.get('AcademicYearId').setValue(this.AcademicYearId);

      this.SearchBy.PageIndex = 0; // delete after script changed
      this.SearchBy.PageSize = 25; // delete after script changed

      this.onLoadStudentsByCriteria();

      this.studentSearchForm.get('SearchText').valueChanges.pipe(
        // if character length greater then 2
        filter(res => {
          this.SearchBy.PageIndex = 0;

          if (res.length == 0) {
            this.SearchBy.Name = '';
            this.onLoadStudentsByCriteria();
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
        this.onLoadStudentsByCriteria();
      });
    });
  }

  ngAfterViewInit() {
    this.tableDataSource.paginator = this.ListPaginator;
  }

  onPageIndexChanged(evt) {
    this.SearchBy.PageIndex = evt.pageIndex;
    this.SearchBy.PageSize = evt.pageSize;

    this.onLoadStudentsByCriteria();
  }

  onLoadStudentsByCriteria(): any {
    this.IsLoading = true;

    let studentParams: any = {
      AcademicYearId: this.studentFilterForm.controls["AcademicYearId"].value,
      VTPId: this.studentFilterForm.controls["VTPId"].value,
      VCId: this.UserModel.RoleCode == 'VC' ? this.UserModel.UserTypeId : this.studentFilterForm.controls['VCId'].value,
      VTId: this.UserModel.RoleCode == 'VT' ? this.UserModel.UserTypeId : this.studentFilterForm.controls['VTId'].value,
      SectorId: this.studentFilterForm.controls["SectorId"].value,
      JobRoleId: this.studentFilterForm.controls['JobRoleId'].value,
      SchoolId: this.studentFilterForm.controls["SchoolId"].value,
      ClassId: this.studentFilterForm.controls['ClassId'].value,
      HMId: null,
      Status: this.studentFilterForm.controls["Status"].value,
      IsRollover: 0,
      Name: this.studentSearchForm.controls["SearchText"].value,
      CharBy: null,
      PageIndex: this.SearchBy.PageIndex,
      PageSize: this.SearchBy.PageSize
    };

    if (this.UserModel.RoleCode == "HM") {
      studentParams.HMId = this.UserModel.UserTypeId;
    }

    this.studentClassService.GetAllByCriteria(studentParams).subscribe(response => {
      this.displayedColumns = ['AcademicYear', 'SchoolName', 'ClassName', 'SectionName', 'VTName', 'StudentName', 'Gender', 'DateOfEnrollment', 'IsActive', 'Actions'];

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

  onLoadStudentsByFilters(): any {
    this.SearchBy.PageIndex = 0;
    this.onLoadStudentsByCriteria();
  }

  resetFilters(): void {
    this.studentSearchForm.reset();
    this.studentFilterForm.reset();
    this.studentFilterForm.get('AcademicYearId').setValue(this.UserModel.AcademicYearId);

    this.vcList = [];
    this.filteredVCItems = [];
    this.vtList = [];
    this.filteredVTItems = [];
    this.schoolList = [];
    this.filteredSchoolItems = [];
    this.jobRoleList = [];

    this.onLoadStudentsByCriteria();
  }

  onChangeAY(academicYearId): Promise<any> {
    this.AcademicYearId = academicYearId;
    this.vtpList = [];
    this.filteredVTPItems = [];

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetVTPByAYId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.AcademicYearId).subscribe((response: any) => {
        if (response.Success) {
          this.vtpList = response.Results;
          this.filteredVTPItems = this.vtpList.slice();
        }

        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onChangeVTP(vtpId): Promise<any> {
    this.vcList = [];
    this.filteredVCItems = [];

    let promise = new Promise((resolve, reject) => {
      this.commonService.GetVCByAYAndVTPId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.AcademicYearId, vtpId).subscribe((response: any) => {
        if (response.Success) {
          this.vcList = response.Results;
          this.filteredVCItems = this.vcList.slice();

          if (this.UserModel.RoleCode == 'VC') {
            this.studentFilterForm.get('VCId').setValue(response.Results[0].Id);
            this.studentFilterForm.controls['VCId'].disable();
            this.onChangeVC(this.vcId)
          }
        }

        this.IsLoading = false;
        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onChangeVC(vcId): Promise<any> {
    this.vtList = [];
    this.filteredVTItems = [];
    let vtRequest = null;

    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;

      if (this.UserModel.RoleCode == 'HM') {
        vtRequest = this.commonService.GetVTByHMId(this.AcademicYearId, this.UserModel.UserTypeId, vcId);
      }
      else {
        vtRequest = this.commonService.GetVTByAYAndVCId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.AcademicYearId, vcId);
      }

      vtRequest.subscribe((response: any) => {
        if (response.Success) {
          this.vtList = response.Results;
          this.filteredVTItems = this.vtList.slice();

          if (this.UserModel.RoleCode == 'VT') {
            this.studentFilterForm.get('VTId').setValue(response.Results[0].Id);
            this.studentFilterForm.controls['VTId'].disable();
            this.onChangeVT(response.Results[0].Id)
          }
        }

        this.IsLoading = false;
        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onChangeSector(sectorId: string): void {
    this.commonService.GetMasterDataByType({ DataType: 'JobRoles', ParentId: sectorId, SelectTitle: 'Job Role' }).subscribe((response: any) => {
      this.jobRoleList = response.Results;
    });
  }

  onChangeVT(vtId) {
    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;
      this.commonService.GetMasterDataByType({ DataType: 'SchoolsByVT', userId: this.UserModel.LoginId, ParentId: vtId, SelectTitle: 'School' }, false).subscribe((response: any) => {
        if (response.Success) {
          this.schoolList = response.Results;
          this.filteredSchoolItems = this.schoolList.slice();
        }

        this.IsLoading = false;
        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onDeleteStudentClass(studentId: string) {
    this.dialogService
      .openConfirmDialog(this.Constants.Messages.DeleteConfirmationMessage)
      .afterClosed()
      .subscribe(confResp => {
        if (confResp) {
          this.studentClassService.deleteStudentClassById(studentId)
            .subscribe((studentClassResp: any) => {

              this.zone.run(() => {
                if (studentClassResp.Success) {
                  this.showActionMessage(
                    this.Constants.Messages.RecordDeletedMessage,
                    this.Constants.Html.SuccessSnackbar
                  );
                }
                this.ngOnInit();
              }, error => {
                console.log('StudentClass deletion errors =>', error);
              });

            });
          this.ngOnInit();
        }
      });
  }

  exportExcel(): void {
    this.IsLoading = true;
    this.SearchBy.PageIndex = 0;
    this.SearchBy.PageSize = 1000000;

    let studentParams: any = {
      AcademicYearId: this.studentFilterForm.controls["AcademicYearId"].value,
      VTPId: this.studentFilterForm.controls["VTPId"].value,
      VCId: this.UserModel.RoleCode == 'VC' ? this.UserModel.UserTypeId : this.studentFilterForm.controls['VCId'].value,
      VTId: this.UserModel.RoleCode == 'VT' ? this.UserModel.UserTypeId : this.studentFilterForm.controls['VTId'].value,
      SectorId: this.studentFilterForm.controls["SectorId"].value,
      JobRoleId: this.studentFilterForm.controls['JobRoleId'].value,
      SchoolId: this.studentFilterForm.controls["SchoolId"].value,
      ClassId: this.studentFilterForm.controls['ClassId'].value,
      HMId: null,
      Status: this.studentFilterForm.controls["Status"].value,
      IsRollover: 0,
      Name: this.studentSearchForm.controls["SearchText"].value,
      CharBy: null,
      PageIndex: this.SearchBy.PageIndex,
      PageSize: this.SearchBy.PageSize
    };

    if (this.UserModel.RoleCode == "HM") {
      studentParams.HMId = this.UserModel.UserTypeId;
    }

    this.studentClassService.GetAllByCriteria(studentParams).subscribe(response => {
      response.Results.forEach(
        function (obj) {
          if (obj.hasOwnProperty('IsActive')) {
            obj.IsActive = obj.IsActive ? 'Yes' : 'No';
          }

          delete obj.StudentId;
          delete obj.IsAYRollover;
          delete obj.DeletedBy;
          delete obj.TotalRows;
        });

      this.exportExcelFromTable(response.Results, "Students");

      this.IsLoading = false;
      this.SearchBy.PageIndex = 0;
      this.SearchBy.PageSize = 10;
    }, error => {
      console.log(error);
      this.IsLoading = false;
    });
  }

  //Create StudentFilter form and returns {FormGroup}
  createStudentFilterForm(): FormGroup {
    return this.formBuilder.group({
      AcademicYearId: new FormControl(),
      VTPId: new FormControl(),
      VCId: new FormControl(),
      VTId: new FormControl(),
      SectorId: new FormControl(),
      JobRoleId: new FormControl(),
      SchoolId: new FormControl(),
      ClassId: new FormControl(),
      Status: new FormControl()
    });
  }
}
