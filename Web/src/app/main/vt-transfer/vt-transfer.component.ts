import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { VTTransferService } from './vt-transfer.service';
import { VTTransferModel } from './vt-transfer.model';
import { DropdownModel } from 'app/models/dropdown.model';
import { BaseListComponent } from 'app/common/base-list/base.list.component';

@Component({
  selector: 'vt-transfer',
  templateUrl: './vt-transfer.component.html',
  styleUrls: ['./vt-transfer.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class VTTransferComponent extends BaseListComponent<VTTransferModel> implements OnInit {
  vtTransferForm: FormGroup;
  vtTransferModel: VTTransferModel;
  oldAYId: string;
  oldVTPId: string;
  oldVCId: string;
  oldSectorId: string;
  oldSchoolId:string;
  newVTPId: string;
  isSchool:boolean=false;

  AcademicYearId: string;
  academicYearList: [DropdownModel];
  AcademicYearToId: string;
  academicYearToList: [DropdownModel];

  vtpList: DropdownModel[];
  vtpToList: DropdownModel[];
  fromVCList: DropdownModel[];
  toVCList: DropdownModel[];

  fromVTList: DropdownModel[];
  fromSchoolList: DropdownModel[];
  filteredFromVTItems: any;

  toVTList: DropdownModel[];
  toSchoolList: DropdownModel[];
  filteredToVTItems: any;

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    private dialogService: DialogService,
    private vtTransferService: VTTransferService,
    private formBuilder: FormBuilder) {
    super(commonService, router, routeParams, snackBar, zone);

    // Set the default vt-transfer Model
    this.vtTransferModel = new VTTransferModel();

    this.vtTransferForm = this.createVTTransferForm();
  }

  ngOnInit(): void {
    this.vtTransferService.getDropdownforClass(this.UserModel).subscribe(results => {
      if (results[0].Success) {
        this.academicYearList = results[0].Results;
        this.academicYearToList = results[0].Results;
      }

      if (results[1].Success) {
        this.vtpList = results[1].Results;
        this.vtpToList = results[1].Results;
      }

      let currentYearItem = this.academicYearList.find(ay => ay.IsSelected == true)
      if (currentYearItem != null) {
        this.AcademicYearId = currentYearItem.Id;
        this.vtTransferForm.get('AcademicYearId').setValue(this.AcademicYearId);
      }

      let currentYearToItem = this.academicYearToList.find(ay => ay.IsSelected == true)
      if (currentYearToItem != null) {
        this.AcademicYearToId = currentYearToItem.Id;
        this.vtTransferForm.get('AcademicYearToId').setValue(this.AcademicYearToId);
      }
    });
  }

  onChangeAY(AYId): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      this.vtpList = [];
      let vtpRequest = this.commonService.GetVTPByAYId(this.UserModel.RoleCode, this.UserModel.UserTypeId, AYId)

      vtpRequest.subscribe((response: any) => {
        if (response.Success) {
          this.vtpList = response.Results;
        }

        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onChangeToAY(ayId): Promise<any> {
    this.oldAYId = ayId;
    let promise = new Promise((resolve, reject) => {
      this.vtpToList = [];
      let vtpRequest = this.commonService.GetVTPByAYIdSectorId(ayId, this.oldSectorId)

      vtpRequest.subscribe((response: any) => {
        if (response.Success) {
          this.vtpToList = response.Results;
        }

        resolve(true);
      }, error => {
        console.log(error);
        resolve(false);
      });
    });
    return promise;
  }

  onChangeFromVTP(vtpId) {
    this.oldVTPId = vtpId;
    let vcRequest = this.commonService.GetVCByAYAndVTPId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.AcademicYearId, vtpId);
    vcRequest.subscribe((response: any) => {
      if (response.Success) {
        this.fromVCList = response.Results;
      }
    });
  }

  onChangeToVTP(vtpId) {
    this.newVTPId = vtpId;
    let vcRequest = this.commonService.GetVCByVTPIdSectorId(this.AcademicYearId, vtpId, this.oldSectorId);
    vcRequest.subscribe((response: any) => {
      if (response.Success) {
        this.toVCList = response.Results;
      }
    });
  }

  onChangeFromVC(vcId: any) {
    this.oldVCId = vcId;
    let schoolRequest = this.commonService.GetSchoolByAYAndVCId(this.UserModel.RoleCode, this.UserModel.UserTypeId, this.AcademicYearId, this.oldVTPId, vcId);
    schoolRequest.subscribe((response: any) => {
      this.fromSchoolList = response.Results
    });

  }

  onChangeToVC(vcId: any) {
    let vtRequest = this.commonService.GetVTByAYIdAndVTPIdVCId(this.AcademicYearId, this.newVTPId, vcId);
    vtRequest.subscribe((response: any) => {
      this.toVTList = response.Results;
      this.filteredToVTItems = this.toVTList.slice();
    });

    let schoolRequest = this.commonService.GetSchoolByVTPIdVCIdSectorId(this.AcademicYearId, this.newVTPId, vcId, this.oldSectorId);
    schoolRequest.subscribe((response: any) => {
      this.toSchoolList = response.Results
      let schoolcount=this.toSchoolList.filter(s=>s.Id == this.oldSchoolId)
      if(schoolcount.length > 0){
        this.isSchool =true;
        this.vtTransferForm.get('ToSchoolId').setValue(this.oldSchoolId);
      }else{
        this.isSchool =false;
      }  
    });
   
  }

  onChangeFromSchool(schoolId: any) {
    this.oldSchoolId =schoolId;
    let vtParams = {
      DataId: this.UserModel.RoleCode,
      DataId1: this.UserModel.UserTypeId,
      DataId2: this.AcademicYearId,
      DataId3: this.oldVTPId,
      DataId4: this.oldVCId,
      DataId5: schoolId,
    };

    let vtRequest = this.commonService.GetVTByAYAndSchoolId(vtParams);
    vtRequest.subscribe((response: any) => {
      this.fromVTList = response.Results;
      this.filteredFromVTItems = this.fromVTList.slice();
    });
  }

  onChangeFromVT(vtId: any) {
    this.vtTransferService.GetSchoolStudentsByVTId(this.AcademicYearId, vtId).subscribe(response => {
      this.displayedColumns = ['AcademicYear', 'SchoolName', 'SectorName', 'JobRoleName', 'VTName', 'ClassName', 'SectionName', 'StudentCount'];

      this.tableDataSource.data = response.Results;
      this.oldSectorId = this.tableDataSource.data[0]["SectorId"];
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

  saveVTTransfers() {
    if (!this.vtTransferForm.valid) {
      this.validateAllFormFields(this.vtTransferForm);
      return;
    }

    this.setValueFromFormGroup(this.vtTransferForm, this.vtTransferModel);
    this.vtTransferModel.UserId = this.UserModel.UserId;

    this.vtTransferService.saveVTTransfers(this.vtTransferModel)
      .subscribe((settingResp: any) => {
        console.log("VT", settingResp)
        if (settingResp.Success) {
          this.dialogService.openShowDialog(settingResp.Messages[0]);       
        }
        else {
          var errorMessages = this.getHtmlMessage(settingResp.Errors)
          this.dialogService.openShowDialog(errorMessages);
        }
      }, error => {
        console.log('VT Transfer saving errors =>', error);
      });
  }

  //Create vt-transfer form and returns {FormGroup}
  createVTTransferForm(): FormGroup {
    return this.formBuilder.group({
      AcademicYearId: new FormControl({ value: this.vtTransferModel.AcademicYearId, disabled: false }, Validators.required),
      FromVTPId: new FormControl({ value: this.vtTransferModel.FromVTPId, disabled: false }, Validators.required),
      FromVCId: new FormControl({ value: this.vtTransferModel.FromVCId, disabled: false }, Validators.required),
      FromVTId: new FormControl({ value: this.vtTransferModel.FromVTId, disabled: false }, Validators.required),
      FromSchoolId: new FormControl({ value: this.vtTransferModel.FromSchoolId, disabled: false }, Validators.required),
      IsVTResigned: new FormControl({ value: this.vtTransferModel.IsVTResigned, disabled: false }, Validators.required),
      DateOfRemoval: new FormControl({ value: new Date(this.vtTransferModel.DateOfRemoval), disabled: false }, Validators.required),
      DateOfResignation: new FormControl({ value: new Date(this.vtTransferModel.DateOfResignation), disabled: false }, Validators.required),

      AcademicYearToId: new FormControl({ value: this.vtTransferModel.AcademicYearToId, disabled: false }, Validators.required),
      ToVTPId: new FormControl({ value: this.vtTransferModel.ToVTPId, disabled: false }, Validators.required),
      ToVCId: new FormControl({ value: this.vtTransferModel.ToVCId, disabled: false }, Validators.required),
      ToSchoolId: new FormControl({ value: this.vtTransferModel.ToSchoolId, disabled: false }, Validators.required),
      ToVTId: new FormControl({ value: this.vtTransferModel.ToVTId, disabled: false }, Validators.required),
      DateOfAllocation: new FormControl({ value: new Date(this.vtTransferModel.DateOfAllocation), disabled: false }, Validators.required),
      ToDateOfRemoval: new FormControl({ value: new Date(this.vtTransferModel.ToDateOfRemoval), disabled: false }, Validators.required),
    });
  }
}
