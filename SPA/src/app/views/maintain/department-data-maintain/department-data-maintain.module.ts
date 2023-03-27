import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentDataMaintainRoutingModule } from './department-data-maintain-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';

@NgModule({
  declarations: [
    MainComponent,
    EditComponent,
    AddComponent
  ],
  imports: [
    CommonModule,
    DepartmentDataMaintainRoutingModule,
    FormsModule,
    PaginationModule.forRoot()
  ]
})
export class DepartmentDataMaintainModule { }
