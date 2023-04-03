import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftDataMaintenaceRoutingModule } from './shift-data-maintenance.routing';
import { FormsModule } from '@angular/forms';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  imports: [
    CommonModule,
    ShiftDataMaintenaceRoutingModule,
    FormsModule,
    PaginationModule.forRoot()
  ],
  declarations: [
    MainComponent,
    FormComponent
  ]
})
export class ShiftDataMaintenaceModule { }
