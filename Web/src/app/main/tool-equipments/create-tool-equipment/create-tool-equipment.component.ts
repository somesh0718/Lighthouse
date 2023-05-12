import { Component, OnInit, NgZone, ViewEncapsulation, ViewChild, ChangeDetectorRef, ElementRef } from '@angular/core';
import { BaseComponent } from 'app/common/base/base.component';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';
import { RouteConstants } from 'app/constants/route.constant'
import { ToolEquipmentService } from '../tool-equipment.service';
import { RMListModel, ToolAndEquimentListModel, ToolEquipmentModel } from '../tool-equipment.model';
import { DropdownModel } from 'app/models/dropdown.model';
import { MatOption } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';
import { FileUploadModel } from 'app/models/file.upload.model';
import { FuseUtils } from '@fuse/utils';
import { MatTableDataSource } from '@angular/material/table';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { letterSpacing } from 'html2canvas/dist/types/css/property-descriptors/letter-spacing';

@Component({
  selector: 'tool-equipment',
  templateUrl: './create-tool-equipment.component.html',
  styleUrls: ['./create-tool-equipment.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class CreateToolEquipmentComponent extends BaseComponent<ToolEquipmentModel> implements OnInit {
  toolEquipmentForm: FormGroup;
  toolEquipmentModel: ToolEquipmentModel;
  vtpId: string;
  vcId: string;
  schoolId: string;
  vtId: string;
  AcademicYearId: string;
  academicYearAllList: [DropdownModel];
  vtSchoolSectorList: [DropdownModel];
  academicYearList: [DropdownModel];
  sectorList: [DropdownModel];
  //sectorList: DropdownModel[];
  jobRoleList: [DropdownModel];
  divisionList: [DropdownModel];
  districtList: DropdownModel[];

  vtpList: DropdownModel[];
  vtpFilterList: any;

  vcList: DropdownModel[];
  VCList: any = [];

  vtList: DropdownModel[];
  VTList: any = [];

  schoolList: DropdownModel[];
  filteredSchoolItems: any;

  toolList: DropdownModel[];
  filteredToolListItems: any;

  rawMaterialList: DropdownModel[];
  filteredRawMaterialItems: any;

  roomDamgedList: { Id: string, Name: string }[];
  imageUrl: any = "https://i.ibb.co/fDWsn3G/buck.jpg";
  editFile: boolean = true;
  removeUpload: boolean = false;
  selectedRawMaterialName: string;

  @ViewChild('districtMatSelect') districtSelections: MatSelect;
  imageUrlTLFilePath: string | ArrayBuffer;
  imageUrlLabFilePath: string | ArrayBuffer;
  fileUploadModel: unknown;
  TLPhotoFile: FileUploadModel;
  tLPhotoFile: FileUploadModel;
  labPhotoFile: FileUploadModel;
  minDate: any;

  //Tool Equipment llist
  toolEquimentListForm: FormGroup;
  toolAndEquimentListModel: ToolAndEquimentListModel;
  toolEquimentListAction: string = 'add';
  currentToolEquimentListIndex: number = 0;

  displayedColumns: string[];
  tableDataSource = new MatTableDataSource<ToolAndEquimentListModel>();;
  @ViewChild(MatPaginator, { static: true })
  ListPaginator: MatPaginator;

  @ViewChild(MatSort, { static: true })
  ListSort: MatSort;

  //Raw Material list
  rMListForm: FormGroup;
  rMListModel: RMListModel;
  rawMaterialListAction: string = 'add';
  currentRawMaterialListIndex: number = 0;

  displayedColumnsRM: string[];
  tableDataSourceRM = new MatTableDataSource<RMListModel>();

  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    private zone: NgZone,
    private route: ActivatedRoute,
    private toolEquipmentService: ToolEquipmentService,
    private dialogService: DialogService,
    private formBuilder: FormBuilder,
    private elRef: ElementRef,
    private cd: ChangeDetectorRef) {
    super(commonService, router, routeParams, snackBar);

    // Set the default toolEquipment Model
    this.toolEquipmentModel = new ToolEquipmentModel();
    this.toolAndEquimentListModel = new ToolAndEquimentListModel;
    this.rMListModel = new RMListModel;
    this.toolEquipmentForm = this.createToolEquipmentForm();
    this.toolEquimentListAction = 'add';
    this.rawMaterialListAction = 'add';
    this.toolEquimentListForm = this.toolEquipmentForm.controls.toolEquimentListForm as FormGroup;
    this.rMListForm = this.toolEquipmentForm.controls.rMListForm as FormGroup;
  }

  ngOnInit(): void {
    this.minDate = new Date(this.UserModel.DateOfAllocation);

    this.toolEquipmentService.getVTPByUser(this.UserModel).subscribe(results => {
      if (results[0].Success) {
        this.vtpFilterList = results[0].Results;
        this.vtpList = this.vtpFilterList.slice();
      }
      if (results[1].Success) {
        this.academicYearAllList = results[1].Results;
      }

      if (results[3].Success) {
        this.divisionList = results[3].Results;
      }

      if (this.UserModel.RoleCode == 'VT') {
        if (results[2].Success) {
          this.schoolList = results[2].Results;
          this.filteredSchoolItems = this.schoolList.slice();

          if (this.schoolList.length > 0) {
            this.schoolId = this.schoolList[0].Id;
          }
        }
      }

      this.roomDamgedList = [{ Id: "Room's Wall", Name: "Room's Wall" }, { Id: 'Window/s', Name: 'Window/s' }, { Id: 'Grills', Name: 'Grills' }, { Id: 'Floor', Name: 'Floor' }, { Id: 'Electrical board/s & Fitting', Name: 'Window/sElectrical board/s & Fitting' }]

      this.AcademicYearId = this.UserModel.AcademicYearId;

      this.route.paramMap.subscribe(params => {
        if (params.keys.length > 0) {
          this.PageRights.ActionType = params.get('actionType');

          if (this.PageRights.ActionType == this.Constants.Actions.New) {
            this.toolEquipmentModel = new ToolEquipmentModel();

            if (this.UserModel.RoleCode == 'VC') {
              this.toolEquipmentModel.VTId = this.UserModel.UserTypeId;
              this.toolEquimentListForm = this.toolEquipmentForm.controls.toolEquimentListForm as FormGroup;
              this.rMListForm = this.toolEquipmentForm.controls.rMListForm as FormGroup;
              this.commonService.getVTPByVC(this.UserModel).then(resp => {
                this.toolEquipmentModel.VTPId = resp[0].Id;
                this.toolEquipmentModel.VCId = resp[0].Name;

                this.toolEquipmentForm.get('VTPId').setValue(this.toolEquipmentModel.VTPId);
                this.toolEquipmentForm.controls['VTPId'].disable();

                this.onChangeVTP(this.toolEquipmentModel.VTPId).then(vtpResp => {
                  this.toolEquipmentForm.get('VCId').setValue(this.toolEquipmentModel.VCId);
                  this.toolEquipmentForm.controls['VCId'].disable();
                  this.onChangeVC(this.toolEquipmentModel.VCId);
                });
              });
            }
            else if (this.UserModel.RoleCode == 'VT') {
              this.vtId = this.UserModel.UserTypeId;

              this.onChangeVT(this.UserModel.UserTypeId).then(vtpResp => {
                this.onChangeAY(this.UserModel.AcademicYearId).then(ayResp => {
                  this.toolEquipmentForm = this.createToolEquipmentForm();
                  this.toolEquimentListForm = this.toolEquipmentForm.controls.toolEquimentListForm as FormGroup;
                  this.rMListForm = this.toolEquipmentForm.controls.rMListForm as FormGroup;
                });
              });
            }
          }
          else {
            var toolEquipmentId: string = params.get('toolEquipmentId')

            this.toolEquipmentService.getToolEquipmentById(toolEquipmentId)
              .subscribe((response: any) => {
                this.toolEquipmentModel = response.Result;
                if (this.PageRights.ActionType == this.Constants.Actions.Edit) {
                  this.toolEquipmentModel.RequestType = this.Constants.PageType.Edit;

                  this.toolEquipmentForm.get('VTPId').setValue(this.toolEquipmentModel.VTPId);
                  this.toolEquipmentForm.controls['VTPId'].disable();

                  this.onChangeVTP(this.toolEquipmentModel.VTPId).then(vtpResp => {
                    this.toolEquipmentForm.get('VCId').setValue(this.toolEquipmentModel.VCId);
                    this.toolEquipmentForm.controls['VCId'].disable();
                    this.toolEquipmentForm.get('SchoolId').setValue(this.toolEquipmentModel.SchoolId);
                    this.toolEquipmentForm.controls['SchoolId'].disable();

                    this.onChangeVC(this.toolEquipmentModel.VCId).then(vcResp => {
                      this.onChangeSchool(this.toolEquipmentModel.SchoolId).then(schoolResp => {
                        this.onChangeVT(this.toolEquipmentModel.VTId).then(vtResp => {
                          this.onChangeSector(this.toolEquipmentModel.SectorId).then(sectorResp => {
                            this.toolEquipmentForm = this.createToolEquipmentForm();
                            this.populateToolEquiementList();
                            this.populateRMList();
                            this.toolEquimentListForm = this.toolEquipmentForm.controls.toolEquimentListForm as FormGroup;
                            this.rMListForm = this.toolEquipmentForm.controls.rMListForm as FormGroup;

                          });
                        });
                      });
                    });
                  });
                }
                else if (this.PageRights.ActionType == this.Constants.Actions.View) {
                  this.toolEquipmentModel.RequestType = this.Constants.PageType.View;
                  this.PageRights.IsReadOnly = true;
                }

                if (this.UserModel.RoleCode == 'VT') {
                  this.onChangeVT(this.UserModel.UserTypeId).then(vtpResp => {
                    this.onChangeAY(this.UserModel.AcademicYearId).then(ayResp => {
                      this.onChangeSector(this.toolEquipmentModel.SectorId).then(sectorResp => {

                        this.populateToolEquiementList();
                        this.populateRMList();
                        this.toolEquipmentForm = this.createToolEquipmentForm();
                        this.toolEquimentListForm = this.toolEquipmentForm.controls.toolEquimentListForm as FormGroup;
                        this.rMListForm = this.toolEquipmentForm.controls.rMListForm as FormGroup;
                      });
                    });
                  });
                }
                else {
                  this.onChangeVTP(this.toolEquipmentModel.VTPId).then(vtpResp => {
                    this.onChangeVC(this.toolEquipmentModel.VCId).then(vcResp => {
                      this.onChangeSchool(this.toolEquipmentModel.SchoolId).then(schoolResp => {
                        this.onChangeVT(this.toolEquipmentModel.VTId).then(vtResp => {
                          this.onChangeAY(this.UserModel.AcademicYearId).then(ayResp => {
                            this.onChangeSector(this.toolEquipmentModel.SectorId).then(sectorResp => {
                              this.populateToolEquiementList();
                              this.populateRMList();
                              this.toolEquipmentForm = this.createToolEquipmentForm();
                              this.toolEquimentListForm = this.toolEquipmentForm.controls.toolEquimentListForm as FormGroup;
                              this.rMListForm = this.toolEquipmentForm.controls.rMListForm as FormGroup;
                            });
                          });
                        });
                      });
                    });
                  });
                }
              });
          }
        }
      });
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

  onChangeVTP(vtpId): Promise<any> {
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
          this.VCList = [];
          this.vtList = [];
          this.filteredSchoolItems = [];

          this.VCList = response.Results;
          this.vcList = this.VCList.slice();
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

      let schoolRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        schoolRequest = this.commonService.GetSchoolByHMId(this.AcademicYearId, this.UserModel.UserTypeId, vcId);
      }
      else {
        schoolRequest = this.commonService.GetMasterDataByType({ DataType: 'SchoolsByVC', ParentId: vcId, SelectTitle: 'School' }, false);
      }

      schoolRequest.subscribe((response: any) => {
        if (response.Success) {
          this.vcId = vcId;
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

  onChangeSchool(schoolId): Promise<any> {
    this.schoolId = schoolId;
    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;

      let vtRequest = null;
      if (this.UserModel.RoleCode == 'HM') {
        vtRequest = this.commonService.GetVTBySchoolIdHMId(this.AcademicYearId, this.UserModel.UserTypeId, this.vcId, schoolId);
      }
      else {
        vtRequest = this.commonService.GetMasterDataByType({ DataType: 'TETrainersBySchool', ParentId: schoolId, SelectTitle: 'Vocational Trainer' }, false);
      }

      vtRequest.subscribe((response: any) => {
        if (response.Success) {
          this.VTList = response.Results;
          this.vtList = this.VTList.slice();
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

  onChangeVT(vtId): Promise<any> {
    this.vtId = vtId;
    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;
      this.toolEquipmentService.getAcademicYearSectorByUser(vtId).subscribe((results: any) => {
        if (results[0].Success) {
          this.academicYearList = results[0].Results;
        }

        if (results[1].Success) {
          this.vtSchoolSectorList = results[1].Results;
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

  onChangeAY(ayId): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      this.AcademicYearId = ayId;

      if (this.UserModel.RoleCode === 'VT') {
        this.vtId = this.UserModel.UserTypeId;
      }

      this.commonService.GetMasterDataByType({ DataType: 'SectorsByVT', RoleId: this.AcademicYearId, UserId: this.vtId, SelectTitle: 'Sector' }).subscribe((results: any) => {
        if (results.Success) {
          this.sectorList = results.Results;
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

  onChangeSector(sectorId: string): Promise<any> {
    let promise = new Promise((resolve, reject) => {
      this.IsLoading = true;

      let sectorParams = {
        AcademicYearId: this.AcademicYearId,
        VTId: this.vtId,
        SectorId: sectorId
      };

      this.toolEquipmentService.getJobRoleToolListRawMaterialBySector(sectorParams).subscribe((results: any) => {
        if (results[0].Success) {
          this.jobRoleList = results[0].Results;
          let sectorItem = this.sectorList.find(s => s.Id == sectorId);

          //this.toolEquipmentForm.controls['JobRoleId'].setValue(sectorItem.Description);
          //this.toolEquipmentForm.controls['JobRoleId'].disable();

          if (results[1].Success) {
            this.toolList = results[1].Results.filter(te => te.TEType == 'ToolEquipment' && te.SectorId == sectorItem.Description)
            this.filteredToolListItems = this.toolList.slice();

            this.rawMaterialList = results[1].Results.filter(te => te.TEType == 'RawMaterial' && te.SectorId == sectorItem.Description)
            this.filteredRawMaterialItems = this.rawMaterialList.slice();
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

  uploadTLFilePath(evtTLImg) {
    if (evtTLImg.target.files.length > 0) {
      var fileExtn = evtTLImg.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.toolEquipmentForm.get('TLPhotoFile').setValue(null);
        this.dialogService.openShowDialog("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(evtTLImg, this.Constants.DocumentType.TEPhoto).then((response: FileUploadModel) => {
        this.tLPhotoFile = response;
      });
    }
  }

  uploadLabFilePath(evtLabImg) {
    if (evtLabImg.target.files.length > 0) {
      var fileExtn = evtLabImg.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.toolEquipmentForm.get('LabPhotoFile').setValue(null);
        this.dialogService.openShowDialog("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(evtLabImg, this.Constants.DocumentType.TEPhoto).then((response: FileUploadModel) => {
        this.labPhotoFile = response;
      });
    }

  }

  saveOrUpdateToolEquipmentDetails() {
    if (!this.toolEquipmentForm.valid) {
      this.validateAllFormFields(this.toolEquipmentForm);
      return;
    }
    this.setValueFromFormGroup(this.toolEquipmentForm, this.toolEquipmentModel);

    if (this.UserModel.RoleCode == 'VT') {
      this.toolEquipmentModel.VTId = this.UserModel.UserTypeId;
      this.toolEquipmentModel.SchoolId = this.schoolId
    }

    this.toolEquipmentModel.TLPhotoFile = this.setUploadedFile(this.tLPhotoFile)
    this.toolEquipmentModel.LabPhotoFile = this.setUploadedFile(this.labPhotoFile)

    this.toolEquipmentService.createOrUpdateToolEquipment(this.toolEquipmentModel)
      .subscribe((toolEquipmentResp: any) => {

        if (toolEquipmentResp.Success) {
          this.zone.run(() => {
            this.showActionMessage(
              this.Constants.Messages.RecordSavedMessage,
              this.Constants.Html.SuccessSnackbar
            );

            this.router.navigate([RouteConstants.ToolEquipment.List]);
          });
        }
        else {
          var errorMessages = this.getHtmlMessage(toolEquipmentResp.Errors)
          this.dialogService.openShowDialog(errorMessages);
        }
      }, error => {
        console.log('ToolEquipment deletion errors =>', error);
      });
  }

  //Create toolEquipment form and returns {FormGroup}
  createToolEquipmentForm(): FormGroup {
    return this.formBuilder.group({
      VTPId: new FormControl({ value: this.toolEquipmentModel.VTPId, disabled: this.PageRights.IsReadOnly }),
      VCId: new FormControl({ value: this.toolEquipmentModel.VCId, disabled: this.PageRights.IsReadOnly }),
      SchoolId: new FormControl({ value: this.toolEquipmentModel.SchoolId, disabled: this.PageRights.IsReadOnly }),
      VTId: new FormControl({ value: (this.UserModel.RoleCode == 'VT' ? this.UserModel.UserTypeId : this.toolEquipmentModel.VTId), disabled: this.PageRights.IsReadOnly }),
      ToolEquipmentId: new FormControl(this.toolEquipmentModel.ToolEquipmentId),
      AcademicYearId: new FormControl({ value: this.toolEquipmentModel.AcademicYearId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      SectorId: new FormControl({ value: this.toolEquipmentModel.SectorId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      JobRoleId: new FormControl({ value: this.toolEquipmentModel.JobRoleId, disabled: this.PageRights.IsReadOnly }, Validators.required),
      ReceiptDate: new FormControl({ value: this.getDateValue(this.toolEquipmentModel.ReceiptDate), disabled: this.PageRights.IsReadOnly }),
      TEReceiveStatus: new FormControl({ value: this.toolEquipmentModel.TEReceiveStatus, disabled: this.PageRights.IsReadOnly }, Validators.required),
      TEStatus: new FormControl({ value: this.toolEquipmentModel.TEStatus, disabled: this.PageRights.IsReadOnly }),
      RMStatus: new FormControl({ value: this.toolEquipmentModel.RMStatus, disabled: this.PageRights.IsReadOnly },),
      RMFundStatus: new FormControl({ value: this.toolEquipmentModel.RMFundStatus, disabled: this.PageRights.IsReadOnly },),
      Details: new FormControl({ value: this.toolEquipmentModel.Details, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(350)),

      //New Questions
      OATEStatus: new FormControl({ value: this.toolEquipmentModel.OATEStatus, disabled: this.PageRights.IsReadOnly }),
      OFTEStatus: new FormControl({ value: this.toolEquipmentModel.OFTEStatus, disabled: this.PageRights.IsReadOnly }),
      Reason: new FormControl({ value: this.toolEquipmentModel.Reason, disabled: this.PageRights.IsReadOnly }),
      IsSelected: new FormControl({ value: this.toolEquipmentModel.IsSelected, disabled: this.PageRights.IsReadOnly }),
      IsSpecify: new FormControl({ value: this.toolEquipmentModel.IsSpecify, disabled: this.PageRights.IsReadOnly }),
      RFNReceiveStatus: new FormControl({ value: this.toolEquipmentModel.RFNReceiveStatus, disabled: this.PageRights.IsReadOnly }),
      IsCommunicated: new FormControl({ value: this.toolEquipmentModel.IsCommunicated, disabled: this.PageRights.IsReadOnly }),
      IsSetUpWorkShop: new FormControl({ value: this.toolEquipmentModel.IsSetUpWorkShop, disabled: this.PageRights.IsReadOnly }),
      RoomType: new FormControl({ value: this.toolEquipmentModel.RoomType, disabled: this.PageRights.IsReadOnly }),
      AccommodateTools: new FormControl({ value: this.toolEquipmentModel.AccommodateTools, disabled: this.PageRights.IsReadOnly }),
      RoomSize: new FormControl({ value: this.toolEquipmentModel.RoomSize, disabled: this.PageRights.IsReadOnly }, Validators.maxLength(500)),
      IsDoorLock: new FormControl({ value: this.toolEquipmentModel.IsDoorLock, disabled: this.PageRights.IsReadOnly }),
      Flooring: new FormControl({ value: this.toolEquipmentModel.Flooring, disabled: this.PageRights.IsReadOnly }),
      RoomWindows: new FormControl({ value: this.toolEquipmentModel.RoomWindows, disabled: this.PageRights.IsReadOnly }),
      TotalWindowCount: new FormControl({ value: this.toolEquipmentModel.TotalWindowCount, disabled: this.PageRights.IsReadOnly }),
      IsWindowGrills: new FormControl({ value: this.toolEquipmentModel.IsWindowGrills, disabled: this.PageRights.IsReadOnly }),
      IsWindowLocked: new FormControl({ value: this.toolEquipmentModel.IsWindowLocked, disabled: this.PageRights.IsReadOnly }),
      IsRoomActive: new FormControl({ value: this.toolEquipmentModel.IsRoomActive, disabled: this.PageRights.IsReadOnly }),
      REFInstalled: new FormControl({ value: this.toolEquipmentModel.REFInstalled, disabled: this.PageRights.IsReadOnly }),
      WorkingSwitchBoard: new FormControl({ value: this.toolEquipmentModel.WorkingSwitchBoard, disabled: this.PageRights.IsReadOnly }),
      PSSCount: new FormControl({ value: this.toolEquipmentModel.PSSCount, disabled: this.PageRights.IsReadOnly }),
      WLCount: new FormControl({ value: this.toolEquipmentModel.WLCount, disabled: this.PageRights.IsReadOnly }),
      WFCount: new FormControl({ value: this.toolEquipmentModel.WFCount, disabled: this.PageRights.IsReadOnly }),
      RoomDamaged: new FormControl({ value: this.toolEquipmentModel.RoomDamaged, disabled: this.PageRights.IsReadOnly }),
      DivisionId: new FormControl({ value: this.toolEquipmentModel.DivisionId, disabled: this.PageRights.IsReadOnly }),
      DistrictCode: new FormControl({ value: this.toolEquipmentModel.DistrictCode, disabled: this.PageRights.IsReadOnly }),
      TLPhotoFile: new FormControl({ value: this.toolEquipmentModel.TLPhotoFile, disabled: this.PageRights.IsReadOnly }),
      LabPhotoFile: new FormControl({ value: this.toolEquipmentModel.LabPhotoFile, disabled: this.PageRights.IsReadOnly }),
      Remarks: new FormControl({ value: this.toolEquipmentModel.Remarks, disabled: this.PageRights.IsReadOnly }),
      RawMaterialRequired: new FormControl({ value: this.toolEquipmentModel.RawMaterialRequired, disabled: this.PageRights.IsReadOnly }, Validators.required),

      toolEquimentListForm: this.formBuilder.group({
        ToolEquipmentId: new FormControl(this.toolEquipmentModel.ToolEquipmentId),
        ToolListId: new FormControl({ value: this.toolAndEquimentListModel.ToolListId, disabled: this.PageRights.IsReadOnly }),
        ToolListName: new FormControl({ value: this.toolAndEquimentListModel.ToolListName, disabled: this.PageRights.IsReadOnly }),
        ToolListStatus: new FormControl({ value: this.toolAndEquimentListModel.ToolListStatus, disabled: this.PageRights.IsReadOnly }),
        TLActionNeeded1: new FormControl({ value: this.toolAndEquimentListModel.TLActionNeeded1, disabled: this.PageRights.IsReadOnly }),
      }),

      rMListForm: this.formBuilder.group({
        ToolEquipmentId: new FormControl(this.toolEquipmentModel.ToolEquipmentId),
        RawMaterialId: new FormControl({ value: this.rMListModel.RawMaterialId, disabled: this.PageRights.IsReadOnly }),
        RawMaterialName: new FormControl({ value: this.rMListModel.RawMaterialName, disabled: this.PageRights.IsReadOnly }),
        RawMaterialStatus: new FormControl({ value: this.rMListModel.RawMaterialStatus, disabled: this.PageRights.IsReadOnly }),
        RMLastReceivedDate: new FormControl({ value: this.rMListModel.RMLastReceivedDate, disabled: this.PageRights.IsReadOnly }),
        RawMaterialAction: new FormControl({ value: this.rMListModel.RawMaterialAction, disabled: this.PageRights.IsReadOnly }),
        QuantityCount: new FormControl({ value: this.rMListModel.QuantityCount, disabled: this.PageRights.IsReadOnly }),
      })
    });
  }

  onChangeOnTEReceiveStatusType(chk) {
    if (chk.value == "No") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['IsCommunicated', 'ReceiptDate', 'OATEStatus', 'OFTEStatus', 'Reason',
        'AccommodateTools', 'IsSelected', 'IsSpecify']);
    }
    else {
      this.setFormControlInitialState(this.toolEquipmentForm, ['RFNReceiveStatus', 'IsCommunicated']);
      this.toolEquipmentForm.controls["ReceiptDate"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["OATEStatus"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["AccommodateTools"].setValidators([Validators.required]);
    }
  }

  onChangeOnReason(chk) {
    if (chk.value == "Received damaged") {
      this.toolEquipmentForm.controls["IsCommunicated"].setValidators([Validators.required]);
    } else {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['IsCommunicated']);
    }
  }

  onChangeAccommodateTools(chk) {
    if (chk.value == "No") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['REFInstalled', 'WorkingSwitchBoard', 'PSSCount']);
    }
  }

  onChangeSetUpWorkShop(chk) {
    if (chk.value == "No") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['RoomType', 'IsDoorLock', 'Flooring', 'RoomWindows',
        'IsWindowGrills', 'IsWindowLocked', 'IsRoomActive', 'REFInstalled', 'WorkingSwitchBoard', 'RoomSize', 'TotalWindowCount'
        , 'PSSCount', 'WLCount', 'WFCount', 'RoomDamaged']);
      this.toolEquipmentForm.controls["AccommodateTools"].setValidators([Validators.required]);
    }
    else {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['AccommodateTools']);

      this.toolEquipmentForm.controls["RoomType"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["IsDoorLock"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["Flooring"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["RoomWindows"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["IsWindowGrills"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["IsWindowLocked"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["IsRoomActive"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["REFInstalled"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["WorkingSwitchBoard"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["RoomDamaged"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["PSSCount"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["WLCount"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["WFCount"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["RoomSize"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["TotalWindowCount"].setValidators([Validators.required]);
    }

  }

  onChangeOATEStatus(chk) {
    if (chk.value == "Not Available") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['OFTEStatus', 'AccommodateTools', 'Reason', 'IsSpecify']);
      this.toolEquipmentForm.controls["IsSelected"].setValidators([Validators.required]);

    }
    else if (chk.value == "Available" || chk.value == "Partially Available") {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['IsSelected']);
      this.toolEquipmentForm.controls["OFTEStatus"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["AccommodateTools"].setValidators([Validators.required]);;
    }
  }

  onChangeOnOFTEStatus(chk) {
    if (chk.value == "Partially Functional" || chk.value == "Not Functional") {
      this.toolEquipmentForm.controls["Reason"].setValidators([Validators.required]);
    } else if (chk.value == "Fully Functional") {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['Reason']);
    }
  }

  onChangeOnIsSelected(chk) {
    this.toolEquipmentForm.controls["IsCommunicated"].setValidators([Validators.required]);
    if (chk.value == "Theft") {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['IsSpecify']);
    }
  }

  onChangeOnRoomWindows(chk) {
    if (chk.value == "No") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['IsWindowGrills', 'IsWindowLocked', 'TotalWindowCount']);
    }
    else {
      this.toolEquipmentForm.controls["IsWindowGrills"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["IsWindowLocked"].setValidators([Validators.required]);;
    }
  }

  onChangeOnIsRoomActive(chk) {
    if (chk.value == "No") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['WorkingSwitchBoard', 'PSSCount', 'REFInstalled', 'WLCount', 'WFCount']);
    }
    else {
      this.toolEquipmentForm.controls["WorkingSwitchBoard"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["REFInstalled"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["PSSCount"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["WLCount"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["WFCount"].setValidators([Validators.required]);
    }
  }

  onChangeOnREFInstalled(chk) {
    if (chk.value == "No") {
      this.setFormControlInitialState(this.toolEquipmentForm, ['WLCount', 'WFCount']);
    }
    else {
      this.toolEquipmentForm.controls["WLCount"].setValidators([Validators.required]);
      this.toolEquipmentForm.controls["WFCount"].setValidators([Validators.required]);
    }
  }

  onChangeOnToolListStatus(chk) {
    let toolEquimentFormGroup: any = this.toolEquipmentForm.controls.toolEquimentListForm;

    if (chk.value == 'Available but Not Functional' || chk.value == 'Received but Not Available' || chk.value == "Not Received") {
      toolEquimentFormGroup.controls["TLActionNeeded1"].setValidators([Validators.required]);
    } else {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['TLActionNeeded1']);
    }
  }

  onChangeOnRawMaterialAction(chk) {
    let rawMaterialListFormGroup: any = this.toolEquipmentForm.controls.rMListForm;
    if (chk.value == "Not required") {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['QuantityCount']);
    }
  }

  onChangeOnWorkingSwitchBoard(chk) {
    if (chk.value == "No") {
      this.clearFormControlAsInitialState(this.toolEquipmentForm.controls['PSSCount']);
    }
  }

  checkValue(event) {
    if (event.target.value < 0) {
      event.target.value = 0;
    }
  }

  //Tool and Equiment List
  populateToolEquiementList(): void {
    this.displayedColumns = ['ToolListName', 'ToolListStatus', 'TLActionNeeded1', 'Actions'];

    this.tableDataSource.data = this.toolEquipmentModel.TEToolList;
    this.tableDataSource.paginator = this.ListPaginator;
    this.tableDataSource.filteredData = this.tableDataSource.data;

    let toolEquimentFormGroup: any = this.toolEquipmentForm.controls.toolEquimentListForm;
    toolEquimentFormGroup.controls["ToolListId"].patchValue(null);
    toolEquimentFormGroup.controls["ToolListStatus"].patchValue(null);
    toolEquimentFormGroup.controls["ToolListStatus"].patchValue(null);
    toolEquimentFormGroup.controls["TLActionNeeded1"].patchValue(null);
  }

  onAddToolEquiementList(): void {
    let toolEquimentFormGroup: any = this.toolEquipmentForm.controls.toolEquimentListForm;

    if (toolEquimentFormGroup.controls["ToolListId"].value == null || toolEquimentFormGroup.controls["ToolListStatus"].value == null) {
      this.dialogService.openShowDialog('Please enter Tool List');
      return;
    }

    this.toolAndEquimentListModel = {
      TEToolListId: (this.toolEquimentListAction == 'edit' ? this.toolAndEquimentListModel.TEToolListId : FuseUtils.NewGuid()),
      ToolEquipmentId: this.toolEquipmentModel.ToolEquipmentId,
      ToolListId: toolEquimentFormGroup.controls["ToolListId"].value,
      ToolListName: this.elRef.nativeElement.querySelector('mat-select[name="toolListId"]').innerText,
      ToolListStatus: toolEquimentFormGroup.controls["ToolListStatus"].value,
      TLActionNeeded1: toolEquimentFormGroup.controls["TLActionNeeded1"].value,
      RequestType: (this.toolEquimentListAction == 'add' ? this.Constants.PageType.New : this.Constants.PageType.Edit),
    };

    if (this.toolEquimentListAction == 'add') {
      let toolEquimentList = this.toolEquipmentModel.TEToolList.filter(tel => tel.ToolListId == this.toolAndEquimentListModel.ToolListId);

      if (toolEquimentList.length > 0) {

        this.dialogService.openShowDialog('Current Tool Name is already exists');
        return;
      }
    }

    if (this.toolEquimentListAction == 'edit') {
      this.toolEquipmentModel.TEToolList[this.currentToolEquimentListIndex] = this.toolAndEquimentListModel;
    }
    else {
      this.toolEquipmentModel.TEToolList.push(this.toolAndEquimentListModel);
    }

    this.populateToolEquiementList();
    this.onClearToolEquiementList();
    this.scrollToBottom();
  }

  onEditToolEquiementList(wlIndex: any): void {
    this.toolAndEquimentListModel = this.toolEquipmentModel.TEToolList[wlIndex];
    this.currentToolEquimentListIndex = wlIndex;
    this.toolEquimentListAction = 'edit';

  }

  onDeleteTEList(wlIndex: any): void {
    this.toolEquipmentModel.TEToolList.splice(wlIndex, 1);
    this.populateToolEquiementList();
  }

  onClearToolEquiementList(): void {
    this.toolEquimentListAction = 'add';
    let toolEquimentFormGroup: any = this.toolEquipmentForm.controls.toolEquimentListForm;

    toolEquimentFormGroup.controls["ToolListId"].patchValue(null);
    toolEquimentFormGroup.controls["ToolListStatus"].patchValue(null);
    toolEquimentFormGroup.controls["ToolListStatus"].patchValue(null);
    toolEquimentFormGroup.controls["TLActionNeeded1"].patchValue(null);

    for (let control in toolEquimentFormGroup.controls) {
      toolEquimentFormGroup.controls[control].updateValueAndValidity();
      toolEquimentFormGroup.controls[control].markAsPristine();
      toolEquimentFormGroup.controls[control].markAsUntouched();
    }
  }

  //Raw Materail List
  populateRMList(): void {
    this.displayedColumnsRM = ['RawMaterialName', 'RawMaterialStatus', 'RMLastReceivedDate', 'RawMaterialAction', 'QuantityCount', 'Actions'];

    this.tableDataSourceRM.data = this.toolEquipmentModel.TEMaterialList;
    this.tableDataSourceRM.paginator = this.ListPaginator;
    this.tableDataSourceRM.filteredData = this.tableDataSourceRM.data;

    let rawMaterialListFormGroup: any = this.toolEquipmentForm.controls.rMListForm;
    rawMaterialListFormGroup.controls["RawMaterialId"].patchValue(null);
    rawMaterialListFormGroup.controls["RawMaterialStatus"].patchValue(null);
    rawMaterialListFormGroup.controls["RMLastReceivedDate"].patchValue(null);
    rawMaterialListFormGroup.controls["RawMaterialAction"].patchValue(null);
    rawMaterialListFormGroup.controls["QuantityCount"].patchValue(null);
  }

  onAddRMList(): void {
    let rawMaterialListFormGroup: any = this.toolEquipmentForm.controls.rMListForm;

    if (rawMaterialListFormGroup.controls["RawMaterialId"].value == null || rawMaterialListFormGroup.controls["RawMaterialStatus"].value == null || rawMaterialListFormGroup.controls["RawMaterialAction"].value == null) {
      this.dialogService.openShowDialog('Please enter Raw Material List');
      return;
    }

    this.rMListModel = {
      TEMaterialListId: (this.rawMaterialListAction == 'edit' ? this.rMListModel.TEMaterialListId : FuseUtils.NewGuid()),
      ToolEquipmentId: this.toolEquipmentModel.ToolEquipmentId,
      RawMaterialId: rawMaterialListFormGroup.controls["RawMaterialId"].value,
      RawMaterialName: this.elRef.nativeElement.querySelector('mat-select[name="rawMaterialId"]').innerText,
      RawMaterialStatus: rawMaterialListFormGroup.controls["RawMaterialStatus"].value,
      RMLastReceivedDate: rawMaterialListFormGroup.controls["RMLastReceivedDate"].value,
      RawMaterialAction: rawMaterialListFormGroup.controls["RawMaterialAction"].value,
      QuantityCount: (rawMaterialListFormGroup.controls["QuantityCount"].value == null ? 0 : rawMaterialListFormGroup.controls["QuantityCount"].value),
      RequestType: (this.rawMaterialListAction == 'add' ? this.Constants.PageType.New : this.Constants.PageType.Edit),
    };

    if (this.rawMaterialListAction == 'add') {
      let rawMaterialList = this.toolEquipmentModel.TEMaterialList.filter(tel => tel.RawMaterialId == this.rMListModel.RawMaterialId);
      if (rawMaterialList.length > 0) {
        this.dialogService.openShowDialog('Current Raw Material is already exists');
        return;
      }
    }

    if (this.rawMaterialListAction == 'edit') {
      this.toolEquipmentModel.TEMaterialList[this.currentRawMaterialListIndex] = this.rMListModel;
    }
    else {
      this.toolEquipmentModel.TEMaterialList.push(this.rMListModel);
    }

    this.populateRMList();
    this.onClearRMList();
    this.scrollToBottom();
  }

  onEditRMList(wlIndex: any): void {
    this.rMListModel = this.toolEquipmentModel.TEMaterialList[wlIndex];
    this.currentRawMaterialListIndex = wlIndex;
    this.rawMaterialListAction = 'edit';

  }

  onDeleteRMList(wlIndex: any): void {
    this.toolEquipmentModel.TEMaterialList.splice(wlIndex, 1);
    this.populateRMList();
  }

  onChangeRawMaterial(rawMaterialId) {
    setTimeout(() => {
      this.selectedRawMaterialName = this.elRef.nativeElement.querySelector('mat-select[name="rawMaterialId"]').innerText;
    }, 500);
  }

  onClearRMList(): void {
    this.rawMaterialListAction = 'add';
    let rawMaterialListFormGroup: any = this.toolEquipmentForm.controls.rMListForm;

    rawMaterialListFormGroup.controls["RawMaterialId"].patchValue(null);
    rawMaterialListFormGroup.controls["RawMaterialStatus"].patchValue(null);
    rawMaterialListFormGroup.controls["RMLastReceivedDate"].patchValue(null);
    rawMaterialListFormGroup.controls["RawMaterialAction"].patchValue(null);
    rawMaterialListFormGroup.controls["QuantityCount"].patchValue(null);

    for (let control in rawMaterialListFormGroup.controls) {
      rawMaterialListFormGroup.controls[control].updateValueAndValidity();
      rawMaterialListFormGroup.controls[control].markAsPristine();
      rawMaterialListFormGroup.controls[control].markAsUntouched();
    }
  }
}
