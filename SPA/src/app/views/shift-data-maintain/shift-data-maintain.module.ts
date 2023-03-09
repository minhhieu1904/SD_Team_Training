import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftDataMaintainRoutingModule } from './shift-data-maintain.routing.mudule.routing';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
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
    FormComponent
  ]
})
export class ShiftDataMaintainModule { }
