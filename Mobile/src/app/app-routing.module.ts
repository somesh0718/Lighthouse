import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'folder/:id',
    loadChildren: () => import('./folder/folder.module').then(m => m.FolderPageModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./login/login.module').then(m => m.LoginPageModule)
  },
  {
    path: 'vt-issue-reporting',
    loadChildren: () => import('./vt-issue-reporting/vt-issue-reporting.module').then(m => m.VtIssueReportingPageModule)
  },
  {
    path: 'vc-daily-reporting',
    loadChildren: () => import('./vc-daily-reporting/vc-daily-reporting.module').then(m => m.VcDailyReportingPageModule)
  },
  {
    path: 'vc-issue-reporting',
    loadChildren: () => import('./vc-issue-reporting/vc-issue-reporting.module').then(m => m.VcIssueReportingPageModule)
  },
  {
    path: 'hm-issue-reporting',
    loadChildren: () => import('./hm-issue-reporting/hm-issue-reporting.module').then(m => m.HmIssueReportingPageModule)
  },
  {
    path: 'vt-field-industry-visit-conducted',
    loadChildren: () => import('./vt-field-industry-visit-conducted/vt-field-industry-visit-conducted.module').then(m => m.VtFieldIndustryVisitConductedPageModule)
  },
  {
    path: 'cal-modal',
    loadChildren: () => import('./calendar/cal-modal/cal-modal.module').then( m => m.CalModalPageModule)
  },
  {
    path: 'vt-daily-reporting',
    loadChildren: () => import('./vt-daily-reporting/vt-daily-reporting.module').then( m => m.VtDailyReportingPageModule)
  },
  {
    path: 'vt-guest-lecture-conducted',
    loadChildren: () => import('./vt-guest-lecture-conducted/vt-guest-lecture-conducted.module').then( m => m.VtGuestLectureConductedPageModule)
  },
  {
    path: 'vc-school-visit',
    loadChildren: () => import('./vc-school-visit/vc-school-visit.module').then( m => m.VcSchoolVisitPageModule)
  },
  {
    path: 'broadcast-message',
    loadChildren: () => import('./broadcast-message/broadcast-message.module').then( m => m.BroadcastMessagePageModule)
  },
  {
    path: 'complaint-registration',
    loadChildren: () => import('./complaint-registration/complaint-registration.module').then( m => m.ComplaintRegistrationPageModule)
  },
  {
    path: 'drp-daily-reporting',
    loadChildren: () => import('./drp-daily-reportings/drp-daily-reporting.module').then(m => m.DRPDailyReportingPageModule)
  },
  {
    path: 'vc-school-visit-reporting',
    loadChildren: () => import('./vc-school-visit-reporting/vc-school-visit-reporting.module').then(m => m.VcSchoolVisitReportingPageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
