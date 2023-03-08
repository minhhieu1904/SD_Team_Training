import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftDataMaintainRoutingModule } from './shift-data-maintain.routing.mudule.routing';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule } from '@angular/forms';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ShiftDataMaintainRoutingModule
  ],
  declarations: [
    MainComponent,
    FormComponent
  ]
})
export class ShiftDataMaintainModule { }
