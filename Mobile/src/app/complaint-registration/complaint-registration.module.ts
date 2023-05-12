import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { ComplaintRegistrationPageRoutingModule } from './complaint-registration-routing.module';
import { ComplaintRegistrationPage } from './complaint-registration.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    ComplaintRegistrationPageRoutingModule
  ],
  declarations: [ComplaintRegistrationPage]
})
export class ComplaintRegistrationPageModule { }
