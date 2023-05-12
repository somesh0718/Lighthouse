import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';

import { ActionSheetController, AlertController, IonRouterOutlet, MenuController, ModalController, NavController, Platform, PopoverController, ToastController } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { Storage } from '@ionic/storage';
import { BaseService } from './services/base.service';
import { Router } from '@angular/router';
import { ApiService } from './services/api.service';
import { AppConstants } from './app.constants';
import { HelperService } from './services/helper.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})

// tslint:disable: prefer-const
// tslint:disable: variable-name
// tslint:disable: no-string-literal
// tslint:disable: only-arrow-functions
// tslint:disable: triple-equals
// tslint:disable: space-before-function-paren
// tslint:disable: no-debugger

export class AppComponent implements OnInit {
  public selectedIndex = 0;
  public appInfo = environment;

  // set up hardware back button event.
  private lastTimeBackPress = 0;
  private timePeriodToExit = 2000;

  @ViewChildren(IonRouterOutlet) routerOutlets: QueryList<IonRouterOutlet>;

  public appPages = [
    {
      title: 'Home',
      url: '/folder/Home',
      icon: 'home'
    }
  ];
  expandSync = false;
  user: any;

  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private localStorage: Storage,
    private base: BaseService,
    private router: Router,
    private menu: MenuController,
    private api: ApiService,
    private helperService: HelperService,
    private alertCtrl: AlertController,
    private statusBar: StatusBar,
    private actionSheetCtrl: ActionSheetController,
    private popoverCtrl: PopoverController,
    public modalCtrl: ModalController,
    public nav: NavController,
    private toast: ToastController
  ) {
    this.initializeApp();

    // Initialize BackButton Eevent.
    this.backButtonEvent();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
      this.localStorage.get('currentUser').then((r) => {
        if (r !== null && r !== undefined) {
          let currUser = JSON.parse(r);
          this.user = currUser.UserName;
          AppConstants.AuthToken = currUser.AuthToken;
        }
      });
    });
  }

  // active hardware back button
  backButtonEvent() {
    this.platform.backButton.subscribe(async () => {
      // close action sheet
      try {
        const element = await this.actionSheetCtrl.getTop();
        if (element) {
          element.dismiss();
          return;
        }

      } catch (error) {
        console.log(error);
      }

      // close popover
      try {
        const element = await this.popoverCtrl.getTop();
        if (element) {
          element.dismiss();
          return;
        }

      } catch (error) {
        console.log(error);
      }

      // close modal
      try {
        const element = await this.modalCtrl.getTop();
        if (element) {
          element.dismiss();
          return;
        }

      } catch (error) {
        console.log(error);
      }

      // close side menua
      try {
        const element = await this.menu.getOpen();
        if (element) {
          this.menu.close();
          return;
        }

      } catch (error) {
      }

      this.routerOutlets.forEach((outlet: IonRouterOutlet) => {

        if (outlet && outlet.canGoBack()) {
          outlet.pop();

        } else if (this.router.url === '/folder/Home') {
          if (new Date().getTime() - this.lastTimeBackPress < this.timePeriodToExit) {
            // this.platform.exitApp(); // Exit from app
            navigator['app'].exitApp(); // work for ionic 4

          } else {
            this.presentToast('Press back again to exit App.');
            this.lastTimeBackPress = new Date().getTime();

            AppConstants.CurrentPageUrl = '/Home';
            this.router.navigateByUrl('/folder/Home');
          }
        } else {
          // go to previous page
          this.nav.navigateBack('/folder/Home');
        }
      });
    });
  }

  getLoginInfo() {
    this.localStorage.get('currentUser').then((r) => {
      let currUser = JSON.parse(r);
      this.user = JSON.parse(r).UserName;
      AppConstants.AuthToken = currUser.AuthToken;
    });
  }

  onMenuOpen() {
    this.expandSync = false;
    this.getLoginInfo();
  }

  ngOnInit() {
    const path = window.location.pathname.split('folder/')[1];
    if (path !== undefined) {
      this.selectedIndex = this.appPages.findIndex(page => page.title.toLowerCase() === path.toLowerCase());
    }
  }

  logout() {
    this.alertCtrl.create({
      message: 'Are you sure you want to Logout from the app?',
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            this.localStorage.remove('currentUser');
            this.api.dropUploadTables();
            this.api.dropGetTables();
            this.localStorage.get('masterList').then((mL) => {
              const masterList = JSON.parse(mL);
              this.api.dropMasterTables(masterList);
              this.router.navigateByUrl('/login');
            });
          }
        },
        {
          text: 'Cancel',
        }
      ],
      backdropDismiss: false
    }).then((al) => {
      al.present();
    });
  }

  exit() {
    navigator['app'].exitApp();
  }

  expand() {
    this.expandSync = this.expandSync ? false : true;
  }

  uploadSync() {
    this.alertCtrl.create({
      message: 'Are you sure you want to do Upload Sync?',
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            if (this.helperService.checkInternetConnection()) {
              this.base.uploadDataSync();
            } else {
              alert('Internet Connection is Required.');
            }
          }
        },
        {
          text: 'Cancel',
        }
      ],
      backdropDismiss: false
    }).then((al) => {
      al.present();
    });
  }

  masterSync() {
    this.alertCtrl.create({
      message: 'Are you sure you want to do Master Sync?',
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            if (this.helperService.checkInternetConnection()) {
              this.base.masterDataSync();
            } else {
              alert('Internet Connection is Required.');
            }
          }
        },
        {
          text: 'Cancel',
        }
      ],
      backdropDismiss: false
    }).then((al) => {
      al.present();
    });
  }

  getSync() {
    this.alertCtrl.create({
      message: 'Are you sure you want to do My Sync?',
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            if (this.helperService.checkInternetConnection()) {
              this.base.getDataSync();
            } else {
              alert('Internet Connection is Required.');
            }
          }
        },
        {
          text: 'Cancel',
        }
      ],
      backdropDismiss: false
    }).then((al) => {
      al.present();
    });
  }

  async presentToast(messageTest) {
    const toast = await this.toast.create({
      message: messageTest,
      duration: 2000
    });

    toast.present();
  }
}
