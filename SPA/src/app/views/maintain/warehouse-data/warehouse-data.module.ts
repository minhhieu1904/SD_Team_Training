import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseDataRoutingModule } from './warehouse-data-routing.module';
import { MainComponent } from './main/main.component';
import { AddWarehouseDataComponent } from './add-warehouse-data/add-warehouse-data.component';
import { EditWarehouseDataComponent } from './edit-warehouse-data/edit-warehouse-data.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent,
    AddWarehouseDataComponent,
    EditWarehouseDataComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    TranslateModule,

    WarehouseDataRoutingModule
  ]
})
export class WarehouseDataModule { }
