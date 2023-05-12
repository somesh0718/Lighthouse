import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VtGuestLectureConductedPageRoutingModule } from './vt-guest-lecture-conducted-routing.module';

import { VtGuestLectureConductedPage } from './vt-guest-lecture-conducted.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    VtGuestLectureConductedPageRoutingModule
  ],
  declarations: [VtGuestLectureConductedPage]
})
export class VtGuestLectureConductedPageModule {}
