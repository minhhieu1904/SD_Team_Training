import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftDataMaintainRoutingModule } from './shift-data-maintain.routing.mudule.routing';
import { MainComponent } from './main/main.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ShiftDataMaintainRoutingModule,
    PaginationModule.forRoot()
  ],
  declarations: [
    MainComponent,
    AddComponent,
    EditComponent
  ]
})
export class ShiftDataMaintainModule { }
