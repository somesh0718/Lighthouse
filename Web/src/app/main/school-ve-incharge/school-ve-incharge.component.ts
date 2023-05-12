import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SchoolVEInchargeModel } from './school-ve-incharge.model';
import { SchoolVEInchargeService } from './school-ve-incharge.service';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { DropdownModel } from 'app/models/dropdown.model';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter } from 'rxjs/operators';

@Component({
  selector: 'data-list-view',
  templateUrl: './school-ve-incharge.component.html',
  styleUrls: ['./school-ve-incharge.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})

export class SchoolVEInchargeComponent extends BaseListComponent<SchoolVEInchargeModel> implements OnInit {
  schoolVEInchargeSearchForm: FormGroup;
  schoolVEInchargeFilterForm: FormGroup;
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

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
    public zone: NgZone,
    private dialogService: DialogService,
    private schoolVEInchargeService: SchoolVEInchargeService) {
    super(commonService, router, routeParams, snackBar, zone);
    this.schoolVEInchargeSearchForm = this.formBuilder.group({ filterText: new FormControl() });
    this.schoolVEInchargeFilterForm = this.createSchoolVEInchargeClassesFilterForm()
  }

  ngOnInit(): void {

    this.SearchBy.PageIndex = 0; // delete after script changed
    this.SearchBy.PageSize = 10; // delete after script changed

    this.schoolVEInchargeService.getAcademicYearClassSection(this.UserModel).subscribe(results => {
      if (results[2].Success) {
        this.academicYearList = results[2].Results;
      }

      if (results[1].Success) {
        this.vtpList = results[1].Results;
        this.filteredVTPItems = this.vtpList.slice();
      }

      let currentYearItem = this.academicYearList.find(ay => ay.IsSelected == true)
      if (currentYearItem != null) {
        this.currentAcademicYearId = currentYearItem.Id;
        this.schoolVEInchargeFilterForm.get('AcademicYearId').setValue(this.currentAcademicYearId);
      }

      if (this.UserModel.RoleCode == 'VC') {
        this.SearchBy.VTId = this.UserModel.UserTypeId;

        this.commonService.getVTPByVC(this.UserModel).then(resp => {
          this.vtpId = resp[0].Id;
          this.vcId = resp[0].Name;

          this.schoolVEInchargeFilterForm.get('VTPId').setValue(this.vtpId);
          this.schoolVEInchargeFilterForm.controls['VTPId'].disable();

          this.onChangeVTP(this.vtpId).then(vtpResp => {
            this.schoolVEInchargeFilterForm.get('VCId').setValue(this.vcId);
            this.schoolVEInchargeFilterForm.controls['VCId'].disable();

            this.onChangeVC(this.UserModel.UserTypeId);
            this.onLoadSchoolVEInchargeByCriteria();
          });
        });
      }
      else if (this.UserModel.RoleCode == 'VT') {
        this.SearchBy.VTId = this.UserModel.UserTypeId;

        this.commonService.getVCVTPByVT(this.UserModel).then(resp => {
          this.vtpId = resp[0].Id;
          this.vcId = resp[0].Name;

          this.schoolVEInchargeFilterForm.get('VTPId').setValue(this.vtpId);
          this.schoolVEInchargeFilterForm.controls['VTPId'].disable();

          this.onChangeVTP(this.vtpId).then(vtpResp => {
            this.schoolVEInchargeFilterForm.get('VCId').setValue(this.vcId);
            this.schoolVEInchargeFilterForm.controls['VCId'].disable();

            this.onChangeVC(this.vcId).then(vtpResp => {
              this.schoolVEInchargeFilterForm.get('VTId').setValue(resp[0].Description);
              this.schoolVEInchargeFilterForm.controls['VTId'].disable();

              this.onLoadSchoolVEInchargeByCriteria();
            });
          });
        });
      }
      else {
        //Load initial VTSchoolSectors data
        this.onLoadSchoolVEInchargeByCriteria();
      }

      this.schoolVEInchargeSearchForm.get('filterText').valueChanges.pipe(
        // if character length greater then 2
        filter(res => {
          this.SearchBy.PageIndex = 0;

          if (res.length == 0) {
            this.SearchBy.Name = '';
            this.onLoadSchoolVEInchargeByCriteria();
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
        this.onLoadSchoolVEInchargeByCriteria();
      });
    });
  }

  ngAfterViewInit() {
    this.tableDataSource.paginator = this.ListPaginator;
  }

  onPageIndexChanged(evt) {
    this.SearchBy.PageIndex = evt.pageIndex;
    this.SearchBy.PageSize = evt.pageSize;

    this.onLoadSchoolVEInchargeByCriteria();
  }

  onLoadSchoolVEInchargeByCriteria(): void {
    this.IsLoading = true;
    this.SearchBy.AcademicYearId = this.schoolVEInchargeFilterForm.controls['AcademicYearId'].value;
    this.SearchBy.VTPId = this.schoolVEInchargeFilterForm.controls['VTPId'].value;
    this.SearchBy.VCId = this.schoolVEInchargeFilterForm.controls['VCId'].value;
    this.SearchBy.VTId = this.schoolVEInchargeFilterForm.controls['VTId'].value;
    this.SearchBy.SchoolId = this.schoolVEInchargeFilterForm.controls['SchoolId'].value;

    if (this.UserModel.RoleCode == "HM")
      this.SearchBy.HMId = this.UserModel.UserTypeId;
    else if (this.UserModel.RoleCode == "VT")
      this.SearchBy.VTId = this.UserModel.UserTypeId;

    this.schoolVEInchargeService.GetAllByCriteria(this.SearchBy).subscribe(response => {
      this.displayedColumns = ['SchoolName', 'FullName', 'Mobile', 'Email', 'Gender', 'DateOfJoining', 'IsActive', 'Actions'];

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
    });
  }

  onLoadSchoolVEInchargeByFilters(): any {
    this.SearchBy.PageIndex = 0;
    this.onLoadSchoolVEInchargeByCriteria();
  }

  resetFilters(): void {
    this.SearchBy.PageIndex = 0;
    this.schoolVEInchargeFilterForm.reset();
    this.schoolVEInchargeFilterForm.get('AcademicYearId').setValue(this.currentAcademicYearId);

    if (this.UserModel.RoleCode == 'VC') {
      this.schoolVEInchargeFilterForm.get('VTPId').setValue(this.vtpId);
      this.schoolVEInchargeFilterForm.get('VCId').setValue(this.vcId);
      this.schoolVEInchargeFilterForm.controls['VTPId'].disable();
      this.schoolVEInchargeFilterForm.controls['VCId'].disable();

      this.schoolList = [];
      this.filteredSchoolItems = [];
    }
    else if (this.UserModel.RoleCode == 'VT') {
      this.schoolVEInchargeFilterForm.get('VTPId').setValue(this.vtpId);
      this.schoolVEInchargeFilterForm.get('VCId').setValue(this.vcId);
      this.schoolVEInchargeFilterForm.get('VTId').setValue(this.UserModel.UserTypeId);
      this.schoolVEInchargeFilterForm.controls['VTPId'].disable();
      this.schoolVEInchargeFilterForm.controls['VCId'].disable();
      this.schoolVEInchargeFilterForm.controls['VTId'].disable();
    }
    else {
      this.vcList = [];
      this.filteredVCItems = [];
      this.vtList = [];
      this.filteredVTItems = [];
      this.schoolList = [];
      this.filteredSchoolItems = [];
    }

    this.onLoadSchoolVEInchargeByCriteria();
  }

  onChangeVTP(vtpId): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      this.commonService.GetMasterDataByType({ DataType: 'VocationalCoordinators', ParentId: vtpId, SelectTitle: 'Vocational Coordinator' }, false)
        .subscribe((response: any) => {
          if (response.Success) {
            this.vcList = response.Results;
            this.filteredVCItems = this.vcList.slice();
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
    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;
      this.commonService.GetMasterDataByType({ DataType: 'SchoolsByVC', RoleId: this.UserModel.RoleCode, UserId: this.UserModel.LoginId, ParentId: vcId, SelectTitle: 'School' }, false).subscribe((response: any) => {
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

  onDeleteSchoolVEIncharge(veiId: string) {
    this.dialogService
      .openConfirmDialog(this.Constants.Messages.DeleteConfirmationMessage)
      .afterClosed()
      .subscribe(confResp => {
        if (confResp) {
          this.schoolVEInchargeService.deleteSchoolVEInchargeById(veiId)
            .subscribe((schoolVEInchargeResp: any) => {

              this.zone.run(() => {
                if (schoolVEInchargeResp.Success) {
                  this.showActionMessage(
                    this.Constants.Messages.RecordDeletedMessage,
                    this.Constants.Html.SuccessSnackbar
                  );
                }
                this.ngOnInit();
              }, error => {
                console.log('SchoolVEIncharge deletion errors =>', error);
              });

            });
          this.ngOnInit();
        }
      });
  }

  //Create SchoolVEInchargeFilter form and returns {FormGroup}
  createSchoolVEInchargeClassesFilterForm(): FormGroup {
    return this.formBuilder.group({
      AcademicYearId: new FormControl(),
      VTPId: new FormControl(),
      VCId: new FormControl(),
      VTId: new FormControl(),
      SchoolId: new FormControl()
    });
  }
}
