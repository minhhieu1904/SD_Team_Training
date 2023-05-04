import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentDataMaintenanceRoutingModule } from './department-data-maintenance-routing.module';
import { MainComponent } from './main/main.component';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination'
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent,
    EditComponent,
    AddComponent
  ],
  imports: [
    CommonModule,
    DepartmentDataMaintenanceRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule
  ]
})
export class DepartmentDataMaintenanceModule { }
