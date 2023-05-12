import { Component, OnInit, NgZone, ViewEncapsulation } from '@angular/core';
import { CommonService } from 'app/services/common.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SiteHeaderModel } from './site-header.model';
import { SiteHeaderService } from './site-header.service';
import { BaseListComponent } from 'app/common/base-list/base.list.component';
import { fuseAnimations } from '@fuse/animations';
import { DialogService } from 'app/common/confirm-dialog/dialog.service';

@Component({
  selector: 'data-list-view',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})

export class SiteHeaderComponent extends BaseListComponent<SiteHeaderModel> implements OnInit {
  constructor(public commonService: CommonService,
    public router: Router,
    public routeParams: ActivatedRoute,
    public snackBar: MatSnackBar,
    public zone: NgZone,
    private dialogService: DialogService,
    private siteHeaderService: SiteHeaderService) {
    super(commonService, router, routeParams, snackBar, zone);
  }

  ngOnInit(): void {
    this.siteHeaderService.GetAllByCriteria(this.SearchBy).subscribe(response => {
      this.displayedColumns = ['ShortName', 'LongName', 'Description', 'DisplayOrder', 'Remarks', 'IsActive', 'Actions'];

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

  onDeleteSiteHeader(siteHeaderId: string) {
    this.dialogService
      .openConfirmDialog(this.Constants.Messages.DeleteConfirmationMessage)
      .afterClosed()
      .subscribe(confResp => {
        if (confResp) {
          this.siteHeaderService.deleteSiteHeaderById(siteHeaderId)
            .subscribe((siteHeaderResp: any) => {

              this.zone.run(() => {
                if (siteHeaderResp.Success) {
                  this.showActionMessage(
                    this.Constants.Messages.RecordDeletedMessage,
                    this.Constants.Html.SuccessSnackbar
                  );
                }
                this.ngOnInit();
              }, error => {
                console.log('SiteHeader deletion errors =>', error);
              });

            });
          this.ngOnInit();
        }
      });
  }
}
