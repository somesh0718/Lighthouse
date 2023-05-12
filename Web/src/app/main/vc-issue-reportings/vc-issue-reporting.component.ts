import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VCIssueReportingModel } from './vc-issue-reporting.model';
import { VCIssueReportingService } from './vc-issue-reporting.service';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';

@Component({
  selector: 'data-list-view',
  templateUrl: './vc-issue-reporting.component.html',
  styleUrls: ['./vc-issue-reporting.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})

export class VCIssueReportingComponent extends BaseListComponent<VCIssueReportingModel> implements OnInit {
  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    private dialogService: DialogService,
    private vcIssueReportingService: VCIssueReportingService) {
    super(commonService, router, routeParams, snackBar, zone);
  }

  ngOnInit(): void {
    this.vcIssueReportingService.GetAllByCriteria(this.SearchBy).subscribe(response => {
      this.displayedColumns = ['IssueReportDate', 'MainIssue', 'SubIssue', 'StudentType', 'NoOfStudents', 'ApprovalStatus', 'Actions'];
      
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

  onDeleteVCIssueReporting(vcIssueReportingId: string) {
    this.dialogService
      .openConfirmDialog(this.Constants.Messages.DeleteConfirmationMessage)
      .afterClosed()
      .subscribe(confResp => {
        if (confResp) {
          this.vcIssueReportingService.deleteVCIssueReportingById(vcIssueReportingId)
            .subscribe((vcIssueReportingResp: any) => {

              this.zone.run(() => {
                if (vcIssueReportingResp.Success) {
                  this.showActionMessage(
                    this.Constants.Messages.RecordDeletedMessage,
                    this.Constants.Html.SuccessSnackbar
                  );
                }
                this.ngOnInit();
              }, error => {
                console.log('VCIssueReporting deletion errors =>', error);
              });

            });
          this.ngOnInit();
        }
      });
  }
}
