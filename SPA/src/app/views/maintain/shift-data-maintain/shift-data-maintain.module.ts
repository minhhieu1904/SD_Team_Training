import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { MainComponent } from '../shift-data-maintain/main/main.component';
import { AddComponent } from '../shift-data-maintain/add/add.component';
import { EditComponent } from '../shift-data-maintain/edit/edit.component';
import { ShiftDataMaintainRoutingModule } from '../shift-data-maintain/shift-data-maintain.routing.mudule.routing';
import { TranslateModule } from '@ngx-translate/core';
@NgModule({
  imports: [
    CommonModule,   
    ShiftDataMaintainRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule
  ],
  declarations: [
    MainComponent,
    AddComponent,
    EditComponent
  ]
})
export class ShiftDataMaintainModule { }
