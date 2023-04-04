import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { WarehouseBasicDataMaintenanceRoutingModule } from './warehouse-basic-data-maintenance.routing'
import { WarehouseMainComponent } from './warehouse-main/warehouse-main.component';
import { WarehouseFormComponent } from './warehouse-form/warehouse-form.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    WarehouseBasicDataMaintenanceRoutingModule,
    PaginationModule.forRoot()
  ],
  declarations: [
    WarehouseMainComponent,
    WarehouseFormComponent
  ]
})
export class WarehouseBasicDataMaintenanceModule { }
