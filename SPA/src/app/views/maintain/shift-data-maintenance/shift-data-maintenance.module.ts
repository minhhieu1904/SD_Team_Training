import { UpdateComponent } from './update/update.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftDataMaintenanceRoutingModule } from './shift-data-maintenance-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent,
    UpdateComponent
  ],
  imports: [
    CommonModule,
    ShiftDataMaintenanceRoutingModule,
    PaginationModule.forRoot(),
    FormsModule
  ]
})
export class ShiftDataMaintenanceModule { }
