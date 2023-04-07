import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentDataMaintenanceRoutingModule } from './department-data-maintenance.routing';
import { DepartmentMainComponent } from './department-main/department-main.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { DepartmentFormComponent } from './department-form/department-form.component'

@NgModule({
  imports: [
    CommonModule,
    DepartmentDataMaintenanceRoutingModule,
    PaginationModule.forRoot(),
    FormsModule
  ],
  declarations: [
    DepartmentMainComponent,
    DepartmentFormComponent
  ]
})
export class DepartmentDataMaintenanceModule { }
