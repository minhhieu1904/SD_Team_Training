import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main/main.component';
import { ShiftDataRoutingModule } from './shift-data-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AddShiftDataComponent } from './add-shift-data/add-shift-data.component';
import { RouterModule } from '@angular/router';
import { EditShiftDataComponent } from './edit-shift-data/edit-shift-data.component';



@NgModule({
  declarations: [
    MainComponent,
    AddShiftDataComponent,
    EditShiftDataComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    ShiftDataRoutingModule
  ]
})
export class ShiftDataModule { }
