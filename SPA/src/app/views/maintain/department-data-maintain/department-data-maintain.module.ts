import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentDataMaintainRoutingModule } from './department-data-maintain-routing.module';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [MainComponent, AddComponent, EditComponent],
  imports: [
    CommonModule,
    DepartmentDataMaintainRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
  ],
})
export class DepartmentDataMaintainModule {}
