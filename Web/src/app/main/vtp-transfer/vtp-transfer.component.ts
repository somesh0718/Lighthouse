import { Component, NgZone, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { VTPTransferService } from './vtp-transfer.service';
import { VTPTransferModel } from './vtp-transfer.model';
import { DropdownModel } from 'app/models/dropdown.model';
import { BaseListComponent } from 'app/common/base-list/base.list.component';

@Component({
  selector: 'app-vtp-transfer',
  templateUrl: './vtp-transfer.component.html',
  styleUrls: ['./vtp-transfer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class VTPTransferComponent extends BaseListComponent<VTPTransferModel> implements OnInit {
  vtpTransferForm: FormGroup;
  vtpTransferModel: VTPTransferModel;
  vtpId: string;
  sectorId: string;

  vtpList: DropdownModel[];
  filteredVTPItems: any;

  toVtpList: DropdownModel[];

  vcList: [DropdownModel];
  filteredVCItems: any;

  sectorList: DropdownModel[];

  toSectorList: DropdownModel[];

  vtpSchoolList: any = [];
  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    private dialogService: DialogService,
    private vTPTransferService: VTPTransferService,
    private formBuilder: FormBuilder) {
    super(commonService, router, routeParams, snackBar, zone);

    // Set the default vt-transfer Model
    this.vtpTransferModel = new VTPTransferModel();

    this.vtpTransferForm = this.createvtpTransferForm();
  }

  ngOnInit(): void {
    this.vTPTransferService.getDropdownforClass(this.UserModel).subscribe(results => {


      if (results[0].Success) {
        this.vtpList = results[0].Results;
        this.filteredVTPItems = this.vtpList.slice();

        this.toVtpList = results[0].Results;
      }

    });
  }

  onChangeVTP(vtpId): Promise<any> {
    this.vtpId = vtpId;
    let promise = new Promise((resolve, reject) => {
      this.commonService.GetSectorByAyIdVTPId(this.UserModel.AcademicYearId, vtpId)
        .subscribe((response: any) => {
          if (response.Success) {
            this.sectorList = response.Results;
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

  onChangeSector(sectorId) {
    this.sectorId = sectorId;
  }

  onChangeToVTP(vtpId, vtpSchoolItem): Promise<any> {
    vtpSchoolItem.ToVTPId = vtpId;
    let promise = new Promise((resolve, reject) => {

      let vcRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        vcRequest = this.commonService.GetVCByHMId(this.AcademicYearId, this.UserModel.UserTypeId, vtpId);
      }
      else {
        vcRequest = this.commonService.GetVCByVTPIdSectorId(this.UserModel.AcademicYearId, vtpId, this.sectorId);
      }

      vcRequest.subscribe((response: any) => {
        if (response.Success) {
          vtpSchoolItem.ToVCList = response.Results;
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

  onChangeVC(vcId, vtpSchoolItem): Promise<any> {
    vtpSchoolItem.ToVCId = vcId;
    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;

      this.commonService.GetSectorByAyIdVCId(this.UserModel.AcademicYearId, vcId).subscribe((response: any) => {
        if (response.Success) {
          vtpSchoolItem.toSectorList = response.Results;
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

  onChangeDOA(doa, vtpSchoolItem) {
    vtpSchoolItem.DateOfAllocation = doa.format('YYYY-MM-DD HH:mm:ss');
  }

  onChangeDOJ(doj, vtpSchoolItem) {
    vtpSchoolItem.DateOfJoining = doj.format('YYYY-MM-DD HH:mm:ss');
  }

  onChangeToSector(sectorId, vtpSchoolItem) {
    vtpSchoolItem.ToSectorId = sectorId;
  }

  onVTPSchoolForTransfer(event, vtpItem) {
    if (vtpItem == null) {
      this.vtpSchoolList.forEach(vcs => { vcs.IsSelected = event.checked; });
    }
    else {
      let vtpSchoolItem = this.vtpSchoolList.find(vs => vs.VCSchoolSectorId === vtpItem.VCSchoolSectorId);
      vtpSchoolItem.IsSelected = event.checked;
    }
  }

  SearchVTPSchools() {
    if (!this.vtpTransferForm.valid) {
      this.validateAllFormFields(this.vtpTransferForm);
      return;
    }

    this.vTPTransferService.GetSchoolByVTPIdSectorId(this.UserModel.AcademicYearId, this.vtpId, this.sectorId).subscribe(response => {
      response.Results.forEach(function (obj) {
        obj.IsSelected = false;
        obj.ToVCList = [];
        obj.toSectorList = [];
      });

      this.vtpSchoolList = response.Results;
      this.displayedColumns = ['SrNo', 'School', 'IsSelected', 'ToVTPId', 'ToVCId', 'DateOfAllocation', 'DateOfJoining', 'ToSectorId', 'Remarks'];
      this.tableDataSource.data = this.vtpSchoolList;
      this.tableDataSource.filteredData = this.tableDataSource.data;
      this.tableDataSource.sort = this.ListSort;
      this.tableDataSource.paginator = this.ListPaginator;

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

  saveVTTransfers() {
    if (!this.vtpTransferForm.valid) {
      this.validateAllFormFields(this.vtpTransferForm);
      return;
    }
    let vtpSchoolCount = this.vtpSchoolList.filter(vs => vs.IsSelected === true && (vs.ToVTPId == "" || vs.ToVCId == "" || vs.DateOfAllocation == null || vs.DateOfJoining == null));
    if (vtpSchoolCount.length > 0) {
      this.dialogService.openShowDialog("Please fill DOA and DOJ required fields");
      return;
    }

    this.dialogService
      .openConfirmDialog("Are you sure to transfer selected records?")
      .afterClosed()
      .subscribe(confResp => {
        if (confResp) {
          let vtpSchoolItems = this.vtpSchoolList.filter(vs => vs.IsSelected === true);
          let vcTransferModel = new VTPTransferModel({
            UserId: this.UserModel.UserId,
            VTPSchoolModels: vtpSchoolItems,
          });

          this.vTPTransferService.saveVTPTransfers(vcTransferModel)
            .subscribe((settingResp: any) => {

              if (settingResp.Success) {
                settingResp.Result.VTPSchoolModels.forEach(vtps => {
                  let vtpSchoolItem = this.vtpSchoolList.find(vs => vs.VCSchoolSectorId === vtps.VCSchoolSectorId);
                  vtpSchoolItem.Remarks = vtps.Remarks;
                });

                this.dialogService.openShowDialog("VTP Transfers completed. Please check remarks against selected records");
              }
              else {
                var errorMessages = this.getHtmlMessage(settingResp.Errors)
                this.dialogService.openShowDialog(errorMessages);
              }
            }, error => {
              console.log('VTP Transfer saving errors =>', error);
            });
        }
      });
  }

  //Create vtp-transfer form and returns {FormGroup}
  createvtpTransferForm(): FormGroup {
    return this.formBuilder.group({
      VTPId: new FormControl({ value: "", disabled: false }, Validators.required),
      SectorId: new FormControl({ value: "", disabled: false }, Validators.required)
    });
  }
}
