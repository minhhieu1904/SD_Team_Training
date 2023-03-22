import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentDataMaintenanceRoutingModule } from './department-data-maintenance-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UpdateComponent } from './update/update.component';

@NgModule({
  declarations: [MainComponent, FormComponent, UpdateComponent],
  imports: [
    CommonModule,
    DepartmentDataMaintenanceRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
  ],
})
export class DepartmentDataMaintenanceModule {}
