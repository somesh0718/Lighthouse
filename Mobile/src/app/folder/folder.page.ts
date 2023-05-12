import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingController } from '@ionic/angular';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { Storage } from '@ionic/storage';
import { AppConstants } from '../app.constants';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.page.html',
  styleUrls: ['./folder.page.scss'],
})
export class FolderPage implements OnInit {
  public folder: string;
  public list = [];
  public GeoLocation;
  public appPages = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private helperService: HelperService,
    private api: ApiService,
    private base: BaseService,
    private router: Router,
    private localStorage: Storage,
    public loadingController: LoadingController,
  ) { }

  ngOnInit() {
    this.folder = this.activatedRoute.snapshot.paramMap.get('id') as string;
    this.localStorage.get('currentUser').then((r) => {
      if (r !== null && r !== undefined) {
        let currUser = JSON.parse(r);
        AppConstants.AuthToken = currUser.AuthToken;
      }
    });
  }

  ionViewWillEnter() {
    this.localStorage.get('allowedPages').then((r) => {
      let temp = JSON.parse(r);
      this.appPages = temp.filter((x) => x.access === true);
    });
  }

  ionViewDidEnter() {
    this.api.getDbData();
    this.localStorage.get('currentUser').then((r) => {
      if (r !== null && r !== undefined) {
        let currUser = JSON.parse(r);
        AppConstants.AuthToken = currUser.AuthToken;
      }
    });
  }

  getGeoLoc() {
    this.helperService.getCurrentCoordinates().then((res: any) => {
      this.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
    }, (err) => {
      return;
    });
  }

  getBroadcastMessages() {
    this.router.navigateByUrl('/broadcast-message');
  }
}
