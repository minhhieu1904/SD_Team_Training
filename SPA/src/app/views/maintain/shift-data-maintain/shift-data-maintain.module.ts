import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftDataMaintainRoutingModule } from './shift-data-maintain-routing.module';
import { MainComponent } from './main/main.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ShiftDataMaintainRoutingModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
  ],
  declarations: [
    MainComponent,
    AddComponent,
    EditComponent
  ]
})
export class ShiftDataMaintainModule { }
