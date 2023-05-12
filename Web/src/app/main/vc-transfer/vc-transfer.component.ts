import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { VCTransferService } from './vc-transfer.service';
import { VCTransferModel } from './vc-transfer.model';
import { DropdownModel } from 'app/models/dropdown.model';
import { BaseListComponent } from 'app/common/base-list/base.list.component';

@Component({
  selector: 'vc-transfer',
  templateUrl: './vc-transfer.component.html',
  styleUrls: ['./vc-transfer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class VCTransferComponent extends BaseListComponent<VCTransferModel> implements OnInit {
  vcTransferForm: FormGroup;
  vcSchoolList: any = [];

  fromVTPList: DropdownModel[];
  filteredFromVTItems: any = [];
  fromVCList: DropdownModel[];
  filteredFromVCItems: any = [];
  toVCList: DropdownModel[];

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    private dialogService: DialogService,
    private vcTransferService: VCTransferService,
    private formBuilder: FormBuilder) {
    super(commonService, router, routeParams, snackBar, zone);

    this.vcTransferForm = this.createVCTransferForm();
  }

  ngOnInit(): void {
    this.commonService.GetVTPByAYId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.UserModel.AcademicYearId).subscribe((response) => {
      if (response.Success) {
        this.fromVTPList = response.Results;
        this.filteredFromVTItems = this.fromVTPList.slice();
      }
    });
  }

  onChangeFromVTP(vtpId) {
    let promise = new Promise((resolve, reject) => {

      let vcRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        vcRequest = this.commonService.GetVCByHMId(this.AcademicYearId, this.UserModel.UserTypeId, vtpId);
      }
      else {
        vcRequest = this.commonService.GetMasterDataByType({ DataType: 'VocationalCoordinators', ParentId: vtpId, SelectTitle: 'Vocational Coordinator' }, false);
      }

      vcRequest.subscribe((response: any) => {
        if (response.Success) {
          this.fromVCList = response.Results;
          this.filteredFromVCItems = this.fromVCList.slice();
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

  onChangeToVTP(vtpId, vcSchoolItem) {
    vcSchoolItem.ToVTPId = vtpId;

    let promise = new Promise((resolve, reject) => {

      let vcRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        vcRequest = this.commonService.GetVCByHMId(this.AcademicYearId, this.UserModel.UserTypeId, vtpId);
      }
      else {
        vcRequest = this.commonService.GetMasterDataByType({ DataType: 'VocationalCoordinators', ParentId: vtpId, SelectTitle: 'Vocational Coordinator' }, false);
      }

      vcRequest.subscribe((response: any) => {
        if (response.Success) {
          vcSchoolItem.ToVCList = response.Results;
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

  onChangeToVC(vcId, vcSchoolItem) {
    vcSchoolItem.ToVCId = vcId;
  }

  onChangeDOA(doa, vcSchoolItem) {
    vcSchoolItem.DateOfAllocation = doa.format('YYYY-MM-DD HH:mm:ss');
  }

  onChangeDOJ(doj, vcSchoolItem) {
    vcSchoolItem.DateOfJoining = doj.format('YYYY-MM-DD HH:mm:ss');
  }

  searchVCSchools() {
    if (!this.vcTransferForm.valid) {
      this.validateAllFormFields(this.vcTransferForm);
      return;
    }

    let vcSchoolParams: any = {
      DataId: this.UserModel.AcademicYearId,
      DataId1: this.vcTransferForm.controls["FromVTPId"].value,
      DataId2: this.vcTransferForm.controls['FromVCId'].value
    };

    this.vcTransferService.getVCSchoolsByVTPAndVCId(vcSchoolParams).subscribe(response => {
      response.Results.forEach(function (obj) {
        obj.IsSelected = false;
        obj.ToVCList = [];
      });

      this.vcSchoolList = response.Results
      this.displayedColumns = ["SrNo", 'SectorName', 'SchoolName', 'IsSelected', 'ToVTPId', 'ToVCId', 'DateOfAllocation', 'DateOfJoining', 'Remarks'];

      this.tableDataSource.data = this.vcSchoolList;
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

  onVCSchoolForTransfer(event, vcsItem) {
    if (vcsItem == null) {
      this.vcSchoolList.forEach(vcs => { vcs.IsSelected = event.checked; });
    }
    else {
      let vcSchoolItem = this.vcSchoolList.find(vs => vs.VCSchoolSectorId === vcsItem.VCSchoolSectorId);
      vcSchoolItem.IsSelected = event.checked;
    }
  }

  saveVCTransfers() {
    if (!this.vcTransferForm.valid) {
      this.validateAllFormFields(this.vcTransferForm);
      return;
    }

    let vcSchoolCount = this.vcSchoolList.filter(vs => vs.IsSelected === true && (vs.ToVTPId == "" || vs.ToVCId == "" || vs.DateOfAllocation == null || vs.DateOfJoining == null));
    if (vcSchoolCount.length > 0) {
      this.dialogService.openShowDialog("Please fill DOA and DOJ required fields");
      return;
    }

    this.dialogService
      .openConfirmDialog("Are you sure to transfer selected records?")
      .afterClosed()
      .subscribe(confResp => {
        if (confResp) {

          let vcSchoolItems = this.vcSchoolList.filter(vs => vs.IsSelected === true);
          let vcTransferModel = new VCTransferModel({
            UserId: this.UserModel.UserId,
            VCSchoolModels: vcSchoolItems,
          });

          this.vcTransferService.saveVCTransfers(vcTransferModel)
            .subscribe((settingResp: any) => {

              if (settingResp.Success) {
                settingResp.Result.VCSchoolModels.forEach(vcs => {
                  let vcSchoolItem = this.vcSchoolList.find(vs => vs.VCSchoolSectorId === vcs.VCSchoolSectorId);
                  vcSchoolItem.Remarks = vcs.Remarks;
                });

                this.dialogService.openShowDialog("VC Transfers completed. Please check remarks against selected records");
              }
              else {
                var errorMessages = this.getHtmlMessage(settingResp.Errors)
                this.dialogService.openShowDialog(errorMessages);
              }
            }, error => {
              console.log('VC Transfer saving errors =>', error);
            });
        }
      });
  }

  //Create vc-transfer form and returns {FormGroup}
  createVCTransferForm(): FormGroup {
    return this.formBuilder.group({
      FromVTPId: new FormControl({ value: "", disabled: false }, Validators.required),
      FromVCId: new FormControl({ value: "", disabled: false }, Validators.required),
    });
  }
}
