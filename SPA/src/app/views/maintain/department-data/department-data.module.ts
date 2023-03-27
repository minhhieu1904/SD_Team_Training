import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentDataRoutingModule } from './department-data-routing.module';
import { MainComponent } from './main/main.component';
import { AddDepartmentDataComponent } from './add-department-data/add-department-data.component';
import { EditDepartmentDataComponent } from './edit-department-data/edit-department-data.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    MainComponent,
    AddDepartmentDataComponent,
    EditDepartmentDataComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    DepartmentDataRoutingModule
  ]
})
export class DepartmentDataModule { }
