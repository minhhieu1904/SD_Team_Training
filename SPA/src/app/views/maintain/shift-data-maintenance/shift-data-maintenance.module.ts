import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftDataMaintenanceRoutingModule } from './shift-data-maintenance-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent
  ],
  imports: [
    CommonModule,
    ShiftDataMaintenanceRoutingModule,
    FormsModule
  ]
})
export class ShiftDataMaintenanceModule { }
